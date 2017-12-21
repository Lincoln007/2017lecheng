using BLLServices;
using CommHelper;
using DBModel.Common;
using DBModel.DBmodel.Global;
using DBModel.DBmodel;
using DBModel.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace LCWebApp.Infrastructure
{
    /// <summary>
    /// 操作权限判断过滤器
    /// </summary>
    public class AuthorizationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (actionName.ToLower().Equals("login") || actionName.ToLower().Equals("LoginCheck") || actionName.ToLower().Equals("userlogin") || actionName.ToLower().Equals("logout") || controllerName.ToLower().Equals("validcode") )
                return;
            //获取当前用户信息
            //用户是否登入
            string sessionID = CookieHelper.GetCookieValue("tockenid");
            object obj = SessionHelper.Get(sessionID);
            if (obj == null)//如果没有登录
            {
                var result = new ContentResult();
                result.Content = "<script>if(window.parent){window.parent.location.href='/Login/login';}else{window.location.href='/Login/login';}</script>";
                filterContext.Result = result;
                return;
            }
            //通过cookie识别是否来自主库登录
            try
            {
                User user = null;
                if (CookieHelper.GetCookieValue("ismainlogin") == "False")
                {
                    //从库
                    var userModel = JsonHelper.DeserializeJsonToObject<base_users>(obj.ToString());
                     user = EntityViewModelConverter.ViewModelToEntity<base_users, User>(userModel);
                }
                else
                {
                    //主库
                    var userModel = JsonHelper.DeserializeJsonToObject<tbl_users>(obj.ToString());
                    user = EntityViewModelConverter.ViewModelToEntity<tbl_users, User>(userModel);
                }
                if (user!=null)
                {
                      //判断权限
                    UserService perService = new UserService();
                    List<SingUserPermissionModel> sysFunList = perService.GetUserPermissionServiceByUserId(user.user_id);
                    string funUrl = "/" + controllerName + "/" + actionName;

                    //设置过这个请求的权限
                    if (sysFunList != null && sysFunList.Count() > 0 && sysFunList[0].menu_id != 0)
                    {
                        SingUserPermissionModel fun = sysFunList.FirstOrDefault(a => a.url_absolute_path.ToLower().Equals(funUrl.ToLower()));

                        if (fun != null)
                            return;
                    }
                   
                        DoRedirect(filterContext);
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private void DoRedirect(AuthenticationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var result = new JsonResult();
                result.Data = new
                {
                    success = false,
                    msg = "您没有权限",
                    jump = true
                };
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = result;
            }
            else
            {
                var result = new ContentResult();
                result.Content = "<script>alert('您没有权限')</script>";
                filterContext.Result = result;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}