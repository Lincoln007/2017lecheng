using DBModel.Base;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService.Base.StorageBin;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Base
{
    public class StorageBinController : Controller
    {

        private IStorageBinService _service;
        public StorageBinController(IStorageBinService service) //构造注入 
        {
            _service = service;
        }

        // GET: StorageBin
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, long? wh_id, string locat_code, string sku_code, int ty_pe)
        {
            StorageBinResult com = new StorageBinResult();
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
            List<StorageBinModel> mylist = new List<StorageBinModel>();
            if (ty_pe == 1)
            {
                mylist = _service.GetStorageBinList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg, wh_id, locat_code);
            }
            else if (ty_pe == 2)
            {
                mylist = _service.GetStorageBinInOutList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg, wh_id, locat_code, sku_code);
            }

            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                StorageBinViewModel mylistview = new StorageBinViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                com.ty_pe = ty_pe;
                return Json(com);
            }
        }




        [HttpPost]
        public ActionResult GetWh_nameList()
        {
            StorageBinResult com = new StorageBinResult();
            try
            {
                com = _service.GetWh_nameList();
                return Json(com);
            }
            catch (Exception ex)
            {
                com.Msg = ex.ToString();
                com.success = false;
                return Json(com);
            }
        }


        [HttpPost]
        public ActionResult Save(base_location model)
        {
            StorageBinResult com = new StorageBinResult();
            try
            {
                com = _service.Save(model);
                return Json(com);
            }
            catch (Exception ex)
            {
                com.Msg = ex.ToString();
                com.success = false;
                return Json(com);
            }
        }


        [HttpPost]
        public ActionResult Save_sku(Int64 wh_id, Int64 locat_id, string sku, int skucount)
        {
            StorageBinResult com = new StorageBinResult();
            try
            {
                com = _service.Save_sku(wh_id, locat_id, sku, skucount);
                return Json(com);
            }
            catch (Exception ex)
            {
                com.Msg = ex.ToString();
                com.success = false;
                return Json(com);
            }
        }



        [HttpPost]
        public ActionResult SaveInOut(Guid? id, Decimal count, int type)
        {
            StorageBinResult com = new StorageBinResult();
            try
            {
                com = _service.StockInOut(id, count, type);
                return Json(com);
            }
            catch (Exception ex)
            {
                com.Msg = ex.ToString();
                com.success = false;
                return Json(com);
            }
        }




        [HttpPost]
        public ActionResult GetStorageBinEList(long? wh_id, long? locat_id)
        {
            StorageBinResult com = new StorageBinResult();
            string exmsg = string.Empty;
            List<StorageBinModel> mylist = _service.GetStorageBinEList(out exmsg, wh_id, locat_id);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                StorageBinViewModel mylistview = new StorageBinViewModel();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }

    }
}