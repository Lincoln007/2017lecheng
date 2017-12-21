using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.GoodsReceived
{
    public class GoodsReceivedResult
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
    }

    public class GoodsReceivedModel
    {
        public Int64 purch_id { get; set; }
        public string purch_code { get; set; }
        public string supp_name { get; set; }
        public string user_name { get; set; }
        public DateTime? purchase_time { get; set; }
        public int? purch_status { get; set; }
        public string purchase_timeE { get; set; }
        public string purch_statusE { get; set; }
        public Int64 express_id { get; set; }
        public string express_code { get; set; }
        public string express_name { get; set; }
        public int? purch_type { get; set; }
        public string purch_typeE { get; set; }
        public string OrderCode { get; set; }
        public string OrderCodeE { get; set; }
        public Boolean isLocked { get; set; }
        public string isLockedE { get; set; }
        public Int64 Locked_userid { get; set; }

    }

}
