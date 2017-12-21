using DBModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.Base.Stock
{
    public interface IStockService
    {
        /// <summary>
        /// 获取总库存
        /// </summary>
        /// <returns></returns>
        StockResult GetStockSum();


        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        List<StockModel> GetStockList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, int? orderby, string sku_code);

        /// <summary>
        /// 获取信息详细
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<StockModel> GetStockEList(out string exmsg, Guid? stock_id);

    }
}
