using BLLServices;
using IBLLService;
using IBLLService.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBModel.ViewModel;
using System.IO;
using LCWebApp.Controllers.Base;
using System.Collections.Specialized;
using CommHelper;
using System.Data;
using DBModel.DBmodel;
using System.Text;
using System.Text.RegularExpressions;
using DBModel.Order;
using DBModel.ViewModel.Order;
using LCWebApp.Infrastructure;
using STO.Utility;

namespace LCWebApp.Controllers.Order
{

    public class ImportOrderController : Controller
    {
        private IOrderService _service;
        public ImportOrderController(IOrderService service) //构造注入  
        {
            _service = service;
        }
        //
        // GET: /ImportOrder/ 
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            List<base_platform> platformlist = CommService.GetPlatformList();
            ViewData["platformlist"] = platformlist;
            return View();
        }

        /// <summary>
        /// 插入正式库
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="platformID"></param>
        /// <returns></returns>
        public ActionResult InsertOrder(string shopID, string platformID, List<base_LSorder> list)
        {

            ComResult com = new ComResult();
            try
            {
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
                if (!Regex.IsMatch(platformID, @"^\+?[1-9][0-9]*$") && !string.IsNullOrEmpty(shopID))
                {
                    com.Msg = "只能是数字";
                    com.State = 0;
                    return Json(com);
                }
                if (string.IsNullOrEmpty(platformID))
                {
                    com.Msg = "没有选择平台";
                    com.State = 0;
                    return Json(com);
                }
                int lvcount = 0;
                int count = _service.InsertOrder(Convert.ToInt32(platformID), Convert.ToInt32(shopID), list, out lvcount);
                if (count > 0)
                {
                    com.Msg = "解析成功，一共插入" + count.ToString() + "条订单数据,过滤了" + lvcount + "条数数据,请查看并删除过滤的数据!";
                    com.State = 1;
                    return Json(com);
                }
                else
                {
                    com.Msg = "解析失败";
                    com.State = 0;
                    return Json(com);
                }

            }
            catch (Exception ex)
            {
                com.Msg = ex.Message;
                com.State = 0;
                return Json(com);
            }
        }
        public ActionResult UpdateLSSKU(string shopID, string id, string clientname, string sku, string num, string telephone, string phone, string address)
        {
            ComResult com = new ComResult();
            try
            {
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
                bool isok = _service.UpdateLStable(Convert.ToInt32(shopID), Convert.ToInt32(id), clientname, sku, num, telephone, phone, address);
                if (isok)
                {
                    com.Msg = "更新成功";
                    com.State = 1;
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
                com.Msg = "更新失败" + ex.ToString();
                com.State = 0;
                return Json(com);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public ActionResult DeleteLs(string id, string shopID)
        {
            ComResult com = new ComResult();
            try
            {
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
                bool isok = _service.DeleteLStable(Convert.ToInt32(shopID), Convert.ToInt32(id));
                if (isok)
                {
                    com.Msg = "删除成功";
                    com.State = 1;
                    return Json(com);
                }
                else
                {
                    com.Msg = "删除失败";
                    com.State = 0;
                    return Json(com);
                }

            }
            catch (Exception ex)
            {
                com.Msg = "删除失败" + ex.ToString();
                com.State = 0;
                return Json(com);
            }

        }
        public ActionResult GetLSpage(int shopID)//string pagenum, string onepagecount,
        {
            ComResult com = new ComResult();
            try
            {
                //if (!Regex.IsMatch(pagenum, @"^\+?[1-9][0-9]*$") && !string.IsNullOrEmpty(pagenum))
                //{
                //    com.Msg = "只能是数字";
                //    com.State = 0;
                //    return Json(com);
                //}
                //if (!Regex.IsMatch(onepagecount, @"^\+?[1-9][0-9]*$") && !string.IsNullOrEmpty(onepagecount))
                //{
                //    com.Msg = "只能是数字";
                //    com.State = 0;
                //    return Json(com);
                //}
                if (!Regex.IsMatch(shopID.ToString(), @"^\+?[1-9][0-9]*$") && !string.IsNullOrEmpty(shopID.ToString()))
                {
                    com.Msg = "只能是数字";
                    com.State = 0;
                    return Json(com);
                }
                if (string.IsNullOrEmpty(shopID.ToString()))
                {
                    com.Msg = "没有选择店铺";
                    com.State = 0;
                    return Json(com);
                }
                int totil = 0;
                //int totilpage = 0;
                string exmsg = string.Empty;
                var PSKUlist = _service.SearchLStable(shopID, out  totil, out  exmsg);
                MyPageList pagelist = new MyPageList();
                pagelist.count = totil;
                pagelist.list = PSKUlist;
                //pagelist.pageCount = packgecount;
                //pagelist.pageIndex = pageIndex;
                //pagelist.pageSize = pageSize;
                return Json(pagelist);

                //LSOrderViewmodel mypkskulist = new LSOrderViewmodel();
                //mypkskulist.PSKUlist = PSKUlist;
                //mypkskulist.totilcount = totil.ToString();
                ////mypkskulist.totilpage = totilpage.ToString();
                //com.State = 1;
                //com.DataResult = mypkskulist;
                //return Json(com);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过时间查询订单数量
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [AuthorizationAttribute]
        public ActionResult GetOrderCountByTime()
        {
            string time = Request["time"] == null ? "" : Request["time"].ToString();
            var result = _service.GetOrderCountByTime(time);
            //获取供应商信息
            var suplist = _service.GetSupPlier();
            ViewBag.NowTime = DateTime.Now.ToShortDateString();
            ViewBag.Result = result;
            ViewBag.ShowTime = time;
            ViewBag.SupList = suplist;
            return View();
        }
        /// <summary>
        /// 通过店铺ID获取店铺订单
        /// </summary>
        /// <param name="shop_id"></param>
        /// <returns></returns>
        public ActionResult GetShopOrderById(long shop_id = 0, int pageIndex = 1)
        {
            var pageSize = 10;
            var count = 0;
            string time = Request["date"] == null ? "" : Request["date"].ToString();
            var result = _service.GetShopOrderById(shop_id, pageIndex, pageSize, out count, time);
            //计算页数
            var size = count % pageSize > 0 ? count / pageSize + 1 : count / pageSize;
            ViewBag.Size = size;
            ViewBag.ShopId = shop_id;
            ViewBag.List = result;
            ViewBag.pageIndex = pageIndex;
            ViewBag.ShowTime = time;
            return View();
        }

        /// <summary>
        /// 通过订单ID获取作业表信息
        /// </summary>
        /// <param name="shop_id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetOrderWorkById(long order_id = 0)
        {

            OrderResult com = new OrderResult();
            string exmsg = string.Empty;
            List<OrderWorkE> mylist = _service.GetOrderEById(order_id, out exmsg);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.success = false;
                return Json(com);
            }
            else
            {
                OrderViewModel mylistview = new OrderViewModel();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);
            }
        }



        [HttpPost]
        public ActionResult GetInfoByID(Int64? order_id)
        {
            OrderResult com = new OrderResult();
            try
            {
                List<busi_sendorder> mylist = _service.GetInfoByID(order_id.Value);
                OrderQueryViewModelEE mylistview = new OrderQueryViewModelEE();
                mylistview.supplist = mylist;
                com.DataResult = mylistview;
                com.success = true;
                return Json(com);

            }
            catch (Exception ex)
            {
                com.Msg = ex.ToString();
                com.success = false;
                return Json(com);
            }
        }


        /// <summary>
        /// 处理选中的订单
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="ischeck"></param>
        /// <param name="shopid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProcessCustOrder(List<long> ids, bool ischeck, long shopid, long pageIndex, string time)
        {
            return Json(_service.ProcessCustOrder(ids, ischeck, shopid, pageIndex, time));
        }


        /// <summary>
        /// 处理所有订单
        /// </summary>
        /// <param name="ischeck"></param>
        /// <param name="shopid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProcessAllCustOrder(bool ischeck, long shopid, long pageIndex, string time)
        {
            return Json(_service.ProcessAllCustOrder(ischeck, shopid, pageIndex, time));
        }


        /// <summary>
        /// 通过发货订单ID更新用户地址信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="receive_name"></param>
        /// <param name="receive_address"></param>
        /// <param name="receive_phone"></param>
        /// <param name="receive_zip"></param>
        /// <param name="receive_mobile"></param>
        /// <returns></returns>
        public ActionResult UpdateSendOrderUserInfoById(int order_id, string receive_name, string receive_address, string receive_phone, string receive_zip, string receive_mobile)
        {
            return Json(_service.UpdateSendOrderUserInfoById(order_id, receive_name, receive_address, receive_phone, receive_zip, receive_mobile));
        }

        /// <summary>
        /// 更新作业表信息
        /// </summary>
        /// <param name="detail_id"></param>
        /// <param name="old_wh_id"></param>
        /// <param name="detail_source"></param>
        /// <param name="new_wh_id"></param>
        /// <returns></returns>
        public ActionResult UpdateWorkInfo(int detail_id, int old_wh_id, int detail_source, int new_wh_id, int num, int proId)
        {
            return Json(_service.UpdateWorkInfo(detail_id, old_wh_id, detail_source, new_wh_id, num, proId));
        }

        /// <summary>
        /// 获取所有供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSupPlier()
        {
            return Json(_service.GetSupPlier(), JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 通过入库订单ID更新订单状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCustOrder(List<long> ids, int state)
        {
            return Json(_service.UpdateCustOrder(ids, state));
        }


        /// <summary>
        /// 通过店铺ID和转运单号获取发货订单
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="exp_code"></param>
        /// <returns></returns>
        public ActionResult GetSendOrderList(int shop_id = 0, string tran_code = "", int pageIndex = 1)
        {
            var pageSize = 10;
            var count = 0;
            var result = _service.GetSendOrderList(shop_id, tran_code, pageIndex, pageSize, out count);
            //计算页数
            var size = count % pageSize > 0 ? count / pageSize + 1 : count / pageSize;
            ViewBag.Size = size;
            ViewBag.Shop_id = shop_id;
            ViewBag.Tran_code = tran_code;
            ViewBag.List = result;
            return View();
        }



        public ActionResult PeizhiSKU()
        {

            return View();
        }


        public ActionResult GetAllPSkuByShopID(int shopID)
        {
            ComResult com = new ComResult();
            try
            {
                int totil = 0;
                int totilpage = 0;
                //默认取第一页，每页2000条数据,相当于全部查询出来，和以前的函数兼容
                var PSKUlist = CommService.GetPSKUListByshopID(1, 2000, shopID, out totil, out totilpage);
                com.State = 1;
                com.DataResult = PSKUlist;
                return Json(com);
            }
            catch (Exception ex)
            {
                com.State = 0;
                com.Msg = ex.Message;
                return Json(com);
            }
        }

        /// <summary>
        /// 上传CSV，xls 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns> HttpPostedFileBase
        public ActionResult ImportOrderFile()
        {
            ComResult com = new ComResult();
            try
            {
                string[] extends = { ".xls", ".xlsx", ".csv" };
                //NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
                string shopid = Request["shopid"].ToString();
                string naqi = Request["naqi"].ToString();
                var files = Request.Files;
                #region 多个文件导入(雅虎变态平台)
                if (files != null && files.Count > 1)//多文件
                {
                    HttpPostedFileBase fileitem1=files[0];
                    HttpPostedFileBase fileitem2 = files[1];//两个文件但是不清楚哪个是主哪个是详情
                    //for (int i = 0; i < files.Count; i++)
                    //{
                    //    HttpPostedFileBase fileitem1 = files[i];
                    //}
                    //判定文件的大小  
                    string strExtension1 = Path.GetExtension(fileitem1.FileName);
                    string strExtension2 = Path.GetExtension(fileitem2.FileName);
                    if (!extends.Contains(strExtension1) || !extends.Contains(strExtension2))
                    {
                        com.Msg = "文件格式不正确，必须是csv文件或者excel文件";
                        com.State = 0;
                        return Json(com);
                    }
                    double dFileSize1 = fileitem1.ContentLength; //字节大小
                    double dFileSize2 = fileitem2.ContentLength; //字节大小
                    if (dFileSize1 > 1024 * 1024 * 1 || dFileSize2 > 1024 * 1024 * 1)
                    {
                        com.Msg = "单个文件大小不能超过1M";
                        com.State = 0;
                        return Json(com);
                    }
                    else
                    {
                        string filePath = "~/UploadOrder/";
                        string uploadpath = Server.MapPath(filePath);
                        Directory.CreateDirectory(uploadpath);//服务器路径
                        string fFullDir1 = uploadpath + fileitem1.FileName;
                        string fFullDir2 = uploadpath + fileitem2.FileName;
                        fileitem1.SaveAs(fFullDir1); //保存
                        fileitem2.SaveAs(fFullDir2); //保存
                        //业务在此写
                        DataTable csvtable1 = null;
                        DataTable csvtable2 = null;
                        if (".csv" == strExtension1 && ".csv" == strExtension2)//不同的格式用不同的方式解析
                        {
                            csvtable1 = CsvHelper.CSVToDataTableForShiftJis(fFullDir1); //第一行是标题
                            csvtable2 = CsvHelper.CSVToDataTableForShiftJis(fFullDir2); //第一行是标题
                        }
                        else
                        {
                            csvtable1 = ExcelHelper.ReadExcel(fFullDir1); //excel
                            csvtable2 = ExcelHelper.ReadExcel(fFullDir2); //excel
                        }
                        if (csvtable1.Rows.Count > 0 || csvtable2.Rows.Count > 0)//如果读取成功就删除（代表读取成功）
                        {
                            //删除文件
                            FileHelper.FileDel(fFullDir1);
                            FileHelper.FileDel(fFullDir2);
                        }
                        DataTable table1 = new DataTable();
                        DataTable table2 = new DataTable();
                        DataTable result = new DataTable();//用来存储合并的数据
                        result.Columns.Add("Buyer", typeof(string));
                        result.Columns.Add("telephone", typeof(string));
                        result.Columns.Add("phone", typeof(string));
                        result.Columns.Add("zip", typeof(string));
                        result.Columns.Add("address", typeof(string));
                        result.Columns.Add("Fee", typeof(string));
                        result.Columns.Add("totilMoney", typeof(string));
                        result.Columns.Add("SKU1", typeof(string));
                        result.Columns.Add("SKU2", typeof(string));
                        result.Columns.Add("Num", typeof(string));
                        result.Columns.Add("OrderNub", typeof(string));
                        result.Columns.Add("beizhu", typeof(string));
                        result.Columns.Add("CustmerOrderTime", typeof(string));
                        //1.判断datatabel是否有包裹号这个表头
                        if (csvtable1.Columns.Contains("ShipName"))//主表判断
                        {
                            table1 = csvtable1.Copy(); //主表
                            table2 = csvtable2.Copy();
                        }
                        else
                        {
                            table1 = csvtable2.Copy();//主表
                            table2 = csvtable1.Copy();
                        }                      
                        foreach (DataRow dr in table1.Rows)
                        {
                            string orderid = dr["OrderId"].ToString();
                            foreach (DataRow dw in table2.Rows)
                            {
                                DataRow ndr = result.NewRow();
                                if (orderid.Equals(dw["OrderId"]))
                                {
                                    ndr["Buyer"] = dr["ShipName"].ToString();
                                    ndr["telephone"] = dr["ShipPhoneNumber"].ToString();
                                    ndr["phone"] = dr["ShipPhoneNumber"].ToString();
                                    ndr["zip"] = dr["ShipZipCode"].ToString();
                                    ndr["address"] = dr["BillPrefecture"].ToString()+ dr["BillCity"].ToString()+ dr["BillAddress1"].ToString()+ dr["BillAddress2"].ToString();
                                    ndr["Fee"] = dr["TotalPrice"].ToString();
                                    ndr["totilMoney"] = dw["UnitPrice"].ToString();
                                    ndr["SKU1"] = dw["SubCode"].ToString();
                                    ndr["SKU2"] ="";
                                    ndr["Num"] = dw["Quantity"].ToString();
                                    ndr["OrderNub"] = orderid;
                                    ndr["beizhu"] = dr["ShipMethodName"].ToString(); //放上快递类型
                                    ndr["CustmerOrderTime"] = DateTime.Now.ToString();
                                    result.Rows.Add(ndr);
                                }
                            }
                        }

                        bool isok = _service.InsertLStable(Convert.ToInt32(shopid), Convert.ToDateTime(naqi), result);
                        if (isok)
                        {
                            com.State = 1;
                            com.Msg = "成功插入" + result.Rows.Count.ToString() + "  条数据";
                        }
                    }
                }
                #endregion
                #region 单个文件导入
                else  //
                {
                    if (files != null && files.Count == 1)
                    {
                        HttpPostedFileBase fileitem = files[0];
                        //判定文件的大小  
                        string strExtension = Path.GetExtension(fileitem.FileName);
                        if (!extends.Contains(strExtension))
                        {
                            com.Msg = "文件格式不正确，必须是csv文件或者excel文件";
                            com.State = 0;
                            return Json(com);
                        }
                        double dFileSize = fileitem.ContentLength; //字节大小
                        if (dFileSize > 1024 * 1024 * 1)
                        {
                            com.Msg = "单个文件大小不能超过1M";
                            com.State = 0;
                            return Json(com);
                        }
                        else
                        {
                            string filePath = "~/UploadOrder/";
                            string uploadpath = Server.MapPath(filePath);
                            Directory.CreateDirectory(uploadpath);//服务器路径
                            string fFullDir = uploadpath + fileitem.FileName;
                            fileitem.SaveAs(fFullDir); //保存
                            //业务在此写
                            DataTable csvtable = null;
                            if (".csv" == strExtension)
                            {
                                csvtable = CsvHelper.CSVToDataTable(fFullDir); //第一行是标题
                            }
                            else
                            {
                                csvtable = ExcelHelper.ReadExcel(fFullDir); //excel
                            }
                            if (csvtable.Rows.Count > 0)//如果读取成功就删除
                            {
                                //删除文件
                                FileHelper.FileDel(fFullDir);
                            }
                            //DataTable Ncsvtable = HasGSKU(Convert.ToInt32(shopid), csvtable);//过滤完之后
                            bool isok = _service.InsertLStable(Convert.ToInt32(shopid), Convert.ToDateTime(naqi), csvtable);
                            if (isok)
                            {
                                com.State = 1;
                                com.Msg = "成功插入" + csvtable.Rows.Count.ToString() + "  条数据";
                            }
                        }
                    }
                }
                #endregion
                return Json(com);
            }
            catch (Exception ex)
            {
                com.State = 0;
                com.Msg = ex.ToString();
                return Json(com);
            }

        }


        /// <summary>
        /// 过滤不插入的SKU
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataTable HasGSKU(int shopid, DataTable table)
        {
            try
            {
                List<GSKU> GSKUlist = CommService.GetGSKUListByshopID(shopid);
                GSKU[] str = GSKUlist.ToArray();
                List<string> skulist = new List<string>();   //取出所有的需要过滤的SKU GetCSVcolByshopID
                foreach (var item in str)
                {
                    skulist.Add(item.gsku);
                }
                string csvcol = CommService.GetCSVcolByshopID(shopid);//取出每个店铺SKU存储列
                //csvcol.Replace("\"","");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (skulist.Contains(table.Rows[i][csvcol].ToString()))
                    {
                        table.Rows.Remove(table.Rows[i]);
                    }
                }
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}