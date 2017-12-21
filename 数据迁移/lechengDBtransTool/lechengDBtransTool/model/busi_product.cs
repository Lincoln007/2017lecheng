using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_product
    {
        
        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ProID {get;set;}

        /// <summary>
        /// Desc:所属商铺 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int ShopID {get;set;}

        /// <summary>
        /// Desc:品牌 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int BrandID {get;set;}

        /// <summary>
        /// Desc:类别 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int CID {get;set;}

        /// <summary>
        /// Desc:标题 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Name {get;set;}

        /// <summary>
        /// Desc:款号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Code {get;set;}

        /// <summary>
        /// Desc:是否上架 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Status {get;set;}

        /// <summary>
        /// Desc:参考价格（人民币） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal PriceR {get;set;}

        /// <summary>
        /// Desc:参考价格（美元） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal PriceD {get;set;}

        /// <summary>
        /// Desc:参考价格（日元） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal PriceJ {get;set;}

        /// <summary>
        /// Desc:商品性质（0未指定，1普货，2仿货） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Property {get;set;}

        /// <summary>
        /// Desc:配送类型（0未指定，1挂号，2平邮） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int ExType {get;set;}

        /// <summary>
        /// Desc:商品主图片地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PicUrl {get;set;}

        /// <summary>
        /// Desc:销售量7天 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int SaltNum7 {get;set;}

        /// <summary>
        /// Desc:销售量30天 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int SaltNum30 {get;set;}

        /// <summary>
        /// Desc:销售量 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int SaltNum {get;set;}

        /// <summary>
        /// Desc:英文标题 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string EnTitle {get;set;}

        /// <summary>
        /// Desc:日文标题 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JpTitle {get;set;}

        /// <summary>
        /// Desc:产品备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Remark {get;set;}

        /// <summary>
        /// Desc:商品库存数量 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int StockNum {get;set;}

        /// <summary>
        /// Desc:预估重量（克） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int YWeight {get;set;}

        /// <summary>
        /// Desc:实际重量（克） 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int SWeight {get;set;}

        /// <summary>
        /// Desc:供应商ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int SupplierID {get;set;}

        /// <summary>
        /// Desc:图片质量 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Quality {get;set;}

        /// <summary>
        /// Desc:产品链接 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Url {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PicLoc {get;set;}

        /// <summary>
        /// Desc:供应商关键字 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Keyword {get;set;}

        /// <summary>
        /// Desc:颜色/型号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Model {get;set;}

        /// <summary>
        /// Desc:尺码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Size {get;set;}

        /// <summary>
        /// Desc:报关编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BName {get;set;}

        /// <summary>
        /// Desc:报关编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BCode {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal BPrice {get;set;}

        /// <summary>
        /// Desc:标签 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Tag {get;set;}

        /// <summary>
        /// Desc:开发人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DevePeople {get;set;}

        /// <summary>
        /// Desc:是否删除 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean IsDel {get;set;}

        /// <summary>
        /// Desc:更新时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime UpdateTime {get;set;}

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
        public string Spec {get;set;}

    }
}
