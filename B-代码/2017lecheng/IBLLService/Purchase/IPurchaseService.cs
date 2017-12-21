using DBModel.Common;
using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.Purchase
{
    public interface IPurchaseService
    {
        /// <summary>
        /// 添加库存采购信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ComResult AddPurchaseService(string sku, int num, int supId);
    }
}
