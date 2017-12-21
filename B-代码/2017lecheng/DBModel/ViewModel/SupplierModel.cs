using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class SupplierModel
    {

        /// <summary>
        /// Desc:ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 rel_id { get; set; }

        /// <summary>
        /// Desc:供应商id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int supp_id { get; set; }

        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 prod_id { get; set; }

        /// <summary>
        /// Desc:采购等级 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Byte? lev_purch { get; set; }

        

        public string suppliername { get; set; }
    }
}
