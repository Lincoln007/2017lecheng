using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class base_NPSKU
    {
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// Desc:店铺ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// Desc:平台SKU 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PSKU { get; set; }

        /// <summary>
        /// Desc:系统SKU 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SSKU { get; set; }

        /// <summary>
        /// Desc:过滤SKU 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string GSKU { get; set; }
    }
}
