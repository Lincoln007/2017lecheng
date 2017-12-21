using DBModel.Common;
using DBModel.DBmodel;
using DBModel.OverseasDelivery;
using IBLLService.OverseasDelivery;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.OverseasDelivery
{
    public class OverseasDeliveryService : IOverseasDeliveryService
    {

        /// <summary>
        /// 获取 海外管理—待收货处理 信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <param name="express_id"></param>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public List<OverseasDeliveryModel> GetOverseasDeliveryList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
     int? express_id, DateTime? start_time, DateTime? end_time)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<busi_transfer>()
                                   .JoinTable<base_exp_comp>((s1, s2) => s1.express_id == s2.express_id)
                                   .Where("s1.del_flag=1 and s2.del_flag=1 ").OrderBy("s1.tran_id DESC");
                    if (express_id > 0)
                    {
                        getwhere = getwhere.Where(s1 => s1.express_id == express_id);
                    }
                    if (start_time.HasValue)
                    {
                        getwhere = getwhere.Where(s1 => s1.create_time >= start_time);
                    }
                    if (end_time.HasValue)
                    {
                        DateTime time = end_time.Value.AddDays(1);
                        getwhere = getwhere.Where(s1 => s1.create_time < time);
                    }
                    totil = getwhere.Count();
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                               .Select<base_exp_comp, OverseasDeliveryModel>((s1, s2) => new OverseasDeliveryModel
                               {
                                   tran_status = s1.tran_status,
                                   tran_code = s1.tran_code,
                                   express_code = s1.express_code,
                                   create_time = s1.create_time,
                                   tran_id = s1.tran_id,
                                   express_name = s2.express_name,
                                   express_coded = s2.express_coded,
                               }).ToList();


                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.create_timeE = item.create_time.ToString("yyyy-MM-dd HH:MM:ss");
                            item.tran_statusE = item.tran_status.Value == 1 ? "未转运" : (item.tran_status.Value == 2 ? "已转运" : (item.tran_status.Value == 3 ? "已到货" : ""));
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
        /// 确认到货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OverseasDeliveryResult GetSave(Int64? id)
        {
            bool rstNum = false;
            OverseasDeliveryResult result = new OverseasDeliveryResult();
            if (id == 0)
            {
                result.success = false;
                result.Msg = "参数错误";
                return result;
            }
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var model = db.Queryable<busi_transfer>().InSingle(id.Value);
                if (model == null)
                {
                    result.success = false;
                    result.Msg = "不存在的信息";
                    return result;
                }

                if (model.tran_status >= 1 && model.tran_status <= 3)
                {
                    //if (model.tran_status == 1)
                    //{
                    //    result.success = false;
                    //    result.Msg = "该转运单号未转运,操作失败!";
                    //    return result;
                    //}
                    if (model.tran_status == 3)
                    {
                        result.success = false;
                        result.Msg = "该转运单号已确认收货,操作失败!";
                        return result;
                    }
                }
                else
                {
                    result.success = false;
                    result.Msg = "不存在的状态,操作失败!";
                    return result;
                }
                model.edit_time = DateTime.Now;
                model.edit_user_id = LoginUser.Current.user_id;
                model.tran_status = 3;
                rstNum = db.Update<busi_transfer>(model);
                if (rstNum)
                {
                    result.success = true;
                    result.URL = "/OverseasDelivery/Index";
                    result.Msg = "操作成功!";
                    return result;
                }
                else
                {
                    result.success = false;
                    result.Msg = "操作失败!";
                    return result;
                }
            }
        }

    }
}
