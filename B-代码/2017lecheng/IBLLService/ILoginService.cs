using DBModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface ILoginService
    {
        string Getname();
        /// <summary>
        /// 登入用户验证
        /// </summary>
        /// <param name="net_no">网点</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="ischeck">是否没主库登入</param>
        /// <returns></returns>
        ComResult VerifyUserLogin(long net_no, string username, string password, bool ischeck);
    }
}
