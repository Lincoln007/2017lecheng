using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_iccard_scanin
    {
        
        /// <summary>
        /// Desc:ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 ic_card_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:身份证号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string icCard {get;set;}

        /// <summary>
        /// Desc:姓名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string name {get;set;}

        /// <summary>
        /// Desc:性别 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string sex {get;set;}

        /// <summary>
        /// Desc:籍贯 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string nation {get;set;}

        /// <summary>
        /// Desc:出生日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime born {get;set;}

        /// <summary>
        /// Desc:address 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string address {get;set;}

        /// <summary>
        /// Desc:发证机关 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string grantdept {get;set;}

        /// <summary>
        /// Desc:有效期开始日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime start {get;set;}

        /// <summary>
        /// Desc:有效期结束日期 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime end {get;set;}

        /// <summary>
        /// Desc:create_user_no 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 create_user_no {get;set;}

        /// <summary>
        /// Desc:create_user_name 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string create_user_name {get;set;}

        /// <summary>
        /// Desc:create_time 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime create_time {get;set;}

    }
}
