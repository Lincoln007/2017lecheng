using DBModel.Base;
using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.Base.WareHouse
{
    public interface IWareHouseService
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        List<base_wh_warehouse> GetWareHouseList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg);

        /// <summary>
        /// 修改 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        WareHouseResult Save(base_wh_warehouse model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        WareHouseResult Del(long? id);
    }
}
