using DBModel.DBmodel;
using DBModel.OrderQuery_Shop;
using DBModel.ViewModel;
using IBLLService.OrderQuery_Shop;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.OrderQuery_Shop
{
    public class OrderQuery_ShopController : Controller
    {
        private IOrderQuery_ShopService _service;
        public OrderQuery_ShopController(IOrderQuery_ShopService service) //构造注入 
        {
            _service = service;
        }

        // GET: OrderQuery_Shop
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, Int64? shop_id, DateTime? create_time, string order_code, string custorder_code, string emp_name, int? state, int? day)
        {
            OrderQuery_ShopResult com = new OrderQuery_ShopResult();
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
            List<OrderQuery_ShopModel> mylist = _service.GetOrderQuery_ShopList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil,
                out totilpage, out exmsg, shop_id, create_time, order_code, custorder_code, emp_name, state, day);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                OrderQuery_ShopViewModel mylistview = new OrderQuery_ShopViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }



        public ActionResult IndexE(long? id)
        {
            busi_custorder list = _service.GetInfoByID(id.Value);
            if (list != null)
            {
                ViewBag.id = list.order_id;
            }

            return View(new busi_custorder());

        }


        [HttpPost]
        public ActionResult GetpageE(Int64? custorder_id)
        {
            OrderQuery_ShopResult com = new OrderQuery_ShopResult();
            string exmsg = string.Empty;
            List<OrderQuery_ShopModelE> mylist = _service.GetOrderQuery_ShopEList(out exmsg, custorder_id.Value);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                OrderQuery_ShopViewModelE mylistview = new OrderQuery_ShopViewModelE();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }


        [HttpPost]
        public ActionResult Save(List<busi_sendorder> lists)
        {
            OrderQuery_ShopResult com = new OrderQuery_ShopResult();
            try
            {
                com.success = true;
                com = _service.Save(lists);
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
        public ActionResult Del(Int64? detail_id, Int64? work_id, Int64? id)
        {
            OrderQuery_ShopResult com = new OrderQuery_ShopResult();
            try
            {
                if (detail_id == 0 || work_id == 0 || id == 0)
                {
                    com.Msg = "参数错误!";
                    com.success = false;
                    return Json(com);
                }
                com = _service.Del(detail_id, work_id, id);
                return Json(com);

            }
            catch (Exception ex)
            {
                com.Msg = ex.ToString();
                com.success = false;
                return Json(com);
            }

        }

        /// <summary>
        /// 换货
        /// </summary>
        /// <param name="detail_id"></param>
        /// <param name="work_id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Barter(Int64? detail_id, Int64? work_id, string sku, Int64? id)
        {
            OrderQuery_ShopResult com = new OrderQuery_ShopResult();
            try
            {
                if (detail_id == 0 || work_id == 0 || id == 0)
                {
                    com.Msg = "参数错误!";
                    com.success = false;
                    return Json(com);
                }
                if (string.IsNullOrWhiteSpace(sku))
                {
                    com.Msg = "请输入sku!";
                    com.success = false;
                    return Json(com);

                }
                com = _service.Barter(detail_id, work_id, sku, id);
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