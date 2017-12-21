using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.DBmodel
{
    public class PrintSelect
    {
        public string packgecode { get; set; }
        public string skucode { get; set; }
        public string shopname { get; set; }
        public string ordercode { get; set; }
        public string depotname { get; set; } //默认全部金华仓
        public string location { get; set; }   //默认全部ZZ-ZZ-ZZ
        /// <summary>
        /// busi_workinfo中ID
        /// </summary>
        public long workid { get; set; }
        /// <summary>
        /// 是否配货
        /// </summary>
        public bool skuststus { get; set; }
    }
}
