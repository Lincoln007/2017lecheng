using CommHelper;
using DBModel.Common;
using DBModel.DBmodel.Global;
using DBModel.DBmodel;
using IBLLService;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices
{
    public class LoginService : ILoginService
    {
        public string Getname()
        {
            return "benci";
        }

        /// <summary>
        /// 登入用户验证
        /// </summary>
        /// <param name="net_no">网点</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="ischeck">是否没主库登入</param>
        /// <returns></returns>
        public ComResult VerifyUserLogin(long net_no, string username, string password, bool ischeck)
        {
            ComResult result = new ComResult();
            //用户是否访问的为主库
            if (ischeck)
            {
                //访问主库
                return ZKlogin(new tbl_users() { user_pwd = password, user_name = username, net_no = net_no });
            }
            else
            {
                //访问从库

                //通过网点查询数据库信息
                var dbinfo = new tbl_sys_db_config();
                try
                {
                    using (var db = SugarDao.GetInstance(SugarDao.ConnectionGloablString))
                    {
                        dbinfo = db.Queryable<tbl_sys_db_config>()
                            .JoinTable<tbl_sys_net_config>((s1, s2) => s1.db_conf_id == s2.db_conf_id, JoinType.Inner)
                            .Where<tbl_sys_net_config>((s1, s2) => s2.net_no == net_no).Select("s1.*").SingleOrDefault();
                    }
                }
                catch (Exception e)
                {
                    result.State = 0;
                    result.Msg = e.Message;
                    return result;
                }
                //判断网点是否存在
                if (dbinfo == null || dbinfo.db_name == "")
                {
                    result.State = 0;
                    result.Msg = "公司网点不存在";
                    return result;
                }
                else
                {
                    //获取从库的信息  EncodeHepler.DecDataBasePass(dbinfo.login_pwd, SystemConfig.encDataBasePassword)
                    var connetionstr = "server=" + dbinfo.ip_address + ";uid=" + dbinfo.login_name + ";pwd=" + dbinfo.login_pwd + ";database=" + dbinfo.db_name + "";
                    //将从库链接字符串记录缓存中
                    SessionHelper.Add(SystemConfig.connectionStringKey, connetionstr, SystemConfig.loginExpireTime);
                    //从库登录
                    return CKlogin(new base_users() { user_pwd = password, user_name = username, net_no = net_no }, connetionstr);
                }
            }
        }

        #region 主库登入
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private ComResult ZKlogin(tbl_users userModel)
        {
            using (var db = SugarDao.GetInstance(SugarDao.ConnectionGloablString))
            {
                ComResult result = new ComResult();
                //验证公司网点
                var singlenet_no = db.Queryable<tbl_users>().SingleOrDefault(c => c.net_no == userModel.net_no);
                if (singlenet_no == null)
                {
                    result.State = 0;
                    result.Msg = "公司网点不存在";

                    return result;
                }
                //验证用户名和密码
                var singleuser = db.Queryable<tbl_users>().SingleOrDefault(c => c.user_name == userModel.user_name);
                if (singleuser == null)
                {
                    result.State = 0;
                    result.Msg = "用户不存在";

                    return result;
                }
                //密码校验错误次数，超过5次设置10分钟的缓冲时间
                //记录登录错误日志
                var reeoruser = db.Queryable<tbl_user_login_error>().SingleOrDefault(c => c.user_id == singleuser.user_id);
                if (reeoruser != null && reeoruser.check_times > 0 && reeoruser.check_times % 5 == 0 && DateTime.Now <= reeoruser.login_date.AddMinutes(10))
                {
                    result.State = 0;
                    result.Msg = "密码校验错误次数超过5次，请10分钟后尝试";

                    return result;
                }
                var usermodel = db.Queryable<tbl_users>().SingleOrDefault(c => c.user_name == userModel.user_name && c.user_pwd == userModel.user_pwd);
                if (usermodel == null)
                {
                    if (reeoruser == null)
                    {
                        db.Insert<tbl_user_login_error>(new tbl_user_login_error() { user_id = singleuser.user_id, check_times = 1, login_date = DateTime.Now });
                    }
                    else
                    {
                        db.Update<tbl_user_login_error>(new tbl_user_login_error() { user_id = singleuser.user_id, check_times = Convert.ToByte(reeoruser.check_times + 1), login_date = DateTime.Now, login_error_id = reeoruser.login_error_id });
                    }
                    //记录登录日志
                    db.Insert<tbl_user_login_log>(insertlog(singleuser.emp_no, false));
                    result.State = 0;
                    result.Msg = "密码错误";

                    return result;
                }
                if (!usermodel.user_status)
                {
                    //记录登录日志
                    db.Insert<tbl_user_login_log>(insertlog(singleuser.emp_no, false));
                    result.State = 0;
                    result.Msg = "该账号不可用，请联系管理员";

                    return result;
                }
                //记录登录日志
                //用户登录成功清除登录错误次数
                if (reeoruser != null)
                    db.Update<tbl_user_login_error>(new tbl_user_login_error() { user_id = singleuser.user_id, check_times = 0, login_date = DateTime.Now, login_error_id = reeoruser.login_error_id });
                db.Insert<tbl_user_login_log>(insertlog(singleuser.user_id, true));
                result.State = 1;
                result.DataResult = usermodel;
                //将登入状态存入缓存
                SessionHelper.Add(CookieHelper.GetCookieValue("tockenid"), JsonHelper.SerializeObject(usermodel), SystemConfig.loginExpireTime);
                return result;
            }
        }

        /// <summary>
        /// 登入日志
        /// </summary>
        /// <param name="_user_id"></param>
        /// <param name="_is_success"></param>
        /// <returns></returns>
        private static tbl_user_login_log insertlog(long _user_id, bool _is_success)
        {
            tbl_user_login_log log = new tbl_user_login_log()
            {
                user_id = _user_id,
                login_ip = CommonHelper.GetIp(),
                login_time = DateTime.Now,
                is_success = _is_success

            };
            return log;
        }

        #endregion


        #region 从库登入
        /// <summary>
        /// 从库登入
        /// </summary>
        /// <param name="user"></param>
        /// <param name="connectionStr">从库数据库链接字符串</param>
        /// <returns></returns>
        private ComResult CKlogin(base_users userModel, string connectionStr)
        {
            ComResult result = new ComResult();
            try
            {
                using (var db = SugarDao.GetInstance(connectionStr))
                {

                    //验证公司网点
                    var singlenet_no = db.Queryable<base_users>().SingleOrDefault(c => c.net_no == userModel.net_no && c.user_name == userModel.user_name);
                    if (singlenet_no == null)
                    {
                        result.State = 0;
                        result.Msg = "公司网点与用户不匹配";

                        return result;
                    }
                    //验证用户名和密码
                    var singleuser = db.Queryable<base_users>().SingleOrDefault(c => c.user_name == userModel.user_name);
                    if (singleuser == null)
                    {
                        result.State = 0;
                        result.Msg = "用户不存在";

                        return result;
                    }
                    //密码校验错误次数，超过5次设置10分钟的缓冲时间
                    //记录登录错误日志
                    var reeoruser = db.Queryable<base_login_error>().SingleOrDefault(c => c.user_id == singleuser.user_id);
                    if (reeoruser != null && reeoruser.check_times > 0 && reeoruser.check_times % 5 == 0 && DateTime.Now <= reeoruser.login_date.AddMinutes(10))
                    {
                        result.State = 0;
                        result.Msg = "密码校验错误次数超过5次，请10分钟后尝试";
                        return result;
                    }
                    var usermodel = db.Queryable<base_users>().SingleOrDefault(c => c.user_name == userModel.user_name && c.user_pwd == userModel.user_pwd);
                    if (usermodel == null)
                    {
                        if (reeoruser == null)
                        {
                            db.Insert<base_login_error>(new base_login_error() { user_id = singleuser.user_id, check_times = 1, login_date = DateTime.Now, create_time = DateTime.Now });
                        }
                        else
                        {
                            db.Update<base_login_error>(new base_login_error() { user_id = singleuser.user_id, check_times = (byte)(reeoruser.check_times + 1), login_date = DateTime.Now, login_error_id = reeoruser.login_error_id, create_time = DateTime.Now });
                        }
                        //记录登录日志
                        db.Insert<base_login_log>(Ckinsertlog(singleuser.user_id, false, singleuser.emp_id));
                        result.State = 0;
                        result.Msg = "密码错误";

                        return result;
                    }
                    if (!usermodel.user_status)
                    {
                        //记录登录日志
                        db.Insert<base_login_log>(Ckinsertlog(singleuser.user_id, false, singleuser.emp_id));
                        result.State = 0;
                        result.Msg = "该账号不可用，请联系管理员";

                        return result;
                    }
                    //记录登录日志
                    //用户登录成功清除登录错误次数
                    if (reeoruser != null)
                        db.Update<base_login_error>(new base_login_error() { user_id = singleuser.user_id, check_times = 0, login_date = DateTime.Now, login_error_id = reeoruser.login_error_id, create_time = DateTime.Now });
                    db.Insert<base_login_log>(Ckinsertlog(singleuser.user_id, true, singleuser.emp_id));
                    result.State = 1;
                    result.DataResult = usermodel;
                    //将登入状态存入缓存
                    SessionHelper.Add(CookieHelper.GetCookieValue("tockenid"), JsonHelper.SerializeObject(usermodel), SystemConfig.loginExpireTime);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.State = 0;
                result.Msg = "系统异常" + ex.Message;
                return result;
                throw;
            }
        }

        /// <summary>
        /// 登入日志
        /// </summary>
        /// <param name="_user_id"></param>
        /// <param name="_is_success"></param>
        /// <returns></returns>
        private static base_login_log Ckinsertlog(long _user_id, bool _is_success, long empId)
        {
            base_login_log log = new base_login_log()
            {
                user_id = _user_id,
                login_ip = CommonHelper.GetIp(),
                login_time = DateTime.Now,
                is_success = _is_success,
                create_time = DateTime.Now,
                emp_id = empId

            };
            return log;
        }

        #endregion
    }
}
