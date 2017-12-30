using DBModel.Base;
using DBModel.Common;
using DBModel.DBmodel;
using IBLLService.Base.Stock;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Base.Stock
{
    public class StockService : IStockService
    {
        /// <summary>
        /// 获取总库存
        /// </summary>
        /// <returns></returns>
        public StockResult GetStockSum()
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                StockResult result = new StockResult();
                var list = db.Queryable<base_wh_stock>().Where(s => s.del_flag).ToList();
                if (list.Count == 0)
                {
                    result.Msg = "0";
                    return result;
                }
                Decimal sum = 0;
                foreach (var item in list)
                {
                    sum += item.stock_qty;
                }

                string stock_qty = sum.ToString();
                result.Msg = stock_qty;
                return result;



            }
        }

        /// <summary>
        /// 获取库存信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="sku_code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<StockModel> GetStockList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, int? orderby, string sku_code)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var sql = db.Queryable<base_wh_stock>()
                             .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                             .JoinTable<base_location>((s1,s3)=>s1.location_id==s3.locat_id)
                             .Where("s1.del_flag=1 and s2.del_flag=1")
                             .GroupBy("s1.code_id,s2.sku_code,s1.stock_id,s3.locat_code")
                             .OrderBy("s1.code_id DESC");
                    if (!string.IsNullOrWhiteSpace(sku_code))
                    {
                        sql = sql.Where<base_prod_code>((s1, s2) => s2.sku_code.Contains(sku_code));
                    }
                    var list = sql.Select<StockModel>("s1.stock_id,s1.code_id,s3.locat_code,SUM(s1.stock_qty) AS stock_qty ,s2.sku_code").ToList();
                    totil = list.Count();
                    var jList = list.Skip(onepagecount * (pagenum - 1)).Take(onepagecount).ToList();


                    //if (jList2.Count > 1)
                    //{
                    //    if (orderby == 1)
                    //    {
                    //        jList.OrderByDescending(a => a.stock_qty);
                    //    }
                    //    else if (orderby == 2)
                    //    {
                    //        jList.OrderBy(a => a.stock_qty);
                    //    }
                    //}
                    totilpage = totil / onepagecount;
                    exmsg = "";
                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }
                    return jList.ToList();
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
        /// 根据库存id 获取库存详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<StockModel> GetStockEList(out string exmsg, Guid? stock_id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (!stock_id.HasValue)
                {
                    exmsg = "参数错误!";
                    return null;
                }
                try
                {
                    var sql = db.Queryable<base_wh_stock>()
                                .JoinTable<base_wh_warehouse>((s1, s2) => s1.wh_id == s2.wh_id)
                                .JoinTable<base_prod_code>((s1, s3) => s1.code_id == s3.code_id)
                                .JoinTable<base_product>((s1, s4) => s4.prod_id == s1.prod_id)
                                .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1  and s1.stock_id='" + stock_id + "'")
                                .OrderBy("s1.code_id DESC")
                                .Select<StockModel>("s1.stock_qty ,s2.wh_name,s3.sku_code,s4.prod_title").ToList();
                    exmsg = "";
                    return sql.ToList();
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
