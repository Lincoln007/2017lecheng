using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel.Order
{
    public class OrderSum
    {
        /// <summary>
        /// 平台ID
        /// </summary>
        public int platform_id { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        public string platform_name { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public long shop_id { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string shop_name { get; set; }

        /// <summary>
        /// 店铺订单总数
        /// </summary>
        public int OrderCount { get; set; }

        public DateTime? order_date { get; set; }

    }


    public class PurchaseInfo
    {
        public Decimal prod_num { get; set; }
        public decimal? price_cn { get; set; }
        public Int64 prod_id { get; set; }

        public Int64 supp_id { get; set; }
    }

}
