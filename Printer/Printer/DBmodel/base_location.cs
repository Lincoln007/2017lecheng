using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_location
    {
        
        /// <summary>
        /// Desc:库位ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int locat_id {get;set;}

        /// <summary>
        /// Desc:仓库ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 wh_id {get;set;}

        /// <summary>
        /// Desc:库位号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string locat_code {get;set;}

        /// <summary>
        /// Desc:1.可用;0.停用 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean locat_status {get;set;}

        /// <summary>
        /// Desc:1.临时库位;2.普通库位 
        /// Default:((2)) 
        /// Nullable:False 
        /// </summary>
        public Byte locat_type {get;set;}

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
        /// Desc:1.有效;0.已删除 
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
