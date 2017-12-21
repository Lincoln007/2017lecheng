using DBModel.DBmodel;
using DBModel.Procurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.Procurement
{
    public interface IProcurementService
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        /// 
        List<ProcurementModel> GetProcurementList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, int? type, string purch_code, string OrderCode, string express_code, string supp_name);

        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        busi_purchase GetInfoByID(long? id);


        List<ProcurementModelE> GetProcurementEList(out string exmsg, Int64? purch_id);

    }
}
