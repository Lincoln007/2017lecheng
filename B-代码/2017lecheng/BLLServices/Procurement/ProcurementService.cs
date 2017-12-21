using DBModel.Common;
using DBModel.DBmodel;
using DBModel.Procurement;
using IBLLService.Procurement;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Procurement
{
    public class ProcurementService : IProcurementService
    {
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <param name="type"></param>
        /// <param name="purch_code"></param>
        /// <returns></returns>
        public List<ProcurementModel> GetProcurementList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, int? type, string purch_code, string OrderCode, string express_code, string supp_name)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<busi_purchase>()
                            .JoinTable<base_users>((s1, s2) => s1.create_user_id == s2.user_id)
                             .JoinTable<base_supplier>((s1, s3) => s1.supp_id == s3.supp_id)
                                   .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1").OrderBy("s1.purch_id DESC");
                    if (type.HasValue && type > 0)
                    {
                        getwhere = getwhere.Where("s1.purch_status=" + type + "");
                    }
                    if (!string.IsNullOrWhiteSpace(purch_code))
                    {
                        getwhere = getwhere.Where("s1.purch_code LIKE '%" + purch_code + "%'");
                    }
                    if (!string.IsNullOrWhiteSpace(OrderCode))
                    {
                        getwhere = getwhere.Where("s1.OrderCode LIKE '%" + OrderCode + "%'");
                    }

                    if (!string.IsNullOrWhiteSpace(express_code))
                    {
                        getwhere = getwhere.Where("s1.express_code LIKE '%" + express_code + "%'");
                    }
                    if (!string.IsNullOrWhiteSpace(supp_name))
                    {
                        getwhere = getwhere.Where("s3.supp_name LIKE '%" + supp_name + "%'");

                    }


                    totil = getwhere.Count();
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                      .Select<base_users, base_supplier, ProcurementModel>((s1, s2, s3) => new ProcurementModel
                      {
                          express_code = s1.express_code,
                          purch_id = s1.purch_id,
                          purch_code = s1.purch_code,
                          purchase_time = s1.create_time,
                          purch_status = s1.purch_status,
                          emp_name = s2.user_name,
                          purch_type = s1.purch_type,
                          supp_name = s3.supp_name,
                          OrderCode = s1.OrderCode,
                          isLocked = s1.isLocked,
                          Locked_userid = s1.Locked_userid,
                      }).ToList();
                    if (totil > 0)
                    {
                        foreach (var item in list)
                        {
                            if (item.purchase_time == null)
                            {
                                item.purchase_timeE = "";
                            }
                            else
                            {
                                item.purchase_timeE = item.purchase_time.Value.ToString("yyyy-MM-dd");
                            }
                            item.purch_statusE = item.purch_status == 1 ? "初始" : (item.purch_status == 2 ? "已采购" : (item.purch_status == 3 ? "待收货" : (item.purch_status == 4 ? "已全部到货" : "")));
                            item.purch_typeE = item.purch_type == 1 ? "订单采购" : (item.purch_type == 2 ? "库存采购" : "");
                            item.OrderCodeE = item.OrderCode == null ? "" : item.OrderCode;
                            item.express_codeE = item.express_code == null ? "" : item.express_code;

                            //if (item.isLocked)
                            //{
                            //    var info = db.Queryable<base_users>().InSingle(item.Locked_userid);
                            //    if (info != null)
                            //    {
                            //        item.isLockedE = "已锁定&nbsp;&nbsp;(" + info.user_name + ")";
                            //    }
                            //    else
                            //    {
                            //        item.isLockedE = "已锁定&nbsp;&nbsp;" + "()";
                            //    }

                            //}
                            //else
                            //{
                            //    item.isLockedE = "未锁定";
                            //}
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
        /// 根据id获取信息
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
        /// 根据采购ID获取详情
        /// </summary>
        /// <param name="exmsg"></param>
        /// <param name="purch_id"></param>
        /// <returns></returns>
        public List<ProcurementModelE> GetProcurementEList(out string exmsg, Int64? purch_id)
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
                                 .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1 and s5.del_flag=1 and s6.del_flag=1 and s1.purch_id=" + purch_id + "")
                                 .GroupBy("s1.code_id, s1.supp_id,s3.sku_code,s4.supp_name,s5.prod_url,s5.prod_id,s6.pic_url,s5.prod_id, s5.prod_title,s4.supp_url")
                                 .Select<ProcurementModelE>("s1.code_id, s1.supp_id,s3.sku_code,s4.supp_name,s5.prod_url,SUM(s1.purch_count) as purch_count,s6.pic_url,s4.supp_url")
                                 .ToList();
                    if (getwhere.Count > 0)
                    {

                        foreach (var item in getwhere)
                        {
                            List<ProcurementModelEE> getwhere2 = new List<ProcurementModelEE>();
                            var getwhere1 = db.Queryable<busi_purchasedetail>()
                           .Where("del_flag=1 and purch_id=" + purch_id + " and code_id=" + item.code_id + " and supp_id=" + item.supp_id + "").ToList();
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
                                            .Select<ProcurementModelEE>("s4.shop_name,s2.order_code,s1.prod_num,s2.order_tatus").ToList();

                                //if (getwhere2.Count > 0)
                                //{
                                //    foreach (var item2 in getwhere2)
                                //    {
                                //        //40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
                                //        item2.order_tatusE = item2.order_tatus == 40 ? "已配货" : (item2.order_tatus == 50 ? "已拣选" :
                                //            (item2.order_tatus == 60 ? "已包装" : (item2.order_tatus == 70 ? "已发货" : (item2.order_tatus == 80 ? "已转运" : (item2.order_tatus == 90 ? "已再入库" : "")))));
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

    }
}
