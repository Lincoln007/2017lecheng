using DBModel.ProcurementCosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.ProcurementCosts
{
    public interface IProcurementCostsService
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        /// 
        List<ProcurementCostsModel> GetProcurementCostsList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, DateTime? start_time, DateTime? end_time);

    }
}
