using IBLLService.Purchase;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Purchase
{
    public class PurchaseController : Controller
    {
        private IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        // GET: Purchase
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加库存采购
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="num"></param>
        /// <param name="url"></param>
        /// <param name="supId"></param>
        /// <returns></returns>
        public ActionResult AddPurchaseService(string sku, int num, int supId)
        {
            return Json(_purchaseService.AddPurchaseService(sku, num, supId));
        }
    }
}