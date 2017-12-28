using DBModel.OverseasDelivery;
using DBModel.ViewModel;
using IBLLService.OverseasDelivery;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.OverseasDelivery
{
    public class OverseasDeliveryController : Controller
    {
        private IOverseasDeliveryService _service;
        public OverseasDeliveryController(IOverseasDeliveryService service) //构造注入 
        {
            _service = service;
        }

        // GET: OverseasDelivery
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, int? express_id, DateTime? start_time, DateTime? end_time)
        {
            OverseasDeliveryResult com = new OverseasDeliveryResult();
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
            List<OverseasDeliveryModel> mylist = _service.GetOverseasDeliveryList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil,
                out totilpage, out exmsg, express_id, start_time, end_time);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                OverseasDeliveryViewModel mylistview = new OverseasDeliveryViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }


        [HttpPost]
        public ActionResult GetSave(Int64? id)
        {
            OverseasDeliveryResult res = new OverseasDeliveryResult();
            try
            {
                res = _service.GetSave(id.Value);
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