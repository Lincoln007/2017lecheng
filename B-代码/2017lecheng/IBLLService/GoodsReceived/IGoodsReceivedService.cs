using DBModel.GoodsReceived;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.GoodsReceived
{
    public interface IGoodsReceivedService
    {

        /// <summary>
        /// 获取信息ResetOrdercode
        /// </summary>
        /// <returns></returns>
        /// 
        List<GoodsReceivedModel> GetGoodsReceivedList(int pagenum, int onepagecount, string tb_order_code,out int totil, out int totilpage, out string exmsg);


        GoodsReceivedResult AddCode(Int64? purch_id, Int64? express_id, string express_code, string express_name, string OrderCode);

        GoodsReceivedResult ResetOrdercode(Int64? purch_id, string OrderCode);
    }
}
