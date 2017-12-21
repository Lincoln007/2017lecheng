using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_org
    {
        
        /// <summary>
        /// Desc:组织ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 org_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)表主键 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:父节点组织 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 parent_org_id {get;set;}

        /// <summary>
        /// Desc:名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string org_name {get;set;}

        /// <summary>
        /// Desc:简称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string short_name {get;set;}

        /// <summary>
        /// Desc:全网唯一,例如YWSTO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string only_code {get;set;}

        /// <summary>
        /// Desc:1：公司，2：部门 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int org_type {get;set;}

        /// <summary>
        /// Desc:0：停用；1：正常 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int org_status {get;set;}

        /// <summary>
        /// Desc:组织地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string org_address {get;set;}

        /// <summary>
        /// Desc:组织权限路径 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string org_path {get;set;}

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
        /// Desc:1.有效;0.已删除 
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
