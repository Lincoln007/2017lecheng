using CommHelper;
using LCWebApp.Controllers.Base;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/Logout 
        
        public ActionResult Index()
        {
            string username = CookieHelper.GetCookieValue("username");
            ViewData["username"] = username;
            return View();
        }

        public ActionResult Logout()
        {
            CookieHelper.ClearCookie("username");
            CookieHelper.ClearCookie("password");
            CookieHelper.ClearCookie("remember");
            CookieHelper.ClearCookie("tockenid");
            ComResult com = new ComResult();
            com.State = 1;
            com.URL = "/login/login";
            return Json(com);
        }
	}
}