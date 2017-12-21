using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.InternationalQuiry
{
    public class InternationalQuiryResult
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


    public class InternationalQuiryModel
    {
        public string tran_code { get; set; }
        public string express_code { get; set; }
        public DateTime create_time { get; set; }
        public Int64 tran_id { get; set; }
        public string express_name { get; set; }

        public string create_timeE { get; set; }

        public string express_coded { get; set; }
    }


    public class ExpressMode
    {
        public string EBusinessID { get; set; }
        public string ShipperCode { get; set; }
        public string Success { get; set; }
        public string LogisticCode { get; set; }
        public int State { get; set; }
        public List<ExpTraces> Traces { get; set; }
    }

    public class ExpTraces
    {
        public string AcceptTime { get; set; }
        public string AcceptStation { get; set; }
    }

}
