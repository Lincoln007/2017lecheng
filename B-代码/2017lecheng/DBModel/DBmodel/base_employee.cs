using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_employee
    {
        
        /// <summary>
        /// Desc:员工ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 emp_id {get;set;}

        /// <summary>
        /// Desc:员工工号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 emp_no {get;set;}

        /// <summary>
        /// Desc:员工姓名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string emp_name {get;set;}

        /// <summary>
        /// Desc:组织ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 org_id {get;set;}

        /// <summary>
        /// Desc:员工卡ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 ic_card_id {get;set;}

        /// <summary>
        /// Desc:岗位ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 job_id {get;set;}

        /// <summary>
        /// Desc:所属网点(公司)表ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:登记照地址 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string photo_url {get;set;}

        /// <summary>
        /// Desc:身份证号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string idcard_no {get;set;}

        /// <summary>
        /// Desc:对应字典表中:性别 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Byte emp_sex {get;set;}

        /// <summary>
        /// Desc:籍贯 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string emp_root {get;set;}

        /// <summary>
        /// Desc:联系电话 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string link_phone {get;set;}

        /// <summary>
        /// Desc:联系手机 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string link_mobile {get;set;}

        /// <summary>
        /// Desc:联系地址 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string link_address {get;set;}

        /// <summary>
        /// Desc:公司编码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 comp_no {get;set;}

        /// <summary>
        /// Desc:部门ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 dep_id {get;set;}

        /// <summary>
        /// Desc:对应字典中的银行 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string bank_name {get;set;}

        /// <summary>
        /// Desc:银行卡号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string bank_no {get;set;}

        /// <summary>
        /// Desc:入职日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? date_join {get;set;}

        /// <summary>
        /// Desc:转正日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? date_qualify {get;set;}

        /// <summary>
        /// Desc:离职日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? date_leave {get;set;}

        /// <summary>
        /// Desc:默认为0 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal salary_join {get;set;}

        /// <summary>
        /// Desc:转正工资 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal salary_qualify {get;set;}

        /// <summary>
        /// Desc:考核工资标准 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 standard_assess {get;set;}

        /// <summary>
        /// Desc:保密工资标准 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 standard_secrecy {get;set;}

        /// <summary>
        /// Desc:加班工资标准 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 standard__overtime {get;set;}

        /// <summary>
        /// Desc:从字典:员工状态中获取 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int emp_status {get;set;}

        /// <summary>
        /// Desc:是否在员工黑名单中 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Boolean in_blacklist {get;set;}

        /// <summary>
        /// Desc:例如校招等,从字典:员工来源 获取 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Byte emp_source {get;set;}

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

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark {get;set;}

    }
}
