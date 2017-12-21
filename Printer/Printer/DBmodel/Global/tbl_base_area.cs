using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_base_area
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 area_id {get;set;}

        /// <summary>
        /// Desc:父级别 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Int64 parent_area_id {get;set;}

        /// <summary>
        /// Desc:辖区名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string area_name {get;set;}

        /// <summary>
        /// Desc:辖区类型 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int area_type {get;set;}

        /// <summary>
        /// Desc:辖区路径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string area_path {get;set;}

        /// <summary>
        /// Desc:状态：0：停用；1：正常 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean area_status {get;set;}

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
        /// Desc:删除标记 
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
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string remark {get;set;}

    }
}
