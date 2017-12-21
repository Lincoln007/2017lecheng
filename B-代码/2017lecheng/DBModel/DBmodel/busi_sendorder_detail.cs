using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_sendorder_detail
    {
        
        /// <summary>
        /// Desc:明细id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 detail_id {get;set;}

        /// <summary>
        /// Desc:发货订单id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 order_id {get;set;}

        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 prod_id {get;set;}

        /// <summary>
        /// Desc:商品名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_name {get;set;}

        /// <summary>
        /// Desc:商品编码ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 code_id {get;set;}

        /// <summary>
        /// Desc:商品发货数量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal prod_num {get;set;}

        /// <summary>
        /// Desc:0.初始;1.作业中;2.已完成 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? detail_status {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime create_time {get;set;}

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 create_user_id {get;set;}

        /// <summary>
        /// Desc:编辑时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime edit_time {get;set;}

        /// <summary>
        /// Desc:编辑用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 edit_user_id {get;set;}

        /// <summary>
        /// Desc:0:已删除;1:正常 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Boolean del_flag {get;set;}

        /// <summary>
        /// Desc:删除时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? del_time {get;set;}

        /// <summary>
        /// Desc:删除用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 del_user_id {get;set;}

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark {get;set;}



        /// <summary>
        /// Desc:商品单价 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? prod_price { get; set; }

        /// <summary>
        /// Desc:商品图片地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string pic_url { get; set; }

        /// <summary>
        /// Desc:商品重量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? prod_weight { get; set; }

    }
}
