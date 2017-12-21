using DBModel.Base;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService.Base.Shop;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Base
{
    public class ShopController : Controller
    {

        private IShopService _service;
        public ShopController(IShopService service) //构造注入 
        {
            _service = service;
        }
        // GET: Shop
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount)
        {
            ShopResult com = new ShopResult();
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
            List<ShopResultModel> mylist = _service.GetShopList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                ShopViewModel mylistview = new ShopViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }

         [HttpPost]
        public ActionResult GetShopList() 
        {
            ShopResult com = new ShopResult();
            try
            {
                com = _service.GetShopList();
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
        /// 根据ID获取店铺实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [HttpPost]
         public ActionResult GetShopByID(int id) 
         {
             ComResult com = new ComResult();
             try
             {
                 base_shop shop=_service.GetShopByID(id);
                 if (null != shop)
                 {
                     com.DataResult = shop;
                     com.State = 1;
                     return Json(com);
                 }
                 else
                 {
                     com.Msg = "获取店铺信息失败!";
                     com.State = 0;
                     return Json(com);
                 }
             }
             catch (Exception ex)
             {
                 com.Msg = ex.ToString();
                 com.State = 0;
                 return Json(com);
             }

         }

        [HttpPost]
        public ActionResult GetPlatformList()
        {
            ShopResult com = new ShopResult();
            try
            {
                com = _service.GetPlatformList();
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
        public ActionResult GetCountryList()
        {
            ShopResult com = new ShopResult();
            try
            {
                com = _service.GetCountryList();
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
        public ActionResult Save(base_shop model)
        {
            ShopResult com = new ShopResult();
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
        public ActionResult UpdateShop(base_shop model)
        {
            ComResult com = new ComResult();
            try
            {
                bool isok = _service.UpdateShop(model);
                if (isok)
                {
                    com.Msg ="更新成功";
                    com.State = 1;
                    return Json(com);
                }
                else
                {
                    com.Msg = "更新失败";
                    com.State = 0;
                    return Json(com);
                }
            }
            catch (Exception ex)
            {
                com.Msg = ex.Message;
                com.State = 0;
                return Json(com);
            }
        }

    }
}