using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_productsku
    {
        
        /// <summary>
        /// Desc:skuid 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int SkuID {get;set;}

        /// <summary>
        /// Desc:商品id 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int ProID {get;set;}

        /// <summary>
        /// Desc:商品编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Code {get;set;}

        /// <summary>
        /// Desc:sku库存数量 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Num {get;set;}

        /// <summary>
        /// Desc:参考价格（美元） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal PriceD {get;set;}

        /// <summary>
        /// Desc:参考价格（人民币） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal PriceR {get;set;}

        /// <summary>
        /// Desc:参考价格（日元） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal PriceJ {get;set;}

        /// <summary>
        /// Desc:预估重量 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int YWeight {get;set;}

        /// <summary>
        /// Desc:实际重量 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int SWeight {get;set;}

        /// <summary>
        /// Desc:采购均价 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal PurPrice {get;set;}

        /// <summary>
        /// Desc:销售数量 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int SaltNum {get;set;}

        /// <summary>
        /// Desc:销售状态（未上架，出售中，已下架） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Status {get;set;}

        /// <summary>
        /// Desc:预警数量 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Warning {get;set;}

        /// <summary>
        /// Desc:是否删除 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean IsDel {get;set;}

        /// <summary>
        /// Desc:添加时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime AppTime {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PicUrl {get;set;}

    }
}
