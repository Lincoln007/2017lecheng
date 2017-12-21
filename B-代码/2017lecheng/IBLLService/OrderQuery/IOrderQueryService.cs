using DBModel.DBmodel;
using DBModel.OrderQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.OrderQuery
{
    public interface IOrderQueryService
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        /// 
        List<OrderQueryModel> GetIOrderQueryList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
            Int64? shop_id, DateTime? create_time, string order_code, string custorder_code, string emp_name);



        List<OrderQueryModelE> GetIOrderQueryListE(out string exmsg, Int64? order_id);

        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<busi_sendorder> GetInfoByID(Int64? id);
    }
}
