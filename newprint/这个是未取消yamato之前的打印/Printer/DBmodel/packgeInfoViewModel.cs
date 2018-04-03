using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer.DBmodel
{
    public class packgeInfoViewModel
    {
        /// <summary>
        /// Print_work中的主键
        /// </summary>
        public int workID { get; set; }

        /// <summary>
        /// 包裹号
        /// </summary>
        public string Packgecode { get; set; }

        /// <summary>
        /// 包裹SKU数量
        /// </summary>
        public string Number
        {
            get;
            set;
        }

        /// <summary>
        /// 纳期时间
        /// </summary>
        public string LastTime
        {
            get;
            set;
        }

        /// <summary>
        ///店铺名
        /// </summary>
        public string ShopName
        {
            get;
            set;
        }
    }
}
