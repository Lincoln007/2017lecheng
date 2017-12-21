using BLLServices;
using DBModel.Common;
using DBModel.DBmodel;
using IBLLService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers
{
    public class ExportCSVController : Controller
    {
        private IExportCSV _service;
        public ExportCSVController(IExportCSV service) //构造注入 
        {
            _service = service;
        }
        // GET: ExportCSV
        public ActionResult Index()
        {
            List<base_shop> shoplist = CommService.GetShopList();
            List<base_exp_comp> expresslist = CommService.GetExpress();
            ViewData["Expresslist"] = expresslist;
            ViewData["shoplist"] = shoplist;
            return View();
        }


        public ActionResult ExportReturnPlat(string zhuanyuncode, long express, long shop)
        {
            ComResult com = new ComResult();
            if (string.IsNullOrEmpty(zhuanyuncode))
            {
                com.State = 0;
                com.Msg = "转运单号不能为空";
                return Json(com);
            }
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string path = "/DownExcel/";
            string fullpath = Server.MapPath(path) + filename;
            bool isok = _service.IsExitZY(zhuanyuncode);
            if (!isok)
            {
                com.State = 0;
                com.URL = "转运单号不存在";
                return Json(com);
            }
            try
            {
                bool isok2 = _service.GetDeclarationDHLExcelUrl(zhuanyuncode, fullpath, express, shop);
                if (isok2)
                {
                    com.State = 1;
                    com.URL = "../DownExcel/" + filename;
                    return Json(com);
                }
                else
                {
                    com.State = 0;
                    com.URL = "生成失败!"; 
                    return Json(com);
                }
            }
            catch (Exception ex)
            {
                com.State = 0;
                com.Msg = "生成失败原因:" + ex.Message;
                return Json(com);
            }
        }
    }
}