using DBModel.Common;
using DBModel.DBmodel;
using DBModel.NotPicking;
using IBLLService.NotPicking;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.NotPicking
{
    public class NotPickingService : INotPickingService
    {
        /// <summary>
        /// 获取未配货信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <param name="shop_id"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public List<NotPickingModelE> GetNotPickingList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
           Int64? shop_id, string sku)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    string where = string.Empty;
                    if (shop_id.HasValue && shop_id > 0)
                    {
                        where = "  AND (busi_custorder.shop_id = " + shop_id + ") ";
                    }
                    if (!string.IsNullOrWhiteSpace(sku))
                    {
                        where += " AND (base_prod_code.sku_code like '%" + sku + "%') AND (busi_workinfo.is_work = 0)";
                    }

                    var list1 = db.Queryable<busi_sendorder>().Where(a => a.del_flag && a.order_tatus != 40).OrderBy("order_id DESC").ToList();
                    var list3 = list1.Skip(onepagecount * (pagenum - 1)).Take(onepagecount).ToList();

                    var list = new List<NotPickingModel>();
                    List<NotPickingModelE> result = new List<NotPickingModelE>();
                    string ids = string.Empty;
                    string id_s = string.Empty;
                    if (list3.Count > 0)
                    {
                        foreach (var item in list3)
                        {
                            ids += item.order_id + ",";
                        }
                        id_s = ids.Substring(0, ids.Length - 1);
                        var sql = "select count(1) as num, order_id,order_code,sku_code,is_work,cus_name" +
                                                  " ,shop_name, shop_id,code_id FROM" +
                                  " (" +
                                  " SELECT   busi_sendorder_detail.order_id, busi_sendorder.order_code, base_prod_code.sku_code, busi_workinfo.is_work, " +
                                                  " busi_custorder.cus_name, base_shop.shop_name, busi_custorder.shop_id, busi_sendorder_detail.code_id" +
                                  " FROM      busi_sendorder_detail INNER JOIN" +
                                                  " busi_sendorder ON busi_sendorder_detail.order_id = busi_sendorder.order_id INNER JOIN" +
                                                  " base_prod_code ON busi_sendorder_detail.code_id = base_prod_code.code_id INNER JOIN" +
                                                  " busi_workinfo ON busi_sendorder_detail.detail_id = busi_workinfo.sendorder_detail_id INNER JOIN" +
                                                  " busi_custorder ON busi_workinfo.custorder_id = busi_custorder.order_id INNER JOIN" +
                                                  " base_shop ON busi_custorder.shop_id = base_shop.shop_id" +
                                  " WHERE   (busi_sendorder_detail.del_flag = 1) AND (busi_sendorder.del_flag = 1) AND (busi_workinfo.del_flag = 1) AND " +
                                                  " (base_prod_code.del_flag = 1) AND (busi_custorder.del_flag = 1) AND (busi_sendorder.order_tatus <> 40) AND " +
                                                  " (base_shop.del_flag = 1) " + where + " AND (busi_workinfo.packid in (" + id_s + "))) " +
                                                 "  as b  group by b.order_id,b.order_code,b.sku_code,b.is_work,b.cus_name,b.shop_name ,b.shop_id,b.code_id  ";

                        list = db.SqlQuery<NotPickingModel>(sql).ToList();
                        if (list.Count > 0)
                        {
                            foreach (var item in list3)
                            {
                                NotPickingModelE res = new NotPickingModelE();
                                int peinumsum = 0;
                                int nopeinum = 0;
                                int sum = 0;
                                var order_code = list.Where(s => s.order_id == item.order_id).FirstOrDefault().order_code;
                                var emp_name = list.Where(s => s.order_id == item.order_id).FirstOrDefault().cus_name;
                                var shop_name = list.Where(s => s.order_id == item.order_id).FirstOrDefault().shop_name;
                                var nopei = list.Where(s => s.order_id == item.order_id && !s.is_work)
                                      .Select<NotPickingModel, NotPickingModelNO>(s => new NotPickingModelNO
                                      {
                                          no_sku_code = s.sku_code,
                                          nopeinum = s.num,
                                      }).ToList();
                                if (nopei.Count > 0)
                                {
                                    foreach (var item1 in nopei)
                                    {
                                        nopeinum += item1.nopeinum;
                                    }
                                }

                                var pei = list.Where(s => s.order_id == item.order_id && s.is_work)
                                      .Select<NotPickingModel, NotPickingModelYes>(s => new NotPickingModelYes
                                      {
                                          yes_sku_code = s.sku_code,
                                          peinum = s.num,
                                      }).ToList();
                                if (pei.Count > 0)
                                {
                                    foreach (var item2 in pei)
                                    {
                                        peinumsum += item2.peinum;
                                    }
                                }
                                sum = peinumsum + nopeinum;

                                res.nopei = nopei;
                                res.emp_name = emp_name;
                                res.order_code = order_code;
                                res.pei = pei;
                                res.peinumsum = peinumsum;
                                res.shop_name = shop_name;
                                res.sum = sum;
                                result.Add(res);

                                //var newlist = new List<NotPickingModelYes>();
                                //if (pei.Count > 0 && nopei.Count == 0)
                                //{
                                //    pei.ForEach(b =>
                                //    {
                                //        b.peinumcount = b.peinum;
                                //        newlist.Add(b);
                                //    });
                                //}
                                //else if (pei.Count == 0 && nopei.Count > 0)
                                //{
                                //    pei.ForEach(b =>
                                //    {
                                //        b.peinumcount = b.peinum;
                                //        newlist.Add(b);
                                //    });
                                //}
                                //pei.ForEach(a =>
                                //{
                                //    nopei.ForEach(b =>
                                //    {
                                //        if (a.yes_sku_code == b.no_sku_code)
                                //        {
                                //            a.peinumcount = a.peinum + b.nopeinum;
                                //            newlist.Add(a);
                                //        }
                                //    });
                                //});
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(sku))
                    {
                        totil = list.Count();
                        totilpage = totil / onepagecount;
                        if (totil % onepagecount > 0)
                        {
                            totilpage++;
                        }
                    }
                    else
                    {

                        totil = list1.Count();
                        totilpage = totil / onepagecount;
                        if (totil % onepagecount > 0)
                        {
                            totilpage++;
                        }
                    }
                    exmsg = "";
                    return result.ToList();

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
    }
}
