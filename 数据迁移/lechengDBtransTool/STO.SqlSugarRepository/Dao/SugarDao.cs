using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace SqlSugarRepository
{
    /// <summary>
    /// SqlSugar
    /// </summary>
    public class SugarDao
    {
        //禁止实例化
        private SugarDao()
        {

        }
        /// <summary>
        /// 全局连接字符串
        /// </summary>
        public static string ConnectionGloablString
        {
            get
            {
                string reval = "server=10.1.1.200;uid=sa;pwd=jhqn89808980;database=Newlecheng"; //这里可以动态根据cookies或session实现多库切换
                return reval;
            }
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string reval = "server=10.1.1.200;uid=sa;pwd=jhqn89808980;database=20170717Newlecheng"; //这里可以动态根据cookies或session实现多库切换server=10.10.200.74;uid=sa;pwd=123!@#QAZ;database=Newlecheng
                return reval;
            }
        }


        /// <summary>
        /// 链接数据库
        /// </summary>
        /// <returns></returns>
        public static ISqlSugarClient GetInstance(string constr)
        {
            var db = DbRepository.GetInstance(DbType.SqlServer, constr);
            db.IsEnableLogEvent = true;  //启用日志事件
            db.LogEventStarting = (sql, par) => 
            {
                Console.WriteLine(sql + " " + par + "\r\n"); //在这儿打段点可以查看生成的SQL语句
            };    
          
            db.LogEventCompleted = (sql, pars) => 
            {
                Console.WriteLine(sql + " " + pars + "\r\n"); //在这儿打段点可以查看生成的SQL语句
            }; 
            return db;
        }

        /// <summary>
        /// 返回链接数据库,默认使用ConnectionString
        /// </summary>
        /// <returns></returns>
        public static ISqlSugarClient GetInstance()
        {
            return GetInstance(ConnectionString);
        }

    }
}