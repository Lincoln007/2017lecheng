using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_puch_freight
    {
        
        /// <summary>
        /// Desc:运费ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 freight_id {get;set;}

        /// <summary>
        /// Desc:采购单id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 purch_id {get;set;}

        /// <summary>
        /// Desc:供应商表ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 supp_id {get;set;}

        /// <summary>
        /// Desc:运费 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? freight {get;set;}

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
