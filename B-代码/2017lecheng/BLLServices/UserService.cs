using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ViewModel.User;
using IBLLService;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices
{
    public class UserService : IUserService
    {
        /// <summary>
        /// 添加或者更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="state">0代表修改密码1代表删除用户2代表禁用启用用户3代表添加</param>
        /// <returns></returns>
        public ComResult AddOrUpdateUserInfo(base_users model,int state)
        {
            ComResult result = new ComResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (model.user_id > 0)
                {    //判断用户名是否存在
                    var userModel = db.Queryable<base_users>().Where(a => a.user_id == model.user_id).ToList();
                    if (userModel == null || userModel.Count() <= 0)
                    {
                        result.State = 0;
                        result.Msg = "用户不存在";
                        return result;
                    }
                    switch(state)
                    {
                        case 0:
                            db.Update<base_users>(new { user_pwd = model.user_pwd, real_name = model.real_name }, a => a.user_id == model.user_id);
                            break;
                        case 1:
                            db.Update<base_users>(new { del_flag =model.del_flag}, a => a.user_id == model.user_id);
                            break;
                        case 2:
                            db.Update<base_users>(new { user_status = model.user_status }, a => a.user_id == model.user_id);
                            break; ;
                    }
                    result.State =1;
                    return result;
                }
                else
                {
                    //判断用户名是否存在
                    var userModel = db.Queryable<base_users>().Where(a => a.user_name == model.user_name).ToList();
                    if (userModel != null && userModel.Count()>0)
                    {
                        result.State = 0;
                        result.Msg = "用户已经存在";
                        return result;
                    }
                    model.create_time = DateTime.Now;
                    model.edit_time = DateTime.Now;
                    model.del_flag = true;
                    model.user_status = true;
                    model.create_user_id = LoginUser.Current.user_id;
                    model.net_no = 1;
                    model.emp_no = 1;
                    result.State = 1;
                    result.DataResult = db.InsertOrUpdate<base_users>(model);
                    return result;
                }

            }

        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<base_users> GetUserList(int pageIndex,int pageSize,out int count) 
        {
            using(var db=SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var getwhere = db.Queryable<base_users>()
                    .Where(a => a.del_flag == true).OrderBy(a=>a.user_id);
                count = getwhere.Count();
                return getwhere.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// 获取有权限的用户
        /// </summary>
        /// <param name="userId"></param>
        public List<UserMenu> GetUserMenu(long userId)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var sql = db.Queryable<base_users>()
                    .JoinTable<base_user_menu_rel>((s1, s2) => s1.user_id == s2.user_id, JoinType.Inner);
                //如果username不为空则增加条件查询
                if (userId > 0)
                {
                    sql.Where(s1 => s1.user_id == userId);
                }
                var s = sql.ToSql();
                var list = sql.GroupBy("s1.real_name,s1.user_id,s1.emp_no,s2.menu_id,s1.user_status").Select<base_user_menu_rel, UserMenu>((s1, s2) => new UserMenu()
                {
                    menu_id = s2.menu_id,
                    username = s1.real_name,
                    userId = s1.user_id,
                    emp_no = s1.emp_no,
                    user_status = s1.user_status
                }).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取所有权限菜单
        /// </summary>
        /// <returns></returns>
        public List<UserMenu> GetAllUserMenu()
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var usermenulist = db.Queryable<base_menus>().GroupBy(s1 => s1.menu_group_id).GroupBy(s1 => s1.menu_id).GroupBy(s1 => s1.menu_name)
                    .Select<UserMenu>(s1 => new UserMenu() { menu_group_id = s1.menu_group_id, menu_id = s1.menu_id, menu_name = s1.menu_name }).ToList();

                return usermenulist;
            }
        }

        /// <summary>
        /// 获取所有菜单分组
        /// </summary>
        /// <returns></returns>
        public List<base_menu_groups> GetAllUserMenuGroups()
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var usermenulist = db.Queryable<base_menu_groups>().ToList();

                return usermenulist;
            }
        }


        /// <summary>
        /// 添加用户权限信息
        /// </summary>
        /// <returns></returns>
        public bool AddUserMenu(List<base_user_menu_rel> usermenulist)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {

                //获取需要添加的用户
                string[] strlist = new string[usermenulist.Count()];
                var i = 0;
                foreach (var item in usermenulist)
                {
                    strlist[i] = item.user_id.ToString();
                    item.del_flag = true;
                    i++;
                }

                try
                {
                    db.BeginTran();
                    //删除用户权限
                    db.Delete<base_user_menu_rel, string>(a => a.user_id, strlist);
                    //重新添加用户权限
                    db.InsertRange<base_user_menu_rel>(usermenulist);
                    db.CommitTran();
                }
                catch (Exception e)
                {
                    db.RollbackTran();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 通过用户ID获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        public List<SingUserPermissionModel> GetUserPermissionServiceByUserId(long userId)
        {

            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                //没有角色的时候的用户权限
                //1通过用户查询用户与菜单关系表
                var result = db.Queryable<base_user_menu_rel>()
                     .JoinTable<base_menus>((s1, s2) => s1.menu_group_id == s2.menu_group_id || s1.menu_id == s2.menu_id)
                     .Where(s1 => s1.user_id == userId).Select<base_menus, SingUserPermissionModel>
                     ((s1, s2) => new SingUserPermissionModel { menu_id = s2.menu_id, url_absolute_path = s2.url_absolute_path }).ToList();
                //有角色的时候的用户权限
                var vresult = db.Queryable<base_role_user_rel>()
                             .JoinTable<base_role_menu_rel>((s1, s2) => s1.role_id == s2.role_id)
                             .JoinTable<base_role_menu_rel, base_menus>((s1, s2, s3) => s2.menu_id == s3.menu_id || s2.menu_group_id == s3.menu_group_id)
                              .Where(s1 => s1.user_id == userId).Select<base_menus, SingUserPermissionModel>
                    ((s1, s3) => new SingUserPermissionModel { menu_id = s3.menu_id, url_absolute_path = s3.url_absolute_path }).ToList();
                //合并两个组的权限
                return result.Concat(vresult).ToList();

            }
        }

    }
}
