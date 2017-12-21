using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_wh_warehouse
    {
        
        /// <summary>
        /// Desc:仓库ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 wh_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)表主键 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:仓库编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string wh_code {get;set;}

        /// <summary>
        /// Desc:仓库名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string wh_name {get;set;}

        /// <summary>
        /// Desc:仓库地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string wh_address {get;set;}

        /// <summary>
        /// Desc:仓库上限数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? wh_uplimit_qty {get;set;}

        /// <summary>
        /// Desc:仓库下限数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? wh_lowlimit_qty {get;set;}

        /// <summary>
        /// Desc:仓库预警数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? wh_alarm_qty {get;set;}

        /// <summary>
        /// Desc:1.正常;0.停用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? wh_status {get;set;}

        /// <summary>
        /// Desc:仓库联系电话 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string wh_phone {get;set;}

        /// <summary>
        /// Desc:仓库联系人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string wu_contacts {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? create_time {get;set;}

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 create_user_id {get;set;}

        /// <summary>
        /// Desc:编辑时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? edit_time {get;set;}

        /// <summary>
        /// Desc:编辑用户ID 
        /// Default:- 
        /// Nullable:True 
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
