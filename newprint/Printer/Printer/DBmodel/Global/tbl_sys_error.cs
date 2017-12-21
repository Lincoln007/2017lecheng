using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_sys_error
    {
        
        /// <summary>
        /// Desc:错误ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 err_id {get;set;}

        /// <summary>
        /// Desc:错误类型 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string err_type {get;set;}

        /// <summary>
        /// Desc:错误内容 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string err_content {get;set;}

        /// <summary>
        /// Desc:错误发生时间 
        /// Default:(getdate()) 
        /// Nullable:True 
        /// </summary>
        public DateTime? err_time {get;set;}

    }
}
