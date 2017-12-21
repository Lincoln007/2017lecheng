using DBModel.Base;
using DBModel.ViewModel;
using IBLLService.Base.Stock;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Base
{
    public class StockController : Controller
    {
        private IStockService _service;
        public StockController(IStockService service) //构造注入 
        {
            _service = service;
        }

        // GET: Stock
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, int? orderby, string sku_code)
        {
            StockResult com = new StockResult();
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
            List<StockModel> mylist = _service.GetStockList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg, orderby, sku_code);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                StockViewModel mylistview = new StockViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }


        }


        [HttpPost]
        public ActionResult GetpageE(Guid? stock_id)
        {
            StockResult com = new StockResult();
            string exmsg = string.Empty;
            List<StockModel> mylist = _service.GetStockEList(out exmsg, stock_id.Value);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                StockViewModel mylistview = new StockViewModel();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }


        }


        [HttpPost]
        public ActionResult GetStockSum()
        {
            StockResult com = new StockResult();
            com = _service.GetStockSum();
            return Json(com);
        }




    }
}