using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel.User
{
    public class UserMenu
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public int? menu_group_id { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string menu_group_name { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int? menu_id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string menu_name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public long emp_no { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long userId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool user_status { get; set; }
    }
}
