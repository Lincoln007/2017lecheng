using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_login_log
    {
        
        /// <summary>
        /// Desc:正确登录日志ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 login_log_id {get;set;}

        /// <summary>
        /// Desc:登录员工ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 emp_id {get;set;}

        /// <summary>
        /// Desc:登录用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 user_id {get;set;}

        /// <summary>
        /// Desc:登录IP 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string login_ip {get;set;}

        /// <summary>
        /// Desc:登录时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime login_time {get;set;}

        /// <summary>
        /// Desc:0.成功;1.失败 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Boolean is_success {get;set;}

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

    }
}
