using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_wh_stock_inout
    {
        
        /// <summary>
        /// Desc:操作ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 oper_id {get;set;}

        /// <summary>
        /// Desc:库存ID 
        /// Default:(newid()) 
        /// Nullable:False 
        /// </summary>
        public Guid stock_id {get;set;}

        /// <summary>
        /// Desc:操作人姓名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string user_name {get;set;}

        /// <summary>
        /// Desc:1.上架;2.下架  
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int oper_type {get;set;}

        /// <summary>
        /// Desc:操作前数量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal pre_count {get;set;}

        /// <summary>
        /// Desc:操作数量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal oper_count {get;set;}

        /// <summary>
        /// Desc:操作后数量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal last_count {get;set;}

        /// <summary>
        /// Desc:操作人ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 user_id {get;set;}

        /// <summary>
        /// Desc:操作时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime add_time {get;set;}

    }
}
