using DBModel.GoodsReceived;
using DBModel.InternationalQuiry;
using DBModel.ViewModel;
using IBLLService.GoodsReceived;
using IBLLService.InternationalQuiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.GoodsReceived
{
    public class GoodsReceivedController : Controller
    {
        private IGoodsReceivedService _service;
        private IInternationalQuiryService _internationalQuiry;
        public GoodsReceivedController(IGoodsReceivedService service, IInternationalQuiryService internationalQuiry) //构造注入 
        {
            _service = service;
            _internationalQuiry = internationalQuiry;
        }

        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount)
        {
            GoodsReceivedResult com = new GoodsReceivedResult();
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
            List<GoodsReceivedModel> mylist = _service.GetGoodsReceivedList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg);
            coms = _internationalQuiry.GetExpressList();
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                GoodsReceivedViewModel mylistview = new GoodsReceivedViewModel();
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


        // GET: GoodsReceived
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddCode(Int64? purch_id, Int64? express_id, string express_code, string express_name, string OrderCode)
        {
            GoodsReceivedResult com = new GoodsReceivedResult();
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
                com = _service.AddCode(purch_id, express_id, express_code, express_name, OrderCode);
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