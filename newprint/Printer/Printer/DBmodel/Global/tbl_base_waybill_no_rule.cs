using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_base_waybill_no_rule
    {
        
        /// <summary>
        /// Desc:单号规则ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 bill_rule_id {get;set;}

        /// <summary>
        /// Desc:所属网点(公司)编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:单号前缀 
        /// Default:('') 
        /// Nullable:False 
        /// </summary>
        public string bill_prefix {get;set;}

        /// <summary>
        /// Desc:后续数字位数 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int len_after {get;set;}

        /// <summary>
        /// Desc:单号总长度 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int len_whole {get;set;}

        /// <summary>
        /// Desc:默认收件员,若未菜鸟面单则自动设置为菜鸟面单 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 receive_user_no {get;set;}

        /// <summary>
        /// Desc:单据类型,0-申通面单 1-菜鸟面单 2-丰巢面单 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int bill_type {get;set;}

        /// <summary>
        /// Desc:是否允许录入到付款 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean is_arrive_money {get;set;}

        /// <summary>
        /// Desc:区域状态,1.启用;0.停用 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean bill_rule_status {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:(getdate()) 
        /// Nullable:True 
        /// </summary>
        public DateTime? create_time {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 create_user_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:(getdate()) 
        /// Nullable:True 
        /// </summary>
        public DateTime? edit_time {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 edit_user_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean del_flag {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? del_time {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 del_user_id {get;set;}

        /// <summary>
        /// Desc:说明 
        /// Default:('') 
        /// Nullable:True 
        /// </summary>
        public string remark {get;set;}

    }
}
