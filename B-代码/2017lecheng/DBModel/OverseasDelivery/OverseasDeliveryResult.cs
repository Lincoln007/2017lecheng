using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.OverseasDelivery
{
    public class OverseasDeliveryResult
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


    public class OverseasDeliveryModel
    {
        public string tran_code { get; set; }
        public string express_code { get; set; }
        public DateTime create_time { get; set; }
        public Int64 tran_id { get; set; }
        public string express_name { get; set; }
        public Byte? tran_status { get; set; }
        public string tran_statusE { get; set; }
        public string create_timeE { get; set; }

        public string express_coded { get; set; }
    }

}
