using DBModel.Base;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService.Base.WareHouse;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Base
{
  
    public class WareHouseController : Controller
    {


        private IWareHouseService _service;
        public WareHouseController(IWareHouseService service) //构造注入 
        {
            _service = service;
        }

        // GET: WareHouse
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount)
        {
            WareHouseResult com = new WareHouseResult();
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
            List<base_wh_warehouse> mylist = _service.GetWareHouseList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                WareHouseViewModel mylistview = new WareHouseViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }



        [HttpPost]
        public ActionResult Save(base_wh_warehouse model)
        {
            WareHouseResult res = new WareHouseResult();
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
            WareHouseResult res = new WareHouseResult();
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