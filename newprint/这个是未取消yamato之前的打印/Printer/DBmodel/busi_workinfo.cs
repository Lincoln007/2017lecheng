using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_workinfo
    {
        
        /// <summary>
        /// Desc:作业ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 work_id {get;set;}

        /// <summary>
        /// Desc:客户订单表ID(方便配货使用) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 custorder_id {get;set;}

        /// <summary>
        /// Desc:客户订单明细id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 custorder_detail_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 packid {get;set;}

        /// <summary>
        /// Desc:发货单明细ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 sendorder_detail_id {get;set;}

        /// <summary>
        /// Desc:平台 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? plat_id {get;set;}

        /// <summary>
        /// Desc:店铺ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 shop_id {get;set;}

        /// <summary>
        /// Desc:仓库 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? wh_id {get;set;}

        /// <summary>
        /// Desc:区域 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? area_id {get;set;}

        /// <summary>
        /// Desc:货位 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? locat_id {get;set;}

        /// <summary>
        /// Desc:商品编码表ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 prod_code_id {get;set;}

        /// <summary>
        /// Desc:货物使用来源(使用库存1，正常采购2) 
        /// Default:((2)) 
        /// Nullable:False 
        /// </summary>
        public Byte detail_source {get;set;}

        /// <summary>
        /// Desc:是否已配(0,未配 1,已配) 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean is_work {get;set;}

        /// <summary>
        /// Desc:配货方式(1,网页配货，2手持配货，3网页批量，4库存配货) 
        /// Default:((2)) 
        /// Nullable:False 
        /// </summary>
        public Byte work_type {get;set;}

        /// <summary>
        /// Desc:配货时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? work_time {get;set;}

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
        /// Nullable:True 
        /// </summary>
        public DateTime? edit_time {get;set;}

        /// <summary>
        /// Desc:编辑用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 edit_user_id {get;set;}

        /// <summary>
        /// Desc:1.有效;0.已删除 
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
        public int? islock {get;set;}

    }
}
