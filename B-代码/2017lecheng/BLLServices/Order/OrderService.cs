using DBModel.Common;
using IBLLService;
using SqlSugarRepository;
using IBLLService.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DBModel.DBmodel;
using DBModel.ViewModel.Order;
using DBModel.ViewModel;
using System.Collections;
using System.Reflection;
using System.Data.SqlClient;
using DBModel.Order;

namespace BLLServices.Order
{
    public class OrderService : IOrderService
    {

        /// <summary>
        /// 通过时间查询店铺总的订单数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<OrderSum> GetOrderCountByTime(string time)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var sql = db.Queryable<busi_custorder>()
                         .JoinTable<base_shop>((s1, s2) => s1.shop_id == s2.shop_id)
                         .JoinTable<base_platform>((s1, s3) => s1.platform_id == s3.platform_id);
                if (!string.IsNullOrEmpty(time))
                {
                    sql.Where(s1 => s1.order_date >= time.ObjToDate() && time.ObjToDate().AddDays(1) > s1.order_date);
                }
                else
                {
                    sql.Where(s1 => s1.order_date >= DateTime.Now.ObjToDate() && DateTime.Now.ObjToDate().AddDays(1) > s1.order_date);
                }

                var result = sql.GroupBy("s1.shop_id,s1.platform_id,s2.shop_name,s3.platform_name").Select<OrderSum>
                    ("s1.platform_id platform_id,s1.shop_id shop_id,s2.shop_name shop_name,s3.platform_name platform_name,count(s1.order_id) OrderCount").ToList();
                return result;

            }
        }

        /// <summary>
        /// 通过店铺ID获取店铺订单
        /// </summary>
        /// <param name="shop_id"></param>
        /// <returns></returns>
        public List<ShopOrder> GetShopOrderById(long shop_id, int pageIndex, int pageSize, out int count, string time)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var sql = db.Queryable<busi_custorder>()
                         .JoinTable<base_shop>((s1, s2) => s1.shop_id == s2.shop_id)
                         .Where(s1 => s1.shop_id == shop_id);
                if (!string.IsNullOrEmpty(time))
                {
                    DateTime torrow = time.ObjToDate().AddDays(1);
                    sql.Where(s1 => s1.order_date >= time.ObjToDate() && s1.order_date < torrow.ObjToDate());
                }
                count = sql.Count();
                var result = sql.OrderBy("s1.order_id").Skip(pageSize * (pageIndex - 1)).Take(pageSize).Select<base_shop, ShopOrder>((s1, s2) => new ShopOrder
                {
                    shop_id = s1.shop_id,
                    shop_name = s2.shop_name,
                    order_summoney = s1.order_summoney,
                    order_sumqty = s1.order_sumqty,
                    custorder_code = s1.custorder_code,
                    order_code = s1.order_code,
                    order_id = s1.order_id,
                    order_status = s1.order_status

                }).ToList();
                return result;
            }
        }

        /// <summary>
        /// 通过订单ID获取作业表信息
        /// </summary>
        /// <param name="order_detail_id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<OrderWork> GetOrderWorkById(long order_id, int pageIndex, int pageSize, out int count)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                //统计
                var childsql = db.Queryable<busi_workinfo>()
                             .JoinTable<busi_custorder_detail>((s1, s2) => s1.custorder_detail_id == s2.detail_id)
                             .JoinTable<busi_custorder_detail, base_wh_stock>((s1, s2, s3) => s2.prod_id == s3.prod_id)
                             .JoinTable<busi_custorder_detail, busi_custorder>((s1, s2, s4) => s2.order_id == s4.order_id)
                             .Where("s4.order_id=" + order_id + "")
                              .GroupBy("s1.custorder_detail_id,s1.wh_id,s1.detail_source")
                              .Select<OrderWork>("s1.custorder_detail_id custorder_detail_id,count(s1.work_id) num,Sum(s3.stock_qty) sotck_num").ToSql();
                string childTableName = SqlSugar.SqlSugarTool.PackagingSQL(childsql.Key);//将SQL语句用()包成表
                var sql = db.Queryable<busi_workinfo>()
                         .JoinTable<busi_custorder_detail>((s1, s2) => s1.custorder_detail_id == s2.detail_id)
                         .JoinTable<busi_custorder_detail, busi_custorder>((s1, s2, s3) => s2.order_id == s3.order_id)
                         .JoinTable<busi_custorder, busi_sendorder>((s1, s3, s4) => s3.order_id == s4.custorder_id)
                         .JoinTable<base_prod_code>((s1, s5) => s1.prod_code_id == s5.code_id)
                         .JoinTable<busi_custorder_detail, base_wh_stock>((s1, s2, s6) => s2.prod_id == s6.prod_id)
                             .JoinTable<busi_sendorder, base_exp_comp>((s1, s4, s8) => s4.express_id == s8.express_id)
                         .JoinTable(childTableName, "s7", "s7.custorder_detail_id=s2.detail_id", JoinType.Inner)
                         .Where<busi_custorder>((s1, s3) => s3.order_id == order_id);
                count = sql.Count();
                var result = sql.OrderBy(s1 => s1.create_time).Skip(pageSize * (pageIndex - 1)).Take(pageSize)
                 .Select<OrderWork>("s4.order_id send_order_id,s5.sku_code sku_code,s1.wh_id wh_id," +
                "s1.detail_source detail_source,s4.exp_code exp_code,s4.express_id express_id,s8.express_name express_name" +
                ",s4.order_code order_code,s7.num,s7.sotck_num" +
                ",s4.receive_name receive_name,s4.receive_address receive_address,s2.prod_id proId,s4.receive_mobile receive_mobile,s4.receive_phone receive_phone,s4.receive_zip receive_zip").ToList();

                return result;

            }
        }

        /// <summary>
        /// 通过订单ID获取信息
        /// </summary>
        /// <param name="order_detail_id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<OrderWorkE> GetOrderEById(long order_id, out string exmsg)
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
                    List<OrderWorkE> list = new List<OrderWorkE>();
                    var sql1 = db.Queryable<busi_sendorder>()
                              .Where("del_flag=1 and custorder_id=" + order_id + " and express_id = 0")
                              .Select<OrderWorkE>(s1 => new OrderWorkE
                              {
                                  exp_code = s1.exp_code,
                                  express_name = "",
                                  order_code = s1.order_code,
                                  order_id = s1.order_id,
                                  custorder_id = s1.custorder_id,
                              }).ToList();

                    if (sql1.Count > 0)
                    {
                        foreach (var item in sql1)
                        {
                            List<OrderWorkEE> getwhere1 = new List<OrderWorkEE>();
                            var sendorder_detail = db.Queryable<busi_sendorder_detail>().Where(c => c.del_flag && c.order_id == item.order_id).ToList();
                            if (sendorder_detail.Count > 0)
                            {
                                foreach (var item1 in sendorder_detail)
                                {
                                    List<OrderWorkEE> list1 = new List<OrderWorkEE>();
                                    var wh_stock = db.Queryable<base_wh_stock>().Where(s => s.del_flag && s.code_id == item1.code_id && s.wh_id == 1 && s.location_id != 1).FirstOrDefault();
                                    if (wh_stock != null)
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                                   .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                   .JoinTable<base_wh_stock>((s1, s3) => s1.code_id == s3.code_id)
                                                   .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.detail_id=" + item1.detail_id + " and s3.code_id=" + wh_stock.code_id + "  and s3.wh_id=1 and s3.location_id<>1 ")
                                                   .Select<base_prod_code, base_wh_stock, OrderWorkEE>((s1, s2, s3) => new OrderWorkEE
                                                   {
                                                       prod_num = s1.prod_num,
                                                       sku_code = s2.sku_code,
                                                       stock_qty = s3.stock_qty,
                                                   }).ToList();
                                    }
                                    else
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                                   .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                   .Where("s1.del_flag=1 and s2.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                                   .Select<base_prod_code, OrderWorkEE>((s1, s2) => new OrderWorkEE
                                                   {
                                                       prod_num = s1.prod_num,
                                                       sku_code = s2.sku_code,
                                                       stock_qty = 0,
                                                   }).ToList();
                                    }
                                    getwhere1 = getwhere1.Concat(list1).ToList<OrderWorkEE>();
                                }
                                item.details = getwhere1;
                            }
                        }
                        list = list.Concat(sql1).ToList<OrderWorkE>();
                    }
                    var sql2 = db.Queryable<busi_sendorder>()
                        .JoinTable<base_exp_comp>((s1, s2) => s1.express_id == s2.express_id)
                             .Where("s1.del_flag=1 and s2.del_flag=1 and  s1.custorder_id=" + order_id + " and s1.express_id > 0")
                             .Select<base_exp_comp, OrderWorkE>((s1, s2) => new OrderWorkE
                             {
                                 exp_code = s1.exp_code,
                                 order_code = s1.order_code,
                                 order_id = s1.order_id,
                                 express_name = s2.express_name,
                                 custorder_id = s1.custorder_id,
                             }).ToList();
                    if (sql2.Count > 0)
                    {
                        foreach (var item in sql2)
                        {
                            List<OrderWorkEE> getwhere1 = new List<OrderWorkEE>();
                            var sendorder_detail = db.Queryable<busi_sendorder_detail>().Where(c => c.del_flag && c.order_id == item.order_id).ToList();

                            if (sendorder_detail.Count > 0)
                            {
                                foreach (var item1 in sendorder_detail)
                                {
                                    List<OrderWorkEE> list1 = new List<OrderWorkEE>();
                                    var wh_stock = db.Queryable<base_wh_stock>().Where(s => s.del_flag && s.code_id == item1.code_id && s.wh_id == 1 && s.location_id != 1).FirstOrDefault();
                                    if (wh_stock != null)
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                                   .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                   .JoinTable<base_wh_stock>((s1, s3) => s1.code_id == s3.code_id)
                                                   .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.detail_id=" + item1.detail_id + " and s3.code_id=" + wh_stock.code_id + "  and s3.wh_id=1 and s3.location_id<>1 ")
                                                   .Select<base_prod_code, base_wh_stock, OrderWorkEE>((s1, s2, s3) => new OrderWorkEE
                                                   {
                                                       prod_num = s1.prod_num,
                                                       sku_code = s2.sku_code,
                                                       stock_qty = s3.stock_qty,
                                                   }).ToList();
                                    }
                                    else
                                    {
                                        list1 = db.Queryable<busi_sendorder_detail>()
                                                   .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                                                   .Where("s1.del_flag=1 and s2.del_flag=1 and s1.detail_id=" + item1.detail_id + "")
                                                   .Select<base_prod_code, OrderWorkEE>((s1, s2) => new OrderWorkEE
                                                   {
                                                       prod_num = s1.prod_num,
                                                       sku_code = s2.sku_code,
                                                       stock_qty = 0,
                                                   }).ToList();
                                    }
                                    getwhere1 = getwhere1.Concat(list1).ToList<OrderWorkEE>();
                                }
                                item.details = getwhere1;
                            }
                        }
                        list = list.Concat(sql2).ToList<OrderWorkE>();
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
            OrderResult result = new OrderResult();
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

        #region 订单处理

        /// <summary>
        /// 处理选中的订单
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="ischeck"></param>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public OrderResult ProcessCustOrder(List<long> ids, bool ischeck, long shopid, long pageIndex, string date)
        {

            OrderResult com = new OrderResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();//开启事务
                    if (ids.Count > 0)
                    {
                        string res = string.Empty;
                        if (string.IsNullOrWhiteSpace(date))
                        {
                            db.RollbackTran(); //回滚事务
                            com.success = false;
                            com.Msg = "时间参数错误!";
                            return com;
                        }
                        DateTime torrow = date.ObjToDate().AddDays(1);
                        //选中被处理的订单
                        var list = db.Queryable<busi_custorder>().Where(s => s.del_flag && ids.Contains(s.order_id) && s.order_status > 1 && s.order_date >= date.ObjToDate() && s.order_date < torrow.ObjToDate()).ToList();
                        if (list.Count > 0)
                        {
                            foreach (var item in list)
                            {
                                res += item.order_code + ",";
                            }
                        }

                        //选中未被处理的订单
                        List<Int64> orderid = new List<Int64>();
                        var list1 = db.Queryable<busi_custorder>().Where(s => s.del_flag && ids.Contains(s.order_id) && s.order_status == 1 && s.order_date >= date.ObjToDate() && s.order_date < torrow.ObjToDate()).ToList();
                        if (list1.Count > 0)
                        {
                            foreach (var item1 in list1)
                            {
                                orderid.Add(item1.order_id);
                            }

                            var list2 = db.Queryable<busi_sendorder>().Where(s => s.del_flag && orderid.Contains(s.custorder_id)).ToList();
                            List<Int64> sendorderid = new List<Int64>();
                            if (list2.Count > 0)
                            {
                                foreach (var item2 in list2)
                                {
                                    sendorderid.Add(item2.order_id);
                                }
                                var sqll = db.Queryable<busi_sendorder_detail>().Where(s => sendorderid.Contains(s.order_id) && s.del_flag).ToList();
                                if (sqll.Count > 0)
                                {
                                    int suc = 0;
                                    List<Int64> noprod_id = new List<Int64>();
                                    List<Int64> sendorder_detail = new List<Int64>();
                                    foreach (var item in sqll)
                                    {
                                        var list5 = db.Queryable<base_product>().Where(s => s.del_flag).InSingle(item.prod_id);
                                        if (list5 != null)
                                        {
                                            suc += 1;
                                        }
                                        else
                                        {
                                            noprod_id.Add(item.order_id);
                                        }
                                    }
                                    if (suc == sqll.Count)
                                    {
                                        int sucs = 0;
                                        foreach (var itemm in sqll)
                                        {
                                            var info = db.Queryable<busi_sendorder_detail>()
                                          .JoinTable<base_product>((s1, s2) => s1.prod_id == s2.prod_id)
                                          .Where("s1.del_flag=1 and s2.del_flag=1")
                                          .Where(s1 => s1.detail_id == itemm.detail_id)
                                          .Select<PurchaseInfo>("s1.prod_num ,s2.price_cn ,s1.prod_id")
                                          .FirstOrDefault();
                                            if (info != null)
                                            {
                                                #region 库存采购
                                                if (ischeck)
                                                {
                                                    var list55 = db.Queryable<base_wh_stock>().Where(s => s.del_flag && s.code_id == itemm.code_id && s.wh_id == 1 && s.location_id != 1).FirstOrDefault(); // 判断有无库存
                                                    if (list55 != null)
                                                    {
                                                        DateTime time = DateTime.Now.AddDays(-3);
                                                        #region 库存足 库存采购
                                                        if (list55.stock_qty >= info.prod_num)
                                                        {
                                                            var work = false;
                                                            var stock = false;
                                                            //is_work=1，让使用库存可以配货，只是配货方式是使用库存，手持还可以配货到
                                                            work = db.Update<busi_workinfo>(new { detail_source = 1, work_type = 4 }, s => s.del_flag && s.sendorder_detail_id == itemm.detail_id);
                                                            stock = db.Update<base_wh_stock>(new { stock_qty = list55.stock_qty - info.prod_num }, s => s.stock_id == list55.stock_id); //减少库存
                                                            if (work && stock)
                                                            {
                                                                sucs += 1;
                                                            }
                                                        }
                                                        #endregion

                                                        #region 库存不足 正常采购
                                                        else
                                                        {
                                                            OrderResult coms = AddPurchase(itemm.detail_id, info.prod_id, info.prod_num, info.price_cn);
                                                            if (coms.success)
                                                            {
                                                                sucs += 1;
                                                            }
                                                            else
                                                            {
                                                                db.RollbackTran(); //回滚事务
                                                                com.success = false;
                                                                com.Msg = coms.Msg;
                                                                break;
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    #region 无库存 正常采购
                                                    else
                                                    {
                                                        OrderResult coms = AddPurchase(itemm.detail_id, info.prod_id, info.prod_num, info.price_cn);
                                                        if (coms.success)
                                                        {
                                                            sucs += 1;
                                                        }
                                                        else
                                                        {
                                                            db.RollbackTran(); //回滚事务
                                                            com.success = false;
                                                            com.Msg = coms.Msg;
                                                            break;
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                                #region 正常采购
                                                else
                                                {
                                                    OrderResult coms = AddPurchase(itemm.detail_id, info.prod_id, info.prod_num, info.price_cn);
                                                    if (coms.success)
                                                    {
                                                        sucs += 1;
                                                    }
                                                    else
                                                    {
                                                        db.RollbackTran(); //回滚事务
                                                        com.success = false;
                                                        com.Msg = coms.Msg;
                                                        break;
                                                    }
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                db.RollbackTran(); //回滚事务
                                                com.success = false;
                                                com.Msg = "找不到匹配的有效数据!";
                                                break;
                                            }
                                        }
                                        if (sqll.Count == sucs)
                                        {
                                            db.Update<busi_custorder>(new { order_status = 10 }, s1 => orderid.Contains(s1.order_id)); // 批量修改订单表 状态
                                            db.CommitTran(); //提交事务 
                                            com.success = true;
                                            com.URL = "/ImportOrder/GetShopOrderById?shop_id=" + shopid + "&&pageIndex=" + pageIndex + "&&date=" + date;
                                            if (list.Count > 0)
                                            {
                                                com.Msg = "选中订单号已成功处理!(注:系统订单号为" + res + " 是已处理过的订单!不会重复操作!)";
                                            }
                                            else
                                            {
                                                com.Msg = "选中订单号已成功处理!";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var list6 = db.Queryable<busi_sendorder>().Where(s => noprod_id.Contains(s.order_id)).ToList();
                                        List<Int64> cut_id = new List<Int64>();
                                        if (list6.Count > 0)
                                        {
                                            foreach (var item2 in list6)
                                            {
                                                cut_id.Add(item2.custorder_id);
                                            }
                                            string ress = string.Empty;
                                            var list7 = db.Queryable<busi_custorder>().Where(s => cut_id.Contains(s.order_id)).ToList();
                                            if (list7.Count > 0)
                                            {
                                                foreach (var item3 in list7)
                                                {
                                                    ress += item3.order_code + ",";
                                                }

                                                db.RollbackTran(); //回滚事务
                                                com.success = false;
                                                com.Msg = "系统订单号为" + ress + " 未绑定商品,操作失败!";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    db.RollbackTran(); //回滚事务
                                    com.success = false;
                                    com.Msg = "找不到匹配的有效数据!";
                                }
                            }
                            else
                            {
                                db.RollbackTran(); //回滚事务
                                com.success = false;
                                com.Msg = "参数错误!";
                            }
                        }
                        else
                        {
                            db.RollbackTran(); //回滚事务
                            com.success = false;
                            com.Msg = "系统订单号为" + res + " 是已处理过的订单!";
                        }
                    }
                    else
                    {
                        db.RollbackTran(); //回滚事务
                        com.success = false;
                        com.Msg = "您未选中任何数据!";
                    }


                }
                catch (Exception ex)
                {
                    com.success = false;
                    com.Msg = "异常报错!";
                    throw ex;
                }
                return com;
            }
        }


        /// <summary>
        /// 处理所有订单
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="ischeck"></param>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public OrderResult ProcessAllCustOrder(bool ischeck, long shopid, long pageIndex, string date)
        {
            OrderResult com = new OrderResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();//开启事务
                    string res = string.Empty;

                    if (string.IsNullOrWhiteSpace(date))
                    {
                        db.RollbackTran(); //回滚事务
                        com.success = false;
                        com.Msg = "时间参数错误!";
                        return com;
                    }

                    DateTime torrow = date.ObjToDate().AddDays(1);
                    var list = db.Queryable<busi_custorder>().Where(s => s.del_flag && s.shop_id == shopid && s.order_status > 1 && s.order_date >= date.ObjToDate() && s.order_date < torrow.ObjToDate()).ToList();
                    var liste = db.Queryable<busi_custorder>().Where(s => s.del_flag && s.shop_id == shopid && s.order_date >= date.ObjToDate() && s.order_date < torrow.ObjToDate()).ToList();
                    if (list.Count == liste.Count)
                    {
                        db.RollbackTran(); //回滚事务
                        com.success = false;
                        com.Msg = "订单已全部处理,操作失败!";
                        return com;
                    }
                    else
                    {
                        List<Int64> orderid = new List<Int64>();
                        var list1 = db.Queryable<busi_custorder>().Where(s => s.del_flag && s.shop_id == shopid && s.order_status == 1 && s.order_date >= date.ObjToDate() && s.order_date < torrow.ObjToDate()).ToList();
                        if (list1.Count > 0)
                        {
                            foreach (var item1 in list1)
                            {
                                orderid.Add(item1.order_id);
                            }
                            var list2 = db.Queryable<busi_sendorder>().Where(s => s.del_flag && orderid.Contains(s.custorder_id)).ToList();
                            List<Int64> sendorderid = new List<Int64>();
                            if (list2.Count > 0)
                            {
                                foreach (var item2 in list2)
                                {
                                    sendorderid.Add(item2.order_id);
                                }
                                var sqll = db.Queryable<busi_sendorder_detail>().Where(s => sendorderid.Contains(s.order_id) && s.del_flag).ToList();
                                if (sqll.Count > 0)
                                {
                                    int suc = 0;
                                    List<Int64> noprod_id = new List<Int64>();
                                    List<Int64> sendorder_detail = new List<Int64>();
                                    foreach (var item in sqll)
                                    {
                                        var list5 = db.Queryable<base_product>().Where(s => s.del_flag).InSingle(item.prod_id);
                                        if (list5 != null)
                                        {
                                            suc += 1;
                                        }
                                        else
                                        {
                                            noprod_id.Add(item.order_id);
                                        }
                                    }
                                    if (suc == sqll.Count)
                                    {
                                        int sucs = 0;
                                        foreach (var itemm in sqll)
                                        {
                                            var info = db.Queryable<busi_sendorder_detail>()
                                          .JoinTable<base_product>((s1, s2) => s1.prod_id == s2.prod_id)
                                          .Where("s1.del_flag=1 and s2.del_flag=1")
                                          .Where(s1 => s1.detail_id == itemm.detail_id)
                                          .Select<PurchaseInfo>("s1.prod_num ,s2.price_cn ,s1.prod_id")
                                          .FirstOrDefault();
                                            if (info != null)
                                            {
                                                #region 库存采购
                                                if (ischeck)
                                                {
                                                    var list55 = db.Queryable<base_wh_stock>().Where(s => s.del_flag && s.code_id == itemm.code_id && s.wh_id == 1 && s.location_id != 1).FirstOrDefault(); // 判断有无库存
                                                    if (list55 != null)
                                                    {
                                                        DateTime time = DateTime.Now.AddDays(-3);
                                                        #region 库存足 库存采购
                                                        if (list55.stock_qty >= info.prod_num)
                                                        {
                                                            var work = false;
                                                            var stock = false;
                                                            //is_work=1，让使用库存可以配货，只是配货方式是使用库存，手持还可以配货到
                                                            work = db.Update<busi_workinfo>(new { detail_source = 1,work_type=4 }, s => s.del_flag && s.sendorder_detail_id == itemm.detail_id); 
                                                            stock = db.Update<base_wh_stock>(new { stock_qty = list55.stock_qty - info.prod_num }, s => s.stock_id == list55.stock_id); //减少库存
                                                            if (work && stock)
                                                            {
                                                                sucs += 1;
                                                            }
                                                        }
                                                        #endregion

                                                        #region 库存不足 正常采购
                                                        else
                                                        {
                                                            OrderResult coms = AddPurchase(itemm.detail_id, info.prod_id, info.prod_num, info.price_cn);
                                                            if (coms.success)
                                                            {
                                                                sucs += 1;
                                                            }
                                                            else
                                                            {
                                                                db.RollbackTran(); //回滚事务
                                                                com.success = false;
                                                                com.Msg = coms.Msg;
                                                                break;
                                                            }
                                                        }
                                                        #endregion
                                                    }
                                                    #region 无库存 正常采购
                                                    else
                                                    {
                                                        OrderResult coms = AddPurchase(itemm.detail_id, info.prod_id, info.prod_num, info.price_cn);
                                                        if (coms.success)
                                                        {
                                                            sucs += 1;
                                                        }
                                                        else
                                                        {
                                                            db.RollbackTran(); //回滚事务
                                                            com.success = false;
                                                            com.Msg = coms.Msg;
                                                            break;
                                                        }
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                                #region 正常采购
                                                else
                                                {
                                                    OrderResult coms = AddPurchase(itemm.detail_id, info.prod_id, info.prod_num, info.price_cn);
                                                    if (coms.success)
                                                    {
                                                        sucs += 1;
                                                    }
                                                    else
                                                    {
                                                        db.RollbackTran(); //回滚事务
                                                        com.success = false;
                                                        com.Msg = coms.Msg;
                                                        break;
                                                    }
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                db.RollbackTran(); //回滚事务
                                                com.success = false;
                                                com.Msg = "找不到匹配的有效数据!";
                                                break;
                                            }
                                        }

                                        if (sqll.Count == sucs)
                                        {
                                            db.Update<busi_custorder>(new { order_status = 10 }, s1 => orderid.Contains(s1.order_id)); // 批量修改订单表 状态
                                            db.CommitTran(); //提交事务 
                                            com.success = true;
                                            com.URL = "/ImportOrder/GetShopOrderById?shop_id=" + shopid + "&&pageIndex=" + pageIndex + "&&date=" + date;
                                            if (list.Count > 0)
                                            {
                                                com.Msg = "选中订单号已成功处理!(注:系统订单号为" + res + " 是已处理过的订单!不会重复操作!)";
                                            }
                                            else
                                            {
                                                com.Msg = "选中订单号已成功处理!";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var list6 = db.Queryable<busi_sendorder>().Where(s => noprod_id.Contains(s.order_id)).ToList();
                                        List<Int64> cut_id = new List<Int64>();
                                        if (list6.Count > 0)
                                        {
                                            foreach (var item2 in list6)
                                            {
                                                cut_id.Add(item2.custorder_id);
                                            }
                                            string ress = string.Empty;
                                            var list7 = db.Queryable<busi_custorder>().Where(s => cut_id.Contains(s.order_id)).ToList();
                                            if (list7.Count > 0)
                                            {
                                                foreach (var item3 in list7)
                                                {
                                                    ress += item3.order_code + ",";
                                                }

                                                db.RollbackTran(); //回滚事务
                                                com.success = false;
                                                com.Msg = "系统订单号为" + ress + " 未绑定商品,操作失败!";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    db.RollbackTran(); //回滚事务
                                    com.success = false;
                                    com.Msg = "找不到匹配的有效数据!";
                                }
                            }
                            else
                            {
                                db.RollbackTran(); //回滚事务
                                com.success = false;
                                com.Msg = "找不到匹配的有效数据!";
                            }
                        }
                        else
                        {
                            db.RollbackTran(); //回滚事务
                            com.success = false;
                            com.Msg = "系统订单号为" + res + " 是已处理过的订单!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    com.success = false;
                    com.Msg = "异常报错!";
                    throw ex;
                }
                return com;
            }
        }

        /// <summary>
        /// 订单采购,生成采购单
        /// </summary>
        /// <param name="detail_id"></param>
        /// <returns></returns>
        public OrderResult AddPurchase(Int64? detail_id, Int64? prod_id, decimal? prod_num, decimal? price_cn)
        {
            OrderResult com = new OrderResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.BeginTran();//开启事务         
                DateTime time = DateTime.Now.AddDays(-3);
                var sql3 = db.Queryable<base_prod_supp_rel>().Where(s => s.prod_id == prod_id && s.del_flag).ToList();
                int suppid = 0;
                int id = 0;
                var issuc = false;
                if (sql3.Count > 0)
                {
                    suppid = sql3.Find(b => b.lev_purch == sql3.Min(s => s.lev_purch)).supp_id;
                    var ss = db.Queryable<busi_purchase>().Where(s => s.del_flag && s.create_time >= time && s.purch_status == 1 && s.supp_id == suppid && s.purch_type == 1).FirstOrDefault();
                    if (ss != null)
                    {
                        issuc = db.Update<busi_purchase>(new
                        {
                            edit_time = DateTime.Now,
                            edit_user_id = LoginUser.Current.user_id,
                            purch_numb = ss.purch_numb + prod_num,
                            sum_money = ss.sum_money + price_cn * prod_num,
                            purch_sum = price_cn * prod_num + ss.purch_sum,
                        }, s1 => s1.purch_id == ss.purch_id); // 批量修改订单表 状态                                                       
                    }
                    else
                    {
                        busi_purchase pur = new busi_purchase();
                        pur.abnormal_remark = "";
                        pur.create_time = DateTime.Now;
                        pur.create_user_id = LoginUser.Current.user_id;
                        pur.del_flag = true;
                        pur.del_time = DateTime.Now;
                        pur.supp_id = suppid;
                        pur.del_user_id = 0;
                        pur.edit_time = DateTime.Now;
                        pur.edit_user_id = 0;
                        pur.purch_categories = 0;
                        pur.purch_code = GetPurchaseCode();
                        pur.purch_numb = prod_num;
                        pur.purch_remark = "订单采购";
                        pur.purch_status = 1;
                        pur.purch_type = 1;
                        pur.remark = "订单采购";
                        pur.sum_freight = 0;
                        pur.sum_money = price_cn * prod_num;
                        pur.purch_sum = price_cn * prod_num;
                        pur.express_id = 0;
                        pur.express_code = null;
                        pur.express_name = null;
                        pur.OrderCode = null;
                        pur.isLocked = false;
                        pur.Locked_userid = 0;
                        var obj = db.Insert<busi_purchase>(pur);  // 插入采购表
                        id = obj.ObjToInt();
                    }
                    if (id > 0 || issuc)
                    {
                        var sql2 = db.Queryable<busi_sendorder_detail>().Where(s => s.detail_id == detail_id && s.prod_id == prod_id && s.del_flag).FirstOrDefault();
                        if (sql2 != null)
                        {
                            var sql1 = db.Queryable<busi_sendorder_detail>()
                                         .JoinTable<base_prod_supp_rel>((s1, s2) => s1.prod_id == s2.prod_id)
                                         .JoinTable<base_prod_supp_rel, base_supplier>((s1, s2, s3) => s2.supp_id == s3.supp_id)
                                         .JoinTable<busi_sendorder>((s1, s4) => s1.order_id == s4.order_id)
                                         .JoinTable<busi_sendorder, busi_custorder>((s1, s4, s5) => s4.custorder_id == s5.order_id)
                                         .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1 and s5.del_flag=1 and s2.supp_id=" + suppid + "")
                                         .Where(s1 => s1.detail_id == sql2.detail_id)
                                         .Select<base_prod_supp_rel, base_supplier, busi_sendorder, busi_custorder, busi_purchasedetail>((s1, s2, s3, s4, s5) => new busi_purchasedetail
                                         {
                                             send_detail_id = s1.detail_id,
                                             shop_id = s5.shop_id,
                                             prod_id = s1.prod_id,
                                             code_id = s1.code_id,
                                             supp_id = s3.supp_id,
                                             purch_url = s3.supp_url,
                                             purch_rice = s1.prod_price,
                                             purch_count = s1.prod_num,
                                         }).ToList();
                            try
                            {
                                if (sql1.Count > 0)
                                {
                                    Int64 purid = 0;
                                    if (id > 0)
                                    {
                                        purid = id;
                                    }
                                    if (issuc)
                                    {
                                        purid = ss.purch_id;
                                    }
                                    foreach (var item in sql1)
                                    {
                                        item.custorderdetail_id = 0;
                                        item.purch_type = 1;
                                        item.purch_status = 1;
                                        item.wh_id = 0;
                                        item.purch_id = purid;
                                        item.lack_count = 0;
                                        item.err_count = 0;
                                        item.is_cancel = true;
                                        item.cust_send_billcode = "";
                                        item.create_time = DateTime.Now;
                                        item.create_user_id = LoginUser.Current.user_id;
                                        item.edit_time = DateTime.Now;
                                        item.edit_user_id = 0;
                                        item.del_flag = true;
                                        item.del_time = DateTime.Now;
                                        item.del_user_id = 0;
                                        item.remark = "订单采购";
                                    }
                                    db.InsertRange<busi_purchasedetail>(sql1); // 批量插入采购详情表                                                                                  
                                    com.success = true;
                                    return com;
                                }
                                else
                                {
                                    db.RollbackTran(); //回滚事务
                                    com.success = false;
                                    com.Msg = "操作失败!";

                                }
                            }
                            catch (Exception ex)
                            {
                                db.RollbackTran(); //回滚事务
                                com.success = false;
                                com.Msg = "操作失败!";
                                throw ex;
                            }
                        }
                        else
                        {
                            db.RollbackTran(); //回滚事务
                            com.success = false;
                            com.Msg = "找不到匹配的有效数据!";
                        }
                    }
                    else
                    {
                        db.RollbackTran(); //回滚事务
                        com.success = false;
                        com.Msg = "未成功操作采购表数据!";
                    }
                }
                else
                {
                    var list7 = db.Queryable<base_product>().Where(s => s.del_flag).InSingle(prod_id);
                    db.RollbackTran(); //回滚事务
                    com.success = false;
                    com.Msg = "商品 <span style=\"color:red; font-weight:bold;\">" + list7.prod_title + "</span> 采购等级未设置,操作失败!";
                }
                return com;
            }
        }

        /// <summary>
        /// 获取新增采购编号
        /// </summary>
        /// <returns></returns>
        public string GetPurchaseCode()
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:MM");
            Random rd = new Random();
            int num = rd.Next(999999);
            string purchasecode = "C" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + num.ToString();
            return purchasecode;
        }

        #endregion

        /// <summary>
        /// 通过发货订单ID更新用户地址信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="receive_name"></param>
        /// <param name="receive_address"></param>
        /// <param name="receive_phone"></param>
        /// <param name="receive_zip"></param>
        /// <param name="receive_mobile"></param>
        /// <returns></returns>
        public bool UpdateSendOrderUserInfoById(int order_id, string receive_name, string receive_address, string receive_phone, string receive_zip, string receive_mobile)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var result = db.Update<busi_sendorder>
                    (new { receive_name = receive_name, receive_address = receive_address, receive_phone = receive_phone, receive_zip = receive_zip, receive_mobile = receive_mobile }, a => a.order_id == order_id);
                return result;
            }
        }

        /// <summary>
        /// 更新作业表信息
        /// </summary>
        /// <param name="detail_id"></param>
        /// <param name="old_wh_id"></param>
        /// <param name="detail_source"></param>
        /// <param name="new_wh_id"></param>
        /// <returns></returns>
        public ComResult UpdateWorkInfo(int detail_id, int old_wh_id, int detail_source, int new_wh_id, int num, int proId)
        {
            ComResult com = new ComResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();//开启事务
                    //通过状态判断人为干涉是否是使用库存
                    if (detail_source == 1)
                    {
                        //使用库存
                        //1判断仓库库存是否充足
                        var stockModel = db.Queryable<base_wh_stock>().Where(s1 => s1.prod_id == proId && s1.wh_id == new_wh_id).SingleOrDefault();
                        if (stockModel == null || stockModel.stock_qty < num)
                        {
                            com.State = 0;
                            com.Msg = "所选仓库库存不足";
                            return com;
                        }
                        //2删除作业表里面的原先库存的数据通过订单明细和仓库ID
                        com = NewWorkInfo(detail_id, old_wh_id, detail_source, new_wh_id, num, proId);

                    }
                    else
                    {
                        //采购
                        //删除作业表里面的原先库存的数据通过订单明细和仓库ID
                        com = NewWorkInfo(detail_id, old_wh_id, detail_source, new_wh_id, num, proId);
                        if (com.State == 0)
                        {
                            return com;
                        }
                        //添加采购表信息
                        busi_purchasedetail purdetail = new busi_purchasedetail()
                        {
                            send_detail_id = detail_id,
                            purch_status = 1,
                            purch_type = 1,
                            create_time = DateTime.Now,
                            create_user_id = LoginUser.Current.user_id,
                            purch_count = num,
                            prod_id = proId
                        };
                        db.Insert<busi_purchasedetail>(purdetail);

                    }

                    db.CommitTran();//提交事务
                }
                catch (Exception)
                {
                    db.RollbackTran();//回滚事务
                    throw;
                }
            }
            com.State = 1;
            com.Msg = "操作成功";
            return com;
        }

        private ComResult NewWorkInfo(int detail_id, int old_wh_id, int detail_source, int new_wh_id, int num, int proId)
        {
            ComResult com = new ComResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();//开启事务
                    //2删除作业表里面的原先库存的数据通过订单明细和仓库ID
                    db.Delete<busi_workinfo>("custorder_detail_id=" + detail_id + " and wh_id=" + old_wh_id + "");
                    //重新添加作业表
                    var orderModel = db.Queryable<busi_custorder>()
                                    .JoinTable<busi_custorder_detail>((s1, s2) => s1.order_id == s2.order_id)
                                    .Where<busi_custorder_detail>((s1, s2) => s2.detail_id == detail_id).SingleOrDefault();
                    if (orderModel == null)
                    {
                        com.State = 0;
                        com.Msg = "没有找到选择的订单明细";
                        return com;
                    }
                    //查询商品编码
                    var procodeModel = db.Queryable<base_prod_code>().Where(a => a.prod_id == proId).SingleOrDefault();

                    List<busi_workinfo> workList = new List<busi_workinfo>();
                    for (var i = 0; i < num; i++)
                    {
                        workList.Add(new busi_workinfo()
                        {
                            custorder_detail_id = detail_id,
                            plat_id = orderModel.platform_id,
                            shop_id = orderModel.shop_id,
                            wh_id = new_wh_id,
                            create_time = DateTime.Now,
                            create_user_id = LoginUser.Current.user_id,
                            detail_source = (byte)detail_source,
                            prod_code_id = procodeModel.code_id

                        });
                    }
                    db.InsertRange<busi_workinfo>(workList);//批量添加数据
                    com.State = 1;
                    db.CommitTran();//提交事务
                    return com;

                }
                catch (Exception)
                {
                    db.RollbackTran();//回滚事务
                    throw;
                }
            }
        }

        /// <summary>
        /// 获取所有供应商
        /// </summary>
        /// <returns></returns>
        public List<base_supplier> GetSupPlier()
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                return db.Queryable<base_supplier>().Where(a => a.del_flag).ToList();
            }
        }

        /// <summary>
        /// 获取所有仓库信息
        /// </summary>
        /// <returns></returns>
        public List<base_wh_warehouse> GetWareHouse()
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                return db.Queryable<base_wh_warehouse>().Where(a => a.del_flag).ToList();
            }
        }

        /// <summary>
        /// 通过入库订单ID更新订单状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ComResult UpdateCustOrder(List<long> ids, int state)
        {
            ComResult com = new ComResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var result = db.Update<busi_custorder>(new { order_status = state }, a => ids.Contains(a.order_id));
                if (result)
                {
                    com.State = 0;
                    com.Msg = "操作失败";
                }
                else
                {
                    com.State = 1;
                    com.Msg = "操作成功";
                }

                return com;
            }
        }



        /// <summary>
        /// 导出通过店铺ID和转运单号获取发货订单
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="tran_code"></param>
        /// <returns></returns>
        public List<SendOrder> GetSendOrderList(int shop_id, string tran_code)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var sql = db.Queryable<busi_sendorder>()
                        .JoinTable<busi_custorder>((s1, s2) => s1.custorder_id == s2.order_id)
                        .JoinTable<busi_transfer>((s1, s3) => s1.tran_id == s3.tran_id)
                        .JoinTable<base_exp_comp>((s1, s4) => s1.express_id == s4.express_id)
                        .Where<busi_custorder>((s1, s2) => s2.shop_id == shop_id);
                if (tran_code != "")
                {
                    sql = sql.Where<busi_transfer>((s1, s3) => s3.tran_code.Contains(tran_code));
                }
                return sql.Select<busi_custorder, busi_transfer, base_exp_comp, SendOrder>
                ((s1, s2, s3, s4) => new SendOrder
                {
                    custorder_code = s2.custorder_code,
                    exp_code = s1.exp_code,
                    express_id = s1.express_id,
                    express_name = s4.express_name,
                    order_code = s1.order_code
                }).ToList();
            }
        }

        /// <summary>
        /// 通过店铺ID和转运单号获取发货订单
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="tran_code"></param>
        /// <returns></returns>
        public List<SendOrder> GetSendOrderList(int shop_id, string tran_code, int pageIndex, int pageSize, out int count)
        {

            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var sql = db.Queryable<busi_sendorder>()
                         .JoinTable<busi_custorder>((s1, s2) => s1.custorder_id == s2.order_id)
                         .JoinTable<busi_transfer>((s1, s3) => s1.tran_id == s3.tran_id)
                         .JoinTable<base_exp_comp>((s1, s4) => s1.express_id == s4.express_id)
                         .Where<busi_custorder>((s1, s2) => s2.shop_id == shop_id);
                if (tran_code != "")
                {
                    sql = sql.Where<busi_transfer>((s1, s3) => s3.tran_code.Contains(tran_code));
                }
                count = sql.Count();
                return sql.OrderBy(s1 => s1.order_id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).Select<busi_custorder, busi_transfer, base_exp_comp, SendOrder>
                    ((s1, s2, s3, s4) => new SendOrder
                    {
                        custorder_code = s2.custorder_code,
                        exp_code = s1.exp_code,
                        express_id = s1.express_id,
                        express_name = s4.express_name,
                        order_code = s1.order_code
                    }).ToList();
            }

        }



        public List<base_PSKU> GetSKUList(string shopname, string PSKU, string SSKU)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var comresult = db.Queryable<base_PSKU>();
                    if (!string.IsNullOrEmpty(shopname))
                    {
                        base_shop shop = db.Queryable<base_shop>().Where(s1 => s1.shop_name == shopname).FirstOrDefault();
                        comresult = comresult.Where(s1 => s1.ShopID == shop.shop_id);
                    }
                    if (!string.IsNullOrEmpty(PSKU))
                    {
                        comresult = comresult.Where(s1 => s1.PSKU == PSKU.Trim());
                    }
                    if (!string.IsNullOrEmpty(SSKU))
                    {
                        comresult = comresult.Where(s1 => s1.SSKU == SSKU.Trim());
                    }
                    List<base_PSKU> SKUlist = comresult.ToList();
                    return SKUlist;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }

        /// <summary>
        /// 插入到临时表中
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool InsertLStable(int shopID, DateTime naqi, DataTable table)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.CommandTimeOut = 10000;//设置超时时间 10秒
                try
                {
                    db.BeginTran();//开启事务
                    //db.BeginTran(IsolationLevel.ReadCommitted);+3重载可以设置事世隔离级别
                    string LStablename = CommService.GetLStableByshopID(shopID).LStablename; //得到临时表名称LS_OrderQ10   
                    bool isok = false;
                    switch (LStablename)
                    {
                        case "Q10":
                            isok = db.SqlBulkCopy(TranlaterQ10(shopID, naqi, table));   //批量插入 适合海量数据插入
                            break;
                        case "乐天":
                            isok = db.SqlBulkCopy(Tranlaterletian(shopID, naqi, table));
                            break;
                        case "WAWMA":
                            isok = db.SqlBulkCopy(TranlaterWAWMA(shopID, naqi, table));
                            break;
                        case "雅虎":
                            isok = db.SqlBulkCopy(Tranlateryahu(shopID, naqi, table));
                            break;
                        case "速卖通":
                            isok = db.SqlBulkCopy(Tranlatersumaitong(shopID, naqi, table));
                            break;
                        case "亚马逊":
                            isok = db.SqlBulkCopy(Tranlateryamaxun(shopID, naqi, table));
                            break;
                        case "Ebay":
                            isok = db.SqlBulkCopy(TranlaterEbay(shopID, naqi, table));
                            break;
                        case "Wish":
                            isok = db.SqlBulkCopy(TranlaterWish(shopID, naqi, table));
                            break;
                        default:
                            break;
                    }
                    db.CommitTran();//提交事务
                    return true;
                }
                catch (Exception ex)
                {
                    db.RollbackTran();//回滚事务
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <returns></returns>
        public List<base_LSorder> SearchLStable(int shopID, out int totil, out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.CommandTimeOut = 10000;//设置超时时间 10秒
                try
                {
                    db.BeginTran();//开启事务
                    //db.BeginTran(IsolationLevel.ReadCommitted);+3重载可以设置事世隔离级别
                    base_shop shop = CommService.GetLStableByshopID(shopID);
                    if (shop == null)
                    {
                        totil = 0;
                        //totilpage = 0;
                        exmsg = "不存在此店铺";
                        return null;
                    }
                    string LStablename = shop.LStablename; //得到临时表名称LS_OrderQ10   
                    List<base_LSorder> list = new List<base_LSorder>();
                    var comres = db.Queryable<LS_Order>().Where(s => s.firstimport == 1).Where(s => s.shop_id == shopID);
                    totil = comres.Count();
                    exmsg = "";
                    //totilpage = totil / onepagecount;
                    //if (totil % onepagecount > 0)
                    //{
                    //    totilpage++;
                    //}
                    //list = comres.OrderBy(s1 => s1.OrderID).Skip(onepagecount * (pagenum - 1))
                    // .Take(onepagecount).Select<base_LSorder>((s1) => new base_LSorder()
                    list = comres.OrderBy(s1 => s1.OrderID).Select<base_LSorder>((s1) => new base_LSorder()
                    {
                        shopID = s1.shop_id,
                        OrderID = s1.OrderID,
                        address = s1.address,
                        telephone = s1.telephone,
                        phone = s1.phone,
                        zip = s1.zip,
                        Buyer = s1.Buyer,
                        SKU = s1.SKU1,
                        Num = s1.Num,
                        OrderNub = s1.OrderNub,
                        SysOrderNub = s1.SysOrderNub
                    }).ToList();
                    db.CommitTran();//提交事务
                    return list;
                }
                catch (Exception ex)
                {
                    db.RollbackTran();//回滚事务
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 生成新的订单号
        /// </summary>
        /// <param name="oldordercode"></param>
        /// <returns></returns>
        public string GetNewString(string oldordercode, string shopID)
        {
            string patforshopmdaihao = shopID;
            string newordercode = null;               //新的订单号
            string fyear = DateTime.Now.Year.ToString();
            string year = fyear.Remove(0, fyear.Length - 2);
            string fmonth = "0" + DateTime.Now.Month.ToString();
            string month = fmonth.Remove(0, fmonth.Length - 2);
            string fDate = "0" + (DateTime.Now.Day.ToString());
            string Date = fDate.Remove(0, fDate.Length - 2);
            string lastsevencode = oldordercode.Remove(0, oldordercode.Length - 7);              //取后七位
            newordercode = year + month + Date + patforshopmdaihao + lastsevencode;            //11 代表亚马逊平台
            return newordercode;
        }

        /// <summary>
        /// Q10
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<LS_Order> TranlaterQ10(int shopid, DateTime naqi, DataTable table)
        {
            List<LS_Order> list = new List<LS_Order>();
            foreach (DataRow item in table.Rows)
            {
                LS_Order it = new LS_Order();
                it.shop_id = shopid;
                it.Buyer = item["受取人名"].ToString();
                it.telephone = item["受取人携帯電話番号"].ToString();
                it.phone = item["受取人電話番号"].ToString();
                it.zip = item["郵便番号"].ToString();
                it.address = item["住所"].ToString();
                it.firstimport = 1;
                it.ImportTime = DateTime.Now;
                it.Fee = item["供給原価の合計"].ObjToDecimal();
                it.totilMoney = item["供給原価の合計"].ObjToDecimal();
                it.SKU1 = item["オプションコード"].ToString();
                it.SKU2 = string.Empty;
                it.Num = item["数量"].ObjToInt();
                it.OrderNub = item["カート番号"].ToString();
                it.SysOrderNub = GetNewString(item["カート番号"].ToString(), shopid.ToString());
                it.lasttime = naqi;  //纳期
                it.beizhu = item["配送要請事項"].ToString();
                it.CustmerOrderTime = item["入金日"].ObjToDate();
                list.Add(it);
            }
            return list;
        }

        /// <summary>
        /// 乐天
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<LS_Order> Tranlaterletian(int shopid, DateTime naqi, DataTable table)
        {
            List<LS_Order> list = new List<LS_Order>();
            System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
            dtfi.ShortTimePattern = "t";
            foreach (DataRow item in table.Rows)
            {
              //==================老的乐天杂货店铺==========================
                //LS_Order it = new LS_Order();
                //it.shop_id = shopid;
                //it.Buyer = item["受取人名"].ToString();
                //it.telephone = item["受取人携帯電話番号"].ToString();
                //it.phone = item["受取人電話番号"].ToString();
                //it.zip = item["郵便番号"].ToString();
                //it.address = item["住所"].ToString();
                //it.firstimport = 1;
                //it.ImportTime = DateTime.Now;
                //it.Fee = item["供給原価の合計"].ObjToDecimal();
                //it.totilMoney = item["供給原価の合計"].ObjToDecimal();
                //it.SKU1 = item["オプションコード"].ToString();
                //it.SKU2 = string.Empty;
                //it.Num = item["数量"].ObjToInt();
                //it.OrderNub = item["カート番号"].ToString();
                //it.SysOrderNub = item["オプションコード"].ToString();
                //it.lasttime = naqi;  //纳期
                //it.beizhu = item["配送要請事項"].ToString();
                //it.CustmerOrderTime = item["入金日"].ObjToDate();
                //========================================================
                decimal price=item["単価"].ObjToDecimal();
                decimal num=item["個数"].ObjToDecimal();
                decimal totil=price*num;
                LS_Order it = new LS_Order();
                it.shop_id = shopid;
                it.Buyer = item["送付先氏名"].ToString();
                it.telephone = item["送付先電話番号"].ToString();
                it.phone = item["送付先電話番号"].ToString();
                it.zip = item["送付先郵便番号"].ToString();
                it.address = item["送付先住所1"].ToString() + item["送付先住所2"].ToString();
                it.firstimport = 1;
                it.ImportTime = DateTime.Now;
                it.Fee = item["単価"].ObjToDecimal();
                it.totilMoney = totil;
                it.SKU1 = item["項目選択肢"].ToString();
                it.SKU2 = string.Empty;
                it.Num = item["個数"].ObjToInt();
                it.OrderNub = item["注文番号"].ToString();
                it.SysOrderNub = item["注文番号"].ToString();
                it.lasttime = naqi;  //纳期
                it.beizhu = item["寄託者管理番号"].ToString();
                it.CustmerOrderTime = DateTime.Now; //item["注文日時"].ToString().ObjToDate();
                list.Add(it);
            }
            return list;
        }

        /// <summary>
        /// WAWMA
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<LS_Order> TranlaterWAWMA(int shopid, DateTime naqi, DataTable table)
        {
            List<LS_Order> list = new List<LS_Order>();
            foreach (DataRow item in table.Rows)
            {
                decimal price = item["販売単価"].ObjToDecimal();
                decimal num = item["販売個数"].ObjToDecimal();
                decimal totil = price * num;
                LS_Order it = new LS_Order();
                it.shop_id = shopid;
                it.Buyer = item["送付先氏名"].ToString();
                it.telephone = item["送付先電話番号"].ToString();
                it.phone = item["送付先電話番号"].ToString();
                it.zip = item["購入者住所"].ToString().Substring(0,8);
                it.address = item["購入者住所"].ToString().Substring(8);
                it.firstimport = 1;
                it.ImportTime = DateTime.Now;
                it.Fee = item["小計"].ObjToDecimal();
                it.totilMoney = totil; // item["総合計"].ObjToDecimal();
                it.SKU1 = item["商品タイトル"].ToString() + item["アイテムオプション"].ToString();
                it.SKU2 = string.Empty;
                it.Num = item["販売個数"].ObjToInt();
                it.OrderNub = item["取引ナンバー"].ToString();
                it.SysOrderNub = GetNewString(item["取引ナンバー"].ToString(), shopid.ToString());
                it.lasttime = naqi;  //纳期
                it.beizhu = item["管理ID"].ToString();
                it.CustmerOrderTime = DateTime.Now;
                list.Add(it);
            }
            return list;
        }

        /// <summary>
        /// 雅虎
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<LS_Order> Tranlateryahu(int shopid, DateTime naqi, DataTable table)
        {
            List<LS_Order> list = new List<LS_Order>();
            foreach (DataRow item in table.Rows)
            {
                decimal price = item["totilMoney"].ObjToDecimal();
                decimal num = item["Num"].ObjToDecimal();
                decimal totil = price * num;
                LS_Order it = new LS_Order();
                it.shop_id = shopid;
                it.Buyer = item["Buyer"].ToString();
                it.telephone = item["telephone"].ToString();
                it.phone = item["telephone"].ToString();
                it.zip = item["zip"].ToString();
                it.address = item["address"].ToString();
                it.firstimport = 1;
                it.ImportTime = DateTime.Now;
                it.Fee = item["Fee"].ObjToDecimal();
                it.totilMoney = totil; //item["totilMoney"].ObjToDecimal();
                it.SKU1 = item["SKU1"].ToString();
                it.SKU2 = string.Empty;
                it.Num = item["Num"].ObjToInt();
                it.OrderNub = item["OrderNub"].ToString();
                it.SysOrderNub = GetNewString(item["OrderNub"].ToString(), shopid.ToString());
                it.lasttime = naqi;  //纳期
                it.beizhu = item["beizhu"].ToString();
                it.CustmerOrderTime = item["CustmerOrderTime"].ObjToDate();
                list.Add(it);
            }
            return list;
        }

        /// <summary>
        /// 速卖通
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<LS_Ordersumaitong> Tranlatersumaitong(int shopid, DateTime naqi, DataTable table)
        {
            List<LS_Ordersumaitong> list = new List<LS_Ordersumaitong>();
            foreach (DataRow item in table.Rows)
            {
                LS_Ordersumaitong it = new LS_Ordersumaitong();
                it.shop_id = shopid;
                it.Buyer = item["受取人名"].ToString();
                it.telephone = item["受取人携帯電話番号"].ToString();
                it.phone = item["受取人電話番号"].ToString();
                it.zip = item["郵便番号"].ToString();
                it.address = item["住所"].ToString();
                it.firstimport = 1;
                it.ImportTime = DateTime.Now;
                it.Fee = item["供給原価の合計"].ObjToDecimal();
                it.totilMoney = item["供給原価の合計"].ObjToDecimal();
                it.SKU1 = item["オプションコード"].ToString();
                it.SKU2 = string.Empty;
                it.Num = item["数量"].ObjToInt();
                it.OrderNub = item["カート番号"].ToString();
                it.SysOrderNub = item["オプションコード"].ToString();
                it.lasttime = naqi;  //纳期
                it.beizhu = item["配送要請事項"].ToString();
                // it.CustmerOrderTime = item["入金日"].ObjToDate();
                list.Add(it);
            }
            return list;
        }

        /// <summary>
        /// 亚马逊
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<LS_Orderyamaxun> Tranlateryamaxun(int shopid, DateTime naqi, DataTable table)
        {
            List<LS_Orderyamaxun> list = new List<LS_Orderyamaxun>();
            foreach (DataRow item in table.Rows)
            {
                LS_Orderyamaxun it = new LS_Orderyamaxun();
                it.shop_id = shopid;
                it.Buyer = item["受取人名"].ToString();
                it.telephone = item["受取人携帯電話番号"].ToString();
                it.phone = item["受取人電話番号"].ToString();
                it.zip = item["郵便番号"].ToString();
                it.address = item["住所"].ToString();
                it.firstimport = 1;
                it.ImportTime = DateTime.Now;
                it.Fee = item["供給原価の合計"].ObjToDecimal();
                it.totilMoney = item["供給原価の合計"].ObjToDecimal();
                it.SKU1 = item["オプションコード"].ToString();
                it.SKU2 = string.Empty;
                it.Num = item["数量"].ObjToInt();
                it.OrderNub = item["カート番号"].ToString();
                it.SysOrderNub = item["オプションコード"].ToString();
                it.lasttime = naqi;  //纳期
                it.beizhu = item["配送要請事項"].ToString();
                //  it.CustmerOrderTime = item["入金日"].ObjToDate();
                list.Add(it);
            }
            return list;
        }

        /// <summary>
        /// EBay
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<LS_OrderEbay> TranlaterWish(int shopid, DateTime naqi, DataTable table)
        {
            List<LS_OrderEbay> list = new List<LS_OrderEbay>();
            foreach (DataRow item in table.Rows)
            {
                LS_OrderEbay it = new LS_OrderEbay();
                it.shop_id = shopid;
                it.Buyer = item["受取人名"].ToString();
                it.telephone = item["受取人携帯電話番号"].ToString();
                it.phone = item["受取人電話番号"].ToString();
                it.zip = item["郵便番号"].ToString();
                it.address = item["住所"].ToString();
                it.firstimport = 1;
                it.ImportTime = DateTime.Now;
                it.Fee = item["供給原価の合計"].ObjToDecimal();
                it.totilMoney = item["供給原価の合計"].ObjToDecimal();
                it.SKU1 = item["オプションコード"].ToString();
                it.SKU2 = string.Empty;
                it.Num = item["数量"].ObjToInt();
                it.OrderNub = item["カート番号"].ToString();
                it.SysOrderNub = item["オプションコード"].ToString();
                it.lasttime = naqi;  //纳期
                it.beizhu = item["配送要請事項"].ToString();
                // it.CustmerOrderTime = item["入金日"].ObjToDate();
                list.Add(it);
            }
            return list;
        }
        /// <summary>
        /// EBay
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<LS_OrderEbay> TranlaterEbay(int shopid, DateTime naqi, DataTable table)
        {
            List<LS_OrderEbay> list = new List<LS_OrderEbay>();
            foreach (DataRow item in table.Rows)
            {
                LS_OrderEbay it = new LS_OrderEbay();
                it.shop_id = shopid;
                it.Buyer = item["受取人名"].ToString();
                it.telephone = item["受取人携帯電話番号"].ToString();
                it.phone = item["受取人電話番号"].ToString();
                it.zip = item["郵便番号"].ToString();
                it.address = item["住所"].ToString();
                it.firstimport = 1;
                it.ImportTime = DateTime.Now;
                it.Fee = item["供給原価の合計"].ObjToDecimal();
                it.totilMoney = item["供給原価の合計"].ObjToDecimal();
                it.SKU1 = item["オプションコード"].ToString();
                it.SKU2 = string.Empty;
                it.Num = item["数量"].ObjToInt();
                it.OrderNub = item["カート番号"].ToString();
                it.SysOrderNub = item["オプションコード"].ToString();
                it.lasttime = naqi;  //纳期
                it.beizhu = item["配送要請事項"].ToString();
                // it.CustmerOrderTime = item["入金日"].ObjToDate();
                list.Add(it);
            }
            return list;
        }

        /// <summary>
        /// 删除某条信息
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteLStable(int shopID, int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {

                try
                {
                    db.BeginTran();//开启事务
                    //db.BeginTran(IsolationLevel.ReadCommitted);+3重载可以设置事世隔离级别
                    base_shop shop = CommService.GetLStableByshopID(shopID);
                    if (shop == null)
                    {
                        throw new Exception("店铺不存在!");
                    }
                    bool isok;
                    isok = db.Delete<LS_Order, int>(id);

                    return isok;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        List<LSModel> TranlatermodelQ10(List<LS_Order> inlist)
        {
            List<LSModel> outlist = new List<LSModel>();
            LSModel lsmodel = new LSModel();
            foreach (var item in inlist)
            {
                lsmodel.address = item.address;
                lsmodel.telephone = item.telephone;
                lsmodel.phone = item.phone;
                lsmodel.zip = item.zip;
                lsmodel.Buyer = item.Buyer;
                lsmodel.shop_id = item.shop_id;
                lsmodel.SKU1 = item.SKU1;
                lsmodel.SKU2 = item.SKU2;
                lsmodel.SysOrderNub = item.SysOrderNub;
                lsmodel.totilMoney = item.totilMoney;
                lsmodel.OrderNub = item.OrderNub;
                lsmodel.Num = item.Num;
                lsmodel.ImportTime = item.ImportTime;
                lsmodel.lasttime = item.lasttime;
                lsmodel.firstimport = item.firstimport;
                lsmodel.CustmerOrderTime = item.CustmerOrderTime;
                outlist.Add(lsmodel);
            }
            return outlist;
        }

        public static DataTable TrangeDataTable(List<LS_Order> list)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("PlatformID", typeof(string));
            table.Columns.Add("ShopID", typeof(string));
            table.Columns.Add("SKU", typeof(string));
            table.Columns.Add("Num", typeof(string));
            table.Columns.Add("SysOrderNub", typeof(string));
            table.Columns.Add("lasttime", typeof(string));
            table.Columns.Add("OrderNub", typeof(string));
            table.Columns.Add("totilMoney", typeof(string));
            table.Columns.Add("Buyer", typeof(string));
            table.Columns.Add("telephone", typeof(string));
            table.Columns.Add("phone", typeof(string));
            table.Columns.Add("zip", typeof(string));
            table.Columns.Add("address", typeof(string));
            table.Columns.Add("firstimport", typeof(string));
            table.Clear();  //清除所有的数据

            return null;
        }

        /// <summary>    
        /// 将泛型集合类转换成DataTable    
        /// </summary>    
        /// <typeparam name="T">集合项类型</typeparam>    
        /// <param name="list">集合</param>    
        /// <param name="propertyName">需要返回的列的列名</param>    
        /// <returns>数据集(表)</returns>    
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// 过滤不插入的SKU
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<LS_Order> HasGSKU(int shopid, List<LS_Order> relist, out int count)
        {
            try
            {
                int a = 0;
                List<GSKU> GSKUlist = CommService.GetGSKUListByshopID(shopid);
                GSKU[] str = GSKUlist.ToArray();
                List<string> skulist = new List<string>();   //取出所有的需要过滤的SKU GetCSVcolByshopID
                foreach (var item in str)
                {
                    skulist.Add(item.gsku);
                }
                //string csvcol = CommService.GetCSVcolByshopID(shopid);//取出每个店铺SKU存储列
                //csvcol.Replace("\"","");
                //foreach(var item in relist) //foreach不能更改集合的数据，换成for语句
                //{
                //    if (skulist.Contains(item.SKU1))
                //    {
                //        relist.Remove(item);
                //        a++;
                //    }
                //}
                for (int i = 0; i < relist.Count; i++)
                {
                    if (skulist.Contains(relist[i].SKU1))
                    {
                        relist.Remove(relist[i]);
                        a++;
                    }
                }
                //List<LS_Order> result = relist;
                count = a;//有几条订单是被过滤掉的
                return relist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 插入到正式表中
        /// </summary>
        /// <param name="platformID"></param>
        /// <param name="shopID"></param>
        ///   <param name="list">前台获取的数据集合</param>
        /// <returns></returns>
        public int InsertOrder(int platformID, int shopID, List<base_LSorder> list, out int lvcount)
        {
            int count = 0;
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.CommandTimeOut = 10000;//设置超时时间 10秒
                try
                {

                    //db.BeginTran(IsolationLevel.ReadCommitted);+3重载可以设置事世隔离级别
                    base_shop shop = CommService.GetLStableByshopID(shopID);
                    if (shop == null)
                    {
                        throw new Exception("店铺不存在!");
                    }
                    //先查找到数据
                    //List<LS_Order> list111 = db.Queryable<LS_Order>().Where(s => s.shop_id == shopID).Where(s => s.firstimport == 1).ToList();
                    List<LS_Order> relist = new List<LS_Order>();
                    foreach (var item in list)
                    {
                        LS_Order order = new LS_Order();
                        order.address = item.address;
                        order.Buyer = item.Buyer;
                        order.Num = item.Num.ObjToInt();
                        order.OrderID = item.OrderID;
                        order.OrderNub = item.OrderNub;
                        order.phone = item.phone;
                        order.shop_id = item.shopID;
                        order.SKU1 = item.SKU;
                        order.SysOrderNub = item.SysOrderNub;
                        order.telephone = item.telephone;
                        order.zip = item.zip;
                        relist.Add(order);
                    }
                    //设置不更新列
                    db.DisableUpdateColumns = new string[] { "firstimport", "ImportTime", "totilMoney", "Fee", "lasttime", "beizhu", "CustmerOrderTime" };//设置CreateTime不更新
                    //先更新
                    bool isok = db.SqlBulkReplace<LS_Order>(relist);
                    if (!isok)
                    {
                        throw new Exception("更新数据到临时库失败");
                    }
                    //再查询出来
                    var nlist = db.Queryable<LS_Order>().Where(s => s.shop_id == shopID).Where(s => s.firstimport == 1).ToList();
                    //List<base_platform> mm = db.Queryable<base_platform>().ToList();
                    if (nlist.Count == 0 || nlist == null)
                    {
                        throw new Exception("没有需要解析入库的数据");
                    }
                    db.BeginTran();//开启事务
                    //需要判断一下SKU是否存在系统中,每一个SKU都判断
                    int glcount = 0;
                    List<LS_Order> Lastlist = HasGSKU(shopID, nlist, out  glcount);
                    lvcount = glcount;
                    count = InsertToOrderdetail(Lastlist);
                    db.CommitTran();//提交事务
                    return count;
                }
                catch (Exception ex)
                {
                    db.RollbackTran();//回滚事务
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 插入到五张 正式表中
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int InsertToOrderdetail(List<LS_Order> list)
        {
            string ordercode = string.Empty;
            busi_custorder num = null;
            base_prod_code sku = null;
            busi_sendorder Lsend = null;
            base_shop shop = null;
            int mysendID = 0;
            int orderID = 0;

            //需要插入的五张表
            busi_custorder cusorder = new busi_custorder();
            busi_custorder_detail detail = new busi_custorder_detail();
            busi_sendorder cussendorde = new busi_sendorder();
            busi_sendorder_detail sdetail = new busi_sendorder_detail();
            busi_workinfo workinfo = new busi_workinfo();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.CommandTimeOut = 10000;//设置超时时间 10秒
                List<string> order = new List<string>(); //用来储存订单号
                try
                {
                    db.BeginTran();//开启事务
                    long newpack = GetPackCode(); //得到包裹号
                    foreach (var item in list)
                    {
                        ordercode = item.SysOrderNub;//系统订单号
                        #region 插入数据
                        //如果没有插入订单表和包裹表，第一次插入数据
                        if (!order.Contains(ordercode)) 
                        {
                            sku = db.Queryable<base_prod_code>().Where(s1 => s1.sku_code == item.SKU1 + item.SKU2).FirstOrDefault();
                            if (sku == null)
                            {
                                throw new Exception("在系统中不存在此SKU:" + item.SKU1 + item.SKU2);
                            }
                            shop = db.Queryable<base_shop>().Where(s => s.shop_id == item.shop_id).FirstOrDefault();
                            cusorder.create_time = DateTime.Now;
                            cusorder.create_user_id = LoginUser.Current.user_id;
                            cusorder.custorder_code = item.OrderNub;
                            cusorder.del_flag = true;
                            cusorder.del_time = DateTime.Now;
                            cusorder.del_user_id = 0;
                            cusorder.edit_time = DateTime.Now;
                            cusorder.edit_user_id = 0;
                            cusorder.latest_date = item.lasttime;
                            cusorder.order_code = item.SysOrderNub;
                            cusorder.order_date = item.ImportTime;
                            cusorder.order_status = 1;
                            cusorder.order_summoney = (decimal)item.totilMoney;
                            cusorder.order_sumqty = item.Num;
                            cusorder.platform_id = (int)shop.platform_id;
                            cusorder.shop_id = item.shop_id;
                            cusorder.storage_type = 0;
                            cusorder.validity_day = DateTime.Now.AddDays(5); //预计发货日是订单导入日的5天
                            //--------------------------20170719-------------------------------------------------
                            cusorder.cus_name = item.Buyer;
                            cusorder.cus_address = item.address;
                            cusorder.cus_mobile = item.telephone;
                            cusorder.cus_phone = item.phone;
                            cusorder.cus_zip = item.zip;

                            orderID = db.Insert<busi_custorder>(cusorder).ObjToInt();
                            order.Add(item.SysOrderNub);//添加到集合中


                            cussendorde.create_time = DateTime.Now;
                            cussendorde.create_user_id = LoginUser.Current.user_id;
                            cussendorde.custorder_id = orderID;
                            cussendorde.del_flag = true;
                            cussendorde.del_time = DateTime.Now;
                            cussendorde.del_user_id = 0;
                            cussendorde.edit_time = DateTime.Now;
                            cussendorde.edit_user_id = 0;
                            cussendorde.exp_code = string.Empty;
                            cussendorde.express_id = 0;
                            cussendorde.is_csv_export = true;
                            cussendorde.is_export = false;
                            cussendorde.is_pack = false;
                            cussendorde.is_print = false;
                            cussendorde.order_code = newpack.ToString();//包裹号
                            cussendorde.order_tatus = 1;
                            cussendorde.pack_datetime = DateTime.Now;
                            cussendorde.prod_money = (decimal)item.totilMoney;
                            cussendorde.prod_num = (int)item.Num;
                            cussendorde.receive_address = item.address;
                            cussendorde.receive_mobile = item.telephone;
                            cussendorde.receive_name = item.Buyer;
                            cussendorde.receive_phone = item.phone;
                            cussendorde.receive_zip = item.zip;
                            cussendorde.remark = "";
                            cussendorde.send_datetime = DateTime.Now;
                            cussendorde.tran_id = 0;
                            mysendID = db.Insert<busi_sendorder>(cussendorde).ObjToInt();
                            newpack++;

                            detail.create_time = DateTime.Now;
                            detail.create_user_id = LoginUser.Current.user_id;
                            detail.del_flag = true;
                            detail.del_time = null;
                            detail.del_user_id = 0;
                            detail.code_id = sku.code_id;
                            //detail.detail_source = 2;
                            detail.detail_status = 0;
                            detail.edit_time = DateTime.Now;
                            detail.edit_user_id = 0;
                            detail.order_id = orderID.ObjToInt();

                            detail.prod_id = sku.prod_id;
                            detail.prod_name = "";
                            detail.prod_num = (int)item.Num;
                            detail.prod_price = item.totilMoney;
                            detail.remark = "";
                            detail.prod_weight = 0;
                            int idetail = db.Insert<busi_custorder_detail>(detail).ObjToInt();

                            sdetail.code_id = sku.code_id;
                            sdetail.create_time = DateTime.Now;
                            sdetail.create_user_id = LoginUser.Current.user_id;
                            sdetail.del_flag = true;
                            sdetail.del_time = DateTime.Now;
                            sdetail.del_user_id = 0;
                            sdetail.detail_status = 0;
                            sdetail.edit_time = DateTime.Now;
                            sdetail.edit_user_id = 0;
                            sdetail.order_id = mysendID;
                            sdetail.prod_id = sku.prod_id;
                            sdetail.prod_name = "";
                            sdetail.prod_num = (int)item.Num;
                            sdetail.remark = "";
                            sdetail.prod_price = item.totilMoney;
                            var isdetail = db.Insert<busi_sendorder_detail>(sdetail).ObjToInt();

                            for (int i = 0; i < (int)item.Num; i++)
                            {
                                workinfo.area_id = 0;
                                workinfo.packid = mysendID;
                                workinfo.create_time = DateTime.Now;
                                workinfo.custorder_id = orderID;
                                workinfo.create_user_id = LoginUser.Current.user_id;
                                workinfo.custorder_detail_id = Convert.ToInt64(isdetail);
                                workinfo.del_flag = true;
                                workinfo.del_time = null;
                                workinfo.del_user_id = 0;
                                workinfo.detail_source = 2;
                                workinfo.edit_time = DateTime.Now;
                                workinfo.edit_user_id = 0;
                                workinfo.is_work = false;
                                workinfo.locat_id = 0;
                                workinfo.plat_id = 0;
                                workinfo.prod_code_id = sku.code_id;
                                workinfo.remark = "";
                                workinfo.sendorder_detail_id = Convert.ToInt64(isdetail);
                                workinfo.shop_id = shop.shop_id;
                                workinfo.wh_id = 0;
                                workinfo.work_time = DateTime.Now;
                                workinfo.work_type = 0;
                                workinfo.islock = 0;
                                workinfo.DelOrBarter = 0;
                                workinfo.DelOrBarter_work_id = 0;
                                db.Insert<busi_workinfo>(workinfo);
                            }
                        }
                        else
                        {
                            num = db.Queryable<busi_custorder>().Where(s => s.order_code == ordercode).FirstOrDefault();
                            num.order_sumqty = num.order_sumqty + item.Num;
                            num.order_summoney = (decimal)(num.order_summoney + item.totilMoney); //加上刚才插入的数量
                            db.Update<busi_custorder>(num, it => it.order_id == num.order_id);  //加上刚才插入的金额
                            sku = db.Queryable<base_prod_code>().Where(s1 => s1.sku_code == item.SKU1 + item.SKU2).FirstOrDefault();
                            if (sku == null)
                            {
                                throw new Exception("在系统中不存在此SKU:" + item.SKU1 + item.SKU2);
                            }
                            shop = db.Queryable<base_shop>().Where(s => s.shop_id == item.shop_id).FirstOrDefault();
                            //修复订单多件但是中间有其他订单信息隔开的情况，防止配错货物
                            Lsend = db.Queryable<busi_sendorder>().Where(s => s.custorder_id == num.order_id).FirstOrDefault();
                            Lsend.prod_num = Lsend.prod_num + (int)item.Num;
                            Lsend.prod_money = (decimal)(Lsend.prod_money + item.totilMoney);
                            db.Update<busi_sendorder>(Lsend, it => it.order_id == Lsend.order_id);


                            detail.create_time = DateTime.Now;
                            detail.create_user_id = LoginUser.Current.user_id;
                            detail.del_flag = true;
                            detail.del_time = null;
                            detail.del_user_id = 0;
                            detail.code_id = sku.code_id;
                            // detail.detail_source = 2;
                            detail.detail_status = 0;
                            detail.edit_time = DateTime.Now;
                            detail.edit_user_id = 0;
                            detail.order_id = num.order_id;
                            detail.prod_id = sku.prod_id;
                            detail.prod_name = null;
                            detail.prod_num = (int)item.Num;
                            detail.prod_price = item.totilMoney;
                            detail.remark = null;
                            detail.prod_weight = null;
                            var test = db.Insert<busi_custorder_detail>(detail).ObjToInt();

                            sdetail.code_id = sku.code_id;
                            sdetail.create_time = DateTime.Now;
                            sdetail.create_user_id = LoginUser.Current.user_id;
                            sdetail.del_flag = true;
                            sdetail.del_time = DateTime.Now;
                            sdetail.del_user_id = 0;
                            sdetail.detail_status = 0;
                            sdetail.edit_time = DateTime.Now;
                            sdetail.edit_user_id = 0;
                            sdetail.order_id = Lsend.order_id;
                            sdetail.prod_id = sku.prod_id;
                            sdetail.prod_name = "";
                            sdetail.prod_num = (int)item.Num;
                            sdetail.remark = "";
                            sdetail.prod_price = item.totilMoney;
                            var isdetail2 = db.Insert<busi_sendorder_detail>(sdetail);

                            for (int i = 0; i < (int)item.Num; i++)
                            {
                                workinfo.area_id = null;
                                workinfo.packid = Lsend.order_id;
                                workinfo.create_time = DateTime.Now;
                                workinfo.custorder_id = num.order_id;
                                workinfo.create_user_id = LoginUser.Current.user_id;
                                workinfo.custorder_detail_id = Convert.ToInt64(isdetail2);
                                workinfo.del_flag = true;
                                workinfo.del_time = null;
                                workinfo.del_user_id = 0;
                                workinfo.detail_source = 2;
                                workinfo.edit_time = DateTime.Now;
                                workinfo.edit_user_id = 0;
                                workinfo.is_work = false;
                                workinfo.locat_id = 0;
                                workinfo.plat_id = 0;
                                workinfo.prod_code_id = sku.code_id;
                                workinfo.remark = "";
                                workinfo.sendorder_detail_id = Convert.ToInt64(isdetail2);
                                workinfo.shop_id = shop.shop_id;
                                workinfo.wh_id = 0;
                                workinfo.work_time = DateTime.Now;
                                workinfo.work_type = 0;
                                workinfo.islock = 0;
                                workinfo.DelOrBarter = 0;
                                workinfo.DelOrBarter_work_id = 0;
                                db.Insert<busi_workinfo>(workinfo);
                            }
                        }
                        #endregion

                        item.firstimport = 0;
                        db.Update<LS_Order>(item, it => it.OrderID == item.OrderID);
                    }
                    db.CommitTran(); //提交事务   
                    int count = list.Count;
                    order = null;
                    return count;  //订单数
                }
                catch (Exception ex)
                {
                    db.RollbackTran(); //回滚事务
                    throw ex;
                }
            }


        }

        private static readonly object Locker = new object();
        private static long GetPackCode()
        {
            lock (Locker)  //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                long lastpack = 0;
                string date = DateTime.Now.ToString("yyyyMMdd") + "0001";  //得到当天第一个包裹号       
                using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
                {
                    busi_sendorder send = db.SqlQuery<busi_sendorder>("select * from busi_sendorder where order_code=(select MAX(order_code) from  busi_sendorder)").FirstOrDefault();
                    if (send == null)
                    {
                        return Convert.ToInt64(date);
                    }
                    if (Convert.ToInt64(send.order_code) >= Convert.ToInt64(date))
                    {
                        lastpack = Convert.ToInt64(send.order_code) + 1;
                        return lastpack;
                    }
                    else
                    {
                        return Convert.ToInt64(date);
                    }
                }
            }
        }
        private string GetPackCode1()
        {
            long lastpack = 0;
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                string date = DateTime.Now.ToString("yyyyMMdd") + "0001";  //得到当天第一个包裹号
                busi_sendorder exit = db.Queryable<busi_sendorder>().Where(s => s.order_code == date).FirstOrDefault();
                if (exit == null)
                {
                    return date;
                }
                Dictionary<string, string> maxpack = db.SqlQuery<KeyValuePair<string, string>>("select top 1 order_id,order_code,order_code from busi_sendorder order by order_id desc").ToDictionary(it => it.Key, it => it.Value);
                //busi_sendorder send = db.SqlQuery<busi_sendorder>("select * from busi_sendorder where order_code=(select MAX(order_code) from  busi_sendorder)").FirstOrDefault();
                if (maxpack.Count <= 0)
                {
                    return date;
                }
                else
                {
                    lastpack = Convert.ToInt64(maxpack.Values.First()) + 1;
                    return lastpack.ToString();
                }

            }
        }
        public bool UpdateLStable(int shopID, int id, string clientname, string sku, string num, string telephone, string phone, string address)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.CommandTimeOut = 10000;//设置超时时间 10秒
                try
                {
                    db.BeginTran();//开启事务
                    //db.BeginTran(IsolationLevel.ReadCommitted);+3重载可以设置事世隔离级别
                    base_shop shop = CommService.GetLStableByshopID(shopID);
                    if (shop == null)
                    {
                        throw new Exception("店铺不存在!");
                    }
                    bool isok = false;
                    //支持字典更新，适合动态权限
                    var dic = new Dictionary<string, string>();
                    dic.Add("Buyer", clientname);
                    dic.Add("SKU1", sku);
                    dic.Add("Num", num);
                    dic.Add("telephone", telephone);
                    dic.Add("phone", phone);
                    dic.Add("address", address);
                    isok = db.Update<LS_Order, int>(dic, id);
                    db.CommitTran();//提交事务
                    return isok;
                }
                catch (Exception ex)
                {
                    db.RollbackTran();//回滚事务
                    throw ex;
                }
            }
        }
    }
}
