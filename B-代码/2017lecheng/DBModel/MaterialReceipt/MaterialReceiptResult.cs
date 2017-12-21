using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.MaterialReceipt
{
    public class MaterialReceiptResult
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


    public class MaterialReceiptModel
    {
        public Int64 purch_id { get; set; }
        public string purch_code { get; set; }
        public DateTime? purchase_time { get; set; }
        public string emp_name { get; set; }
        public int? purch_status { get; set; }
        public string purchase_timeE { get; set; }
        public string purch_statusE { get; set; }
        public string express_coded { get; set; }
        public string express_code { get; set; }
        public string express_name { get; set; }
        public int? purch_type { get; set; }
        public string purch_typeE { get; set; }
        public string supp_name { get; set; }
        public string OrderCode { get; set; }

        public string OrderCodeE { get; set; }

        public Boolean isLocked { get; set; }
        public string isLockedE { get; set; }
        public Int64 Locked_userid { get; set; }

        public Int64 express_id { get; set; }
    }

    public class MaterialReceiptModelE
    {
        public string supp_url { get; set; }
        public string sku_code { get; set; }
        public string supp_name { get; set; }
        public string prod_url { get; set; }
        public decimal? purch_count { get; set; }
        public string pic_url { get; set; }
        public Int64 code_id { get; set; }
        public Int64 supp_id { get; set; }
        public Int64 prod_id { get; set; }
        public string prod_title { get; set; }

        public List<MaterialReceiptModelEE> details { get; set; }
    }


    public class MaterialReceiptModelEE
    {
        public string shop_name { get; set; }

        public string order_code { get; set; }

        public Decimal prod_num { get; set; }

        public int order_tatus { get; set; }

        public string order_tatusE { get; set; }
    }



    public class MaterialReceiptSaveModel
    {
        public Int64 code_id { get; set; }
        public Int64 prod_id { get; set; }
        public Decimal prod_num { get; set; }
        public Int64 supp_id { get; set; }
    }

}
