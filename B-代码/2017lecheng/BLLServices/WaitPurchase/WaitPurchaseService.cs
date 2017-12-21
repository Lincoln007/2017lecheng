using DBModel.Common;
using DBModel.DBmodel;
using DBModel.WaitPurchase;
using IBLLService.WaitPurchase;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLLServices.WaitPurchase
{
    public class WaitPurchaseService : IWaitPurchaseService
    {
        /// <summary>
        /// 获取代采购信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <returns></returns>
        public List<WaitPurchaseModel> GetWaitPurchaseList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<busi_purchase>()
                                     .JoinTable<base_users>((s1, s2) => s1.create_user_id == s2.user_id)
                                     .JoinTable<base_supplier>((s1, s3) => s1.supp_id == s3.supp_id)
                                     .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s1.purch_status=1")
                                     .OrderBy("s1.purch_id DESC");
                    totil = getwhere.Count();
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                      .Select<base_users, base_supplier, WaitPurchaseModel>((s1, s2, s3) => new WaitPurchaseModel
                      {
                          purch_id = s1.purch_id,
                          purch_code = s1.purch_code,
                          create_time = s1.create_time,
                          purch_status = s1.purch_status,
                          emp_name = s2.user_name,
                          purch_type = s1.purch_type,
                          supp_name = s3.supp_name,
                          isLocked = s1.isLocked,
                          Locked_userid = s1.Locked_userid,
                      }).ToList();
                    if (totil > 0)
                    {
                        foreach (var item in list)
                        {
                            item.create_timeE = item.create_time.ToString("yyyy-MM-dd");
                            item.purch_statusE = item.purch_status == 1 ? "初始" : (item.purch_status == 2 ? "已采购" : (item.purch_status == 3 ? "待收货" : (item.purch_status == 4 ? "已全部到货" : "")));
                            item.purch_typeE = item.purch_type == 1 ? "订单采购" : (item.purch_type == 2 ? "库存采购" : "");
                            if (item.isLocked)
                            {
                                var info = db.Queryable<base_users>().InSingle(item.Locked_userid);
                                if (info != null)
                                {
                                    item.isLockedE = "已锁定&nbsp;&nbsp;(" + info.user_name + ")";
                                }
                                else
                                {
                                    item.isLockedE = "已锁定&nbsp;&nbsp;" + "()";
                                }

                            }
                            else
                            {
                                item.isLockedE = "未锁定";
                            }
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
                    exmsg = ex.ToString();
                    totil = 0;
                    totilpage = 0;
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据id获取采购单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public busi_purchase GetInfoByID(long? id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (!id.HasValue)
                {
                    return new busi_purchase();
                }
                try
                {
                    var list = db.Queryable<busi_purchase>().Where(s => s.del_flag).InSingle(id.Value);
                    if (list == null)
                    {
                        return new busi_purchase();
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
        /// 根据采购id获取采购单详情
        /// </summary>
        /// <param name="exmsg"></param>
        /// <param name="purch_id"></param>
        /// <returns></returns>
        public List<WaitPurchaseModelE> GetWaitPurchaseEList(out string exmsg, Int64? purch_id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (purch_id == 0)
                {
                    exmsg = "参数错误";
                    return null;
                }
                try
                {
                    var getwhere = db.Queryable<busi_purchasedetail>()
                                   .JoinTable<busi_purchase>((s1, s2) => s1.purch_id == s2.purch_id)
                                   .JoinTable<base_prod_code>((s1, s3) => s1.code_id == s3.code_id)
                                   .JoinTable<base_supplier>((s1, s4) => s1.supp_id == s4.supp_id)
                                   .JoinTable<base_prod_code, base_product>((s1, s3, s5) => s3.prod_id == s5.prod_id)
                                   .JoinTable<base_product_imgs>((s1, s6) => s1.code_id == s6.code_id)
                                   .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1 and s5.del_flag=1 and s6.del_flag=1 and s1.purch_id=" + purch_id + "  and s1.purch_status=1")
                                   .GroupBy("s1.code_id, s1.supp_id,s3.sku_code,s4.supp_name,s5.prod_url,s5.prod_id,s6.pic_url,s5.prod_id, s5.prod_title,s4.supp_url")
                                   .Select<WaitPurchaseModelE>("s1.code_id, s1.supp_id,s3.sku_code,s4.supp_name,s5.prod_url,SUM(s1.purch_count) as purch_count,s6.pic_url,s5.prod_id, s5.prod_title,s4.supp_url")
                                   .ToList();

                    if (getwhere.Count > 0)
                    {
                        foreach (var item in getwhere)
                        {
                            List<WaitPurchaseModelEE> getwhere2 = new List<WaitPurchaseModelEE>();
                            var getwhere1 = db.Queryable<busi_purchasedetail>()
                           .Where("del_flag=1 and purch_id=" + purch_id + " and code_id=" + item.code_id + " and supp_id=" + item.supp_id + "  and purch_status=1").ToList();
                            if (getwhere1.Count > 0)
                            {
                                List<Int64> ss = new List<Int64>();
                                foreach (var item1 in getwhere1)
                                {
                                    ss.Add(item1.send_detail_id);
                                }
                                getwhere2 = db.Queryable<busi_sendorder_detail>()
                                      .JoinTable<busi_sendorder>((s1, s2) => s1.order_id == s2.order_id)
                                      .JoinTable<busi_sendorder, busi_custorder>((s1, s2, s3) => s2.custorder_id == s3.order_id)
                                       .JoinTable<busi_custorder, base_shop>((s1, s3, s4) => s3.shop_id == s4.shop_id)
                                              .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1")
                                              .Where(s1 => ss.Contains(s1.detail_id))
                                              .Select<WaitPurchaseModelEE>("s4.shop_name,s2.order_code,s1.prod_num,s2.order_tatus").ToList();

                                //if (getwhere2.Count > 0)
                                //{
                                //    foreach (var item2 in getwhere2)
                                //    {
                                //        //40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
                                //        item2.order_tatusE = item2.order_tatus == 40 ? "已配货" : (item2.order_tatus == 50 ? "已拣选" :
                                //        (item2.order_tatus == 60 ? "已包装" : (item2.order_tatus == 70 ? "已发货" : (item2.order_tatus == 80 ? "已转运" :
                                //        (item2.order_tatus == 90 ? "已再入库" : "")))));
                                //    }
                                //}
                            }
                            item.details = getwhere2;
                        }
                    }
                    exmsg = "";
                    return getwhere.ToList();
                }
                catch (Exception ex)
                {
                    exmsg = ex.ToString();
                    return null;
                }
            }

        }

        /// <summary>
        /// 采购
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WaitPurchaseResult Save(busi_purchase model)
        {
            bool rstNum = false;
            bool rstNums = false;
            WaitPurchaseResult result = new WaitPurchaseResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();
                    #region 判断
                    if (model == null)
                    {
                        result.success = false;
                        result.Msg = "请填写信息!";
                        return result;
                    }

                    if (model.purch_id == 0)
                    {
                        result.success = false;
                        result.Msg = "参数错误!";
                        return result;
                    }

                    if (string.IsNullOrWhiteSpace(model.OrderCode))
                    {
                        result.success = false;
                        result.Msg = "淘宝订单号不得为空!";
                        return result;
                    }
                    string sum_freight = model.sum_freight.ToString();
                    if (string.IsNullOrWhiteSpace(sum_freight))
                    {
                        result.success = false;
                        result.Msg = "请输入正确格式的运费!";
                        return result;
                    }
                    #endregion


                    var list = db.Queryable<busi_purchase>().Where(s => s.del_flag).InSingle(model.purch_id);
                    if (list == null)
                    {
                        result.success = false;
                        result.Msg = "不存在的采购信息";
                        return result;
                    }

                    if (list.isLocked)
                    {
                        var info = db.Queryable<base_users>().InSingle(list.Locked_userid);
                        if (info != null)
                        {
                            result.success = false;
                            result.Msg = "该数据已被 " + info.user_name + " 锁定,操作失败!";
                            return result;

                        }
                        else
                        {
                            result.success = false;
                            result.Msg = "该数据已被锁定,操作失败!";
                            return result;
                        }
                    }
                    if (list.purch_status == 2)
                    {
                        result.success = false;
                        result.Msg = "该单号已采购过,请勿重复操作!";
                        return result;
                    }
                    list.sum_money = model.sum_freight + list.purch_sum;
                    list.sum_freight = model.sum_freight;
                    list.purch_remark = model.purch_remark;
                    list.OrderCode = model.OrderCode;
                    list.isLocked = false;
                    list.Locked_userid = 0;
                    list.edit_time = DateTime.Now;
                    list.purch_status = 2;
                    list.edit_user_id = LoginUser.Current.user_id;
                    list.create_time = DateTime.Now;
                    rstNum = db.Update<busi_purchase>(list);
                    if (rstNum)
                    {
                        rstNums = db.Update<busi_purchasedetail>(new { purch_status = 2 }, a => a.purch_id == model.purch_id && a.purch_status == 1);
                    }


                    if (rstNum && rstNums)
                    {
                        db.CommitTran();
                        result.success = true;
                        // result.URL = "/WaitPurchase/IndexE?id=" + model.purch_id + "";
                        result.Msg = "操作成功";
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
        /// 根据ID锁定 解锁 采购信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WaitPurchaseResult isLocked(busi_purchase model)
        {
            bool rstNum = false;
            WaitPurchaseResult result = new WaitPurchaseResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    if (model == null)
                    {
                        result.success = false;
                        result.Msg = "请填写信息!";
                        return result;
                    }

                    if (model.purch_id == 0)
                    {
                        result.success = false;
                        result.Msg = "参数错误!";
                        return result;
                    }
                    var list = db.Queryable<busi_purchase>().Where(s => s.del_flag).InSingle(model.purch_id);
                    if (list == null)
                    {
                        result.success = false;
                        result.Msg = "不存在的采购信息";
                        return result;
                    }
                    Int64 uid = 0;
                    if (model.isLocked)
                    {
                        uid = LoginUser.Current.user_id;
                    }
                    else
                    {
                        if (LoginUser.Current.user_id != list.Locked_userid)
                        {
                            var info = db.Queryable<base_users>().InSingle(list.Locked_userid);
                            if (info != null)
                            {
                                result.success = false;
                                result.Msg = "该数据已被 " + info.user_name + " 锁定,操作失败!";
                                return result;
                            }
                            else
                            {
                                result.success = false;
                                result.Msg = "该数据已被锁定,操作失败!";
                                return result;
                            }
                        }
                    }
                    rstNum = db.Update<busi_purchase>(new { isLocked = model.isLocked, Locked_userid = uid }, a => a.purch_id == model.purch_id);
                    if (rstNum)
                    {
                        result.success = true;
                        result.Msg = "操作成功";
                        return result;
                    }
                    else
                    {
                        result.success = false;
                        result.Msg = "操作失败";
                        return result;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }
}
