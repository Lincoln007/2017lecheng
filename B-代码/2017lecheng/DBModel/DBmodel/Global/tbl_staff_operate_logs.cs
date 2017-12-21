using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_staff_operate_logs
    {
        
        /// <summary>
        /// Desc:权限操作日志ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int staff_oplog_id {get;set;}

        /// <summary>
        /// Desc:操作标题 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string op_title {get;set;}

        /// <summary>
        /// Desc:操作内容 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string op_memo {get;set;}

        /// <summary>
        /// Desc:操作员 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 staff_id {get;set;}

        /// <summary>
        /// Desc:类型 0：添加 1：编辑 2：删除 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? op_type {get;set;}

        /// <summary>
        /// Desc:操作控制器 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string class_name {get;set;}

        /// <summary>
        /// Desc:操作地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string url_path {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? create_date {get;set;}

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 create_user_id {get;set;}

    }
}
