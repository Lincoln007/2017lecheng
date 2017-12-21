using DBModel.Common;
using DBModel.DBmodel;
using DBModel.InternationalQuiry;
using IBLLService.InternationalQuiry;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.InternationalQuiry
{
    public class InternationalQuiryService : IInternationalQuiryService
    {
        /// <summary>
        /// 获取快递类型信息
        /// </summary>
        /// <returns></returns>
        public InternationalQuiryResult GetExpressList()
        {
            InternationalQuiryResult result = new InternationalQuiryResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var list = db.Queryable<base_exp_comp>().Where(a => a.del_flag).OrderBy("express_id DESC").ToList();
                    if (list.Count <= 0)
                    {
                        result.success = false;
                        result.Msg = "暂无快递类型信息!";
                        return result;
                    }
                    var list1 = "<option value=\"0\">请选择...</option>";
                    foreach (var item in list)
                    {
                        list1 += "<option value=\"" + item.express_id + "\">" + item.express_name + "</option>";
                    }

                    result.success = true;
                    result.Msg = list1;
                    return result;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.Msg = "获取快递类型失败!";
                    return result;
                }
            }
        }

        /// <summary>
        /// 获取国际物流信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <param name="express_id"></param>
        /// <param name="express_code"></param>
        /// <returns></returns>
        public List<InternationalQuiryModel> GetInternationalQuiryList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
       int? express_id, string express_code)
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
                    if (!string.IsNullOrWhiteSpace(express_code))
                    {
                        getwhere = getwhere.Where(s1 => s1.express_code.Contains(express_code));
                    }

                    totil = getwhere.Count();
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                               .Select<base_exp_comp, InternationalQuiryModel>((s1, s2) => new InternationalQuiryModel
                               {
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

    }
}
