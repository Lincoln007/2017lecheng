using DBModel.StoreSales;
using DBModel.ViewModel;
using IBLLService.StoreSales;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.StoreSales
{
    public class StoreSalesController : Controller
    {

        private IStoreSalesService _service;
        public StoreSalesController(IStoreSalesService service) //构造注入 
        {
            _service = service;
        }
        // GET: StoreSales
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetStoreSalesInfo(Int64? platform_id, Int64? shop_id, DateTime? start_time, DateTime? end_time)
        {
            StoreSalesResult com = new StoreSalesResult();
            if (platform_id == 0)
            {
                com.success = false;
                com.Msg = "请选择平台!";
                return Json(com);
            }
            if (shop_id == 0)
            {
                com.success = false;
                com.Msg = "请填写店铺!";
                return Json(com);
            }
            if (!start_time.HasValue)
            {
                com.success = false;
                com.Msg = "开始时间不得为空!";
                return Json(com);
            }
            if (start_time > end_time)
            {
                com.success = false;
                com.Msg = "结束时间不得小于开始时间!";
                return Json(com);
            }
            DateTime torrow = end_time.Value.AddDays(1);
            string exmsg = string.Empty;
            List<StoreSalesModel> mylist = _service.GetStoreSalesInfo(platform_id, shop_id, start_time, torrow, out exmsg);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                StoreSalesViewModel mylistview = new StoreSalesViewModel();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }

        [HttpPost]
        public ActionResult GetShopList(Int64? platform_id)
        {
            StoreSalesResult com = new StoreSalesResult();
            try
            {
                com = _service.GetShopList(platform_id.Value);
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