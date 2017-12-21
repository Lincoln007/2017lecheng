using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_user_menu_rel
    {
        
        /// <summary>
        /// Desc:用户与菜单关系ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 user_menu_id {get;set;}

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
        /// Desc:用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 user_id {get;set;}

        /// <summary>
        /// Desc:角色菜单ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 role_menu_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)ID 
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
