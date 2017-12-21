using DBModel.DBmodel;
using DBModel.Mycomm;
using DBModel.ViewModel;
using IBLLService;
using LCWebApp.Controllers.Base;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Product
{
    public class ProductController : Controller
    {
        private IProduct _service;
        public ProductController(IProduct service) //构造注入 
        { 
            _service = service;
        }
        [AuthorizationAttribute]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeleteProductSupplier(int relid, int proid)
        {
            ComResult com = new ComResult();
            try
            {
                bool isok = _service.DeleteProductSupplier(relid, proid);
                if (!isok)
                {
                    com.Msg = "删除失败";
                    com.State = 0;
                    return Json(com);
                }
                else
                {
                    com.Msg = "";
                    com.State = 1;
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

        //
        public ActionResult AddProductSupplier(string supplier,int proid)
        {
            ComResult com = new ComResult();
            try
            {
                base_prod_supp_rel suppid = _service.AddProductSupplier(supplier, proid);
                if (null == suppid)
                {
                    com.Msg = "添加失败";
                    com.State = 0;
                    return Json(com);
                }
                else
                {
                    com.Msg = "";
                    com.State = 1;
                    com.DataResult = suppid;
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

        [HttpPost]
        public ActionResult UploadMainImage(int proID)
        {
            ComResult com = new ComResult();
            try
            {
                string[] extends = { ".jpg", ".png", ".gif" };
                var files = Request.Files;
                if (files != null && files.Count == 1)//单个文件
                {
                    HttpPostedFileBase fileitem = files[0];
                    string strExtension = Path.GetExtension(fileitem.FileName);
                    if (!extends.Contains(strExtension))
                    {
                        com.Msg = "文件格式不正确，必须是图片文件";
                        com.State = 0;
                        return Json(com);
                    }
                    string filePath = "/upload/image/product/" + DateTime.Now.ToString("yyyyMM") + "/";
                    string uploadpath = Server.MapPath(filePath);
                    if (Directory.Exists(uploadpath) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(uploadpath);
                    }
                    string newfilename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(fileitem.FileName);
                    string fFullDir = uploadpath + newfilename;
                    fileitem.SaveAs(fFullDir); //保存到硬盘
                    //2.保存到数据库
                    bool isok = _service.SetProMainImg(filePath + newfilename, proID);
                    if (!isok)
                    {
                        com.Msg = "上传失败";
                        com.State = 0;
                        return Json(com);
                    }
                    else
                    {
                        com.Msg = "上传成功";
                        com.State = 1;
                        com.URL = filePath + newfilename;
                        return Json(com);
                    }
                }
                else
                {
                    com.Msg = "不能上传多个文件";
                    com.State = 0;
                    return Json(com);
                }
            }
            catch (Exception ex)
            {
                com.Msg = "上传失败" + ex.Message;
                com.State = 0;
                return Json(com);
            }
        }
        /// <summary>
        /// 上传图片 UploadMainImage
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage(int proID, string model)
        {
            ComResult com = new ComResult();
            try
            {
                string[] extends = { ".jpg", ".png", ".gif" };
                var files = Request.Files;
                if (files != null && files.Count == 1)//单个文件
                {
                    HttpPostedFileBase fileitem = files[0];
                    string strExtension = Path.GetExtension(fileitem.FileName);
                    if (!extends.Contains(strExtension))
                    {
                        com.Msg = "文件格式不正确，必须是图片文件";
                        com.State = 0;
                        return Json(com);
                    }
                    string filePath = "/upload/image/product/" + DateTime.Now.ToString("yyyyMM") + "/";
                    string uploadpath = Server.MapPath(filePath);
                    if (Directory.Exists(uploadpath) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(uploadpath);
                    }
                    string newfilename= Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(fileitem.FileName);
                    string fFullDir = uploadpath + newfilename;
                    fileitem.SaveAs(fFullDir); //保存到硬盘
                    //2.保存到数据库
                    bool isok = _service.SetProImg(filePath + newfilename, proID, model);
                    if (!isok)
                    {
                        com.Msg = "上传失败";
                        com.State = 0;
                        return Json(com);
                    }
                    else
                    {
                        com.Msg = "上传成功";
                        com.State = 1;
                        com.URL = filePath + newfilename;
                        return Json(com);
                    }
                }
                else
                {
                    com.Msg = "不能上传多个文件";
                    com.State = 0;
                    return Json(com);
                }
            }
            catch (Exception ex)
            {
                com.Msg = "上传失败"+ex.Message;
                com.State = 0;
                return Json(com);
            }
        }

        public ActionResult UpdateSKU(int codeID, string skucode)
        {
            ComResult com = new ComResult();
            try
            {
                if (codeID <= 0)
                {
                    com.State = 0;
                    com.Msg = "SKU ID获取失败";
                    return Json(com);
                }
                bool detail = _service.UpdateSKU(codeID, skucode);
                if (detail)
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
                com.State = 0;
                com.Msg = "产品详情获取失败" + ex.ToString();
                return Json(com);
            }
        }

        public ActionResult GetProductDetail(int proID)
        {
            ComResult com = new ComResult();
            try
            {
                if (proID<=0)
                {
                    com.State = 0;
                    com.Msg = "产品ID获取错误";
                    return Json(com);
                }
                ProductDetail detail= _service.GetProductDetail(proID);
                com.DataResult = detail;
                com.State = 1;
                return Json(com);
            }
            catch (Exception ex)
            {
                com.State = 0;
                com.Msg = "产品详情获取失败"+ex.ToString();
                return Json(com);
            }
            
        }

        [HttpPost]
        public ActionResult Getpage(string pagenum, string onepagecount)
        {
            ComResult com = new ComResult();
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
            List<base_productclass> mylist = _service.Getpages(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), out totil, out totilpage, out exmsg);
            if (!string.IsNullOrEmpty(exmsg))
            {
                com.Msg = exmsg;
                com.State = 0;
                return Json(com);
            }
            else
            {
                ProductClassfyViewModel mylistview = new ProductClassfyViewModel();
                mylistview.supplist = mylist;
                mylistview.totil = totil.ToString();
                mylistview.totilcount = totilpage.ToString();
                com.DataResult = mylistview;
                com.State = 1;
                return Json(com);
            }


        }

        [AuthorizationAttribute]
        public ActionResult GetProcudtList(string pagenum, string onepagecount)
        {
            return View();
        }
        /// <summary>
        /// 得到产品列表
        /// </summary>
        /// <param name="pagenum">几页</param>
        /// <param name="onepagecount">每页数据</param>
        /// <param name="statue">审核，正式，回收站</param>
        /// <returns></returns>
        public ActionResult GetProcudtListByCondtion(string pageIndex , string onepagecount , string status ,string prostyle, string proskucode,  string prodevelpoer)
        {
            ComResult res = new ComResult();
            try
            {
                int totil = 0;
                int totilpage = 0;
                List<ProcudtViewModel> prolist = _service.Getprolist(Convert.ToInt32(pageIndex), Convert.ToInt32(onepagecount), out totil, out totilpage, Convert.ToInt32(status),prostyle,  proskucode,  prodevelpoer);

                //List<ProcudtViewModel> tempCard = prolist.Distinct(new LinqCompare()).ToList();
                if (prolist==null)
                {
                    res.State = 0;
                    res.Msg = "查询失败";
                    return Json(res);
                }
                List<ProductNViewmodel> nprolist = new List<ProductNViewmodel>();               
                foreach (var item in prolist)
                {
                    ProductNViewmodel aa = new ProductNViewmodel();
                    aa.create_time = item.create_time;
                    aa.pic_url = item.pic_url;
                    aa.price_cn = item.price_cn;
                    aa.prod_developer = item.prod_developer;
                    aa.prod_id = item.prod_id;
                    aa.prod_property = Enum.GetName(typeof(Productstyle), Convert.ToInt32(item.prod_property));
                    aa.prod_status = Enum.GetName(typeof(ProductStatus), Convert.ToInt32(item.prod_status));
                    aa.prod_style = item.prod_style;
                    aa.prod_title = item.prod_title;
                    aa.remark = item.remark;
                    nprolist.Add(aa);
                }
                PageList list = new PageList();//
                list.objlist = nprolist;
                list.pageCount = totilpage;
                list.pageIndex = Convert.ToInt32(pageIndex);
                list.pageSize = Convert.ToInt32(onepagecount);
                list.count = totil;
                list.State = 1;
                return Json(list);
            }
            catch (Exception ex)
            {
                res.State = 0;
                res.Msg = "查询失败"+ex.ToString();
                return Json(res);               
            }
          
        }

        [HttpPost]
        public ActionResult AddProductClass(string proclassfyname)
        {
            ComResult res = new ComResult();
            try
            {
             
                 if (!Regex.IsMatch(proclassfyname, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(proclassfyname))
                {
                    res.Msg = "分类名称不能有非法字符";
                    res.State = 0;
                    return Json(res);
                }
                 if (string.IsNullOrEmpty(proclassfyname))
                 {
                     res.Msg = "分类名称不能为空";
                     res.State = 0;
                     return Json(res);
                 }
                 base_productclass pro = new base_productclass();
                 pro.class_name = proclassfyname;
                 pro.class_icon = "";
                 pro.class_sort = 1;
                 pro.create_user_id = 1;
                 pro.edit_user_id = 1;
                 pro.del_flag = true;
                 pro.remark = "";
                 pro.create_time = DateTime.Now;
                 pro.edit_time = DateTime.Now;

                 int isexit = 0;
                 int id = 0;
                 bool isok=_service.AddProclassfy(pro, out  isexit,out id);
                 if (1 == isexit)
                 {
                     res.Msg = "此分类已存在且被删除是否恢复?";
                     res.URL = id.ToString();
                     res.State = 2;                    
                     return Json(res);
                 }
                 if (2 == isexit)
                 {
                     res.Msg = "已存在此分类";
                     res.State = 0;
                     return Json(res);
                 }
                 if (isok)
                 {
                     res.Msg = "添加成功";
                     res.State = 1;
                     res.URL = "/Product/Index";
                     return Json(res);
                 }else
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

        [HttpPost]
        public ActionResult UpdateProClassfy(string proclassfyname, string id) //DeleteProClassfy
        {
            ComResult res = new ComResult();
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
                bool isok=_service.UpdateProclassfy(proclassfyname,Convert.ToInt32(id));
                if (isok)
                {
                    res.Msg = "更新成功";
                    res.URL = "/Product/Index";
                    res.State = 1;
                    return Json(res);
                }else
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

        [HttpPost]
        public ActionResult UpdateProClassfyDel(string proclassfyname, string id) //DeleteProClassfy
        {
            ComResult res = new ComResult();
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
                bool isok = _service.UpdateProclassfyDel(proclassfyname, Convert.ToInt32(id));
                if (isok)
                {
                    res.Msg = "还原成功";
                    res.URL = "/Product/Index";
                    res.State = 1;
                    return Json(res);
                }
                else
                {
                    res.Msg = "还原失败";
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

        [HttpPost]
        public ActionResult DeleteProClassfy(string id) 
        {
            ComResult res = new ComResult();
            try
            {             
                if (!Regex.IsMatch(id, @"^\d+$"))
                {
                    res.Msg = "ID只能是数字,且非负";
                    res.State = 0;
                    return Json(res);
                }
                bool isok = _service.DeleteProClassfy(Convert.ToInt32(id));
                if (isok)
                {
                    res.Msg = "删除成功";
                    res.URL = "/Product/Index";
                    res.State = 1;
                    return Json(res);
                }
                else
                {
                    res.Msg = "删除失败";
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