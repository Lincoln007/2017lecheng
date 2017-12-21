using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_country
    {
        
        /// <summary>
        /// Desc:国家id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int country_id {get;set;}

        /// <summary>
        /// Desc:中文国家名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string cn_name {get;set;}

        /// <summary>
        /// Desc:英文国家名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string en_name {get;set;}

        /// <summary>
        /// Desc:取系统字典表 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 money_type {get;set;}

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
