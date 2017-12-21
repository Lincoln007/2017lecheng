using DBModel.Common;
using DBModel.DBmodel;
using DBModel.LogisticsQuery;
using IBLLService.LogisticsQuery;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.LogisticsQuery
{
    public class LogisticsQueryService : ILogisticsQueryService
    {

        /// <summary>
        /// 获取快递类型信息
        /// </summary>
        /// <returns></returns>
        public LogisticsQueryResult GetExpressList()
        {
            LogisticsQueryResult result = new LogisticsQueryResult();
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
                        list1 += "<option value=\"" + item.express_coded + "\">" + item.express_name + "</option>";
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
    }
}
