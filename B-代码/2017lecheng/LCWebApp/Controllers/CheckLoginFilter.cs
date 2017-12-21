using CommHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace LCWebApp.Controllers
{
    /// <summary>
    /// 登录身份过滤器
    /// </summary>
    public class CheckLoginFilter : FilterAttribute, IAuthenticationFilter
    {

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //满足这几个条件不执行过滤器
            if (actionName.ToLower().Equals("login") || actionName.ToLower().Equals("logout") || actionName.ToLower().Equals("logincheck") || controllerName.ToLower().Equals("validcode"))
                return;

            //string sessionID = filterContext.HttpContext.Request.Cookies["tockenid"] == null ? string.Empty : filterContext.HttpContext.Request.Cookies["tockenid"].Value;
            string sessionID = filterContext.HttpContext.Request.Cookies["islogin"] == null ? string.Empty : filterContext.HttpContext.Request.Cookies["islogin"].Value;

            object obj = SessionHelper.Get(sessionID);
            if (obj == null)
            {
                DoLogout(filterContext);
            }
            else
            {
                SessionHelper.Add(sessionID, obj.ToString(), 6 * 60);
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
                result.Content = "<script>if(window.parent){window.parent.location.href='/login/login';}else{window.location.href='/login/login';}</script>";
                filterContext.Result = result;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}