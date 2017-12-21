using IBLLService;
using LCWebApp.Controllers.Base;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Product
{
    public class NewProductController : Controller
    {
        private INewProduct _service;
        public NewProductController(INewProduct service) //构造注入  
        {
            _service = service;
        }
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMoldelList()
        {
            return View();
        }
        public ActionResult AddNewProduct(string code, string proname, string bgname,string bgcode, string bgprice, string weight, string price, string remark, string clasf, string profly, string promodel, string prosize, List<string> skulist)
        {
            ComResult com = new ComResult();
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    com.State = 0;
                    com.Msg = "款号不能为空";
                    return Json(com);
                }
                //if (!Regex.IsMatch(code, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(code))
                //{
                //    com.Msg = "款号不能包含特殊字符";
                //    com.State = 0;
                //    return Json(com);
                //}
                if (skulist.Count <= 0)
                {
                    com.Msg = "SKU列表为空";
                    com.State = 0;
                    return Json(com);
                }
                bool isok = _service.InsertProdcut(code, proname, bgname,bgcode, bgprice, weight, price, remark, clasf, profly, promodel, prosize, skulist);
                if (isok)
                {
                    com.Msg = "添加成功";
                    com.State = 0;
                    return Json(com);
                }
                else
                {
                    com.Msg = "添加失败";
                    com.State = 0;
                    return Json(com);
                }
            }
            catch (Exception ex)
            {
                com.Msg = "添加失败:原因"+ex.ToString();
                com.State = 0;
                return Json(com);
            }
        }

        public ActionResult UpdateNewProduct(string code, string proname, string bgname,string bgcode, string bgprice, string weight, string price, string remark, string profly, string promodel, string prosize, int proid, int prostatus,string purchase_url)
        {
            ComResult com = new ComResult();
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    com.State = 0;
                    com.Msg = "款号不能为空";
                    return Json(com);
                }
                //if (!Regex.IsMatch(code, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(code))
                //{
                //    com.Msg = "款号不能包含特殊字符";
                //    com.State = 0;
                //    return Json(com);
                //}
                bool isok = _service.UpdateProdcut(code, proname, bgname,bgcode, bgprice, weight, price, remark, profly, promodel, prosize, proid, prostatus, purchase_url);
                if (isok)
                {
                    com.Msg = "更新成功";
                    com.State = 0;
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
                com.Msg = "更新失败:原因" + ex.ToString();
                com.State = 0;
                return Json(com);
            }
        }
        /// <summary>
        /// 判断款号是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult PcodeIsEixt(string code)
        {
            ComResult com = new ComResult();
            if (string.IsNullOrEmpty(code))
            {
                com.State = 0;
                com.Msg = "款号不能为空";
                return Json(com);
            }
            //if (!Regex.IsMatch(code, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(code))
            //{
            //    com.Msg = "款号不能包含特殊字符";
            //    com.State = 0;
            //    return Json(com);
            //}
            bool isexit=_service.PcodeIsExit(code);
            if (isexit)
            {
                com.Msg = "款号已经存在";
                com.State = 0;
                return Json(com);
            }
            else
            {
                com.Msg = "该款号可以使用";
                com.State = 1;
                return Json(com);
            }
           
        }
        
	}
}