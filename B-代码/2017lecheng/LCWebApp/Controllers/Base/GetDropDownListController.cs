using System;
using System.Web.Mvc;
using BLLServices;
using System.Text.RegularExpressions;
using DBModel.ViewModel;
using LCWebApp.Infrastructure;

namespace LCWebApp.Controllers.Base
{
    public class GetDropDownListController : Controller
    {
        //
        // GET: /GetDropDownList/  GetExpress
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetExpress()
        {
            try
            {
                var proclylist = CommService.GetExpress();
                return Json(proclylist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult GetPlatForms()
        {
            try
            {
                var platlist = CommService.GetPlatformList();
                return Json(platlist);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
          
        }
        [HttpPost]
        public ActionResult GetShops(string platformID)
        {
            try
            {
                if (string.IsNullOrEmpty(platformID))
                {
                    throw new Exception("店铺ID错误");
                }
                var shoplist = CommService.GetShopList(platformID);
                return Json(shoplist);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
       
        }
        /// <summary>
        /// 得到所有商品分类
        /// </summary>
        /// <param name="platformID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductClasfly()
        {
            try
            {
                var proclylist = CommService.GetProclsflyList();
                return Json(proclylist);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DeletePSKU(string id)   //AddPSKU
        {
            ComResult com = new ComResult();
            try
            {
                var isok = CommService.DeletePSKU(Convert.ToInt32(id));
                if (isok)
                {
                    com.State = 1;
                    com.Msg = "删除成功";
                }
                else {
                    com.State = 0;
                    com.Msg = "删除失败";
                }
                return Json(com);
            }
            catch (Exception ex) 
            {
                com.State = 0;
                com.Msg = "删除失败，原因："+ex.ToString();
                return Json(com);
            }
       
        }


        [HttpPost]
        public ActionResult AddPSKU(string  PSKU, string SSKU, string GSKU,string shopid)   
        {
            ComResult com = new ComResult();
            try
            {
                if (!Regex.IsMatch(shopid, @"^\+?[1-9][0-9]*$") && !string.IsNullOrEmpty(shopid))
                {
                    com.Msg = "只能是数字且不能为空";
                    com.State = 0;
                    return Json(com);
                }
                var isok = CommService.AddPSKU(PSKU, SSKU, GSKU, Convert.ToInt32(shopid));
                if (isok)
                {
                    com.State = 1;
                    com.Msg = "添加成功";                   
                }
                else
                {
                    com.State = 0;
                    com.Msg = "添加失败";
                }
                return Json(com);
            }
            catch (Exception ex)
            {
                com.State = 0;
                com.Msg = "添加失败，原因：" + ex.ToString();
                return Json(com);
            }

        }

        [HttpPost]
        public ActionResult GetPSKUpage( string pagenum, string onepagecount, string shopID)
        {
            ComResult com = new ComResult();
            try
            {
                if (!Regex.IsMatch(pagenum, @"^\+?[1-9][0-9]*$") && !string.IsNullOrEmpty(pagenum))
                {
                    com.Msg = "只能是数字";
                    com.State = 0;
                    return Json(com);
                }
                if (!Regex.IsMatch(onepagecount, @"^\+?[1-9][0-9]*$") && !string.IsNullOrEmpty(onepagecount))
                {
                    com.Msg = "只能是数字";
                    com.State = 0;
                    return Json(com);
                }
                if (!Regex.IsMatch(shopID, @"^\+?[1-9][0-9]*$") && !string.IsNullOrEmpty(shopID))
                {
                    com.Msg = "只能是数字";
                    com.State = 0;
                    return Json(com);
                }
                if (string.IsNullOrEmpty(shopID))
                {
                    com.Msg = "没有选择店铺";
                    com.State = 0;
                    return Json(com);
                }
                int totil = 0;
                int totilpage = 0;
                var PSKUlist = CommService.GetPSKUListByshopID(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), Convert.ToInt32(shopID), out totil, out totilpage);
                PSKUViewmodel mypkskulist = new PSKUViewmodel();
                mypkskulist.PSKUlist = PSKUlist;
                mypkskulist.totilcount = totil.ToString();
                mypkskulist.totilpage = totilpage.ToString();
                com.State = 1;
                com.DataResult = mypkskulist;
                return Json(com);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

	}
}