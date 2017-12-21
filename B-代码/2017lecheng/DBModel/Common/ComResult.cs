using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Common
{
    public class ComResult
    {
        /// <summary>
        /// 返回的数据
        /// </summary>
        public object DataResult { get; set; }

        /// <summary>
        /// 0失败1成功
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 返回的信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的地址
        /// </summary>
        public string URL { get; set; }
    }
}
