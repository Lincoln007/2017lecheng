using DBModel.NotPicking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.NotPicking
{
    public interface INotPickingService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <param name="shop_id"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        List<NotPickingModelE> GetNotPickingList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
           Int64? shop_id, string sku);
    }
}
