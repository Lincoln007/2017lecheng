using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_users
    {
        
        /// <summary>
        /// Desc:用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 user_id {get;set;}

        /// <summary>
        /// Desc:员工ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 emp_id {get;set;}

        /// <summary>
        /// Desc:用户工号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 emp_no {get;set;}

        /// <summary>
        /// Desc:用户名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string user_name {get;set;}

        /// <summary>
        /// Desc:密码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string user_pwd {get;set;}

        /// <summary>
        /// Desc:网点(公司)表主键 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:员工所属组织 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 org_id {get;set;}

        /// <summary>
        /// Desc:加密种子 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string salt {get;set;}

        /// <summary>
        /// Desc:邮箱 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string email {get;set;}

        /// <summary>
        /// Desc:姓名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string real_name {get;set;}

        /// <summary>
        /// Desc:性别 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string sex {get;set;}

        /// <summary>
        /// Desc:座机 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string telphone {get;set;}

        /// <summary>
        /// Desc:手机 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string mobile {get;set;}

        /// <summary>
        /// Desc:0：账户不可用；1：账户可用 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Boolean user_status {get;set;}

        /// <summary>
        /// Desc:job_id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 job_id {get;set;}

        /// <summary>
        /// Desc:1：领导；2：成员 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? duty {get;set;}

        /// <summary>
        /// Desc:1：首要职务;2:领导的主管；3：成员的主管  可以多选 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? position {get;set;}

        /// <summary>
        /// Desc:操作人IP地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string op_ip {get;set;}

        /// <summary>
        /// Desc:启用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? open_time {get;set;}

        /// <summary>
        /// Desc:停用时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? disable_time {get;set;}

        /// <summary>
        /// Desc:单点登录CODE 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string token_code {get;set;}

        /// <summary>
        /// Desc:单点登录证书有效期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? token_expiry_time {get;set;}

        /// <summary>
        /// Desc:申通总部接口user_id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string sto_user_id {get;set;}

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
