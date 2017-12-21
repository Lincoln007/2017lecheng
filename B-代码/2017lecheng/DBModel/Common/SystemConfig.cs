using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Common
{
    public static class SystemConfig
    {
        /// <summary>
        ///数据库链接字符串缓存KEY
        /// </summary>
        public static string connectionStringKey = "connectionStringKey";
        /// <summary>
        /// 数据库加密Key
        /// </summary>
        public static string encConStrKey = "76b77003-21e0-490e-90a9-991600e093e6";
        /// <summary>
        /// 数据库密码加密Key
        /// </summary>
        public static string encDataBasePassword = "c16b6784-19db-4e95-b51e-0ed60f36044d";
        /// <summary>
        /// 登录的密码的加密Key
        /// </summary>
        public static string encLoginKey = "ca07cade-a3c6-49f8-8730-2766f0eb70c3";
        /// <summary>
        /// 登录过期时间（分钟）
        /// </summary>
        public static int loginExpireTime = 1200;

        /// <summary>
        /// 登录过期时间（分钟）
        /// </summary>
        public static int loginCookieExpireTime = 1440;
    }
}
