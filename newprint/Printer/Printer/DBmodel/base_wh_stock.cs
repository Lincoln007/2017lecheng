using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_wh_stock
    {
        
        /// <summary>
        /// Desc:库存ID 
        /// Default:(newid()) 
        /// Nullable:False 
        /// </summary>
        public Guid stock_id {get;set;}

        /// <summary>
        /// Desc:所属网点(公司)ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:货主ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 consignor_id {get;set;}

        /// <summary>
        /// Desc:供货方ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 supplier_id {get;set;}

        /// <summary>
        /// Desc:区域ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 area_id {get;set;}

        /// <summary>
        /// Desc:货架ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 location_id {get;set;}

        /// <summary>
        /// Desc:仓库ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 wh_id {get;set;}

        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 prod_id {get;set;}

        /// <summary>
        /// Desc:商品编码表ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 code_id {get;set;}

        /// <summary>
        /// Desc:资产类别ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 asset_class_id {get;set;}

        /// <summary>
        /// Desc:1.固定资产;2.物料资产;3.商品 
        /// Default:((1)) 
        /// Nullable:True 
        /// </summary>
        public Byte? stock_class {get;set;}

        /// <summary>
        /// Desc:库存分类为1或2时，该字段不能为空 
        /// Default:('0') 
        /// Nullable:True 
        /// </summary>
        public string stock_code {get;set;}

        /// <summary>
        /// Desc:库存条码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string stock_barcode {get;set;}

        /// <summary>
        /// Desc:使用年限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string service_life {get;set;}

        /// <summary>
        /// Desc:进货价 
        /// Default:((0)) 
        /// Nullable:True 
        /// </summary>
        public decimal? purchase_price {get;set;}

        /// <summary>
        /// Desc:库存预定数 
        /// Default:((0)) 
        /// Nullable:True 
        /// </summary>
        public decimal? reserve_qty {get;set;}

        /// <summary>
        /// Desc:库存数 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Decimal stock_qty {get;set;}

        /// <summary>
        /// Desc:库存占用数 
        /// Default:((0)) 
        /// Nullable:True 
        /// </summary>
        public decimal? occupied_qty {get;set;}

        /// <summary>
        /// Desc:库存锁定数 
        /// Default:((0)) 
        /// Nullable:True 
        /// </summary>
        public decimal? locking_qty {get;set;}

        /// <summary>
        /// Desc:托盘ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 pallet_id {get;set;}

        /// <summary>
        /// Desc:入库时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? storage_time {get;set;}

        /// <summary>
        /// Desc:最后出库时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? retrieval_time {get;set;}

        /// <summary>
        /// Desc:取字典表 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 using_state {get;set;}

        /// <summary>
        /// Desc:1:可用;0:停用 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean stock_status {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
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
        /// Default:(getdate()) 
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
        /// Desc:1.有效;0.已删除 
        /// Default:((1)) 
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

    }
}
