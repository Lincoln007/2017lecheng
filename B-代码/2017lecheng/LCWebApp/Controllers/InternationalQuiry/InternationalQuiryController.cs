using CommHelper;
using DBModel.InternationalQuiry;
using DBModel.ViewModel;
using IBLLService.InternationalQuiry;
using Jade.Lecheng.Server;
using Jade.Lecheng.Server.tool;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.InternationalQuiry
{
  
    public class InternationalQuiryController : Controller
    {

        private IInternationalQuiryService _service;
        public InternationalQuiryController(IInternationalQuiryService service) //构造注入 
        {
            _service = service;
        }

        // GET: InternationalQuiry
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetExpressList()
        {
            InternationalQuiryResult com = new InternationalQuiryResult();
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


        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount, int? express_id, string express_code)
        {
            InternationalQuiryResult com = new InternationalQuiryResult();
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
            List<InternationalQuiryModel> mylist = _service.GetInternationalQuiryList(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil,
                out totilpage, out exmsg, express_id, express_code);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                InternationalQuiryViewModel mylistview = new InternationalQuiryViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }

        /// <summary>
        /// 获取快递信息
        /// </summary>
        /// <param name="code">快递信息单号信息(227865099041|STO)</param>
        /// <returns></returns>
        public ActionResult GetKDContent(string code)
        {
            InternationalQuiryResult com = new InternationalQuiryResult();
            try
            {
                code = code.Replace("<br/>", ",");
                StringBuilder kdContent = new StringBuilder();
                List<string> returnStrList = new List<string>();
                string[] mails = code.Split(',');
                foreach (string item in mails)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] kd = item.Split('|');
                        if (kd.Length == 2)//如果只有一个物流单号的
                        {
                            string requestData = "{'OrderCode':'','ShipperCode':'" + kd[1] + "','LogisticCode':'" + kd[0] + "'}";
                            string returnStr = KdApiSearch.getOrderTracesByJson(requestData);//返回的快递信息数据
                            ExpressMode em = JsonHelper.DeserializeJsonToObject<ExpressMode>(returnStr);//反序列化json数据
                            returnStrList.Add(returnStr);
                        }
                    }
                }
                if (returnStrList.Count > 0)
                {
                    kdContent.Append("[");
                    for (int i = 0; i < returnStrList.Count; i++)
                    {
                        if (i == 0)
                        {
                            kdContent.Append(returnStrList[i]);
                        }
                        else
                        {
                            kdContent.Append("," + returnStrList[i]);
                        }
                    }
                    kdContent.Append("]");
                }
                com.DataResult = kdContent.ToString();
                com.success = true;
                return Json(com);
            }
            catch (Exception ex)
            {
                com.Msg = "参数错误!"+ex.Message;
                com.success = false;
                return null;
            }
        }
    }


}