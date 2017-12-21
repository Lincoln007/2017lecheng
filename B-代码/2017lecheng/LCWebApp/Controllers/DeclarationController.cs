using IBLLService;
using LCWebApp.Controllers.Base;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers
{
    public class DeclarationController : Controller
    {
        private IDeclaration _service;
        public DeclarationController(IDeclaration service) //构造注入 
        { 
            _service = service;
        }
        //
        // GET: /Declaration/
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 生成excel报关单
        /// </summary>
        /// <param name="dhlcode">DHL单号</param>
        /// <param name="zhuanyuncode">转运单号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDeclarationExcel(string dhlcode, string zhuanyuncode)
        {
            ComResult com = new ComResult();
            if (string.IsNullOrEmpty(dhlcode) || string.IsNullOrEmpty(zhuanyuncode))
            {
                com.State = 0;
                com.Msg = "DHL单号和转运单号不能为空";
                return Json(com);
            }
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";
            string path = "/DownExcel/";
            string fullpath=Server.MapPath(path)+filename;
            bool isok=_service.IsExitZY(zhuanyuncode);
            if (!isok)
            {
                com.State = 0;
                com.Msg = "转运单号不存在";
                return Json(com);
            }           
            try
            {
                string excelurl = _service.GetDeclarationExcelUrl(dhlcode, zhuanyuncode, fullpath);
                com.State = 1;
                com.URL = "../DownExcel/" + filename;
                return Json(com);
            }
            catch (Exception ex)
            {
                com.State = 0;
                com.Msg = "生成失败原因:"+ex.Message;
                return Json(com);
            }
        }
	}
}