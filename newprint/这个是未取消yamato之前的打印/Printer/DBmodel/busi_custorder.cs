using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_custorder
    {
        
        /// <summary>
        /// Desc:订单id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 order_id {get;set;}

        /// <summary>
        /// Desc:平台id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int platform_id {get;set;}

        /// <summary>
        /// Desc:店铺id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 shop_id {get;set;}

        /// <summary>
        /// Desc:客户系统订单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string custorder_code {get;set;}

        /// <summary>
        /// Desc:系统唯一 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string order_code {get;set;}

        /// <summary>
        /// Desc:1.导入成功;10.已确认;11.已收货;20.已质检;30.已入库;40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? order_status {get;set;}

        /// <summary>
        /// Desc:单据日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? order_date {get;set;}

        /// <summary>
        /// Desc:预计发货日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? validity_day {get;set;}

        /// <summary>
        /// Desc:最迟发货日期(纳期) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? latest_date {get;set;}

        /// <summary>
        /// Desc:1:紧急;0:正常 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? storage_type {get;set;}

        /// <summary>
        /// Desc:订单总数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? order_sumqty {get;set;}

        /// <summary>
        /// Desc:订单总金额 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal order_summoney {get;set;}

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

    }
}
