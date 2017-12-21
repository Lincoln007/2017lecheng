using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel.Order
{
    public class ShopOrder
    {

        /// <summary>
        /// 订单ID
        /// </summary>
        public long order_id { get; set; }
        /// <summary>
        /// 店铺ID
        /// </summary>
        public long shop_id { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string shop_name { get; set; }

        /// <summary>
        /// 平台订单号
        /// </summary>
        public string custorder_code { get; set; }
        /// <summary>
        /// 系统订单号
        /// </summary>
        public string order_code { get; set; }

        /// <summary>
        /// 订单总数量
        /// </summary>
        public int order_sumqty { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal order_summoney { get; set; }

        /// <summary>
        ///1.导入成功;10.已确认;11.已收货;20.已质检;30.已入库;40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库
        /// </summary>
        public int? order_status { get; set; }
    }
}
