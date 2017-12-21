using DBModel.DBmodel;
using DBModel.Procurement;
using DBModel.ViewModel;
using IBLLService.Procurement;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Procurement
{
    public class ProcurementController : Controller
    {

        private IProcurementService _service;
        public ProcurementController(IProcurementService service) //构造注入 
        {
            _service = service;
        }
        // GET: Procurement
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, int? type, string purch_code, string OrderCode, string express_code, string supp_name)
        {
            ProcurementResult com = new ProcurementResult();
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
            List<ProcurementModel> mylist = _service.GetProcurementList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg, type, purch_code, OrderCode, express_code, supp_name);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                ProcurementViewModel mylistview = new ProcurementViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }


        [HttpGet]
        public ActionResult IndexE(long? id)
        {
            busi_purchase list = _service.GetInfoByID(id.Value);
            if (list.purch_id > 0)
            {
                ViewBag.id = list.purch_id;
                ViewBag.purch_numb = list.purch_numb.ToString();
                ViewBag.purch_code = list.purch_code;
                ViewBag.sum_money = list.sum_money;
                ViewBag.purch_remark = list.purch_remark;
                ViewBag.purch_sum = list.purch_sum;
                ViewBag.create_time = list.create_time.ToString("yyyy-MM-dd");
                ViewBag.sum_freight = list.sum_freight;
                ViewBag.purch_status = list.purch_status == 1 ? "初始" : (list.purch_status == 2 ? "已采购" : (list.purch_status == 3 ? "待收货" : (list.purch_status == 4 ? "已全部到货" : "")));
                ViewBag.express_code = list.express_code;
                ViewBag.express_name = list.express_name;
                ViewBag.purch_type = list.purch_type == 1 ? "订单采购" : (list.purch_type == 2 ? "库存采购" : "");

            }
            return View(new busi_purchase());
        }

        [HttpPost]
        public ActionResult GetpageE(Int64? purch_id)
        {
            ProcurementResult com = new ProcurementResult();
            string exmsg = string.Empty;
            List<ProcurementModelE> mylist = _service.GetProcurementEList(out exmsg, purch_id.Value);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                ProcurementViewModelE mylistview = new ProcurementViewModelE();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }


        }

    }
}