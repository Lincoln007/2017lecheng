using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Base
{
    public class ShopResult
    {

        /// <summary>
        /// 返回的地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object DataResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 返回的信息
        /// </summary>
        public string Msg { get; set; }

        public long id { get; set; }


    }

    public class ShopResultModel
    {
        public Int64 shop_id { get; set; }
        public string platform_name { get; set; }
        public string shop_name { get; set; }
        public string shop_account { get; set; }
        public decimal? platform_lrish { get; set; }
        public DateTime create_time { get; set; }
        public int? country_id { get; set; }
        public Boolean? shop_status { get; set; }
        public string shop_statusE { get; set; }
        public string create_timeE { get; set; }
        public string country_name { get; set; }

    }

}
