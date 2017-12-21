using DBModel.DBmodel;
using DBModel.OrderQuery;
using DBModel.ViewModel;
using IBLLService.OrderQuery;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.OrderQuery
{
    public class OrderQueryController : Controller
    {

        private IOrderQueryService _service;
        public OrderQueryController(IOrderQueryService service) //构造注入 
        {
            _service = service;
        }

        // GET: OrderQuery
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, Int64? shop_id, DateTime? create_time, string order_code, string custorder_code, string emp_name)
        {
            OrderQueryResult com = new OrderQueryResult();
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
            List<OrderQueryModel> mylist = _service.GetIOrderQueryList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil,
                out totilpage, out exmsg, shop_id, create_time, order_code, custorder_code, emp_name);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                OrderQueryViewModel mylistview = new OrderQueryViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }

        [HttpPost]
        public ActionResult GetpageE(Int64? order_id)
        {
            OrderQueryResult com = new OrderQueryResult();
            string exmsg = string.Empty;
            List<OrderQueryModelE> mylist = _service.GetIOrderQueryListE(out exmsg, order_id.Value);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                OrderQueryViewModelE mylistview = new OrderQueryViewModelE();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }


        [HttpPost]
        public ActionResult GetInfoByID(Int64? order_id)
        {
            OrderQueryResult com = new OrderQueryResult();
            try
            {
                List<busi_sendorder> mylist = _service.GetInfoByID(order_id.Value);
                OrderQueryViewModelEE mylistview = new OrderQueryViewModelEE();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
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