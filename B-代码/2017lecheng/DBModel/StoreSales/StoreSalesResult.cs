using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.StoreSales
{
    public class StoreSalesResult
    {
        /// <summary>
        /// 返回的地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object DataResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 返回的信息
        /// </summary>
        public string Msg { get; set; }
    }


    public class StoreSalesModel
    {
        public string platform_name { get; set; }
        public string shop_name { get; set; }
        public Decimal sumshopmoney { get; set; }
        public Decimal sumplatformmoney { get; set; }
    }

}
