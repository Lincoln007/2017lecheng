using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface IUserService
    {
        /// <summary>
        /// 添加或者更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="state">0代表修改密码1代表删除用户2代表禁用启用用户3代表添加</param>
        /// <returns></returns>
        ComResult AddOrUpdateUserInfo(base_users model, int state);

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        List<base_users> GetUserList(int pageIndex, int pageSize, out int count);

        /// <summary>
        /// 获取所有权限菜单
        /// </summary>
        /// <returns></returns>
        List<UserMenu> GetAllUserMenu();


        /// <summary>
        /// 获取所有菜单分组
        /// </summary>
        /// <returns></returns>
        List<base_menu_groups> GetAllUserMenuGroups();

        /// <summary>
        /// 添加用户权限信息
        /// </summary>
        /// <returns></returns>
        bool AddUserMenu(List<base_user_menu_rel> usermenulist);

        /// <summary>
        /// 获取有权限的用户
        /// </summary>
        /// <param name="userId"></param>
        List<UserMenu> GetUserMenu(long userId);

    }
}
