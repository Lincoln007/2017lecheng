using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class ProductDetail
    {
        public base_product pro { get; set; }
        public List<base_prod_code> proskus { get; set; }
        public List<img_model> proimgs { get; set; }//
        public List<SupplierModel> prosupp { get; set; }
    }
}
