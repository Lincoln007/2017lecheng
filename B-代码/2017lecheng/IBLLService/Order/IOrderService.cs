
using DBModel.Common;
using DBModel.DBmodel;
using DBModel.Order;
using DBModel.ViewModel;
using DBModel.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.Order
{
    public interface IOrderService
    {
        /// <summary>
        /// 插入临时表
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        bool InsertLStable(int shopID, DateTime naqi, DataTable table);
        /// <summary>
        /// 查询临时表
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="naqi"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        List<base_LSorder> SearchLStable(int shopID, out int totil, out string exmsg);
        bool DeleteLStable(int shopID, int id);
        bool UpdateLStable(int shopID, int id, string clientname, string sku, string num, string telephone, string phone, string address);
        int InsertOrder(int platformID, int shopID, List<base_LSorder> list, out int lvcount);
        /// <summary>
        /// 通过时间查询店铺总的订单数1
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        List<OrderSum> GetOrderCountByTime(string time);
        /// <summary>
        /// 通过店铺ID获取店铺订单
        /// </summary>
        /// <param name="shop_id"></param>
        /// <returns></returns>
        List<ShopOrder> GetShopOrderById(long shop_id, int pageIndex, int pageSize, out int count, string time);

        /// <summary>
        /// 通过订单ID获取作业表信息
        /// </summary>
        /// <param name="order_detail_id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<OrderWork> GetOrderWorkById(long order_id, int pageIndex, int pageSize, out int count);


        /// <summary>
        /// 通过订单ID获取作业表信息
        /// </summary>
        /// <param name="order_detail_id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<OrderWorkE> GetOrderEById(long order_id, out string exmsg);

        /// <summary>
        /// 通过发货订单ID更新用户地址信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="receive_name"></param>
        /// <param name="receive_address"></param>
        /// <param name="receive_phone"></param>
        /// <param name="receive_zip"></param>
        /// <param name="receive_mobile"></param>
        /// <returns></returns>
        bool UpdateSendOrderUserInfoById(int order_id, string receive_name, string receive_address, string receive_phone, string receive_zip, string receive_mobile);


        /// <summary>
        /// 更新作业表信息
        /// </summary>
        /// <param name="detail_id"></param>
        /// <param name="old_wh_id"></param>
        /// <param name="detail_source"></param>
        /// <param name="new_wh_id"></param>
        /// <returns></returns>
        ComResult UpdateWorkInfo(int detail_id, int old_wh_id, int detail_source, int new_wh_id, int num, int proId);


        /// <summary>
        /// 获取所有供应商
        /// </summary>
        /// <returns></returns>
        List<base_supplier> GetSupPlier();

        /// <summary>
        /// 获取所有仓库信息
        /// </summary>
        /// <returns></returns>
        List<base_wh_warehouse> GetWareHouse();

        /// <summary>
        /// 通过入库订单ID更新订单状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        ComResult UpdateCustOrder(List<long> ids, int state);

        /// <summary>
        /// 导出通过店铺ID和转运单号获取发货订单
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="tran_code"></param>
        /// <returns></returns>
        List<SendOrder> GetSendOrderList(int shop_id, string tran_code);

        /// <summary>
        /// 通过店铺ID和转运单号获取发货订单
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="exp_code"></param>
        /// <returns></returns>
        List<SendOrder> GetSendOrderList(int shop_id, string tran_code, int pageIndex, int pageSize, out int count);



        List<busi_sendorder> GetInfoByID(Int64? id);

        OrderResult ProcessCustOrder(List<long> ids, bool ischeck, long shopid, long pageIndex, string time);

        OrderResult ProcessAllCustOrder(bool ischeck, long shopid, long pageIndex, string time);
    }
}
