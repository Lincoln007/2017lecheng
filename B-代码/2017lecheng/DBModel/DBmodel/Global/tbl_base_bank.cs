using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_base_bank
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string id {get;set;}

        /// <summary>
        /// Desc:银行编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string bankCode {get;set;}

        /// <summary>
        /// Desc:银行名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string bankName {get;set;}

        /// <summary>
        /// Desc:银行LOGO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string bankLogo {get;set;}

    }
}
