using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_staff_menus
    {
        
        /// <summary>
        /// Desc:菜单权限ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int menu_id {get;set;}

        /// <summary>
        /// Desc:菜单组自增编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int menugroup_id {get;set;}

        /// <summary>
        /// Desc:菜单项名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string menu_name {get;set;}

        /// <summary>
        /// Desc:访问地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string url_absolute_path {get;set;}

        /// <summary>
        /// Desc:菜单排序 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int menu_sort {get;set;}

        /// <summary>
        /// Desc:菜单icon 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string menu_icon {get;set;}

        /// <summary>
        /// Desc:菜单状态,0：停用；1：正常 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean menus_status {get;set;}

        /// <summary>
        /// Desc:用户表的主键 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 user_id {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
        /// Nullable:True 
        /// </summary>
        public DateTime? create_time {get;set;}

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:True 
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
        /// Desc:删除标记,1.有效;0.已删除(tbl_sys_db_config.del_type=1时有效) 
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
