using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_prod_code
    {
        
        /// <summary>
        /// Desc:code_id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 code_id {get;set;}

        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 prod_id {get;set;}

        /// <summary>
        /// Desc:sku编码(条码) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string sku_code {get;set;}

        /// <summary>
        /// Desc:jcode码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jcode {get;set;}

        /// <summary>
        /// Desc:ISBN码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string isbn_code {get;set;}

        /// <summary>
        /// Desc:商品条码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string bar_code {get;set;}

        /// <summary>
        /// Desc:颜色/型号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_model {get;set;}

        /// <summary>
        /// Desc:尺码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_size {get;set;}

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
