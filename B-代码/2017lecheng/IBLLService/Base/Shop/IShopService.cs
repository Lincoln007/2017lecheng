
﻿using DBModel.Base;
﻿using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.Base.Shop
{
    public interface IShopService
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        List<ShopResultModel> GetShopList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg);


        /// <summary>
        /// 修改 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ShopResult Save(base_shop model);
        bool UpdateShop(base_shop model);

        /// <summary>
        /// 获取下拉框信息
        /// </summary>
        /// <returns></returns>
        ShopResult GetPlatformList();

        /// <summary>
        /// 获取币种下拉框信息
        /// </summary>
        /// <returns></returns>
        ShopResult GetCountryList();



        ShopResult GetShopList();
        base_shop GetShopByID(int id);
    }
}
