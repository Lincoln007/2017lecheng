using DBModel.Common;
using DBModel.DBmodel;
using DBModel.StoreSales;
using IBLLService.StoreSales;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.StoreSales
{
    public class StoreSalesService : IStoreSalesService
    {
        /// <summary>
        /// 获取店铺销售额信息
        /// </summary>
        /// <param name="platform_id"></param>
        /// <param name="shop_id"></param>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <param name="exmsg"></param>
        /// <returns></returns>
        public List<StoreSalesModel> GetStoreSalesInfo(Int64? platform_id, Int64? shop_id, DateTime? start_time, DateTime? end_time, out string exmsg)
        {
            StoreSalesResult result = new StoreSalesResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var sql = "SELECT   base_platform.platform_name, base_shop.shop_name, " +
                   " (SELECT   SUM(order_summoney) AS Expr2 FROM busi_custorder WHERE   (del_flag = 1) AND (shop_id = " + shop_id + ") GROUP BY shop_id) AS sumshopmoney, " +
                   " (SELECT   SUM(order_summoney) AS Expr1 FROM      busi_custorder AS busi_custorder_2 WHERE   (del_flag = 1) AND (platform_id = " + platform_id + ") GROUP BY platform_id) AS sumplatformmoney" +
                   " FROM      busi_custorder AS busi_custorder_1 INNER JOIN" +
                                   " base_platform ON busi_custorder_1.platform_id = base_platform.platform_id INNER JOIN" +
                                   " base_shop ON base_platform.platform_id = base_shop.platform_id" +
                   " WHERE   (base_platform.del_flag = 1) AND (base_shop.del_flag = 1) AND (busi_custorder_1.del_flag = 1) AND " +
                                   " (busi_custorder_1.platform_id = " + platform_id + ") AND (busi_custorder_1.shop_id = " + shop_id + ")" +
                                   " AND  (busi_custorder_1.create_time >= CONVERT(DATETIME, '" + start_time + "', 102)) " +
                                   " AND (busi_custorder_1.create_time < CONVERT(DATETIME, '" + end_time + "', 102))" +
                   " GROUP BY busi_custorder_1.platform_id, busi_custorder_1.shop_id, base_platform.platform_name, base_shop.shop_name";
                    var list = db.SqlQuery<StoreSalesModel>(sql).ToList();
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
        /// 根据平台ID获取店铺信息
        /// </summary>
        /// <param name="platform_id"></param>
        /// <returns></returns>
        public StoreSalesResult GetShopList(Int64? platform_id)
        {
            StoreSalesResult result = new StoreSalesResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    if (platform_id == 0)
                    {
                        result.success = false;
                        result.Msg = "请选择平台!";
                        return result;
                    }
                    var list = db.Queryable<base_shop>().Where(a => a.del_flag && a.platform_id == platform_id).OrderBy("shop_id DESC").ToList();
                    if (list.Count <= 0)
                    {
                        result.success = false;
                        result.Msg = "该平台下暂无店铺信息!";
                        return result;
                    }
                    var list1 = "<option value=\"0\">请选择...</option>";
                    foreach (var item in list)
                    {
                        list1 += "<option value=\"" + item.shop_id + "\">" + item.shop_name + "</option>";
                    }

                    result.success = true;
                    result.Msg = list1;
                    return result;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.Msg = "获取店铺信息失败!";
                    return result;
                }
            }
        }

    }
}
