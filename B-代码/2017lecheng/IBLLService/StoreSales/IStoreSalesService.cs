using DBModel.StoreSales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.StoreSales
{
    public interface IStoreSalesService
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        /// 
        List<StoreSalesModel> GetStoreSalesInfo(Int64? platform_id, Int64? shop_id, DateTime? start_time, DateTime? end_time, out string exmsg);

        StoreSalesResult GetShopList(Int64? platform_id);
    }
}
