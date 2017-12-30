using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Base
{
    public class StockResult
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

    public class StockModel
    {
        public Int64 code_id { get; set; }
        public int stock_qty { get; set; }
        public string sku_code { get; set; }

        public string wh_name { get; set; }

        public string prod_title { get; set; }

        public Guid stock_id { get; set; }
        public string locat_code { get; set; }

    }

}
