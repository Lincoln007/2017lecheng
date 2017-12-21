using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_product
    {
        
        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 prod_id {get;set;}

        /// <summary>
        /// Desc:分类id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? prod_class_id {get;set;}

        /// <summary>
        /// Desc:暂无 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? brand_id {get;set;}

        /// <summary>
        /// Desc:商品主图片地址 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string pic_url {get;set;}

        /// <summary>
        /// Desc:参考价格（美元） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? price_us {get;set;}

        /// <summary>
        /// Desc:参考价格（人民币） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? price_cn {get;set;}

        /// <summary>
        /// Desc:参考价格（日元） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? price_jp {get;set;}

        /// <summary>
        /// Desc:0未指定，1普货，2仿货 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int prod_property {get;set;}

        /// <summary>
        /// Desc:0未指定，1挂号，2平邮 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int ex_type {get;set;}

        /// <summary>
        /// Desc:是否上架（0待审核，1销售中，2回收站） 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int prod_status {get;set;}

        /// <summary>
        /// Desc:标题(商品名称) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_title {get;set;}

        /// <summary>
        /// Desc:英文标题 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string title_en {get;set;}

        /// <summary>
        /// Desc:日文标题 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string title_jp {get;set;}

        /// <summary>
        /// Desc:预估重量（克） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? pre_weight {get;set;}

        /// <summary>
        /// Desc:实际重量（克） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? real_weight {get;set;}

        /// <summary>
        /// Desc:款号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_style {get;set;}

        /// <summary>
        /// Desc:产品链接 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_url {get;set;}

        /// <summary>
        /// Desc:图片包地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_picpath {get;set;}

        /// <summary>
        /// Desc:开发人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_developer {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime create_time {get;set;}

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 create_user_id {get;set;}

        /// <summary>
        /// Desc:编辑时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime edit_time {get;set;}

        /// <summary>
        /// Desc:编辑用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 edit_user_id {get;set;}

        /// <summary>
        /// Desc:0:已删除;1:正常 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Boolean del_flag {get;set;}

        /// <summary>
        /// Desc:删除时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? del_time {get;set;}

        /// <summary>
        /// Desc:删除用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 del_user_id {get;set;}

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string model {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string size {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string bgcode {get;set;}

    }
}
