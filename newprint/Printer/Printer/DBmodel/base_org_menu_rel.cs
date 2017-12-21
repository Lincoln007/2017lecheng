using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_org_menu_rel
    {
        
        /// <summary>
        /// Desc:组织与菜单关系ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 org_menu_id {get;set;}

        /// <summary>
        /// Desc:组织ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 org_id {get;set;}

        /// <summary>
        /// Desc:菜单权限ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? menu_id {get;set;}

        /// <summary>
        /// Desc:菜单分组ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? menu_group_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 net_no {get;set;}

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

    }
}
