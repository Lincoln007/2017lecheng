using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.OrderQuery_Shop
{
    public class OrderQuery_ShopResult
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


    public class OrderQuery_ShopModel
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

        public DateTime? latest_date { get; set; }
        public string latest_dateE { get; set; }
        public string send_code { get; set; }
    }


    public class OrderQuery_ShopModelE
    {
        public Int64 order_id { get; set; }

        public string order_code { get; set; }
        public int order_tatus { get; set; }
        public string order_tatusE { get; set; }
        public int express_id { get; set; }
        public string express_name { get; set; }
        public string exp_code { get; set; }
        public Int64 tran_id { get; set; }
        public int gjexpress_id { get; set; }
        public string gjexpress_name { get; set; }
        public string tran_code { get; set; }
        public string gjexpress_code { get; set; }
        public Boolean? is_print { get; set; }

        public string is_printE { get; set; }
        public DateTime? print_time { get; set; }
        public string print_timeE { get; set; }

        public List<OrderQuery_ShopModelEE> details { get; set; }
      

    }



    public class OrderQuery_ShopModelEE
    {
        public Int64 detail_id { get; set; }
        public string sku_code { get; set; }
        public Decimal prod_num { get; set; }

        public Int64 work_id { get; set; }

        public Boolean is_work { get; set; }

        public string is_workE { get; set; }

        public string purch_code { get; set; }

        public int purch_status { get; set; }

        public string purch_statusE { get; set; }
        public int work_type { get; set; }
        public string usedepot { get; set; }
    }

}
