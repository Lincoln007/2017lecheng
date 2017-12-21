using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace CommHelper
{
    public class CommonHelper
    {

        /// <summary>
        /// 按时间+随机4位数生成索赔单号
        /// </summary>
        public static string AutoClaimNo
        {
            get
            {
                Thread.Sleep(10);
                return "SP" + String.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + CreateRandomNum(4);//24
            }
        }
        /// <summary>
        /// 转移单编号生成规则
        /// </summary>
        public static string AutoZYNumber 
        {
            get 
            {
                Thread.Sleep(10);
                return "ZY" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + CreateRandomNum(2);//24
            }
        }

        /// <summary>
        /// 入库单编号生成规则
        /// </summary>
        public static string AutoRKNumber
        {
            get
            {
                Thread.Sleep(10);
                return "PK" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + CreateRandomNum(2);//24
            }
        }

        /// <summary>
        /// 退回单编号生成规则
        /// </summary>
        public static string AutoTHNumber
        {
            get
            {
                Thread.Sleep(10);
                return "TH" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + CreateRandomNum(2);//24
            }
        }

        /// <summary>
        /// 领用单编号生成规则
        /// </summary>
        public static string AutoLYNumber
        {
            get
            {
                Thread.Sleep(10);
                return "LY" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + CreateRandomNum(2);//24
            }
        }

        /// <summary>
        /// 维修单编号生成规则
        /// </summary>
        public static string AutoVXNumber
        {
            get
            {
                Thread.Sleep(10);
                return "VX" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + CreateRandomNum(2);//24
            }
        }

        /// <summary>
        /// 报废与变卖单编号生成规则
        /// </summary>
        public static string AutoBBNumber
        {
            get
            {
                Thread.Sleep(10);
                return "BB" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + CreateRandomNum(2);//24
            }
        }
        public static string AutoComplantsNo
        {
            get
            {
                Thread.Sleep(10);
                return "TS" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + CreateRandomNum(4);//24
            }
        }


        public static string CreateRandomNum(int num)//生成数字随机数
        {
            string a = "0123456789";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(1, a.Length - 1)]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (String.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (String.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            return ip;
        }


    }

    /// <summary>
    /// 操作成功返回对象
    /// </summary>
    public class SuccessFul
    {
        public string Data { get; set; }

        public string Success { get; set; }
    }

    /// <summary>
    /// 操作失败返回对象
    /// </summary>
    public class Failure
    {
        public string Msg { get; set; }

        public string Success { get; set; }
    }


}
