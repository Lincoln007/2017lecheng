using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_shop
    {
        
        /// <summary>
        /// Desc:店铺id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 shop_id {get;set;}

        /// <summary>
        /// Desc:平台id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? platform_id {get;set;}

        /// <summary>
        /// Desc:所属国家id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? country_id {get;set;}

        /// <summary>
        /// Desc:店铺名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string shop_name {get;set;}

        /// <summary>
        /// Desc:店铺帐号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string shop_account {get;set;}

        /// <summary>
        /// Desc:取系统字典表 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 settlm_currency {get;set;}

        /// <summary>
        /// Desc:平台扣点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? platform_lrish {get;set;}

        /// <summary>
        /// Desc:1:可用;0:停用 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? shop_status {get;set;}

        /// <summary>
        /// Desc:店铺电话 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string shop_telephone {get;set;}

        /// <summary>
        /// Desc:店铺邮编 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string shop_zipcode {get;set;}

        /// <summary>
        /// Desc:店铺地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string shop_address {get;set;}

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
        /// Desc:导入csv文件的SKU存储列 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Csv_insert {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LStablename {get;set;}

    }
}
