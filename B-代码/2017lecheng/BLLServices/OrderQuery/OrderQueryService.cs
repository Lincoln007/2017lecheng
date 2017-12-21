using CommHelper;
using DBModel.Common;
using DBModel.DBmodel;
using DBModel.OrderQuery;
using IBLLService.OrderQuery;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.OrderQuery
{
    public class OrderQueryService : IOrderQueryService
    {
        /// <summary>
        /// 获取列表
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
        /// <returns></returns>
        public List<OrderQueryModel> GetIOrderQueryList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
            Int64? shop_id, DateTime? create_time, string order_code, string custorder_code, string emp_name)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<busi_custorder>()
                        .JoinTable<busi_sendorder>((s1, s2) => s1.order_id == s2.custorder_id)
                                   .JoinTable<base_shop>((s1, s3) => s1.shop_id == s3.shop_id)
                                   .Where("s1.del_flag=1 and s2.del_flag=1  and s3.del_flag=1").OrderBy("s1.order_id DESC"); ;
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

                    totil = getwhere.Count();
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                        .Select<OrderQueryModel>("s1.order_id,s1.custorder_code,s1.order_code,s1.order_status,s1.order_sumqty,s1.order_summoney,s1.create_time,s1.cus_name,s3.shop_name,s2.order_code as send_code").ToList();


                    if (totil > 0)
                    {
                        foreach (var item in list)
                        {
                            //1.导入成功;10.已确认;11.已收货;20.已质检;30.已入库;40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
                            item.create_timeE = item.create_time.ToString("yyyy-MM-dd");
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
        /// 根据ID获取详细信息
        /// </summary>
        /// <param name="exmsg"></param>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public List<OrderQueryModelE> GetIOrderQueryListE(out string exmsg, Int64? order_id)
        {

            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (order_id == 0)
                {
                    exmsg = "参数错误";
                    return null;
                }
                try
                {
                    List<OrderQueryModelE> list = new List<OrderQueryModelE>();
                    var sql1 = db.Queryable<busi_sendorder>()
                              .Where("del_flag=1 and custorder_id=" + order_id + " and express_id = 0")
                              .Select<OrderQueryModelE>(s1 => new OrderQueryModelE
                              {
                                  exp_code = s1.exp_code,
                                  express_name = "",
                                  order_code = s1.order_code,
                                  order_id = s1.order_id,
                              }).ToList();

                    if (sql1.Count > 0)
                    {
                        foreach (var item in sql1)
                        {
                            List<OrderQueryModelEE> getwhere1 = new List<OrderQueryModelEE>();
                            var sendorder_detail = db.Queryable<busi_sendorder_detail>().Where(c => c.del_flag && c.order_id == item.order_id).ToList();

                            if (sendorder_detail.Count > 0)
                            {
                                foreach (var item1 in sendorder_detail)
                                {
                                    List<OrderQueryModelEE> list1 = new List<OrderQueryModelEE>();
                                    var purchasedetail = db.Queryable<busi_purchasedetail>().Where(c => c.del_flag && c.send_detail_id == item1.detail_id).ToList();
                                    //  var wh_stock = db.Queryable<base_wh_stock>().Where(c => c.del_flag && c.code_id == item1.code_id).ToList();
                                    var wh_stock = db.Queryable<base_wh_stock>().Where(s => s.del_flag && s.code_id == item1.code_id && s.wh_id == 1 && s.location_id != 1).FirstOrDefault();
                                    if (purchasedetail.Count > 0)
                                    {
                                        if (wh_stock != null)
                                        {
                                            list1 = db.Queryable<busi_sendorder_detail>()
                                       .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                       .JoinTable<busi_purchasedetail>((s1, s3) => s1.detail_id == s3.send_detail_id)
                                       .JoinTable<busi_purchasedetail, busi_purchase>((s1, s3, s4) => s3.purch_id == s4.purch_id)
                                        .JoinTable<base_wh_stock>((s1, s5) => s1.code_id == s5.code_id)
                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1  and s4.del_flag=1 and s5.del_flag=1 and  s1.detail_id=" + item1.detail_id + " and s5.code_id=" + wh_stock.code_id + "  and s5.wh_id=1 and s5.location_id<>1 ")
                                       .Select<OrderQueryModelEE>("s1.prod_num,s2.sku_code,s4.purch_code,s4.purch_status,s5.stock_qty")
                                       .ToList();
                                        }
                                        else
                                        {
                                            list1 = db.Queryable<busi_sendorder_detail>()
                                       .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                       .JoinTable<busi_purchasedetail>((s1, s3) => s1.detail_id == s3.send_detail_id)
                                       .JoinTable<busi_purchasedetail, busi_purchase>((s1, s3, s4) => s3.purch_id == s4.purch_id)
                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1  and s4.del_flag=1 and  s1.detail_id=" + item1.detail_id + "")
                                       .Select<base_prod_code, busi_purchase, OrderQueryModelEE>((s1, s2, s4) => new OrderQueryModelEE
                                           {
                                               prod_num = s1.prod_num,
                                               sku_code = s2.sku_code,
                                               purch_code = s4.purch_code,
                                               purch_status = s4.purch_status,
                                               stock_qty = 0,
                                           }).ToList();
                                        }
                                    }
                                    else
                                    {
                                        if (wh_stock != null)
                                        {
                                            list1 = db.Queryable<busi_sendorder_detail>()
                                                       .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                       .JoinTable<base_wh_stock>((s1, s3) => s1.code_id == s3.code_id)
                                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.detail_id=" + item1.detail_id + " and s3.code_id=" + wh_stock.code_id + "  and s3.wh_id=1 and s3.location_id<>1 ")
                                                       .Select<base_prod_code, base_wh_stock, OrderQueryModelEE>((s1, s2, s3) => new OrderQueryModelEE
                                                       {
                                                           prod_num = s1.prod_num,
                                                           sku_code = s2.sku_code,
                                                           purch_code = "",
                                                           purch_status = 0,
                                                           stock_qty = s3.stock_qty,
                                                       }).ToList();
                                        }
                                        else
                                        {
                                            list1 = db.Queryable<busi_sendorder_detail>()
                                                       .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                                       .Select<base_prod_code, OrderQueryModelEE>((s1, s2) => new OrderQueryModelEE
                                                       {
                                                           prod_num = s1.prod_num,
                                                           sku_code = s2.sku_code,
                                                           purch_code = "",
                                                           purch_status = 0,
                                                           stock_qty = 0,
                                                       }).ToList();
                                        }


                                    }
                                    getwhere1 = getwhere1.Concat(list1).ToList<OrderQueryModelEE>();
                                }
                                if (getwhere1.Count > 0)
                                {
                                    foreach (var item1 in getwhere1)
                                    {
                                        item1.purch_statusE = item1.purch_status == 1 ? "初始" : item1.purch_status == 2 ? "已采购" : item1.purch_status == 3 ? "已全部到货" : "未配货";
                                    }
                                }
                                item.details = getwhere1;
                            }
                        }

                        list = list.Concat(sql1).ToList<OrderQueryModelE>();

                    }
                    var sql2 = db.Queryable<busi_sendorder>()
                        .JoinTable<base_exp_comp>((s1, s2) => s1.express_id == s2.express_id)
                             .Where("s1.del_flag=1 and s2.del_flag=1 and  s1.custorder_id=" + order_id + " and s1.express_id > 0")
                             .Select<base_exp_comp, OrderQueryModelE>((s1, s2) => new OrderQueryModelE
                             {
                                 exp_code = s1.exp_code,
                                 order_code = s1.order_code,
                                 order_id = s1.order_id,
                                 express_name = s2.express_name,
                             }).ToList();
                    if (sql2.Count > 0)
                    {
                        foreach (var item in sql2)
                        {
                            List<OrderQueryModelEE> getwhere1 = new List<OrderQueryModelEE>();
                            var sendorder_detail = db.Queryable<busi_sendorder_detail>().Where(c => c.del_flag && c.order_id == item.order_id).ToList();

                            if (sendorder_detail.Count > 0)
                            {
                                foreach (var item1 in sendorder_detail)
                                {
                                    List<OrderQueryModelEE> list1 = new List<OrderQueryModelEE>();
                                    var purchasedetail = db.Queryable<busi_purchasedetail>().Where(c => c.del_flag && c.send_detail_id == item1.detail_id).ToList();

                                    var wh_stock = db.Queryable<base_wh_stock>().Where(s => s.del_flag && s.code_id == item1.code_id && s.wh_id == 1 && s.location_id != 1).FirstOrDefault();
                                    if (purchasedetail.Count > 0)
                                    {
                                        if (wh_stock != null)
                                        {
                                            list1 = db.Queryable<busi_sendorder_detail>()
                                       .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                       .JoinTable<busi_purchasedetail>((s1, s3) => s1.detail_id == s3.send_detail_id)
                                       .JoinTable<busi_purchasedetail, busi_purchase>((s1, s3, s4) => s3.purch_id == s4.purch_id)
                                        .JoinTable<base_wh_stock>((s1, s5) => s1.code_id == s5.code_id)
                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1  and s4.del_flag=1 and s5.del_flag=1 and  s1.detail_id=" + item1.detail_id + " and s5.code_id=" + wh_stock.code_id + "  and s5.wh_id=1 and s5.location_id<>1 ")
                                       .Select<OrderQueryModelEE>("s1.prod_num,s2.sku_code,s4.purch_code,s4.purch_status,s5.stock_qty")
                                       .ToList();
                                        }
                                        else
                                        {
                                            list1 = db.Queryable<busi_sendorder_detail>()
                                       .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                       .JoinTable<busi_purchasedetail>((s1, s3) => s1.detail_id == s3.send_detail_id)
                                       .JoinTable<busi_purchasedetail, busi_purchase>((s1, s3, s4) => s3.purch_id == s4.purch_id)
                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1  and s4.del_flag=1 and  s1.detail_id=" + item1.detail_id + "")
                                       .Select<base_prod_code, busi_purchase, OrderQueryModelEE>((s1, s2, s4) => new OrderQueryModelEE
                                       {
                                           prod_num = s1.prod_num,
                                           sku_code = s2.sku_code,
                                           purch_code = s4.purch_code,
                                           purch_status = s4.purch_status,
                                           stock_qty = 0,
                                       }).ToList();
                                        }
                                    }
                                    else
                                    {
                                        if (wh_stock != null)
                                        {
                                            list1 = db.Queryable<busi_sendorder_detail>()
                                                       .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                       .JoinTable<base_wh_stock>((s1, s3) => s1.code_id == s3.code_id)
                                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.detail_id=" + item1.detail_id + " and s3.code_id=" + wh_stock.code_id + "  and s3.wh_id=1 and s3.location_id<>1 ")
                                                       .Select<base_prod_code, base_wh_stock, OrderQueryModelEE>((s1, s2, s3) => new OrderQueryModelEE
                                                       {
                                                           prod_num = s1.prod_num,
                                                           sku_code = s2.sku_code,
                                                           purch_code = "",
                                                           purch_status = 0,
                                                           stock_qty = s3.stock_qty,
                                                       }).ToList();
                                        }
                                        else
                                        {
                                            list1 = db.Queryable<busi_sendorder_detail>()
                                                       .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                       .Where("s1.del_flag=1 and s2.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                                       .Select<base_prod_code, OrderQueryModelEE>((s1, s2) => new OrderQueryModelEE
                                                       {
                                                           prod_num = s1.prod_num,
                                                           sku_code = s2.sku_code,
                                                           purch_code = "",
                                                           purch_status = 0,
                                                           stock_qty = 0,
                                                       }).ToList();
                                        }


                                    }
                                    getwhere1 = getwhere1.Concat(list1).ToList<OrderQueryModelEE>();
                                }
                                if (getwhere1.Count > 0)
                                {
                                    foreach (var item1 in getwhere1)
                                    {
                                        item1.purch_statusE = item1.purch_status == 1 ? "初始" : item1.purch_status == 2 ? "已采购" : item1.purch_status == 3 ? "已全部到货" : "未配货";
                                    }
                                }
                                item.details = getwhere1;
                            }
                        }

                        list = list.Concat(sql2).ToList<OrderQueryModelE>();

                    }

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
        /// 根据ID获取收件人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<busi_sendorder> GetInfoByID(Int64? id)
        {
            OrderQueryResult result = new OrderQueryResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (id == 0)
                {
                    result.success = false;
                    result.Msg = "参数错误";
                    return null;
                }
                try
                {
                    var list = db.Queryable<busi_sendorder>().Where("del_flag=1 and custorder_id=" + id + "").ToList();
                    if (list.Count == 0)
                    {
                        result.success = false;
                        result.DataResult = "";
                        return null;
                    }

                    result.success = true;
                    result.DataResult = list;
                    return list.ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }

        }


    }
}
