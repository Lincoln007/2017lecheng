using DBModel.DBmodel;
using DBModel.OrderQuery_Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.OrderQuery_Shop
{
    public interface IOrderQuery_ShopService
    {

        List<OrderQuery_ShopModel> GetOrderQuery_ShopList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg,
            Int64? shop_id, DateTime? create_time, string order_code, string custorder_code, string emp_name, int? state, int? day,int? usedepot,int? orderstate, string phone, string exp_code);


        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        busi_custorder GetInfoByID(long? id);


        List<OrderQuery_ShopModelE> GetOrderQuery_ShopEList(out string exmsg, Int64? custorder_id);


        OrderQuery_ShopResult Save(List<busi_sendorder> lists);


        OrderQuery_ShopResult Del(Int64? detail_id, Int64? work_id, Int64? id);
        OrderQuery_ShopResult Barter(Int64? detail_id, Int64? work_id, string sku, Int64? id);
        OrderQuery_ShopResult Delpackge(string packgecode);
    }
}
