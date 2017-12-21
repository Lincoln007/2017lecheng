using DBModel.ProcurementCosts;
using DBModel.ViewModel;
using IBLLService.ProcurementCosts;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.ProcurementCosts
{
    public class ProcurementCostsController : Controller
    {

        private IProcurementCostsService _service;
        public ProcurementCostsController(IProcurementCostsService service) //构造注入 
        {
            _service = service;
        }
        // GET: ProcurementCosts
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, DateTime? start_time, DateTime? end_time)
        {
            ProcurementCostsResult com = new ProcurementCostsResult();
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
            if (!start_time.HasValue)
            {
                com.Msg = "开始时间不得为空!";
                com.success = false;
                return Json(com);
            }
            if (start_time > end_time)
            {
                com.success = false;
                com.Msg = "结束时间不得小于开始时间!";
                return Json(com);
            }
            DateTime torrow = end_time.Value.AddDays(1);
            int totil = 0;
            int totilpage = 0;
            string exmsg = string.Empty;
            List<ProcurementCostsModel> mylist = _service.GetProcurementCostsList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg, start_time, torrow);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                ProcurementCostsViewModel mylistview = new ProcurementCostsViewModel();
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