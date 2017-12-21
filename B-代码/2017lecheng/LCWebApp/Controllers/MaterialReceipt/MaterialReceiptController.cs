using DBModel.DBmodel;
using DBModel.InternationalQuiry;
using DBModel.MaterialReceipt;
using DBModel.ViewModel;
using IBLLService.InternationalQuiry;
using IBLLService.MaterialReceipt;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.MaterialReceipt
{
    public class MaterialReceiptController : Controller
    {

        private IMaterialReceiptService _service;
        private IInternationalQuiryService _internationalQuiry;
        public MaterialReceiptController(IMaterialReceiptService service, IInternationalQuiryService internationalQuiry) //构造注入 
        {
            _service = service;
            _internationalQuiry = internationalQuiry;
        }
        // GET: MaterialReceipt
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, string express_code)
        {
            MaterialReceiptResult com = new MaterialReceiptResult();
            InternationalQuiryResult coms = new InternationalQuiryResult();
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
            List<MaterialReceiptModel> mylist = _service.GetMaterialReceiptList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg, express_code);
            coms = _internationalQuiry.GetExpressList();
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                MaterialReceiptViewModel mylistview = new MaterialReceiptViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                if (coms.success)
                {
                    mylistview.express_info = coms.Msg;
                }
                else
                {
                    mylistview.express_info = null;
                }
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
                ViewBag.purch_type = list.purch_type == 1 ? "订单采购" : (list.purch_type == 2 ? "库存采购" : "");
                ViewBag.purch_status = list.purch_status == 1 ? "初始" : (list.purch_status == 2 ? "已采购" : (list.purch_status == 3 ? "待收货" : (list.purch_status == 4 ? "已全部到货" : "")));
                ViewBag.purch_typeE = list.purch_type;
            }
            return View(new busi_purchase());
        }



        [HttpPost]
        public ActionResult GetpageE(Int64? purch_id)
        {
            MaterialReceiptResult com = new MaterialReceiptResult();
            string exmsg = string.Empty;
            List<MaterialReceiptModelE> mylist = _service.GetMaterialReceiptEList(out exmsg, purch_id.Value);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                MaterialReceiptViewModelE mylistview = new MaterialReceiptViewModelE();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }


        [HttpPost]
        public ActionResult Save1(busi_purchase model, List<MaterialReceiptModelE> lists)
        {
            MaterialReceiptResult com = new MaterialReceiptResult();
            try
            {

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
        public ActionResult Save(busi_purchase model, List<MaterialReceiptSaveModel> lists, int? purch_type)
        {
            MaterialReceiptResult com = new MaterialReceiptResult();
            try
            {
                com = _service.Save(model, lists, purch_type);
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
        public ActionResult Modify(Int64? purch_id, Int64? express_id, string express_code, string express_name, string OrderCode)
        {
            MaterialReceiptResult com = new MaterialReceiptResult();
            if (purch_id == 0)
            {
                com.Msg = "参数错误！";
                com.success = false;
                return Json(com);
            }
            if (express_id == 0 || string.IsNullOrEmpty(express_name))
            {
                com.Msg = "请选择快递公司！";
                com.success = false;
                return Json(com);
            }
            if (string.IsNullOrWhiteSpace(express_code))
            {
                com.Msg = "请填写快递单号！";
                com.success = false;
                return Json(com);
            }
            if (string.IsNullOrWhiteSpace(OrderCode))
            {
                com.Msg = "请填写淘宝订单号！";
                com.success = false;
                return Json(com);
            }
            try
            {
                com = _service.Modify(purch_id, express_id, express_code, express_name, OrderCode);
                return Json(com);
            }
            catch (Exception ex)
            {
                com.Msg = ex.ToString();
                com.success = false;
                return Json(com);
            }
        }

    }
}