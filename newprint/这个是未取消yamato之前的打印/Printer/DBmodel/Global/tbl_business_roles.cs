using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_business_roles
    {
        
        /// <summary>
        /// Desc:用户角色ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 role_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:角色名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string role_name {get;set;}

        /// <summary>
        /// Desc:角色状态,1:正常;0:停用; 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Boolean roles_status {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 parent_id {get;set;}

        /// <summary>
        /// Desc:创建日期 
        /// Default:(getdate()) 
        /// Nullable:True 
        /// </summary>
        public DateTime? create_time {get;set;}

        /// <summary>
        /// Desc:操作员 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 create_user_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:(getdate()) 
        /// Nullable:True 
        /// </summary>
        public DateTime? edit_time {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 edit_user_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean del_flag {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? del_time {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 del_user_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark {get;set;}

    }
}
