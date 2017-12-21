using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_purchase
    {
        
        /// <summary>
        /// Desc:采购单id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 purch_id {get;set;}

        /// <summary>
        /// Desc:采购单编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string purch_code {get;set;}

        /// <summary>
        /// Desc:采购商品种类数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? purch_categories {get;set;}

        /// <summary>
        /// Desc:采购总数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? purch_numb {get;set;}

        /// <summary>
        /// Desc:运费总金额 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? sum_freight {get;set;}

        /// <summary>
        /// Desc:货物总金额 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? sum_money {get;set;}

        /// <summary>
        /// Desc:合计总金额 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? purch_sum {get;set;}

        /// <summary>
        /// Desc:1.初始;2.已采购;3.已全部到货 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? purch_status {get;set;}

        /// <summary>
        /// Desc:采购备注说明 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string purch_remark {get;set;}

        /// <summary>
        /// Desc:异常处理备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string abnormal_remark {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
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
        /// Default:(getdate()) 
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
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark {get;set;}

    }
}
