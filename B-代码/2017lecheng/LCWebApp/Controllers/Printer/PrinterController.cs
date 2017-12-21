using BLLServices;
using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Printer
{
    public class PrinterController : Controller
    {
         private IPrint _service;
        public PrinterController(IPrint service) //构造注入     
        { 
            _service = service;
        }
        //
        // GET: /Printer/
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            List<base_wh_warehouse> depotlist = CommService.GetDepotList();
            ViewData["depotlist"] = depotlist;
            return View();
        }
        /// <summary>
        /// 打印全部的拣选单
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintAllSelect()
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {
               bool isok= _service.ComfirmPrint();
               if(isok)
               {
                   res.Msg="打印成功";
                   res.State=1;
                   return Json(res);
               }else
               {
                   res.Msg="打印失败";
                   res.State=0;
                   return Json(res);
               }
            }
            catch (Exception ex)
            {
                res.Msg = "打印失败"+ex.Message;
                res.State = 0;
                return Json(res);
            }
        }
        /// <summary>
        /// 查询已配完信息或者部分配货信息
        /// </summary>
        /// <param name="packgecode"></param>
        /// <param name="ispacked"></param>
        /// <returns></returns>
        public ActionResult SearchIspack(string packgecode, int ispacked, int pageSize, int pageIndex=1)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            //包裹号不为空的情况下，需要判断包裹号在系统中是否存在
            if (!string.IsNullOrEmpty(packgecode))
            {
                bool isok=_service.IsPackgeInSys(packgecode);
                if (!isok)
                {
                    res.Msg = "输入的包裹号在系统中不存在,请确认";
                    res.State = 0;
                    return Json(res);
                }
            }
            try
            {
                int packgecount=0;
                int count = 0;
                List<PrintSelect> list = _service.GetPrintSelectList(packgecode, ispacked, pageSize, pageIndex, out packgecount,out count);
                MyPageList mylist = new MyPageList();
                mylist.list = list;
                mylist.pageCount = packgecount;
                mylist.count = count;
                mylist.pageIndex = pageIndex;
                mylist.pageSize = pageSize;
                return Json(mylist);
            }
            catch (Exception ex)
            {
                res.State = 0;
                res.Msg = ex.Message;
                return Json(res);
            }

        }

        /// <summary>
        /// 打印拣选
        /// </summary>
        /// <returns></returns>
          [AuthorizationAttribute]
        public ActionResult PrintSelect()
        {
            return View();
        }
        public ActionResult DelUpdatePrinter(int id) //
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {

                if (!Regex.IsMatch(id.ToString(), @"^\d+$"))
                {
                    res.Msg = "ID只能是数字,且非负";
                    res.State = 0;
                    return Json(res);
                }
                bool isok = _service.DelUpdatePrinter(id);
                if (isok)
                {
                    res.Msg = "恢复成功";
                    res.URL = "/Printer/Index";
                    res.State = 1;
                    return Json(res);
                }
                else
                {
                    res.Msg = "恢复失败";
                    res.State = 0;
                    return Json(res);
                }

            }
            catch (Exception ex)
            {
                res.Msg = ex.ToString();
                res.State = 0;
                return Json(res);
            }
        }

        public ActionResult Getlist(string pagenum, string onepagecount)
        {
            DBModel.Common.ComResult com = new DBModel.Common.ComResult();
            if (!Regex.IsMatch(pagenum, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(pagenum))
            {
                com.Msg = "页数不正确";
                com.State = 0;
                return Json(com);
            }

            if (!Regex.IsMatch(onepagecount, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(onepagecount))
            {
                com.Msg = "每页数量不正确";
                com.State = 0;
                return Json(com);
            }
            int totil = 0;
            int totilpage = 0;
            string exmsg = string.Empty;
            List<myprinter> mylist = _service.Getpages(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.State = 0;
                return Json(com);
            }
            else
            {
                PrinterViewModel mylistview = new PrinterViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.State = 1;
                return Json(com);
            }
        }

        public ActionResult UpdatePrinter(string proclassfyname, string id) //DelUpdateExpress
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {
                if (!Regex.IsMatch(proclassfyname, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(proclassfyname))
                {
                    res.Msg = "分类名称不能有非法字符";
                    res.State = 0;
                    return Json(res);
                }
                if (!Regex.IsMatch(id, @"^\d+$"))
                {
                    res.Msg = "ID只能是数字,且非负";
                    res.State = 0;
                    return Json(res);
                }
                bool isok = _service.UpdatePrinter(proclassfyname, Convert.ToInt32(id));
                if (isok)
                {
                    res.Msg = "更新成功";
                    res.URL = "/Printer/Index";
                    res.State = 1;
                    return Json(res);
                }
                else
                {
                    res.Msg = "更新失败";
                    res.State = 0;
                    return Json(res);
                }

            }
            catch (Exception ex)
            {
                res.Msg = ex.ToString();
                res.State = 0;
                return Json(res);
            }
        }
        public ActionResult DeletePrinter(string id)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {
                if (!Regex.IsMatch(id, @"^\d+$"))
                {
                    res.Msg = "ID只能是数字,且非负";
                    res.State = 0;
                    return Json(res);
                }
                int isok = _service.DeletePrinter(Convert.ToInt32(id));
                if (isok == 0)
                {
                    res.Msg = "删除成功";
                    res.URL = "/Printer/Index";
                    res.State = 1;
                    return Json(res);
                }
                else if (isok == 1)
                {
                    res.Msg = "删除失败";
                    res.State = 0;
                    return Json(res);
                }
                else
                {
                    res.Msg = "此打印插件正在使用处于在线状态,请先关闭打印插件再进行删除";
                    res.State = 2;
                    return Json(res);
                }

            }
            catch (Exception ex)
            {
                res.Msg = ex.ToString();
                res.State = 0;
                return Json(res);
            }

        }
       /// <summary>
        /// 增加打印插件  
       /// </summary>
       /// <param name="printername">插件名</param>
       /// <param name="depotID">仓库ID</param>
       /// <returns></returns>
        public ActionResult AddPrinter(string printername,int depotID)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {

                if (!Regex.IsMatch(printername, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(printername))
                {
                    res.Msg = "插件名称不能有非法字符";
                    res.State = 0;
                    return Json(res);
                }
                if (string.IsNullOrEmpty(printername))
                {
                    res.Msg = "插件名称不能为空";
                    res.State = 0;
                    return Json(res);
                }
                base_print pro = new base_print();
                pro.Createtime = DateTime.Now;
                pro.del_flag = 0;
                pro.DepotID = depotID;
                pro.isonline = 0;
                pro.p_name = printername;               
                int isexit = 0;
                int id = 0;
                bool isok = _service.AddPrinter(pro, out  isexit, out id);
                if (1 == isexit)
                {
                    res.Msg = "此打印插件已存在且被删除是否恢复?";
                    res.URL = id.ToString();
                    res.State = 2;
                    return Json(res);
                }
                if (2 == isexit)
                {
                    res.Msg = "已存在此打印插件";
                    res.State = 0;
                    return Json(res);
                }
                if (isok)
                {
                    res.Msg = "添加成功";
                    res.State = 1;
                    res.URL = "/Printer/Index";
                    return Json(res);
                }
                else
                {
                    res.Msg = "添加失败";
                    res.State = 0;
                    return Json(res);
                }

            }
            catch (Exception ex)
            {
                res.Msg = ex.ToString();
                res.State = 0;
                return Json(res);
            }
        }
	}
}