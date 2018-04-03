using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer.DBmodel
{
    public class PackgePrintInfo
    {
        /// <summary>
        /// 快递类型
        /// </summary>
        public int expressid { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpCode { get; set; }
        /// <summary>
        /// 客户邮编
        /// </summary>
        public string zipcode { get; set; }
        /// <summary>
        /// 客户手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 客户名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 包裹SKU数目
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// sku
        /// </summary>
        public string skucode { get; set; }

        /// <summary>
        /// 单个SKU数目
        /// </summary>
        public int skunum { get; set; }
    }
}
