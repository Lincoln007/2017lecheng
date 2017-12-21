using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class LS_OrderDENA
    {
        
        /// <summary>
        /// Desc:OrderID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int OrderID {get;set;}

        /// <summary>
        /// Desc:店铺id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 shop_id {get;set;}

        /// <summary>
        /// Desc:客户名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Buyer {get;set;}

        /// <summary>
        /// Desc:客户电话 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string telephone {get;set;}

        /// <summary>
        /// Desc:客户手机 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string phone {get;set;}

        /// <summary>
        /// Desc:客户邮编 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string zip {get;set;}

        /// <summary>
        /// Desc:客户地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string address {get;set;}

        /// <summary>
        /// Desc:是否第一次导入 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? firstimport {get;set;}

        /// <summary>
        /// Desc:导入时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? ImportTime {get;set;}

        /// <summary>
        /// Desc:运费 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? Fee {get;set;}

        /// <summary>
        /// Desc:商品总价 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? totilMoney {get;set;}

        /// <summary>
        /// Desc:商品SKU信息 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SKU1 {get;set;}

        /// <summary>
        /// Desc:商品SKU信息2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SKU2 {get;set;}

        /// <summary>
        /// Desc:商品数量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? Num {get;set;}

        /// <summary>
        /// Desc:平台订单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string OrderNub {get;set;}

        /// <summary>
        /// Desc:乐诚订单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SysOrderNub {get;set;}

        /// <summary>
        /// Desc:纳期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? lasttime {get;set;}

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string beizhu {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? CustmerOrderTime {get;set;}

    }
}
