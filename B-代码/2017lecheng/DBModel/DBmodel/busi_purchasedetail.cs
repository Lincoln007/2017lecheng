using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_purchasedetail
    {

        /// <summary>
        /// Desc:明细ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 detail_id { get; set; }

        /// <summary>
        /// Desc:发货明细ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 send_detail_id { get; set; }

        /// <summary>
        /// Desc:客户订单明细ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 custorderdetail_id { get; set; }


        /// <summary>
        /// Desc:店铺表ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 shop_id { get; set; }

        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 prod_id { get; set; }

        /// <summary>
        /// Desc:商品编码表ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 code_id { get; set; }

        /// <summary>
        /// Desc:订单采购1,库存采购2 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Byte purch_type { get; set; }

        /// <summary>
        /// Desc:待采购1,已采购2,待收货3,已收货4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? purch_status { get; set; }

        /// <summary>
        /// Desc:仓库ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 wh_id { get; set; }

        /// <summary>
        /// Desc:采购单ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 purch_id { get; set; }

        /// <summary>
        /// Desc:供应商表ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 supp_id { get; set; }

        /// <summary>
        /// Desc:采购链接 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string purch_url { get; set; }

        /// <summary>
        /// Desc:采购单价 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? purch_rice { get; set; }

        /// <summary>
        /// Desc:采购数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? purch_count { get; set; }

        /// <summary>
        /// Desc:缺货数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? lack_count { get; set; }

        /// <summary>
        /// Desc:发错数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? err_count { get; set; }

        /// <summary>
        /// Desc:1.是，0.否 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean is_cancel { get; set; }

        /// <summary>
        /// Desc:供货商发货运单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string cust_send_billcode { get; set; }

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 create_user_id { get; set; }

        /// <summary>
        /// Desc:编辑时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime edit_time { get; set; }

        /// <summary>
        /// Desc:编辑用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 edit_user_id { get; set; }

        /// <summary>
        /// Desc:0:已删除;1:正常 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean del_flag { get; set; }

        /// <summary>
        /// Desc:删除时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? del_time { get; set; }

        /// <summary>
        /// Desc:删除用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 del_user_id { get; set; }

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark { get; set; }

    }
}
