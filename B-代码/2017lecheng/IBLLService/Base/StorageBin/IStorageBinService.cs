using DBModel.Base;
using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.Base.StorageBin
{
    public interface IStorageBinService
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        List<StorageBinModel> GetStorageBinList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, long? wh_id, string locat_code);


        /// <summary>
        /// 修改 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        StorageBinResult Save(base_location model);

        /// <summary>
        /// 获取库存详情
        /// </summary>
        /// <returns></returns>
        List<StorageBinModel> GetStorageBinEList(out string exmsg, long? wh_id, long? locat_id);


        /// <summary>
        /// 获取上下架信息
        /// </summary>
        /// <returns></returns>


        List<StorageBinModel> GetStorageBinInOutList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, long? wh_id, string locat_code, string sku_code);

        /// <summary>
        /// 上下架
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        StorageBinResult StockInOut(Guid? id, Decimal count, int type);


        StorageBinResult GetWh_nameList();



        StorageBinResult Save_sku(Int64 wh_id, Int64 locat_id, string sku, int skucount);

    }
}
