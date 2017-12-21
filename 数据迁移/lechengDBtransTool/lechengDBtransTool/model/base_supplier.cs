using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_supplier
    {
        
        /// <summary>
        /// Desc:供应商id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int supp_id {get;set;}

        /// <summary>
        /// Desc:供应商编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string supp_code {get;set;}

        /// <summary>
        /// Desc:采购来源id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int purc_sourceid {get;set;}

        /// <summary>
        /// Desc:采购方式 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int purc_mode {get;set;}

        /// <summary>
        /// Desc:供应商名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string supp_name {get;set;}

        /// <summary>
        /// Desc:供应商网店地址 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string supp_url {get;set;}

        /// <summary>
        /// Desc:采购优先级 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? purc_priority {get;set;}

        /// <summary>
        /// Desc:1:是;0:否 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? is_grade {get;set;}

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
