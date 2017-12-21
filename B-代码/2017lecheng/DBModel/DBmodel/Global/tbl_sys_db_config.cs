using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_sys_db_config
    {
        
        /// <summary>
        /// Desc:客户数据库编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int db_conf_id {get;set;}

        /// <summary>
        /// Desc:客户数据库ip地址 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ip_address {get;set;}

        /// <summary>
        /// Desc:客户数据库名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string db_name {get;set;}

        /// <summary>
        /// Desc:客户数据库访问用户名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string login_name {get;set;}

        /// <summary>
        /// Desc:客户数据库访问密码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string login_pwd {get;set;}

        /// <summary>
        /// Desc:客户数据库访问端口 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string db_port {get;set;}

        /// <summary>
        /// Desc:客户数据删除类型,1.逻辑删除;2.物理删除;默认1 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Byte del_type {get;set;}

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
        /// Desc:删除标记,1.有效;0.已删除 
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
