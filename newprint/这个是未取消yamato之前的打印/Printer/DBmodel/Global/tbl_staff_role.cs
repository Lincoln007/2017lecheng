using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_staff_role
    {
        
        /// <summary>
        /// Desc:用户与角色关系ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 staff_id {get;set;}

        /// <summary>
        /// Desc:用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 user_id {get;set;}

        /// <summary>
        /// Desc:用户角色ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 role_id {get;set;}

    }
}
