using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_print
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int p_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string p_name {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? DepotID {get;set;}

        /// <summary>
        /// Desc:0.不在线,1.在线 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? isonline {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? Createtime {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int del_flag {get;set;}

    }
}
