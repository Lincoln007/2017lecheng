using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_user_login_error
    {
        
        /// <summary>
        /// Desc:登录错误日志ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 login_error_id {get;set;}

        /// <summary>
        /// Desc:登录用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 user_id {get;set;}

        /// <summary>
        /// Desc:登录员工ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 emp_id {get;set;}

        /// <summary>
        /// Desc:登录IP 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string login_ip {get;set;}

        /// <summary>
        /// Desc:密码校验错误次数，超过5次设置10分钟的缓冲时间 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Byte check_times {get;set;}

        /// <summary>
        /// Desc:最近一次登录错误时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime login_date {get;set;}

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

    }
}
