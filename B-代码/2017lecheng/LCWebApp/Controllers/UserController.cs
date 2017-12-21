using DBModel.Common;
using DBModel.DBmodel;
using IBLLService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlSugarRepository;
using CommHelper;
using LCWebApp.Infrastructure;
namespace LCWebApp.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
    
        public ActionResult UserManage(int pageIndex = 1)
        {
            var pageSize = 10;
            var count = 0;
            var userlist = _userService.GetUserList(pageIndex, pageSize, out count);
            ViewBag.UserList = userlist;
            //计算页数
            var size = count % pageSize > 0 ? count / pageSize + 1 : count / pageSize;
            ViewBag.Size = size;
            //获取用户权限
            var usermenuList = _userService.GetAllUserMenu();
            ViewBag.UserMenuList = usermenuList;
            //获取权限分组
            var menugroupsList = _userService.GetAllUserMenuGroups();
            ViewBag.MenuGroupsList = menugroupsList;
            return View();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user_name"></param>
        /// <param name="real_name"></param>
        /// <param name="user_pwd"></param>
        /// <param name="isAdmin">是否管理员</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUserInfo(string user_name, string real_name, string user_pwd, bool isAdmin)
        {
            base_users model = new base_users()
            {
                user_name = user_name,
                real_name = real_name,
                user_pwd = EncodeHepler.GetMD5(user_pwd)
            };
            var result = _userService.AddOrUpdateUserInfo(model, 3);
            if (isAdmin && result.State == 1)
            {
                //如果是管理员则添加所有权限
                var usermenus = _userService.GetAllUserMenu();
                var relList = new List<base_user_menu_rel>();
                usermenus.ForEach(a =>
                {
                    var rel = new base_user_menu_rel()
                    {
                        create_time = DateTime.Now,
                        edit_time = DateTime.Now,
                        create_user_id = LoginUser.Current.user_id,
                        user_id = result.DataResult.ObjToInt(),
                    };
                    relList.Add(rel);
                });
                _userService.AddUserMenu(relList);
            }
            return Json(result);
        }

        /// <summary>
        /// 通过用户ID更新用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserInfoById(string real_name, string user_pwd, int user_id)
        {
            base_users model = new base_users()
            {
                user_id = user_id,
                real_name = real_name,
                user_pwd = user_pwd
            };
            var result = _userService.AddOrUpdateUserInfo(model, 0);
            return Json(result);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DelUserById(int user_id)
        {
            base_users model = new base_users()
            {
                user_id = user_id,
                del_flag = false
            };
            var result = _userService.AddOrUpdateUserInfo(model, 1);
            return Json(result);
        }

        /// <summary>
        /// 禁用启用用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetUserStateById(int user_id, bool user_state)
        {
            base_users model = new base_users()
            {
                user_id = user_id,
                user_status = user_state
            };
            var result = _userService.AddOrUpdateUserInfo(model, 2);
            return Json(result);
        }

        /// <summary>
        /// 通过用户ID获取用户权限
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public ActionResult GetUserMenuById(long user_id)
        {
            return Json(new ComResult()
            {
                DataResult = _userService.GetUserMenu(user_id)
            });
        }

        /// <summary>
        /// 添加用户权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUserMenuRel(string objlist) 
       {
           if (objlist == "" || objlist==null)
            {
                return Json(new ComResult()
                {
                    State = 0,
                    Msg="未添加任何权限"
                });
            }
           List<base_user_menu_rel> relList = JsonHelper.DeserializeJsonToList<base_user_menu_rel>(objlist);
           relList.ForEach(a => { a.create_time = DateTime.Now; a.create_user_id = LoginUser.Current.user_id;
           a.del_flag = true; a.edit_time = DateTime.Now; a.edit_user_id = LoginUser.Current.user_id;
           });
           return Json(new ComResult() 
           {
               State = _userService.AddUserMenu(relList)==true?1:0
           });
       }
    }
}