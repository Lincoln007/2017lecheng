using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel
{
    public class ExpressPackgeModel
    {
        public int packid { get; set; }
        public DateTime? sendtime { get; set; }
        public string packgecode { get; set; }
        public string custname { get; set; }
        public string custaddress1 { get; set; }
        public string custaddress2 { get; set; }
        public string custphone { get; set; }
        public string custmobile { get; set; }
        public string custzip { get; set; }
        public string shopaddress { get; set; }
        public string shopzip { get; set; }

        public string shopname { get; set; }
        public string shopphone { get; set; }
        public string sku { get; set; }
        public string sku1 { get; set; }
        public string sku2 { get; set; }
        /// <summary>
        /// 发货类型
        /// </summary>
        public int sendtype { get; set; }
        /// <summary>
        /// 请求代号
        /// </summary>
        public string daihao { get; set; }
        /// <summary>
        /// 请求管理代号
        /// </summary>
        public string mangagedaihao { get; set; }
        /// <summary>
        /// 运送管理代号全部为1
        /// </summary>
        public string senddaihao { get; set; }
        /// <summary>
        /// 客户管理番号
        /// </summary>
        public string custmangagedaihao { get; set; }
    }
}
