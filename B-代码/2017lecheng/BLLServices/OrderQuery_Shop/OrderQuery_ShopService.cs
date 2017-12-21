using DBModel.Common;
using DBModel.DBmodel;
using DBModel.OrderQuery_Shop;
using IBLLService.OrderQuery_Shop;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.OrderQuery_Shop
{
    public class OrderQuery_ShopService : IOrderQuery_ShopService
    {
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <param name="shop_id"></param>
        /// <param name="create_time"></param>
        /// <param name="order_code"></param>
        /// <param name="custorder_code"></param>
        /// <param name="emp_name"></param>
        /// <param name="state"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public List<OrderQuery_ShopModel> GetOrderQuery_ShopList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
          Int64? shop_id, DateTime? create_time, string order_code, string custorder_code, string emp_name, int? state, int? day)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<busi_custorder>()
                                   .JoinTable<busi_sendorder>((s1, s2) => s1.order_id == s2.custorder_id)
                                   .JoinTable<base_shop>((s1, s3) => s1.shop_id == s3.shop_id)
                                   .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1")
                                   .OrderBy("s1.order_id DESC");
                    if (create_time.HasValue)
                    {
                        DateTime time = create_time.Value.AddDays(1);
                        getwhere = getwhere.Where(s1 => s1.create_time >= create_time && s1.create_time < time);
                    }
                    if (!string.IsNullOrWhiteSpace(order_code))
                    {
                        getwhere = getwhere.Where<busi_sendorder>((s1, s2) => s2.order_code.Contains(order_code));
                    }
                    if (!string.IsNullOrWhiteSpace(custorder_code))
                    {
                        getwhere = getwhere.Where(s1 => s1.custorder_code.Contains(custorder_code));
                    }
                    if (shop_id > 0)
                    {
                        getwhere = getwhere.Where<base_shop>((s1, s3) => s3.shop_id == shop_id);
                    }
                    if (!string.IsNullOrWhiteSpace(emp_name))
                    {
                        getwhere = getwhere.Where(s1 => s1.cus_name.Contains(emp_name));
                    }
                    if (state > 0)
                    {
                        DateTime time = DateTime.Now;
                        string time3 = DateTime.Now.ToShortDateString();
                        if (state == 1)
                        {
                            if (day > 0)
                            {
                                string time1 = DateTime.Now.AddDays(day.Value).ToString("yyyy-MM-dd");
                                DateTime time2 = Convert.ToDateTime(time1);
                                getwhere = getwhere.Where(s1 => s1.latest_date == time2);
                            }
                            else if (day == -1)
                            {
                                getwhere = getwhere.Where(s1 => s1.latest_date == Convert.ToDateTime(time3));
                            }
                            else
                            {
                                getwhere = getwhere.Where(s1 => s1.latest_date >= time);
                            }
                        }
                        else if (state == 2)
                        {
                            if (day > 0)
                            {
                                int day1 = 0 - day.Value;
                                string time1 = DateTime.Now.AddDays(day1).ToString("yyyy-MM-dd");
                                DateTime time2 = Convert.ToDateTime(time1);
                                getwhere = getwhere.Where(s1 => s1.latest_date == time2);
                            }
                            else if (day == -1)
                            {
                                getwhere = getwhere.Where(s1 => s1.latest_date == Convert.ToDateTime(time3));
                            }
                            else
                            {
                                getwhere = getwhere.Where(s1 => s1.latest_date < time);
                            }
                        }
                    }

                    totil = getwhere.Count();
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                        .Select<OrderQuery_ShopModel>("s1.order_id,s1.custorder_code,s1.order_code,s1.order_status,s1.order_sumqty,s1.order_summoney,s1.create_time,s1.cus_name,s3.shop_name,s1.latest_date,s2.order_code as send_code ").ToList();
                    if (totil > 0)
                    {
                        foreach (var item in list)
                        {
                            //1.导入成功;10.已确认;11.已收货;20.已质检;30.已入库;40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库
                            item.create_timeE = item.create_time.ToString("yyyy-MM-dd");
                            item.latest_dateE = item.latest_date.Value.ToString("yyyy-MM-dd");
                            item.order_statusE = item.order_status == 1 ? "导入成功" : (item.order_status == 10 ? "已确认" : (item.order_status == 11 ? "已收货" : (item.order_status == 20
                                ? "已质检" : (item.order_status == 30 ? "已入库" : (item.order_status == 40 ? "已配货" : (item.order_status == 50 ? "已拣选" : (item.order_status == 60
                                ? "已包装" : (item.order_status == 70 ? "已发货" : (item.order_status == 80 ? "已转运" : (item.order_status == 90 ? "已再入库" : ""))))))))));
                        }
                    }

                    totilpage = totil / onepagecount;
                    exmsg = "";
                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }
                    return list.ToList();
                }
                catch (Exception ex)
                {
                    totilpage = 0;
                    totil = 0;
                    exmsg = ex.ToString();
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public busi_custorder GetInfoByID(long? id)
        {

            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (!id.HasValue)
                {
                    return new busi_custorder();
                }
                try
                {
                    var list = db.Queryable<busi_custorder>().Where(s => s.del_flag).InSingle(id.Value);
                    if (list == null)
                    {
                        return new busi_custorder();
                    }
                    return list;
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }


        /// <summary>
        /// 根据订单ID获取详情
        /// </summary>
        /// <param name="exmsg"></param>
        /// <param name="custorder_id"></param>
        /// <returns></returns>
        public List<OrderQuery_ShopModelE> GetOrderQuery_ShopEList(out string exmsg, Int64? custorder_id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (custorder_id == 0)
                {
                    exmsg = "参数错误";
                    return null;
                }
                try
                {
                    List<OrderQuery_ShopModelE> list = new List<OrderQuery_ShopModelE>();
                    #region 订单未发货
                    var sql1 = db.Queryable<busi_sendorder>()
                                .Where("del_flag=1 and custorder_id=" + custorder_id + " and express_id = 0")
                                .Select<OrderQuery_ShopModelE>(s1 => new OrderQuery_ShopModelE
                                {
                                    order_id = s1.order_id,
                                    order_code = s1.order_code,
                                    order_tatus = s1.order_tatus,
                                    express_id = 0,
                                    express_name = "",
                                    exp_code = "",
                                    tran_id = 0,
                                    gjexpress_id = 0,
                                    gjexpress_name = "",
                                    tran_code = "",
                                    gjexpress_code = "",
                                    is_print = s1.is_print,
                                    print_time = s1.print_time,
                                }).ToList();
                    if (sql1.Count > 0)
                    {
                        foreach (var item in sql1)
                        {//40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
                            item.order_tatusE = item.order_tatus == 40 ? "已配货" : item.order_tatus == 50 ? "已拣选" : item.order_tatus == 60 ? "已包装" :
                                item.order_tatus == 70 ? "已发货" : item.order_tatus == 80 ? "已转运" : item.order_tatus == 90 ? "已再入库" : "";
                            item.is_printE = item.is_print == true ? "是" : "否";
                            item.print_timeE = item.is_print == true ? item.print_time.Value.ToString("yyyy-MM-dd") : "";
                            List<OrderQuery_ShopModelEE> getwhere1 = new List<OrderQuery_ShopModelEE>();
                            List<OrderQuery_ShopModelEE> list1 = new List<OrderQuery_ShopModelEE>();
                            List<Int64> ids = new List<Int64>();
                            var busi_sendorder_detail = db.Queryable<busi_sendorder_detail>().Where(c => c.del_flag && c.order_id == item.order_id).ToList();
                            if (busi_sendorder_detail.Count > 0)
                            {
                                foreach (var item1 in busi_sendorder_detail)
                                {
                                    ids.Add(item1.detail_id);
                                    var purchasedetail = db.Queryable<busi_purchasedetail>().Where(c => c.del_flag && c.send_detail_id == item1.detail_id).ToList();
                                    if (purchasedetail.Count > 0)
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                         .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                         .JoinTable<busi_purchasedetail>((s1, s3) => s1.detail_id == s3.send_detail_id)
                                         .JoinTable<busi_purchasedetail, busi_purchase>((s1, s3, s4) => s3.purch_id == s4.purch_id)
                                         .JoinTable<busi_workinfo>((s1, s5) => s1.detail_id == s5.sendorder_detail_id)
                                         .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1  and s4.del_flag=1 and s5.del_flag=1 and s1.detail_id=" + item1.detail_id + "")

                                         .Select<OrderQuery_ShopModelEE>(" s1.detail_id,s2.sku_code,s4.purch_code,s3.purch_status,s5.is_work,s5.work_id")
                                         .ToList();
                                    }
                                    else
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                                     .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                     .JoinTable<busi_workinfo>((s1, s3) => s1.detail_id == s3.sendorder_detail_id)
                                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                                     .Select<base_prod_code, busi_workinfo, OrderQuery_ShopModelEE>((s1, s2, s3) => new OrderQuery_ShopModelEE
                                                     {
                                                         //prod_num = 1,
                                                         detail_id = s1.detail_id,
                                                         sku_code = s2.sku_code,
                                                         purch_code = "",
                                                         purch_status = 0,
                                                         is_work = s3.is_work,
                                                         work_id = s3.work_id,
                                                     }).ToList();
                                    }

                                    getwhere1 = getwhere1.Concat(list1).ToList<OrderQuery_ShopModelEE>();
                                }
                                if (getwhere1.Count > 0)
                                {
                                    foreach (var item1 in getwhere1)
                                    {
                                        item1.purch_statusE = item1.purch_status == 1 ? "待采购" : item1.purch_status == 2 ? "待填单号" : item1.purch_status == 3 ? "已采购" : item1.purch_status == 4 ? "已收货" : "";
                                        item1.is_workE = item1.is_work == true ? "已配" : "未配";
                                    }
                                }
                                item.details = getwhere1;
                            }
                        }
                        list = list.Concat(sql1).ToList<OrderQuery_ShopModelE>();
                    }
                    #endregion

                    #region 订单已发货 未转运
                    var sql2 = db.Queryable<busi_sendorder>()
                                .JoinTable<base_exp_comp>((s1, s2) => s1.express_id == s2.express_id)
                                .Where("s1.del_flag=1 and s2.del_flag=1 and s1.custorder_id=" + custorder_id + " and s1.express_id >0  and s1.tran_id=0")
                                .Select<base_exp_comp, OrderQuery_ShopModelE>((s1, s2) => new OrderQuery_ShopModelE
                                {
                                    order_id = s1.order_id,
                                    order_code = s1.order_code,
                                    order_tatus = s1.order_tatus,
                                    express_id = s2.express_id,
                                    express_name = s2.express_name,
                                    exp_code = s1.exp_code,
                                    tran_id = 0,
                                    gjexpress_id = 0,
                                    gjexpress_name = "",
                                    tran_code = "",
                                    gjexpress_code = "",
                                    is_print = s1.is_print,
                                    print_time = s1.print_time,
                                }).ToList();

                    if (sql2.Count > 0)
                    {
                        foreach (var item in sql2)
                        {//40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
                            item.order_tatusE = item.order_tatus == 40 ? "已配货" : item.order_tatus == 50 ? "已拣选" : item.order_tatus == 60 ? "已包装" :
                                item.order_tatus == 70 ? "已发货" : item.order_tatus == 80 ? "已转运" : item.order_tatus == 90 ? "已再入库" : "";
                            item.is_printE = item.is_print == true ? "是" : "否";
                            item.print_timeE = item.is_print == true ? item.print_time.Value.ToString("yyyy-MM-dd") : "";
                            List<OrderQuery_ShopModelEE> getwhere1 = new List<OrderQuery_ShopModelEE>();
                            List<OrderQuery_ShopModelEE> list1 = new List<OrderQuery_ShopModelEE>();

                            var busi_sendorder_detail = db.Queryable<busi_sendorder_detail>().Where(c => c.del_flag && c.order_id == item.order_id).ToList();
                            if (busi_sendorder_detail.Count > 0)
                            {
                                foreach (var item1 in busi_sendorder_detail)
                                {
                                    var purchasedetail = db.Queryable<busi_purchasedetail>().Where(c => c.del_flag && c.send_detail_id == item1.detail_id).ToList();
                                    if (purchasedetail.Count > 0)
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                         .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                         .JoinTable<busi_purchasedetail>((s1, s3) => s1.detail_id == s3.send_detail_id)
                                         .JoinTable<busi_purchasedetail, busi_purchase>((s1, s3, s4) => s3.purch_id == s4.purch_id)
                                         .JoinTable<busi_workinfo>((s1, s5) => s1.detail_id == s5.sendorder_detail_id)
                                         .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1  and s4.del_flag=1 and s5.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                         .Select<OrderQuery_ShopModelEE>("s1.detail_id,s2.sku_code,s4.purch_code,s3.purch_status,s5.is_work,s5.work_id")
                                         .ToList();
                                    }
                                    else
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                                     .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                     .JoinTable<busi_workinfo>((s1, s3) => s1.detail_id == s3.sendorder_detail_id)
                                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                                     .Select<base_prod_code, busi_workinfo, OrderQuery_ShopModelEE>((s1, s2, s3) => new OrderQuery_ShopModelEE
                                                     {
                                                         detail_id = s1.detail_id,
                                                         //prod_num = 1,
                                                         sku_code = s2.sku_code,
                                                         purch_code = "",
                                                         purch_status = 0,
                                                         is_work = s3.is_work,
                                                         work_id = s3.work_id,
                                                     }).ToList();
                                    }

                                    getwhere1 = getwhere1.Concat(list1).ToList<OrderQuery_ShopModelEE>();
                                }
                                if (getwhere1.Count > 0)
                                {
                                    foreach (var item1 in getwhere1)
                                    {
                                        item1.purch_statusE = item1.purch_status == 1 ? "待采购" : item1.purch_status == 2 ? "待填单号" : item1.purch_status == 3 ? "已采购" : item1.purch_status == 4 ? "已收货" : "";
                                        item1.is_workE = item1.is_work == true ? "已配" : "未配";
                                    }
                                }
                                item.details = getwhere1;
                            }
                        }
                        list = list.Concat(sql2).ToList<OrderQuery_ShopModelE>();
                    }
                    #endregion

                    #region 订单已发货 已转运
                    var sql3 = db.Queryable<busi_sendorder>()
                               .JoinTable<base_exp_comp>((s1, s2) => s1.express_id == s2.express_id)
                               .JoinTable<busi_transfer>((s1, s3) => s1.tran_id == s3.tran_id)
                               .JoinTable<busi_transfer, base_exp_comp>((s1, s3, s4) => s1.express_id == s4.express_id)
                               .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.custorder_id=" + custorder_id + " and s1.express_id >0  and s1.tran_id>0")
                               .Select<base_exp_comp, busi_transfer, base_exp_comp, OrderQuery_ShopModelE>((s1, s2, s3, s4) => new OrderQuery_ShopModelE
                               {
                                   order_id = s1.order_id,
                                   order_code = s1.order_code,
                                   order_tatus = s1.order_tatus,
                                   express_id = s2.express_id,
                                   express_name = s2.express_name,
                                   exp_code = s1.exp_code,
                                   tran_id = s1.tran_id,
                                   gjexpress_id = s4.express_id,
                                   gjexpress_name = s4.express_name,
                                   tran_code = s3.tran_code,
                                   gjexpress_code = s3.express_code,
                                   is_print = s1.is_print,
                                   print_time = s1.print_time,
                               }).ToList();
                    if (sql3.Count > 0)
                    {
                        foreach (var item in sql3)
                        {//40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
                            item.order_tatusE = item.order_tatus == 40 ? "已配货" : item.order_tatus == 50 ? "已拣选" : item.order_tatus == 60 ? "已包装" :
                                item.order_tatus == 70 ? "已发货" : item.order_tatus == 80 ? "已转运" : item.order_tatus == 90 ? "已再入库" : "";
                            item.is_printE = item.is_print == true ? "是" : "否";
                            item.print_timeE = item.is_print == true ? item.print_time.Value.ToString("yyyy-MM-dd") : "";
                            List<OrderQuery_ShopModelEE> getwhere1 = new List<OrderQuery_ShopModelEE>();
                            List<OrderQuery_ShopModelEE> list1 = new List<OrderQuery_ShopModelEE>();

                            var busi_sendorder_detail = db.Queryable<busi_sendorder_detail>().Where(c => c.del_flag && c.order_id == item.order_id).ToList();
                            if (busi_sendorder_detail.Count > 0)
                            {
                                foreach (var item1 in busi_sendorder_detail)
                                {
                                    var purchasedetail = db.Queryable<busi_purchasedetail>().Where(c => c.del_flag && c.send_detail_id == item1.detail_id).ToList();
                                    if (purchasedetail.Count > 0)
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                         .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                         .JoinTable<busi_purchasedetail>((s1, s3) => s1.detail_id == s3.send_detail_id)
                                         .JoinTable<busi_purchasedetail, busi_purchase>((s1, s3, s4) => s3.purch_id == s4.purch_id)
                                         .JoinTable<busi_workinfo>((s1, s5) => s1.detail_id == s5.sendorder_detail_id)
                                         .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1  and s4.del_flag=1 and s5.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                         .Select<OrderQuery_ShopModelEE>("s1.detail_id,s2.sku_code,s4.purch_code,s3.purch_status,s5.is_work,s5.work_id")
                                         .ToList();
                                    }
                                    else
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                                     .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                     .JoinTable<busi_workinfo>((s1, s3) => s1.detail_id == s3.sendorder_detail_id)
                                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                                     .Select<base_prod_code, busi_workinfo, OrderQuery_ShopModelEE>((s1, s2, s3) => new OrderQuery_ShopModelEE
                                                     {
                                                         detail_id = s1.detail_id,
                                                         //prod_num = 1,
                                                         sku_code = s2.sku_code,
                                                         purch_code = "",
                                                         purch_status = 0,
                                                         is_work = s3.is_work,
                                                         work_id = s3.work_id,
                                                     }).ToList();
                                    }

                                    getwhere1 = getwhere1.Concat(list1).ToList<OrderQuery_ShopModelEE>();
                                }
                                if (getwhere1.Count > 0)
                                {
                                    foreach (var item1 in getwhere1)
                                    {
                                        item1.purch_statusE = item1.purch_status == 1 ? "待采购" : item1.purch_status == 2 ? "待填单号" : item1.purch_status == 3 ? "已采购" : item1.purch_status == 4 ? "已收货" : "";
                                        item1.is_workE = item1.is_work == true ? "已配" : "未配";
                                    }
                                }
                                item.details = getwhere1;
                            }
                        }
                        list = list.Concat(sql3).ToList<OrderQuery_ShopModelE>();
                    }
                    #endregion

                    exmsg = "";
                    return list.ToList();
                }
                catch (Exception ex)
                {
                    exmsg = ex.ToString();
                    return null;
                }
            }

        }

        /// <summary>
        /// 修改收件人信息
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public OrderQuery_ShopResult Save(List<busi_sendorder> lists)
        {
            bool rstNum = false;
            bool rstNums = false;
            string ss = string.Empty;
            OrderQuery_ShopResult result = new OrderQuery_ShopResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();
                    int suc = 0;
                    if (lists.Count > 0)
                    {
                        foreach (var item in lists)
                        {
                            if (item.order_id == 0)
                            {
                                db.RollbackTran();
                                result.success = false;
                                result.Msg = "参数错误!";
                                return result;
                            }
                            var list = db.Queryable<busi_sendorder>().Where(a => a.del_flag).InSingle(item.order_id);
                            if (list == null)
                            {
                                db.RollbackTran();
                                result.success = false;
                                result.Msg = "不存在的信息";
                                return result;
                            }

                            if (string.IsNullOrWhiteSpace(item.receive_name))
                            {
                                db.RollbackTran();
                                result.success = false;
                                result.Msg = "包裹号为 " + list.order_code + ": 姓名不得为空!";
                                return result;
                            }
                            if (string.IsNullOrWhiteSpace(item.receive_address))
                            {
                                db.RollbackTran();
                                result.success = false;
                                result.Msg = "包裹号为 " + list.order_code + ": 地址不得为空!";
                                return result;
                            }
                            if (string.IsNullOrWhiteSpace(item.receive_zip))
                            {
                                db.RollbackTran();
                                result.success = false;
                                result.Msg = "包裹号为 " + list.order_code + ": 邮编不得为空!";
                                return result;
                            }
                            if (string.IsNullOrWhiteSpace(item.receive_mobile))
                            {
                                db.RollbackTran();
                                result.success = false;
                                result.Msg = "包裹号为 " + list.order_code + ": 手机不得为空!";
                                return result;
                            }
                            if (string.IsNullOrWhiteSpace(item.receive_phone))
                            {
                                db.RollbackTran();
                                result.success = false;
                                result.Msg = "包裹号为 " + list.order_code + ": 电话不得为空!";
                                return result;
                            }

                            list.receive_name = item.receive_name;
                            list.receive_address = item.receive_address;
                            list.receive_zip = item.receive_zip;
                            list.receive_mobile = item.receive_mobile;
                            list.receive_phone = item.receive_phone;
                            list.edit_time = DateTime.Now;
                            list.edit_user_id = LoginUser.Current.user_id;
                            rstNum = db.Update<busi_sendorder>(list);
                            if (rstNum)
                            {
                                suc += 1;
                                ss += list.order_code + ",";
                            }
                        }
                        if (suc == lists.Count)
                        {
                            rstNums = true;
                        }

                    }
                    if (rstNums)
                    {
                        db.CommitTran();
                        result.success = true;
                        result.Msg = "已成功修改包裹号为" + ss + "收件人信息!";
                        return result;
                    }
                    else
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "操作失败";
                        return result;
                    }
                }
                catch (Exception)
                {
                    db.RollbackTran();
                    throw;
                }
            }
        }




        /// <summary>
        /// 客户退货
        /// </summary>
        /// <param name="detail_id"></param>
        /// <param name="work_id"></param>
        /// <returns></returns>
        public OrderQuery_ShopResult Del(Int64? detail_id, Int64? work_id, Int64? id)
        {
            bool rstNum = false;
            bool rstNums = false;
            bool rstNumss = false;
            bool rstNumsss = false;
            OrderQuery_ShopResult result = new OrderQuery_ShopResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();
                    var list = db.Queryable<busi_sendorder_detail>().Where(a => a.del_flag).InSingle(detail_id);
                    if (list == null)
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "不存在的信息";
                        return result;
                    }
                    rstNum = db.Update<busi_sendorder_detail>(new { del_flag = false, remark = "订单退货" }, a => a.detail_id == detail_id);
                    rstNums = db.Update<busi_workinfo>(new { del_flag = false, remark = "订单退货", DelOrBarter = 1 }, a => a.work_id == work_id);
                    var sendorder = db.Queryable<busi_sendorder>().Where(a => a.del_flag).InSingle(list.order_id);
                    if (sendorder != null)
                    {
                        if (sendorder.tran_id > 0)
                        {
                            db.RollbackTran();
                            result.success = false;
                            result.Msg = "该包裹已装箱,无法操作";
                            return result;
                        }

                        if (sendorder.prod_num > 1)
                        {
                            rstNumss = db.Update<busi_sendorder>(new { prod_num = sendorder.prod_num - 1, prod_money = sendorder.prod_money - list.prod_price }, a => a.order_id == sendorder.order_id);
                            var custorder = db.Queryable<busi_custorder>().Where(a => a.del_flag).InSingle(sendorder.custorder_id);
                            if (custorder != null)
                            {
                                rstNumsss = db.Update<busi_custorder>(new { order_sumqty = custorder.order_sumqty - 1, order_summoney = custorder.order_summoney - list.prod_price }, a => a.order_id == custorder.order_id);
                            }
                        }
                        else
                        {
                            rstNumss = db.Update<busi_sendorder>(new { del_flag = false }, a => a.order_id == sendorder.order_id);
                            var custorder = db.Queryable<busi_custorder>().Where(a => a.del_flag).InSingle(sendorder.custorder_id);
                            if (custorder != null)
                            {
                                rstNumsss = db.Update<busi_custorder>(new { del_flag = false }, a => a.order_id == custorder.order_id);
                            }
                        }
                    }
                    if (rstNum && rstNums && rstNumss && rstNumsss)
                    {
                        db.CommitTran();
                        result.success = true;
                        result.Msg = "已成功删除该订单信息!";
                        //result.URL = "/OrderQuery_Shop/IndexE?id=" + id;
                        return result;
                    }
                    else
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "操作失败";
                        return result;
                    }
                }
                catch (Exception)
                {
                    db.RollbackTran();
                    throw;
                }
            }
        }



        /// <summary>
        /// 客户换货
        /// </summary>
        /// <param name="detail_id"></param>
        /// <param name="work_id"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public OrderQuery_ShopResult Barter(Int64? detail_id, Int64? work_id, string sku, Int64? id)
        {
            bool rstNum = false;
            bool rstNums = false;
            // bool rstNumss = false;
            //bool rstNumsss = false;
            OrderQuery_ShopResult result = new OrderQuery_ShopResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();
                    var prod_code = db.Queryable<base_prod_code>().Where(a => a.del_flag && a.sku_code == sku).FirstOrDefault();
                    if (prod_code == null)
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "无效的sku,操作失败";
                        return result;
                    }

                    var list = db.Queryable<busi_sendorder_detail>().Where(a => a.del_flag).InSingle(detail_id);
                    if (list == null)
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "不存在的信息";
                        return result;
                    }

                    var sendorder = db.Queryable<busi_sendorder>().Where(a => a.del_flag).InSingle(list.order_id);
                    if (sendorder == null)
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "不存在的信息";
                        return result;
                    }

                    if (sendorder.tran_id > 0)
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "该包裹已装箱,无法操作";
                        return result;
                    }


                    decimal? price_cn;
                    var product = db.Queryable<base_product>().Where(a => a.del_flag).InSingle(prod_code.prod_id);
                    if (product == null) { price_cn = 0; } else { price_cn = product.price_cn; }
                    rstNum = db.Update<busi_sendorder_detail>(new { prod_num = list.prod_num - 1 }, a => a.detail_id == detail_id);
                    busi_sendorder_detail sdetail = new busi_sendorder_detail();
                    sdetail.code_id = prod_code.code_id;
                    sdetail.create_time = DateTime.Now;
                    sdetail.create_user_id = LoginUser.Current.user_id;
                    sdetail.del_flag = true;
                    sdetail.del_time = DateTime.Now;
                    sdetail.del_user_id = 0;
                    sdetail.detail_status = 0;
                    sdetail.edit_time = DateTime.Now;
                    sdetail.edit_user_id = 0;
                    sdetail.order_id = list.order_id;
                    sdetail.prod_id = prod_code.prod_id;
                    sdetail.prod_name = "";
                    sdetail.prod_num = 1;
                    sdetail.remark = "订单换货";
                    sdetail.prod_price = price_cn;
                    var isdetail = db.Insert<busi_sendorder_detail>(sdetail).ObjToInt();

                    var isworkinfo = 0;
                    var workinfos = db.Queryable<busi_workinfo>().Where(a => a.del_flag).InSingle(work_id);
                    if (workinfos != null)
                    {
                        rstNums = db.Update<busi_workinfo>(new { DelOrBarter = 2, del_flag = false, remark = "订单换货" }, a => a.work_id == work_id);
                        busi_workinfo workinfo = new busi_workinfo();
                        workinfo.area_id = 0;
                        workinfo.packid = workinfos.packid;
                        workinfo.create_time = DateTime.Now;
                        workinfo.custorder_id = workinfos.custorder_id;
                        workinfo.create_user_id = LoginUser.Current.user_id;
                        workinfo.custorder_detail_id = Convert.ToInt64(isdetail);
                        workinfo.del_flag = true;
                        workinfo.del_time = DateTime.Now;
                        workinfo.del_user_id = 0;
                        workinfo.detail_source = 2;
                        workinfo.edit_time = DateTime.Now;
                        workinfo.edit_user_id = 0;
                        workinfo.is_work = false;
                        workinfo.locat_id = 0;
                        workinfo.plat_id = 0;
                        workinfo.prod_code_id = prod_code.code_id;
                        workinfo.remark = "订单换货";
                        workinfo.sendorder_detail_id = Convert.ToInt64(isdetail);
                        workinfo.shop_id = workinfos.shop_id;
                        workinfo.wh_id = 0;
                        workinfo.work_time = DateTime.Now;
                        workinfo.work_type = 0;
                        workinfo.islock = 0;
                        workinfo.DelOrBarter = 2;
                        workinfo.DelOrBarter_work_id = workinfos.work_id;
                        isworkinfo = db.Insert<busi_workinfo>(workinfo).ObjToInt();
                    }
                    if (rstNum && rstNums && isdetail > 0 && isworkinfo > 0)
                    {
                        db.CommitTran();
                        result.success = true;
                        result.Msg = "已成功换货该订单信息!";
                        result.URL = "/OrderQuery_Shop/IndexE?id=" + id;
                        return result;
                    }
                    else
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "操作失败";
                        return result;
                    }
                }
                catch (Exception)
                {
                    db.RollbackTran();
                    throw;
                }
            }
        }

    }
}
