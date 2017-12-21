using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.OrderQuery
{
    public class OrderQueryResult
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

    public class OrderQueryModel
    {
        public Int64 order_id { get; set; }
        public string custorder_code { get; set; }
        public string order_code { get; set; }
        public int? order_status { get; set; }
        public string order_statusE { get; set; }
        public decimal? order_sumqty { get; set; }
        public Decimal order_summoney { get; set; }
        public DateTime create_time { get; set; }
        public string create_timeE { get; set; }
        public string cus_name { get; set; }
        public string shop_name { get; set; }
        public string send_code { get; set; }
    }

    public class OrderQueryModelE
    {
        public string exp_code { get; set; }
        public string express_name { get; set; }
        public string order_code { get; set; }
        public Int64 order_id { get; set; }

        public List<OrderQueryModelEE> details { get; set; }

    }

    public class OrderQueryModelEE
    {
        public Decimal prod_num { get; set; }
        public Decimal stock_qty { get; set; }
        public string sku_code { get; set; }

        public int? purch_status { get; set; }
        public string purch_code { get; set; }
        public string purch_statusE { get; set; }
    }

}
