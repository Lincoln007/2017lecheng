using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class base_LSorder
    {

        /// <summary>
        /// Desc:OrderID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int OrderID { get; set; }


        /// <summary>
        /// Desc:店铺名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public long shopID { get; set; }

        /// <summary>
        /// Desc:客户名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Buyer { get; set; }

        /// <summary>
        /// Desc:客户电话 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// Desc:客户手机 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Desc:客户邮编 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string zip { get; set; }

        /// <summary>
        /// Desc:客户地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string address { get; set; }

       

      

      

    
        /// <summary>
        /// Desc:商品SKU信息 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SKU { get; set; }

    

        /// <summary>
        /// Desc:商品数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? Num { get; set; }

        /// <summary>
        /// Desc:平台订单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string OrderNub { get; set; }

        /// <summary>
        /// Desc:乐诚订单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SysOrderNub { get; set; }

       
    }
}
