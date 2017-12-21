using DBModel.NotPicking;
using DBModel.ViewModel;
using IBLLService.NotPicking;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.NotPicking
{
    public class NotPickingController : Controller
    {
        private INotPickingService _service;
        public NotPickingController(INotPickingService service) //构造注入 
        {
            _service = service;
        }


        // GET: NotPicking  
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, Int64? shop_id, string sku)
        {
            NotPickingResult com = new NotPickingResult();
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
            List<NotPickingModelE> mylist = _service.GetNotPickingList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil,
                out totilpage, out exmsg, shop_id, sku);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                NotPickingViewModel mylistview = new NotPickingViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }


    }
}