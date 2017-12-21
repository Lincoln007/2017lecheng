using CommHelper;
using DBModel.DBmodel;
using DBModel.DBmodel.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Common
{
    public class LoginUser
    {
        public static User Current
        {
            get
            {
                //获取当前用户信息
                //用户是否登入
                string sessionID = CookieHelper.GetCookieValue("tockenid");
                object obj = SessionHelper.Get(sessionID);
                if (obj == null || obj=="")
                    return new User() 
                    {
                        user_id=4,
                        emp_no=1,
                        real_name="test"

                    };
                //通过cookie识别是否来自主库登录
                if (CookieHelper.GetCookieValue("ismainlogin") == "False")
                {
                    //从库
                    var userModel = JsonHelper.DeserializeJsonToObject<base_users>(obj.ToString()); 
                    var user = EntityViewModelConverter.ViewModelToEntity<base_users, User>(userModel);
                    return user;
                }
                else
                {
                    //主库
                    var userModel = JsonHelper.DeserializeJsonToObject<tbl_users>(obj.ToString());
                    var user = EntityViewModelConverter.ViewModelToEntity<tbl_users, User>(userModel);
                    return user;
                }
            }
        }

        /// <summary>
        /// 从缓存中获取链接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConstr()
        {
            //获取缓存中的链接字符串    "server=10.10.200.74;uid=sa;pwd=123!@#QAZ;database=Newlecheng";
            var obj = SessionHelper.Get(SystemConfig.connectionStringKey);
            if (obj == null||obj=="")
                return "server=127.0.0.1;uid=sa;pwd=jhqn89808980;database=20170717Newlecheng";
            return obj.ToString();
        }
    }
}
