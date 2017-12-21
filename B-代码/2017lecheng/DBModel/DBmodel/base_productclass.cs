using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_productclass
    {
        
        /// <summary>
        /// Desc:分类id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int prod_class_id {get;set;}

        /// <summary>
        /// Desc:父类别ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? parent_id {get;set;}

        /// <summary>
        /// Desc:分类名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string class_name {get;set;}

        /// <summary>
        /// Desc:图标 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string class_icon {get;set;}

        /// <summary>
        /// Desc:排序 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? class_sort {get;set;}

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
        /// Desc:0:已删除;1:正常 
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
