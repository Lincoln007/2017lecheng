using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class base_exp_comp
    {

        /// <summary>
        /// Desc:物流公司id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int express_id { get; set; }

        /// <summary>
        /// Desc:物流公司名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string express_name { get; set; }

        /// <summary>
        /// Desc:物流公司编码
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string express_coded { get; set; }


        /// <summary>
        /// Desc:物流公司描述 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string express_descrip { get; set; }

        /// <summary>
        /// Desc:国家表ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? country_id { get; set; }

        /// <summary>
        /// Desc:1.启用，0.停用  Isjpexpress
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean express_status { get; set; }

        /// <summary>
        /// Desc:1.是日本快递，0.不是 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean Isjpexpress { get; set; }
        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 create_user_id { get; set; }

        /// <summary>
        /// Desc:编辑时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime edit_time { get; set; }

        /// <summary>
        /// Desc:编辑用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 edit_user_id { get; set; }

        /// <summary>
        /// Desc:0:已删除;1:正常 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean del_flag { get; set; }

        /// <summary>
        /// Desc:删除时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? del_time { get; set; }

        /// <summary>
        /// Desc:删除用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 del_user_id { get; set; }

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark { get; set; }

    }
}
