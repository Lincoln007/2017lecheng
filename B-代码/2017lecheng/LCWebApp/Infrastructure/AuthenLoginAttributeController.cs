using CommHelper;
using DBModel.Common;
using DBModel.DBmodel.Global;
using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace LCWebApp.Infrastructure
{
    /// <summary>
    /// 登录身份过滤器
    /// </summary>
    public class AuthenLoginAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (actionName.ToLower().Equals("login") || actionName.ToLower().Equals("LoginCheck") || actionName.ToLower().Equals("logout") || controllerName.ToLower().Equals("validcode"))
                return;
            //获取当前用户信息
            //用户是否登入
            string sessionID = CookieHelper.GetCookieValue("tockenid");
            object obj = SessionHelper.Get(sessionID);
            if (obj == null)
                DoLogout(filterContext);
            //通过cookie识别是否来自主库登录
            try
            {
                if (CookieHelper.GetCookieValue("ismainlogin") == "False")
                {
                    //从库
                    var userModel = JsonHelper.DeserializeJsonToObject<base_users>(obj.ToString());
                    var user = EntityViewModelConverter.ViewModelToEntity<base_users, User>(userModel);
                }
                else
                {
                    //主库
                    var userModel = JsonHelper.DeserializeJsonToObject<tbl_users>(obj.ToString());
                    var user = EntityViewModelConverter.ViewModelToEntity<tbl_users, User>(userModel);
                }
            }
            catch (Exception)
            {
                 DoLogout(filterContext);
                throw;
            }

        }

        private void DoLogout(AuthenticationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var result = new JsonResult();
                result.Data = new
                {
                    success = false,
                    msg = "请重新登录",
                    jump = true
                };
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                filterContext.Result = result;
            }
            else
            {
                var result = new ContentResult();
                result.Content = "<script>if(window.parent){window.parent.location.href='/User/login';}else{window.location.href='/User/login';}</script>";
                filterContext.Result = result;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}