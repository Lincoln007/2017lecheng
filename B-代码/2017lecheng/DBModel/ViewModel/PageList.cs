using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class PageList
    {
        /// <summary>
        /// 返回的对象集合
        /// </summary>
        public object objlist { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount { get; set; }

        /// <summary>
        /// 每页多少数据
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 总数据量
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

    }
}
