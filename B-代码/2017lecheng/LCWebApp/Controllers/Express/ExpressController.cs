using BLLServices;
using CommHelper;
using DBModel;
using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService;
using LCWebApp.Controllers.Base;
using LCWebApp.Infrastructure;
using STO.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Express
{
    public class ExpressController : Controller
    {
        private IExpress _service;
        List<string> myexpresscode = null;

        public ExpressController(IExpress service) //构造注入     
        { 
            _service = service;
        }
        //
        // GET: /Express/UseExpressCode
          [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 得到对应未使用单号数量
        /// </summary>
        /// <returns></returns>
        public ActionResult GetExpressCodeCount(int expressid)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {
                if (expressid<=0)
                {
                    res.Msg = "选择的快递类型有问题";
                    res.State = 0;
                    return Json(res);
                }
                int count = _service.GetExpressCodeCount(expressid);
                return Json(count);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 使用快递单号页面 
        /// </summary>
        /// <returns></returns>
       [AuthorizationAttribute]
        public ActionResult UseExpressCode()
        {           
            List<base_exp_comp> expresslist = CommService.GetExpress();
            ViewData["Expresslist"] = expresslist;
            return View();
        }
        /// <summary>
        /// 关联单号页面
        /// </summary>
        /// <returns></returns>
        [AuthorizationAttribute]
        public ActionResult AssociateExpressCode()
        {
            List<base_exp_comp> expresslist = CommService.GetExpress();
            ViewData["Expresslist"] = expresslist;
            return View();
        }

        /// <summary>
        /// 转运单和DHL单号关联页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AssociateDHLExpressCode()
        {
            return View();
        }

        /// <summary>
        /// 转运单和DHL单号关联
        /// </summary>
        /// <returns></returns>
        public ActionResult AssociateDHLExpressCodeByExcel(string dhlcode, string zhuanyuncode)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {
                if (string.IsNullOrEmpty(dhlcode) || string.IsNullOrEmpty(zhuanyuncode))
                {
                    res.State = 0;
                    res.Msg = "DHL单号和转运单号不能为空";
                    return Json(res);
                }
                bool isok = _service.AssociateDHLExpressCode(dhlcode, zhuanyuncode);
                if (isok)
                {
                    res.Msg = "关联成功！";
                    res.State = 1;
                    return Json(res);
                }
                else
                {
                    res.Msg = "关联单号失败";
                    res.State = 0;
                    return Json(res);
                }
            }
            catch (Exception ex)
            {
                res.Msg = "关联单号失败" + ex.Message;
                res.State = 0;
                return Json(res);
            }
        }

        [HttpPost]
        public ActionResult AssociateExpressCodeDetail(HttpPostedFileBase file, int expressid, string expressname)
        {
            string[] extends = { ".xls", ".xlsx" };
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            int count = _service.GetExpressCodeCount(expressid);//得到可用的单号数量
            string filePath = "~/UploadOrder/";
            string uploadpath = Server.MapPath(filePath);
            Directory.CreateDirectory(uploadpath);//服务器路径
            string strExtension1 = Path.GetExtension(file.FileName);
            if (!extends.Contains(strExtension1))
            {
                res.Msg = "文件格式不正确，必须是excel文件";
                res.State = 0;
                return Json(res);
            }
            string fFullDir = uploadpath + file.FileName;
            file.SaveAs(fFullDir); //保存到硬盘
          
            DataTable csvtable = ExcelHelper.ReadExcel(fFullDir);
            if (csvtable.Rows.Count > 0)//如果读取成功就删除
            {
                //删除文件
                FileHelper.FileDel(fFullDir);
            }
            //1.判断datatabel是否有包裹号这个表头
            if (!csvtable.Columns.Contains("包裹号") || !csvtable.Columns.Contains("快递单号"))
            {
                res.Msg = "Excel表头信息有误,请按照模板格式";
                res.State = 0;
                return Json(res);
            }
            try
            {
                int usenum = _service.AssociateExpressCode(expressid, csvtable);
                if (usenum > 0)
                {
                    res.Msg = "一共关联" + usenum.ToString() + "个  " + expressname + "   单号";
                    res.State = 1;
                    return Json(res);
                }
                else
                {
                    res.Msg = "关联单号失败";
                    res.State = 0;
                    return Json(res);
                }
            }
            catch (Exception ex)
            {
                res.Msg = "关联单号失败"+ex.Message;
                res.State = 0;
                return Json(res);
            }
          

        }
        /// <summary>
        /// 使用快递单号
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UseExpressCodeDetail(HttpPostedFileBase file, int expressid, string expressname)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            int count = _service.GetExpressCodeCount(expressid);//得到可用的单号数量
            string filePath = "~/UploadOrder/";
            string uploadpath = Server.MapPath(filePath);
            Directory.CreateDirectory(uploadpath);//服务器路径
            string fFullDir = uploadpath + file.FileName;
            file.SaveAs(fFullDir); //保存到硬盘
            DataTable csvtable = ExcelHelper.ReadExcel(fFullDir);
            if (csvtable.Rows.Count > 0)//如果读取成功就删除
            {
                //删除文件
                FileHelper.FileDel(fFullDir);
            }
            if (csvtable.Rows.Count > count)
            {
                res.Msg = "Excel表格中的包裹数大于可以使用的单号数";
                res.State = 0;
                return Json(res);
            }
            //1.判断datatabel是否有包裹号这个表头
            if (!csvtable.Columns.Contains("包裹号"))
            {
                res.Msg = "Excel表头信息有误,请按照模板格式";
                res.State = 0;
                return Json(res);
            }
            int usenum=_service.UseExpressCode(expressid, csvtable);
            if (usenum > 0)
            {
                res.Msg = "使用单号成功，一共使用" + usenum.ToString() + "个  " + expressname + "   单号";
                res.State = 1;
                return Json(res);
            }
            else
            {
                res.Msg = "使用单号失败";
                res.State = 0;
                return Json(res);
            }
             
        }
       
          [AuthorizationAttribute]
        public ActionResult ExportSelectExpress()
        {
           List<base_exp_comp> expresslist= CommService.GetExpress();
           ViewData["Expresslist"] = expresslist;
            return View();
        }


        /// <summary>
        /// 导出已选择快递到excel  string packgecode, int ispacked, int pageSize, int pageIndex=1
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportToExcel(int expressid, int ispacked,DateTime? starttime, DateTime? endtime)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            //if (1==ispacked)
            //{
            //    res.Msg = "已导出包裹不能再导";
            //    res.State = 0;
            //    return Json(res);
            //}
            int count = 0;
            List<ExpressPackgeModel> list = _service.GetExpressPackgeAllList(expressid, ispacked, out count, starttime, endtime);
            ///导出数据
            string mymsg = string.Empty;
            string filename = "Excel" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string FilePath = "\\DownExcel\\" + filename;   // AppDomain.CurrentDomain.BaseDirectory + 
            string expressname = CommService.GetExpressByID(expressid);
            //bool excisok = ExcelHelper.ListToExcel<ExportViewmodel>(newlist, FilePath, out mymsg);
            DataTable dt = new DataTable();
            dt.Columns.Add("发货时间", typeof(string));
            dt.Columns.Add("包裹号", typeof(string));
            dt.Columns.Add("客户名", typeof(string));
            dt.Columns.Add("客户前地址", typeof(string));
            dt.Columns.Add("客户后地址", typeof(string));
            dt.Columns.Add("客户电话", typeof(string));
            dt.Columns.Add("客户邮编", typeof(string));
            dt.Columns.Add("仓库地址", typeof(string));
            dt.Columns.Add("仓库邮编", typeof(string));
            dt.Columns.Add("店铺名称", typeof(string));
            dt.Columns.Add("仓库电话", typeof(string));
            dt.Columns.Add("sku", typeof(string));
            dt.Columns.Add("打印sku1", typeof(string));
            dt.Columns.Add("打印sku2", typeof(string));
            dt.Columns.Add("发货类型", typeof(string));
            dt.Columns.Add("请求代号", typeof(string));
            dt.Columns.Add("请求管理代号", typeof(string));
            dt.Columns.Add("运送管理代号", typeof(string));
            dt.Columns.Add("客户管理番号", typeof(string));

            foreach (var item2 in list)
            {
                DataRow dr = dt.NewRow();
                dr["发货时间"] = item2.sendtime;
                dr["包裹号"] = item2.packgecode;
                dr["客户名"] = item2.custname;
                dr["客户前地址"] = item2.custaddress1;
                dr["客户后地址"] = item2.custaddress2;
                if (item2.custphone.Length < 3)//说明为空
                {
                    dr["客户电话"] = item2.custmobile;
                }
                else
                {
                    dr["客户电话"] = item2.custphone;
                }
                dr["客户邮编"] = item2.custzip;
                dr["仓库地址"] = item2.shopaddress;
                dr["仓库邮编"] = item2.shopzip;
                dr["店铺名称"] = item2.shopname;
                dr["仓库电话"] = item2.shopphone;
                dr["sku"] = item2.sku;
                dr["打印sku1"] = item2.sku1;
                dr["打印sku2"] = item2.sku2;
                if (expressname == "yamato")
                {
                    dr["发货类型"] = 3;
                } else if (expressname == "宅急便")
                {
                    dr["发货类型"] = 0;
                }
                else
                {
                    dr["发货类型"] = 1;//其他暂时为 1 
                }
                dr["请求代号"] = item2.daihao;
                dr["请求管理代号"] = item2.mangagedaihao;
                dr["运送管理代号"] = item2.senddaihao;
                dr["客户管理番号"] = item2.packgecode;
                dt.Rows.Add(dr);
            }
            int a = dt.Rows.Count;
            string error = string.Empty;
            bool excisok = ExcelHelper.ExcelOutResult(FilePath, dt, "", out  error);
            if (excisok)
            {
                res.Msg = "导出成功";
                res.State = 1;
                res.URL = "../DownExcel/" + filename; 
                return Json(res);
            }
            else
            {
                res.Msg = "导出失败";
                res.State = 0;
                return Json(res);
            }
        }
        /// <summary>
        /// 查询快递类型包裹是否导出
        /// </summary>
        /// <param name="expressid"></param>
        /// <param name="ispacked"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Serachpacket(int expressid, int ispacked, int pageSize, DateTime? starttime, DateTime? endtime, int pageIndex = 1)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {
                int packgecount = 0;
                int count = 0;
                List<ExpressPackgeModel> list = _service.GetExpressPackgeList(expressid, ispacked, pageSize, pageIndex, out packgecount, out count, starttime, endtime);
                MyPageList pagelist = new MyPageList();
                pagelist.count = count;
                pagelist.list = list;
                pagelist.pageCount = packgecount;
                pagelist.pageIndex = pageIndex;
                pagelist.pageSize = pageSize;
                return Json(pagelist);
            }
            catch (Exception ex)
            {
                res.State = 0;
                res.Msg = ex.Message;
                return Json(res);
            }
        }
          [AuthorizationAttribute]
        public ActionResult Import()
        {
            return View();
        }
          [AuthorizationAttribute]
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult SearchExp(int express, int pageindex, int isuse)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {
                int totil = 0;
                List<base_expresscode> expcodelist=_service.Searchexp( express,  pageindex,  isuse,out totil);
                res.DataResult = expcodelist;
                res.State = 1;
                res.URL = totil.ToString();
                int pages = totil / 20;
                if (totil % 20 > 0)
                {
                    pages++;
                }
                res.Msg = pages.ToString();
                return Json(res);
            }
            catch (Exception ex)
            {
                res.State = 0;
                res.Msg = ex.ToString();
                return Json(res);
            }
        }
        private List<string> AUTOgetcode(string startcode, string endcode)
        {
            if (Convert.ToInt64(startcode) >= Convert.ToInt64(endcode))
            {
                //MessageBox.Show("你所输入的单号区间存在问题，请确认！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                throw new Exception("你所输入的单号区间存在问题，请确认！");
            }

            string beforecode = startcode.Substring(0, 11);
            long beforecode2 = Convert.ToInt64(beforecode);
            string lastcode = startcode.Substring(11, 1);
            long lastcode2 = Convert.ToInt64(lastcode);
            List<string> expcode = new List<string>();
            string dr2 = startcode;
            expcode.Add(dr2);
            long endcode2 = Convert.ToInt64(endcode);  //结束单号

            long comparre = Convert.ToInt64(startcode);
            //long newexpresscode = 0;
            while (comparre < endcode2)
            {
                if (lastcode2 < 6)
                {
                    lastcode2 = lastcode2 + 1;
                }
                else
                {
                    lastcode2 = 0;
                }
                beforecode2 = beforecode2 + 1;
                comparre = beforecode2 * 10 + lastcode2;               
                string dr = comparre.ToString();
                expcode.Add(dr);
            }

            return expcode;
        }
        /// <summary>
        /// 生成单号区间
        /// </summary>
        /// <param name="express"></param>
        /// <param name="scode"></param>
        /// <param name="lcode"></param>
        /// <returns></returns>
        public ActionResult CreateExpressCode(int express ,string scode,string lcode,int pageindex,int insys)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {
                string[] nexpcode = null;
                myexpresscode=AUTOgetcode(scode, lcode);
                if (insys == 1)
                {
                    List<base_expresscode> listexp=new List<base_expresscode> ();
                    foreach(var item in myexpresscode)
                    {
                         base_expresscode aa = new base_expresscode();
                         aa.express_id = express;
                         aa.Expresscode = item;
                         aa.importtime = DateTime.Now;
                         aa.isuse = false;
                         aa.packgecode = "";
                         aa.Usetime = null;
                         listexp.Add(aa);
                    }
                    bool isok=_service.Import(listexp);
                    if (isok)
                    {
                        res.State = 2;
                        res.Msg = "单号进入系统成功";
                        return Json(res);
                    }
                    else
                    {
                        res.State = 0;
                        res.Msg = "单号进入系统失败";
                        return Json(res);
                    }
                }
                else
                {
                    nexpcode = myexpresscode.Skip(20 * (pageindex - 1)).Take(20).ToArray();
                    res.DataResult = nexpcode;
                    res.State = 1;
                    res.URL = myexpresscode.Count.ToString();
                    int pages = myexpresscode.Count / 20;
                    if (myexpresscode.Count % 20 > 0)
                    {
                        pages++;
                    }
                    res.Msg = pages.ToString();
                    return Json(res);
                }
               
            }
            catch (Exception ex)
            {
                res.State = 0;
                res.Msg = ex.ToString();
                return Json(res);
            }
           
        }
        public ActionResult DeleteExpress(string id)
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
                  int isok = _service.DeleteExpress(Convert.ToInt32(id));
                  if (isok==0)
                  {
                      res.Msg = "删除成功";
                      res.URL = "/Express/Index";
                      res.State = 1;
                      return Json(res);
                  }
                  else if(isok==1)
                  {
                      res.Msg = "删除失败";
                      res.State = 0;
                      return Json(res);
                  }
                  else
                  {
                      res.Msg = "此快递类型在系统中有未使用的单号，不能删除";
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
        public ActionResult DelUpdateExpress(int id) //
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
                bool isok = _service.DelUpdateExpress(id);
                if (isok)
                {
                    res.Msg = "恢复成功";
                    res.URL = "/Express/Index";
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
        public ActionResult UpdateExpress(string proclassfyname, string id) //DelUpdateExpress
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
                bool isok = _service.UpdateExpress(proclassfyname, Convert.ToInt32(id));
                if (isok)
                {
                    res.Msg = "更新成功";
                    res.URL = "/Express/Index";
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
        /// <summary>
        /// 添加快递类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddExpress(string expressname)
        {
            DBModel.Common.ComResult res = new DBModel.Common.ComResult();
            try
            {

                if (!Regex.IsMatch(expressname, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(expressname))
                {
                    res.Msg = "分类名称不能有非法字符";
                    res.State = 0;
                    return Json(res);
                }
                if (string.IsNullOrEmpty(expressname))
                {
                    res.Msg = "分类名称不能为空";
                    res.State = 0;
                    return Json(res);
                }
                base_exp_comp pro = new base_exp_comp();
                pro.country_id = 0; ;
                pro.create_time = DateTime.Now;
                pro.create_user_id = LoginUser.Current.user_id;
                pro.del_flag = true;
                pro.del_time = DateTime.Now;
                pro.del_user_id = 0;
                pro.edit_time = DateTime.Now;
                pro.edit_user_id=0;
                pro.express_descrip = "";
                pro.express_name = expressname;
                pro.express_status = true;
                pro.remark = "";

                int isexit = 0;
                int id = 0;
                bool isok = _service.AddExpress(pro, out  isexit, out id);
                if (1 == isexit)
                {
                    res.Msg = "此快递类型已存在且被删除是否恢复?";
                    res.URL = id.ToString();
                    res.State = 2;
                    return Json(res);
                }
                if (2 == isexit)
                {
                    res.Msg = "已存在此快递类型";
                    res.State = 0;
                    return Json(res);
                }
                if (isok)
                {
                    res.Msg = "添加成功";
                    res.State = 1;
                    res.URL = "/Express/Index";
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
            List<base_exp_comp> mylist = _service.Getpages(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.State = 0;
                return Json(com);
            }
            else
            {
                ExpressViewModel mylistview = new ExpressViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.State = 1;
                return Json(com);
            }
        }
	}
}