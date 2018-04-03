using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel.Global
{
    public class tbl_base_network_config
    {
        
        /// <summary>
        /// Desc:网点(公司)表主键 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 net_no {get;set;}

        /// <summary>
        /// Desc:支付宝商户号 
        /// Default:('') 
        /// Nullable:False 
        /// </summary>
        public string alipay_seller_id {get;set;}

        /// <summary>
        /// Desc:支付宝安全检验码 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public string alipay_key {get;set;}

        /// <summary>
        /// Desc:微信绑定支付的APPID 
        /// Default:('') 
        /// Nullable:False 
        /// </summary>
        public string weipay_appid {get;set;}

        /// <summary>
        /// Desc:微信-商户号 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public string weipay_mchid {get;set;}

        /// <summary>
        /// Desc:微信支付-商户支付密钥 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public string weipay_key {get;set;}

        /// <summary>
        /// Desc:公众帐号secert 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public string weipay_secert {get;set;}

        /// <summary>
        /// Desc:是否允许非绑定面单称重 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean ifNoBindWaybill {get;set;}

        /// <summary>
        /// Desc:可添加直营网点数量 
        /// Default:((10)) 
        /// Nullable:False 
        /// </summary>
        public int cnt_zhiying {get;set;}

        /// <summary>
        /// Desc:可添加承包网点数量 
        /// Default:((10)) 
        /// Nullable:False 
        /// </summary>
        public int cnt_chengbao {get;set;}

        /// <summary>
        /// Desc:默认管理员账号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 admin_user_no {get;set;}

        /// <summary>
        /// Desc:默认管理员密码 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string admin_user_pwd {get;set;}

        /// <summary>
        /// Desc:是否可用,默认为1,可用 
        /// Default:((1)) 
        /// Nullable:False 
        /// </summary>
        public Boolean ifEnable {get;set;}

        /// <summary>
        /// Desc:可添加中心网点数量 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int cnt_zhongxin {get;set;}

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
        /// Nullable:True 
        /// </summary>
        public DateTime? create_time {get;set;}

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:((0)) 
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

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark {get;set;}

    }
}
