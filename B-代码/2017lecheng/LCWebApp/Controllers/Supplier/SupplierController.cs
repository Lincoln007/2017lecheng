using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService;
using LCWebApp.Controllers.Base;
using LCWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Supplier
{
    public class SupplierController : Controller
    {
        private ISupplier _service;
        public SupplierController(ISupplier service) //构造注入 
        {
            _service = service;
        }

        /// <summary>
        /// 得到所有的供应商数据，分页
        /// </summary>
        /// <param name="suppliername">供应商名</param>
        /// <param name="onepagecount"></param>
        /// <param name="pagenum"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllSuppliers(string onepagecount = "10", string suppliername = "", string pagenum = "1")
        {
            ComResult com = new ComResult();
            if (!Regex.IsMatch(suppliername, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(suppliername))
            {
                com.Msg = "供应商名称不能包含特殊字符";
                com.State = 0;
                return Json(com);
            }
            int totil = 0;
            int totilpage = 0;
            List<base_supplier> suppliers = _service.SearchSupplier(Convert.ToInt32(pagenum), Convert.ToInt32(onepagecount), suppliername, out totil, out totilpage);
            List<SupplierViewModel> suppliersView = new List<SupplierViewModel>();
            foreach (var ea in suppliers)
            {
                SupplierViewModel model = new SupplierViewModel();
                model.ID = ea.supp_id.ToString();
                model.supp_code = ea.supp_code;
                model.supp_name = ea.supp_name;
                model.supp_url = ea.supp_url;
                model.supp_remark = ea.remark;
                model.CreateTime = ea.create_time.ToString();
                suppliersView.Add(model);
            }
            SupplierNview mylist = new SupplierNview();
            mylist.supplist = suppliersView;
            mylist.totil = totil.ToString();
            mylist.totilcount = totilpage.ToString();
            com.State = 1;
            com.DataResult = mylist;
            return Json(com);
        }

        // 修改供应商，改链接
        [HttpPost]
        public ActionResult UpdateSupplier(string suppname, string remark, string suppurl, string id)
        {
            ComResult res = new ComResult();
            if (!Regex.IsMatch(id, @"^\d+$"))
            {
                res.Msg = "ID只能是数字,且非负";
                res.State = 0;
                return Json(res);
            }
            if (!Regex.IsMatch(suppname, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$"))
            {
                res.Msg = "供应商名不能有非法字符";
                res.State = 0;
                return Json(res);
            }
            if (!Regex.IsMatch(suppurl, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"))
            {
                res.Msg = "链接不是有效地址！";
                res.State = 0;
                return Json(res);
            }
            if (!Regex.IsMatch(remark, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(remark))
            {
                res.Msg = "备注不能有非法字符";
                res.State = 0;
                return Json(res);
            }
            bool isok = _service.UpdateSupplier(suppname, remark, suppurl, Convert.ToInt32(id));
            if (isok)
            {
                res.Msg = "更新成功!";
                res.State = 1;
                res.URL = "/Supplier/SerachSupplier";
                return Json(res);
            }
            else
            {
                res.Msg = "更新失败!";
                res.State = 0;
                return Json(res);
            }

        }
        // 查找供应商
          [AuthorizationAttribute]
        public ActionResult SerachSupplier()
        {
            return View();
        }
        // 添加供应商
        [HttpPost]
        public ActionResult AddSupplier(string suppname, string remark, string suppurl)
        {
            ComResult res = new ComResult();
            if (!Regex.IsMatch(suppname, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$"))
            {
                res.Msg = "供应商名不能有非法字符";
                res.State = 0;
                return Json(res);
            }
            if (!Regex.IsMatch(suppurl, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"))
            {
                res.Msg = "链接不是有效地址！";
                res.State = 0;
                return Json(res);
            }
            if (!Regex.IsMatch(remark, @"(?i)^[0-9a-z\u4e00-\u9fa5]+$") && !string.IsNullOrEmpty(remark))
            {
                res.Msg = "备注不能有非法字符";
                res.State = 0;
                return Json(res);
            }
            base_supplier supp = new base_supplier();
            supp.create_user_id = 1;
            supp.del_flag = true;
            supp.supp_name = suppname;
            supp.supp_url = suppurl;
            supp.remark = remark;
            supp.supp_code = "GYS" + "001";
            supp.is_grade = true;
            supp.purc_mode = 1;
            supp.purc_priority = 1;
            supp.purc_sourceid = 1;
            supp.create_time = DateTime.Now;
            bool isok = _service.AddSupplier(supp);
            if (isok)
            {
                res.Msg = "添加成功!";
                res.State = 1;
                res.URL = "/Supplier/SerachSupplier";
                return Json(res);
            }
            else
            {
                res.Msg = "添加失败!";
                res.State = 0;
                return Json(res);
            }

        }
        // 删除供应商
        [HttpPost]
        public ActionResult DeleteSupplier(string id)
        {
            ComResult res = new ComResult();
            if (!Regex.IsMatch(id, @"^\d+$"))
            {
                res.Msg = "ID只能是数字,且非负";
                res.State = 0;
                return Json(res);
            }
            bool isok = _service.DeleteSupplier(Convert.ToInt32(id));
            if (isok)
            {
                res.Msg = "删除成功!";
                res.State = 1;
                res.URL = "/Supplier/SerachSupplier";
                return Json(res);
            }
            else
            {
                res.Msg = "删除失败!";
                res.State = 0;
                return Json(res);
            }

        }
	}
}