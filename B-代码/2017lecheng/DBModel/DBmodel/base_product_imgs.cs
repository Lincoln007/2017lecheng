using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_product_imgs
    {
        
        /// <summary>
        /// Desc:图片id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 img_id {get;set;}

        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 prod_id {get;set;}

        /// <summary>
        /// Desc:商品编码ID(商品编码表的ID) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 code_id {get;set;}

        /// <summary>
        /// Desc:按逗号分隔拆分成多行 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_model {get;set;}

        /// <summary>
        /// Desc:图片地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string pic_url {get;set;}

        /// <summary>
        /// Desc:描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string pic_describe {get;set;}

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
