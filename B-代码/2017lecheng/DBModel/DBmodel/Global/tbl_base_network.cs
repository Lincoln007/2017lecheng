using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_base_network
    {
        
        /// <summary>
        /// Desc:网点(公司)ID 
        /// Default:('') 
        /// Nullable:False 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:父网点(上级公司)ID 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Int64 net_pno {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:((0)) 
        /// Nullable:True 
        /// </summary>
        public Int64 region_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:((0)) 
        /// Nullable:True 
        /// </summary>
        public Int64 district_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)所属辖区 
        /// Default:((0)) 
        /// Nullable:True 
        /// </summary>
        public Int64 area_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)LogoID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 logo_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)名称 
        /// Default:('') 
        /// Nullable:False 
        /// </summary>
        public string net_name {get;set;}

        /// <summary>
        /// Desc:网点(公司)类型,1.网点(分公司);2.中心(总公司) 
        /// Default:((2)) 
        /// Nullable:False 
        /// </summary>
        public Byte net_type {get;set;}

        /// <summary>
        /// Desc:网点(公司)联系人姓名 
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string net_contact {get;set;}

        /// <summary>
        /// Desc:网点(公司)联系人手机 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string net_mobile {get;set;}

        /// <summary>
        /// Desc:网点(公司)座机 
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string net_phone {get;set;}

        /// <summary>
        /// Desc:网点(公司)电子邮件 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string email {get;set;}

        /// <summary>
        /// Desc:网点(公司)二级域名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string rr_value {get;set;}

        /// <summary>
        /// Desc:网点(公司)二级域名修改剩余次数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? rr_modify {get;set;}

        /// <summary>
        /// Desc:网点(公司)管理方式,管理方式 1.加盟，2.直营 
        /// Default:((1)) 
        /// Nullable:True 
        /// </summary>
        public Byte? net_style {get;set;}

        /// <summary>
        /// Desc:网点(公司)派送范围 
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string net_sendarea {get;set;}

        /// <summary>
        /// Desc:网点(公司)派送范围关键字 
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string net_sendarea_keywords {get;set;}

        /// <summary>
        /// Desc:网点(公司)不派送范围 
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string net_nosendarea {get;set;}

        /// <summary>
        /// Desc:网点(公司)不派送范围关键字 
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string net_nosendarea_keywords {get;set;}

        /// <summary>
        /// Desc:网点(公司)路线 
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string net_path {get;set;}

        /// <summary>
        /// Desc:网点(公司)状态,1.启用;0.停用 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean net_status {get;set;}

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
        /// Desc:删除标记,1.有效;2.已删除 
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
