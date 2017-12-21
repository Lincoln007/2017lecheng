using CommHelper;
using DBModel.Common;
using IBLLService;
using LCWebApp.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers
{
    public class LoginController : Controller
    {
        private ILoginService _service;
        public LoginController(ILoginService service) //构造注入 
        {
            _service = service;
        }
        //
        // GET: /Login/
        public ActionResult Login()
        {
            var username = CookieHelper.GetCookieValue("username");
            var password = CookieHelper.GetCookieValue("password");
            var remember = CookieHelper.GetCookieValue("remember");

            if (Request.Cookies["tockenid"] == null)
            {
                CookieHelper.SetCookie("tockenid", Guid.NewGuid().ToString("N").ToLower());
            }
            ViewData["username"] = username;
            ViewData["password"] = password;
            ViewData["remember"] = remember;
            return View();
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginCheck(string username, string password, string ValidCode, bool remember)
        {
            //识别是否主库登录
            var ismainlogin = false;//false从库登录true主库登录
            CookieHelper.SetCookie("ismainlogin", ismainlogin.ToString());
            LCWebApp.Controllers.Base.ComResult com = new LCWebApp.Controllers.Base.ComResult();
            //1.判断验证码
            if (ValidCode.ToLower() != SessionHelper.Get("RandomCode").ToString().ToLower())
            {
                com.Msg = "验证码错误！";
                com.State = 0;
                return Json(com);
            }

            //2.验证账号密码
            var result = _service.VerifyUserLogin(1, username, EncodeHepler.GetMD5(password), false);
            //3.判断用户是否记住密码功能  
            if (result.State == 1 && remember)
            {
                CookieHelper.SetCookie("username", username);
                CookieHelper.SetCookie("password", password);
                CookieHelper.SetCookie("remember", remember.ToString());
            }
            else
            {
                CookieHelper.ClearCookie("username");
                CookieHelper.ClearCookie("password");
                CookieHelper.ClearCookie("remember");
                SessionHelper.Delete("RandomCode");
            }
            if (result.State == 1)
            {
                CookieHelper.SetCookie("username", username);
                result.URL = "/Main/index";
                SessionHelper.Delete("RandomCode");
                return Json(result);
            }
            else
            {
                return Json(result);
            }

        }
    }
}