using DBModel.DBmodel;
using DBModel.MaterialReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.MaterialReceipt
{
    public interface IMaterialReceiptService
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        /// 
        List<MaterialReceiptModel> GetMaterialReceiptList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, string express_code);


        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        busi_purchase GetInfoByID(long? id);


        List<MaterialReceiptModelE> GetMaterialReceiptEList(out string exmsg, Int64? purch_id);

        MaterialReceiptResult Save(busi_purchase model, List<MaterialReceiptSaveModel> lists, int? purch_type);

        MaterialReceiptResult Modify(Int64? purch_id, Int64? express_id, string express_code, string express_name, string OrderCode);
    }
}
