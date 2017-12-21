using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.NotPicking
{
    public class NotPickingResult
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

    public class NotPickingModel
    {
        public int num { get; set; }
        public Int64 order_id { get; set; }
        public string order_code { get; set; }
        public Int64 code_id { get; set; }
        public string sku_code { get; set; }
        public Boolean is_work { get; set; }
        public string cus_name { get; set; }
        public Int64 shop_id { get; set; }
        public string shop_name { get; set; }
    }
    public class NotPickingModelNO
    {
        public string no_sku_code { get; set; }
        public int nopeinum { get; set; }
    }

    public class NotPickingModelYes
    {
        public string yes_sku_code { get; set; }
        public int peinum { get; set; }
        public int peinumcount { get; set; }
    }


    public class NotPickingModelE
    {
        public string order_code { get; set; }
        public string emp_name { get; set; }
        public string shop_name { get; set; }
        public List<NotPickingModelNO> nopei { get; set; }
        public List<NotPickingModelYes> pei { get; set; }
        public int peinumsum { get; set; }
        public int sum { get; set; }
    }


}
