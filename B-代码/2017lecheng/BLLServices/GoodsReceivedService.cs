using DBModel.Common;
using DBModel.DBmodel;
using DBModel.GoodsReceived;
using IBLLService.GoodsReceived;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices
{
    public class GoodsReceivedService : IGoodsReceivedService
    {

        /// <summary>
        /// 获取待填单号信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <returns></returns>
        public List<GoodsReceivedModel> GetGoodsReceivedList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<busi_purchase>()
                            .JoinTable<base_users>((s1, s2) => s1.create_user_id == s2.user_id)
                            .JoinTable<base_supplier>((s1, s3) => s1.supp_id == s3.supp_id)
                            .Where("s1.del_flag=1 and s2.del_flag=1 and s1.purch_status=2")
                            .OrderBy("s1.purch_id DESC");
                    totil = getwhere.Count();
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                      .Select<base_users, base_supplier, GoodsReceivedModel>((s1, s2, s3) => new GoodsReceivedModel
                      {
                          purch_id = s1.purch_id,
                          purch_code = s1.purch_code,
                          supp_name = s3.supp_name,
                          user_name = s2.user_name,
                          purchase_time = s1.create_time,
                          purch_status = s1.purch_status,
                          express_id = s1.express_id,
                          express_code = s1.express_code,
                          express_name = s1.express_name,
                          purch_type = s1.purch_type,
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
        /// 填写快递单号
        /// </summary>
        /// <param name="purch_id"></param>
        /// <param name="express_id"></param>
        /// <param name="express_code"></param>
        /// <param name="express_name"></param>
        /// <param name="OrderCode"></param>
        /// <returns></returns>
        public GoodsReceivedResult AddCode(Int64? purch_id, Int64? express_id, string express_code, string express_name, string OrderCode)
        {
            bool rstNum = false;
            bool rstNums = false;
            GoodsReceivedResult result = new GoodsReceivedResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.BeginTran();
                var list = db.Queryable<busi_purchase>().Where(s => s.del_flag).InSingle(purch_id.Value);
                if (list == null)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "无效的采购信息,操作失败!";
                    return result;
                }

                rstNum = db.Update<busi_purchase>(new { purch_status = 3, express_id = express_id, express_code = express_code, express_name = express_name, OrderCode = OrderCode }, a => a.purch_id == purch_id && a.purch_status == 2);
                if (rstNum)
                {
                    rstNums = db.Update<busi_purchasedetail>(new { purch_status = 3 }, a => a.purch_id == purch_id && a.purch_status == 2);
                }
                if (rstNum && rstNums)
                {
                    db.CommitTran();
                    result.success = true;
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

        }

    }
}
