using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_job
    {
        
        /// <summary>
        /// Desc:岗位ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 job_id {get;set;}

        /// <summary>
        /// Desc:所属网点(公司)ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:所属组织架构ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 org_id {get;set;}

        /// <summary>
        /// Desc:关联tbl_hr_Assessment表 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 asse_id {get;set;}

        /// <summary>
        /// Desc:岗位名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string job_name {get;set;}

        /// <summary>
        /// Desc:0：停用；1：正常 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? job_status {get;set;}

        /// <summary>
        /// Desc:上班时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public TimeSpan work_begin_time {get;set;}

        /// <summary>
        /// Desc:下班时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public TimeSpan work_end_time {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:- 
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
        /// Desc:1.有效;0.已删除 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? del_flag {get;set;}

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
