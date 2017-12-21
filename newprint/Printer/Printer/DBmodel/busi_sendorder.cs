using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_sendorder
    {
        
        /// <summary>
        /// Desc:发货订单id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 order_id {get;set;}

        /// <summary>
        /// Desc:转运表ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 tran_id {get;set;}

        /// <summary>
        /// Desc:客户订单ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 custorder_id {get;set;}

        /// <summary>
        /// Desc:发货编码(包裹号) 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string order_code {get;set;}

        /// <summary>
        /// Desc:物流公司表id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int express_id {get;set;}

        /// <summary>
        /// Desc:国外发货快递单号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string exp_code {get;set;}

        /// <summary>
        /// Desc:包裹总金额 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Decimal prod_money {get;set;}

        /// <summary>
        /// Desc:商品总数 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int prod_num {get;set;}

        /// <summary>
        /// Desc:40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int order_tatus {get;set;}

        /// <summary>
        /// Desc:1.是，0.否 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? is_pack {get;set;}

        /// <summary>
        /// Desc:打包时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime pack_datetime {get;set;}

        /// <summary>
        /// Desc:发货时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? send_datetime {get;set;}

        /// <summary>
        /// Desc:(是否导出拣选)0未导出，1 已导出 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? is_export {get;set;}

        /// <summary>
        /// Desc:快递单号是否已导回平台(1.是，0.否) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? is_csv_export {get;set;}

        /// <summary>
        /// Desc:是否打印面单(1.是，0.否) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Boolean? is_print {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? print_time {get;set;}

        /// <summary>
        /// Desc:收件人姓名 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_name {get;set;}

        /// <summary>
        /// Desc:收件人地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_address {get;set;}

        /// <summary>
        /// Desc:收件人手机号码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_mobile {get;set;}

        /// <summary>
        /// Desc:收件人电话 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_phone {get;set;}

        /// <summary>
        /// Desc:收件人邮编 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string receive_zip {get;set;}

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

    }
}
