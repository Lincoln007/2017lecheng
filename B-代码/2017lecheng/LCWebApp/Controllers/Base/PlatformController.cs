using DBModel.Base;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService.Base.Platform;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Base
{
    public class PlatformController : Controller
    {

        private IPlatformService _service;
        public PlatformController(IPlatformService service) //构造注入 
        {
            _service = service;
        }
        // GET: Platform
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount)
        {
            PlatformResult com = new PlatformResult();
            if (!Regex.IsMatch(pagenum, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(pagenum))
            {
                com.Msg = "页数不正确";
                com.success = false;
                return Json(com);
            }

            if (!Regex.IsMatch(onepagecount, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(onepagecount))
            {
                com.Msg = "每页数量不正确";
                com.success = false;
                return Json(com);
            }
            int totil = 0;
            int totilpage = 0;
            string exmsg = string.Empty;
            List<base_platform> mylist = _service.GetPlatformList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                PlatformViewModel mylistview = new PlatformViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }


        }


        [HttpPost]
        public ActionResult Save(base_platform model)
        {
            PlatformResult res = new PlatformResult();
            try
            {
                res = _service.Save(model);
                return Json(res);
            }
            catch (Exception ex)
            {
                res.Msg = ex.ToString();
                res.success = false;
                return Json(res);
            }

        }



        [HttpPost]
        public ActionResult Del(long? id)
        {
            PlatformResult res = new PlatformResult();
            try
            {
                res = _service.Del(id.Value);
                return Json(res);
            }
            catch (Exception ex)
            {
                res.Msg = ex.ToString();
                res.success = false;
                return Json(res);
            }

        }



    }
}