using CommHelper;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService.RFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.RFT
{
    public class RFTController : Controller
    {
        private IRFT _service;
        public RFTController(IRFT service) //构造注入  
        {
            _service = service;
        }
        //
        // GET: /RFT/ Loginout
        /// <summary>
        /// RFT登录首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            DelCookie("userid");
            DelCookie("pwd");
            if (Request.Cookies["tockenid"] == null)
            {
                HttpCookie cook = new HttpCookie("tockenid");
                cook.Value = Guid.NewGuid().ToString("N").ToLower();
                Response.SetCookie(cook);
                Response.Cookies.Add(cook);
            }
            return View();
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCookie(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key);
            cookie.Value = value;
            Response.SetCookie(cookie);
            Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 自动过期cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time">过期时间分钟</param>
        public void SetCookieHasTime(string key, string value, int time)
        {
            HttpCookie cookie = new HttpCookie(key);
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddMinutes(time);
            Response.SetCookie(cookie);
            Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 删除COOKIE
        /// </summary>
        public void DelCookie(string key)
        {
            HttpCookie cookie = new HttpCookie(key);
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.SetCookie(cookie);
            Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 获取COOKIE
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCookie(string key)
        {
            if (Request.Cookies[key] != null)
            {
                return Request.Cookies[key].Value;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 退出登录视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Loginout()  //ConfirmLogout
        {
            return View();
        }

        /// <summary>
        /// 确定退出
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ActionResult ConfirmLogout(string username)  //
        {
            DelCookie("userid");
            DelCookie("password");
            DelCookie("tockenid");
            DelCookie("currshopid");
            Session.Remove("tockenid");
            Session.Remove("userinfo");
            return View("Index");
        }
        /// <summary>
        /// 登录失败
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginError()  //
        {
            ViewData["ErrorInfo"] = "用户名或者密码不能为空";
            return View("LoginError");
        }
        /// <summary>
        /// 菜单主页
        /// </summary>
        /// <returns></returns>
        public ActionResult MainMenu()
        {
            try
            {
                RFTDistrView peihuoinfo = JsonHelper.DeserializeJsonToObject<RFTDistrView>(GetCookie("peihuoinfo")); //得到配货信息
                if (peihuoinfo != null) //代表取消进入此页面
                {
                    bool isok = _service.CancelDistrubilt(peihuoinfo);
                    if (!isok)
                    {
                        ViewData["errorinfo"] = "取消配货失败";
                        return View("ComErrorView");
                    }
                }
                var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
                ViewData["userid"] = Userinfo.real_name;
                DelCookie("peihuoinfo"); //删除之前的配货cookie信息
                return View("MainMenu");
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] =ex.Message.ToString();
                return View("ComErrorView");
            }
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckLogin()
        {
            try
            {
                // string aa=Request["userid"] == null ? "" : Request["userid"].ToString();
                string userid = Request["userid"] == null ? "" : Request["userid"].ToString();
                string password = Request["password"] == null ? "" : Request["password"].ToString();
                if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(password))
                {
                    return View("LoginErrorEmpty");//返回验证失败视图
                }
                //登录验证
                base_users userinfo = _service.CheckLogin(userid, EncodeHepler.GetMD5(password));
                if (userinfo == null)
                {
                    return RedirectToAction("LoginError"); //用户名或者密码错误
                }
                else
                {
                    SetCookie("userinfo", JsonHelper.SerializeObject(userinfo));
                    Session.Add("userinfo", userinfo); //用户信息存入session
                    ViewData["userid"] = userinfo.real_name;
                    return View("MainMenu");
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] =ex.Message.ToString();
                return View("ComErrorView");
            }

        }


        /// <summary>
        /// 配货成功
        /// </summary>
        /// <returns></returns>
        public ActionResult DistrubulitSucc()  //ConfirmLogout
        {
            string currshopid = GetCookie("currshopid"); //如果是店铺优先配货一笔之后跳转到的页面
            if (!string.IsNullOrEmpty(currshopid))
            {
                return View("DistrubulitSucc2");//RedirectToAction("ShopFirstDistrbuilt");                    
            }
            else
            {
                return View("DistrubulitSucc");
            }

        }
        /// <summary>
        /// 跳到配货页面   
        /// </summary>
        /// <returns></returns>
        public ActionResult Distrbuilt() //
        {
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            ViewData["userid"] = Userinfo.real_name;
            return View("Distrbuilt");
        }

        /// <summary>
        /// 拆分包裹
        /// </summary>
        /// <returns></returns>
        public ActionResult Dipartpackge() //
        {
            try
            {
                base_users Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo")); //得到用户信息                           
                RFTDistrView peihuoinfo = JsonHelper.DeserializeJsonToObject<RFTDistrView>(GetCookie("peihuoinfo")); //得到配货信息
                RFTDistrView newpeihuoinfo = _service.Dispartpackge(peihuoinfo);
                //DelCookie("peihuoinfo"); //删除之前的配货cookie信息
                if (newpeihuoinfo == null)
                {
                    ViewData["errorinfo"] = "拆分包裹失败";
                    return View("ComErrorView");
                }
                else
                {
                    var useinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
                    ViewData["userid"] = useinfo.real_name;
                    SetCookie("peihuoinfo", JsonHelper.SerializeObject(newpeihuoinfo)); //将数据放入cookie，记得确认配货完删除cookie
                    if (newpeihuoinfo.info == "可包装")
                    {
                        ViewData["disinfo"] = newpeihuoinfo;
                        return View("DistrbuiltInfo3");
                    }
                    else
                    {
                        ViewData["disinfo"] = newpeihuoinfo;
                        return View("DistrbuiltInfo2");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] =ex.Message.ToString();
                return View("ComErrorView");
            }

        }

        /// <summary>
        /// 优先配货 
        /// </summary>
        /// <param name="SKUcode"></param>
        /// <returns></returns>
        public ActionResult FirstDistrbuilt() //
        {
            DelCookie("currshopid"); //退出店铺优先时，删除cookie
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            ViewData["userid"] = Userinfo.real_name;
            return View("FirstDistrbuilt");
        }

        /// <summary>
        /// 包裹优先配货
        /// </summary>
        /// <returns></returns>
        public ActionResult PFirstDistrbuilt() //
        {
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            ViewData["userid"] = Userinfo.real_name;
            return View("PFirstDistrbuilt");
        }

        /// <summary>
        /// 包裹优先配货
        /// </summary>
        /// <param name="skucode"></param>
        /// <param name="packgecode"></param>
        /// <returns></returns>
        public ActionResult RPFirstDistrbuilt(string skucode, string packgecode) //
        {
            packgecode = packgecode.Replace("A", "").Trim();
            if (string.IsNullOrEmpty(skucode) || string.IsNullOrEmpty(packgecode))
            {
                ViewData["errorinfo"] = "SKU和包裹号不能为空";
                return View("ComErrorView");
            }
            bool isok = _service.IsSKUInSys(skucode); //判断SKU在系统中是否存在
            if (!isok)
            {
                ViewData["errorinfo"] = "此SKU在系统中不存在";
                return View("ComErrorView");
            }
            try
            {
                RFTDistrView dis = _service.Distrbuilt(skucode, 0, packgecode); //得到配货信息
                if (dis == null)
                {
                    ViewData["errorinfo"] = "无可配货信息";
                    return View("ComErrorView");
                }
                else
                {
                    var useinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
                    ViewData["userid"] = useinfo.real_name;
                    SetCookie("peihuoinfo", JsonHelper.SerializeObject(dis)); //将数据放入cookie，记得确认配货完删除cookie
                    if (dis.info == "可包装")
                    {
                        ViewData["disinfo"] = dis;
                        return View("DistrbuiltInfo");
                    }
                    else
                    {
                        ViewData["disinfo"] = dis;
                        return View("DistrbuiltInfo2");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = ex.Message;
                return View("ComErrorView");
            }
        }

        /// <summary>
        /// 店铺优先配货
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopFirstDistrbuilt() //
        {
            //var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            var Userinfo = (base_users)Session["userinfo"];
            if (Userinfo == null)
            {
                ViewData["errorinfo"] = "用户操作过期重新登录!";
                return View("ComErrorView");
            }
            string shopid = GetCookie("currshopid");
            ViewData["userid"] = Userinfo.real_name;
            if (!string.IsNullOrEmpty(shopid))
            {
                ViewData["currshopid"] = Convert.ToInt32(shopid);
            }
            else
            {
                ViewData["currshopid"] = 0;
            }
            return View("ShopFirstDistrbuilt");
        }
        /// <summary>
        /// 店铺确认配货
        /// </summary>
        /// <param name="skucode"></param>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public ActionResult RShopFirstDistrbuilt(string skucode, int shopid) //
        {
            SetCookieHasTime("currshopid", shopid.ToString(), 1); //过期一分钟
            if (string.IsNullOrEmpty(skucode))
            {
                ViewData["errorinfo"] = "SKU和包店铺ID不能为空";
                return View("ComErrorView");
            }
            bool isok = _service.IsSKUInSys(skucode); //判断SKU在系统中是否存在
            if (!isok)
            {
                ViewData["errorinfo"] = "此SKU在系统中不存在";
                return View("ComErrorView");
            }
            try
            {
                RFTDistrView dis = _service.Distrbuilt(skucode, shopid, ""); //得到配货信息
                if (dis == null)
                {
                    ViewData["errorinfo"] = "无可配货信息";
                    return View("ComErrorView");
                }
                else
                {
                    var useinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
                    ViewData["userid"] = useinfo.real_name;
                    SetCookie("peihuoinfo", JsonHelper.SerializeObject(dis)); //将数据放入cookie，记得确认配货完删除cookie
                    if (dis.info == "可包装")
                    {
                        ViewData["disinfo"] = dis;
                        return View("DistrbuiltInfo");
                    }
                    else
                    {
                        ViewData["disinfo"] = dis;
                        return View("DistrbuiltInfo2");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = ex.Message;
                return View("ComErrorView");
            }
        }
        /// <summary>
        /// 确认配货  
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmDistrbuilt() //
        {
            base_users Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo")); //得到用户信息                           
            RFTDistrView peihuoinfo = JsonHelper.DeserializeJsonToObject<RFTDistrView>(GetCookie("peihuoinfo")); //得到配货信息  
            try
            {
                bool isok = _service.ConfirmDistruit(peihuoinfo, Userinfo);
                if (isok)//配货成功
                {
                    //ViewData["userid"] = Userinfo.real_name;
                    //return View("DistrubulitSucc");
                    return RedirectToAction("DistrubulitSucc");
                }
                else
                {
                    ViewData["errorinfo"] = "配货失败!";
                    return View("ComErrorView");
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = "配货程序出错!"+ex.Message;
                return View("ComErrorView");
            }
            finally //删除之前的配货信息
            {
                peihuoinfo = null;
                DelCookie("peihuoinfo");
            }

        }

        /// <summary>
        /// 拆包确认配货
        /// </summary>
        /// <returns></returns>
        public ActionResult DispartConfirmDistrbuilt() //
        {
            base_users Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo")); //得到用户信息                           
            RFTDistrView peihuoinfo = JsonHelper.DeserializeJsonToObject<RFTDistrView>(GetCookie("peihuoinfo")); //得到配货信息  
            try
            {
                bool isok = _service.DispartConfirmDistruit(peihuoinfo, Userinfo);//真正在数据库中拆分掉包裹
                if (isok)  //配货成功
                {
                    //ViewData["userid"] = Userinfo.real_name;
                    return RedirectToAction("DistrubulitSucc");
                }
                else
                {
                    ViewData["errorinfo"] = "配货失败!";
                    return View("ComErrorView");
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = "配货程序出错!"+ex.Message;
                return View("ComErrorView");
            }
            finally //删除之前的配货信息
            {
                peihuoinfo = null;
                DelCookie("peihuoinfo");
            }

        }
        /// <summary>
        /// 配货 
        /// </summary>
        /// <param name="SKUcode">SKU</param>
        /// <returns></returns>
        public ActionResult RDistrbuilt(string skucode)
        {
            //------------------------------测试代码------------------------------------------------------------
            //var objinfo = JsonHelper.DeserializeJsonToObject<base_users>( GetCookie("userinfo"));
            //ViewData["realname"] = objinfo.real_name;
            //return View("test");
            //-------------------------------------------------------------------------------------------
            if (string.IsNullOrEmpty(skucode))
            {
                ViewData["errorinfo"] = "请先扫描SKU";
                return View("ComErrorView");
            }
            bool isok = _service.IsSKUInSys(skucode);//判断SKU在系统中是否存在
            if (!isok)
            {
                ViewData["errorinfo"] = "此SKU在系统中不存在";
                return View("ComErrorView");
            }

            try
            {
                RFTDistrView dis = _service.Distrbuilt(skucode, 0, ""); //得到配货信息
                if (dis == null)
                {
                    ViewData["errorinfo"] = "无可配货信息";
                    return View("ComErrorView");
                }
                else
                {
                    var useinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
                    ViewData["userid"] = useinfo.real_name;
                    SetCookie("peihuoinfo", JsonHelper.SerializeObject(dis)); //将数据放入cookie，记得确认配货完删除cookie
                    if (dis.info == "可包装")
                    {
                        ViewData["disinfo"] = dis;
                        return View("DistrbuiltInfo");
                    }
                    else
                    {
                        ViewData["disinfo"] = dis;
                        return View("DistrbuiltInfo2");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = ex.Message;
                return View("ComErrorView");
            }
        }
        /// <summary>
        /// 选择快递
        /// </summary>
        /// <param name="SKUcode"></param>
        /// <returns></returns>
        public ActionResult SelectExpress()
        {
            var Userinfo = (base_users)Session["userinfo"];
            if (Userinfo == null)
            {
                ViewData["errorinfo"] = "用户操作过期重新登录!";
                return View("ComErrorView");
            }
            ViewData["userid"] = Userinfo.real_name;
            try
            {
                List<base_exp_comp> list = _service.GetAllExpress();
                ViewData["explist"] = list;
                return View("SelectExpress");
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] =ex.Message.ToString();
                return View("ComErrorView");
            }

        }
        /// <summary>
        /// 装箱复核 ConfirmPutinBox
        /// </summary>
        /// <param name="SKUcode"></param>
        /// <returns></returns>
        public ActionResult PutInBox()
        {
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            ViewData["userid"] = Userinfo.real_name;
            Session.Remove("zhuanyuncode");
            return View("Putinbox");
        }
        public ActionResult OutofBoxOfzhuanyun(string zhuanyuncode)
        {
            zhuanyuncode = zhuanyuncode.Replace("A", "").Trim();
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            ViewData["userid"] = Userinfo.real_name;
            if (string.IsNullOrEmpty(zhuanyuncode))
            {
                ViewData["errorinfo"] = "转运单号不能为空!";
                return View("ComErrorView");
            }
            //1.判断转运单号在系统中是否存在
            try
            {
                bool isok = _service.IszhuanyunCodeInSys(zhuanyuncode);
                if (!isok)
                {
                    ViewData["errorinfo"] = "转运单号在系统中不存在!";
                    return View("ComErrorView");
                }
                bool isok2 = _service.IszhuanyunCodeing(zhuanyuncode);
                if (!isok2)
                {
                    ViewData["errorinfo"] = "转运单号已转运!";
                    return View("ComErrorView");
                }
                ViewData["zhuanyuncode"] = zhuanyuncode;
                SetCookie("zhuanyuncode", zhuanyuncode);
                Session.Add("zhuanyuncode", zhuanyuncode); //放入session中
                return View("OutofBoxOfzhuanyun");
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = ex.Message.ToString();
                return View("ComErrorView");
            }
        }
        /// <summary>
        /// 确认出箱
        /// </summary>
        /// <param name="packgecode"></param>
        /// <returns></returns>
        public ActionResult ConfirmOutofBox(string packgecode)
        {
            packgecode = packgecode.Replace("A", "").Trim();
            var Userinfo = (base_users)Session["userinfo"];
            if (null == Userinfo)
            {
                ViewData["errorinfo"] = "用户操作过期重新登录!";
                return View("ComErrorView");
            }
            ViewData["userid"] = Userinfo.real_name;
            try
            {
                string zhuanyuncode = Session["zhuanyuncode"].ToString();
                bool isok = _service.ConfirmOutofBox(zhuanyuncode, packgecode);
                if (isok)
                {
                    return View("OutofBoxSucc");
                }
                else
                {
                    ViewData["errorinfo"] = "包裹出箱失败!";
                    return View("ComErrorView");
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = ex.Message.ToString();
                return View("ComErrorView");
            }

        }
        /// <summary>
        /// 跳到开始装箱页面   
        /// </summary>
        /// <param name="zhuanyuncode"></param>
        /// <returns></returns>
        public ActionResult PutInBoxOfzhuanyun(string zhuanyuncode)
        {
            zhuanyuncode = zhuanyuncode.Replace("A", "").Trim();
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            
            ViewData["userid"] = Userinfo.real_name;
            if (string.IsNullOrEmpty(zhuanyuncode))
            {
                ViewData["errorinfo"] = "转运单号不能为空!";
                return View("ComErrorView");
            }
            //1.判断转运单号在系统中是否存在
            try
            {
                bool isok = _service.IszhuanyunCodeInSys(zhuanyuncode);
                if (!isok)
                {
                    ViewData["errorinfo"] = "转运单号在系统中不存在!";
                    return View("ComErrorView");
                }
                bool isok2 = _service.IszhuanyunCodeing(zhuanyuncode);
                if (!isok2)
                {
                    ViewData["errorinfo"] = "转运单号已转运!";
                    return View("ComErrorView");
                }
                ViewData["zhuanyuncode"] = zhuanyuncode;
                SetCookie("zhuanyuncode", zhuanyuncode);
                Session.Add("zhuanyuncode", zhuanyuncode); //放入session中
                return View("PutInBoxOfzhuanyun");
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] =ex.Message.ToString();
                return View("ComErrorView");
            }
        }
        /// <summary>
        /// 重新跳转到装箱页面 
        /// </summary>
        /// <returns></returns>
        public ActionResult PutInBoxOfzhuanyunAgain()
        {
            try
            {
                string zhuanyuncode=Session["zhuanyuncode"].ToString();
                zhuanyuncode = zhuanyuncode.Replace("A", "").Trim();
                var Userinfo = (base_users)Session["userinfo"];
                if (null == Userinfo)
                {
                    ViewData["errorinfo"] = "用户操作过期重新登录!";
                    return View("ComErrorView");
                }
                ViewData["userid"] = Userinfo.real_name;
                ViewData["zhuanyuncode"] = zhuanyuncode;
                SetCookie("zhuanyuncode", zhuanyuncode);
                return View("PutInBoxOfzhuanyun");
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = ex.Message.ToString();
                return View("ComErrorView");
            }
           
        }
        /// <summary>
        /// 确认装箱  
        /// </summary>
        /// <param name="packgecode"></param>
        /// <returns></returns>
        public ActionResult ConfirmPutInBox(string packgecode)
        {
            packgecode = packgecode.Replace("A", "").Trim();
            var Userinfo = (base_users)Session["userinfo"];
            if (null == Userinfo)
            {
                ViewData["errorinfo"] = "用户操作过期重新登录!";
                return View("ComErrorView");
            }
            ViewData["userid"] = Userinfo.real_name;
            try
            {
                string zhuanyuncode = Session["zhuanyuncode"].ToString();
                bool isok = _service.ConfirmPutInBox(zhuanyuncode, packgecode);
                if (isok)
                {
                    return View("PutinBoxSucc");
                }
                else
                {
                    ViewData["errorinfo"] = "包裹装箱失败!";
                    return View("ComErrorView");
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = ex.Message.ToString();
                return View("ComErrorView");
            }

        }
        /// <summary>
        /// 生成转运单 
        /// </summary>
        /// <param name="zhuanyuncode"></param>
        /// <returns></returns>
        public ActionResult CreatezhuanyunCode()
        {
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            ViewData["userid"] = Userinfo.real_name;
            long Nzhuanyuncode = _service.CreatezhuanyunCode(); //生成新的转运单号
            ViewData["zhuanyuncode"] = Nzhuanyuncode;
            return View("Printzhuanyuncode");
        }

        /// <summary>
        /// 转到打印包裹号界面 
        /// </summary>
        /// <param name="zhuanyuncode"></param>
        /// <returns></returns>
        public ActionResult PrintPackge()
        {
            //var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            //ViewData["userid"] = Userinfo.real_name;
            //long Nzhuanyuncode = _service.CreatezhuanyunCode(); //生成新的转运单号
            //ViewData["zhuanyuncode"] = Nzhuanyuncode;
            return View("PrintPackge");
        }

        /// <summary>
        /// 打印转运单
        /// </summary>
        /// <param name="zhuanyuncode"></param>
        /// <returns></returns>
        public ActionResult ConfirmPrintpackge(string packgecode)
        {
            //var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            //ViewData["userid"] = Userinfo.real_name;
            packgecode = packgecode.Replace("A", "").Trim();
            if (string.IsNullOrEmpty(packgecode))
            {
                ViewData["errorinfo"] = "包裹号不能为空!";
                return View("ComErrorView");
            }
            try
            {
                bool isok = _service.PrintpackgeCode(packgecode);
                if (!isok)
                {
                    ViewData["errorinfo"] = "打印包裹号单失败请确认!";
                    return View("ComErrorView");
                }
                else
                {
                    return View("PrintOK");
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] = ex.Message.ToString();
                return View("ComErrorView");
            }

        }
        /// <summary>
        /// 打印转运单
        /// </summary>
        /// <param name="zhuanyuncode"></param>
        /// <returns></returns>
        public ActionResult ConfirmPrintzhuanyunCode(string zhuanyuncode)
        {
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            ViewData["userid"] = Userinfo.real_name;
            zhuanyuncode = zhuanyuncode.Replace("A", "").Trim();
            if (string.IsNullOrEmpty(zhuanyuncode))
            {
                ViewData["errorinfo"] = "转运单号不能为空!";
                return View("ComErrorView");
            }
            try
            {
                bool isok = _service.PrintzhuanyunCode(zhuanyuncode);
                if (!isok)
                {
                    ViewData["errorinfo"] = "打印转运单失败请确认!";
                    return View("ComErrorView");
                }
                else
                {
                    return View("PrintOK");
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] =ex.Message.ToString();
                return View("ComErrorView");
            }

        }

        /// <summary>
        /// 取消装箱 
        /// </summary>
        /// <param name="SKUcode"></param>
        /// <returns></returns>
        public ActionResult OutOfBox(string SKUcode)
        {
            var Userinfo = JsonHelper.DeserializeJsonToObject<base_users>(GetCookie("userinfo"));
            ViewData["userid"] = Userinfo.real_name;
            Session.Remove("zhuanyuncode");
            return View("Outofbox");
        }


        /// <summary>
        /// 确认快递单号
        /// </summary>
        /// <param name="packgecode">包裹号</param>
        /// <returns></returns>
        public ActionResult ConfirmExpress(string packgecode, int expid)
        {
            packgecode=packgecode.Replace("A", "").Trim();
            if (string.IsNullOrEmpty(packgecode))
            {
                ViewData["errorinfo"] = "包裹号不能为空!";
                return View("ComErrorView");
            }
            try
            {
                bool isok = _service.SetExpress(packgecode, expid);
                if (!isok)
                {
                    ViewData["errorinfo"] = "选择快递类型失败!";
                    return View("ComErrorView");
                }
                else
                {
                    return View("ExpSuccess");
                }
            }
            catch (Exception ex)
            {
                ViewData["errorinfo"] =ex.Message.ToString();
                return View("ComErrorView");
            }
        }
    }
}