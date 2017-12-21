﻿using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_sys_net_config
    {
        
        /// <summary>
        /// Desc:ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 net_db_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)表ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:数据库配置表ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int db_conf_id {get;set;}

        /// <summary>
        /// Desc:网点(公司)与数据库关系状态,1.启用;0.停用 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean net_config_status {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
        /// Nullable:True 
        /// </summary>
        public DateTime? create_time {get;set;}

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Int64 create_user_id {get;set;}

        /// <summary>
        /// Desc:编辑时间 
        /// Default:(getdate()) 
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
        /// Desc:删除标记,1.有效;0.已删除(tbl_sys_db_config.del_type=1时有效) 
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

    }
}
