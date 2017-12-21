using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class RFTDistrView
    {
        /// <summary>
        /// 包裹已配SKU数量
        /// </summary>
        public int Ispackpacknum { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string ordercode { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int orderid { get; set; }
        /// <summary>
        /// sku
        /// </summary>
        public string skucode { get; set; }
        /// <summary>
        /// 包裹号
        /// </summary>
        public string packgecode { get; set; }
        /// <summary>
        /// 包裹SKU总数
        /// </summary>
        public int packnum { get; set; }
   
        /// <summary>
        /// 店铺名
        /// </summary>
        public string shopname { get; set; }
        /// <summary>
        /// 单件多件信息
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// workinfoID  
        /// </summary>
        public int workid { get; set; }
        /// <summary>
        /// 订单中包裹数目
        /// </summary>
        public int packges { get; set; }
        /// <summary>
        /// 包裹ID
        /// </summary>
        public int packid { get; set; }
    }
}
