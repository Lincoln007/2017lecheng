using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_staff_role_menu
    {
        
        /// <summary>
        /// Desc:角色与菜单权限关系ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 role_menu_id {get;set;}

        /// <summary>
        /// Desc:用户角色ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 role_id {get;set;}

        /// <summary>
        /// Desc:菜单ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int menu_id {get;set;}

    }
}
