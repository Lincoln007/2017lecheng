using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel.Order
{
    public class SendOrder
    {
        /// <summary>
        /// 平台订单号
        /// </summary>
        [Display(Name = "平台订单号")]
        public string custorder_code { get; set; }

        /// <summary>
        /// 包裹号
        /// </summary>
        [Display(Name = "包裹号")]
        public string order_code { get; set; }

        /// <summary>
        /// 物流公司ID
        /// </summary>

        public int express_id { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        [Display(Name = "物流公司名称")]
        public string express_name { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        [Display(Name = "物流单号")]
        public string exp_code { get; set; }
    }
}
