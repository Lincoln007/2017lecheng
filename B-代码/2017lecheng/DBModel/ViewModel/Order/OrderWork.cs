using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel.Order
{
    public class OrderWork
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long proId { get; set; }
        /// <summary>
        /// 订单明细ID
        /// </summary>
        public long custorder_detail_id { get; set; }
        /// <summary>
        /// 发货订单id
        /// </summary>
        public long send_order_id { get; set; }

        /// <summary>
        /// 包裹号
        /// </summary>
        public string order_code { get; set; }

        /// <summary>
        /// 快递类型
        /// </summary>
        public int express_id { get; set; }

        /// <summary>
        /// 快递名称
        /// </summary>
        public string express_name { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string exp_code { get; set; }


        /// <summary>
        /// SKU
        /// </summary>
        public string sku_code { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int num { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int sotck_num { get; set; }


        /// <summary>
        /// 使用库存1，正常采购2
        /// </summary>
        public byte? detail_source { get; set; }

        /// <summary>
        ///仓库ID
        /// </summary>
        public int? wh_id { get; set; }

        /// <summary>
        /// Desc:收件人姓名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_name { get; set; }

        /// <summary>
        /// Desc:收件人地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_address { get; set; }

        /// <summary>
        /// Desc:收件人手机号码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_mobile { get; set; }

        /// <summary>
        /// Desc:收件人电话 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_phone { get; set; }

        /// <summary>
        /// Desc:收件人邮编 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_zip { get; set; }
    }


    public class OrderWorkE
    {
        public string exp_code { get; set; }
        public string express_name { get; set; }
        public string order_code { get; set; }
        public Int64 order_id { get; set; }

        public Int64 custorder_id { get; set; }

        public List<OrderWorkEE> details { get; set; }
    }


    public class OrderWorkEE
    {
        public Decimal prod_num { get; set; }
        public Decimal stock_qty { get; set; }
        public string sku_code { get; set; }
    }


}
