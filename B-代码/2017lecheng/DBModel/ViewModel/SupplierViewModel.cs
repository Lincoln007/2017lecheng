using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class SupplierViewModel
    {
        public string ID { get; set; }
        public string supp_name { get; set; }
        public string supp_url { get; set; }
        public string supp_code { get; set; }
        public string CreateTime { get; set; }
        public string supp_remark { get; set; }
        /// <summary>
        /// 采购优先级
        /// </summary>
        public string purc_priority { get; set; }
    }
}
