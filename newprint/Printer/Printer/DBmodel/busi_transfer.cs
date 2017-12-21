using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_transfer
    {
        
        /// <summary>
        /// Desc:转运ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 tran_id {get;set;}

        /// <summary>
        /// Desc:物流公司id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? express_id {get;set;}

        /// <summary>
        /// Desc:系统转运单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string tran_code {get;set;}

        /// <summary>
        /// Desc:转运单发货订单数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? tran_count {get;set;}

        /// <summary>
        /// Desc:1.未转运，2已转运 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? tran_status {get;set;}

        /// <summary>
        /// Desc:物流单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string express_code {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
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
        /// Default:(getdate()) 
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
        /// Default:((1)) 
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
