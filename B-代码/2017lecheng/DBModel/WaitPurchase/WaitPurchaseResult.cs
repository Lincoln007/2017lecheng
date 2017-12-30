using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.WaitPurchase
{
    public class WaitPurchaseResult
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
}
public class WaitPurchaseModel
{
    public Int64 purch_id { get; set; }
    public string purch_code { get; set; }
    public DateTime create_time { get; set; }
    public string emp_name { get; set; }
    public int? purch_status { get; set; }
    public string create_timeE { get; set; }
    public string purch_statusE { get; set; }
    public int? purch_type { get; set; }
    public string purch_typeE { get; set; }
    public string supp_name { get; set; }
    public Boolean isLocked { get; set; }
    public string isLockedE { get; set; }
    public Int64 Locked_userid { get; set; }
    public decimal? purnum { get; set; }

}


public class WaitPurchaseModelE
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
    public List<WaitPurchaseModelEE> details { get; set; }

}

public class WaitPurchaseModelEE
{

    public string shop_name { get; set; }

    public string order_code { get; set; }

    public Decimal prod_num { get; set; }

    public int order_tatus { get; set; }

    public string order_tatusE { get; set; }
}