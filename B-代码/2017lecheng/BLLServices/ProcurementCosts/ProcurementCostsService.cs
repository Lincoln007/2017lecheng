using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ProcurementCosts;
using IBLLService.ProcurementCosts;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.ProcurementCosts
{
    public class ProcurementCostsService : IProcurementCostsService
    {
        /// <summary>
        /// 获取采购成本信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public List<ProcurementCostsModel> GetProcurementCostsList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, DateTime? start_time, DateTime? end_time)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<busi_purchase>()
                            .JoinTable<base_users>((s1, s2) => s1.create_user_id == s2.user_id)
                                   .Where("s1.del_flag=1  AND s1.create_time >= CONVERT(DATETIME, '" + start_time + "', 102) AND s1.create_time < CONVERT(DATETIME, '" + end_time + "', 102)")
                                   .OrderBy("s1.purch_id DESC")
                                   .Select<base_users, ProcurementCostsModel>((s1, s2) => new ProcurementCostsModel
                                   {
                                       purch_code = s1.purch_code,
                                       sum_money = s1.sum_money,
                                       create_time = s1.create_time,
                                       emp_name = s2.user_name,
                                   }).ToList();

                    totil = getwhere.Count();
                    if (totil > 0)
                    {
                        var sql = "SELECT   SUM(sum_money) AS sum_money FROM      busi_purchase " +
                                   " WHERE   (del_flag = 1) AND (create_time >= CONVERT(DATETIME, '" + start_time + "', 102)) AND " +
                                   " (create_time < CONVERT(DATETIME, '" + end_time + "', 102))";
                        var sum_moneyE = db.SqlQuery<ProcurementCostsModel>(sql);
                        getwhere.ForEach(a =>
                        {
                            a.sum_moneyE = sum_moneyE.FirstOrDefault().sum_money;
                        });

                        foreach (var item in getwhere)
                        {
                            item.create_timeE = item.create_time.ToString("yyyy-MM-dd");
                        }
                    }
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount).ToList();
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
    }
}
