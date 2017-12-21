using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel.User
{
    public class SingUserPermissionModel
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public int menu_id { get; set; }

        /// <summary>
        /// 权限访问地址
        /// </summary>
        public string url_absolute_path { get; set; }
    }
}
