using DBModel.LogisticsQuery;
using IBLLService.LogisticsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.LogisticsQuery
{
    public class LogisticsQueryController : Controller
    {
        private ILogisticsQueryService _service;
        public LogisticsQueryController(ILogisticsQueryService service) //构造注入 
        {
            _service = service;
        }
        // GET: LogisticsQuery
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetExpressList()
        {
            LogisticsQueryResult com = new LogisticsQueryResult();
            try
            {
                com = _service.GetExpressList();
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