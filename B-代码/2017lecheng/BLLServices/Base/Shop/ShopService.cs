using DBModel.Base;
using DBModel.Common;
using DBModel.DBmodel;
using IBLLService.Base.Shop;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Base.Shop
{
    public class ShopService : IShopService
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public List<ShopResultModel> GetShopList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<base_shop>()
                    .JoinTable<base_platform>((s1, s2) => s1.platform_id == s2.platform_id, JoinType.Inner)
                    .Where("s1.del_flag=1 and s2.del_flag=1").OrderBy("s1.shop_id DESC")
                    .Select<base_platform, ShopResultModel>((s1, s2) => new ShopResultModel
                    {
                        shop_id = s1.shop_id,
                        shop_name = s1.shop_name,
                        shop_account = s1.shop_account,
                        shop_status = s1.shop_status,
                        create_time = s1.create_time,
                        country_id = s1.country_id,
                        platform_lrish = s1.platform_lrish,
                        platform_name = s2.platform_name,
                    }).ToList();
                    totil = getwhere.Count();
                    if (totil > 0)
                    {
                        foreach (var item in getwhere)
                        {
                            item.shop_statusE = item.shop_status == true ? "<span style=\"color:green;\">可用</span>" : "<span style=\"color:red;\">停用</span>";
                            item.create_timeE = item.create_time.ToString("yyyy-MM-dd");
                            item.country_name = item.country_id == 1 ? "日元" : (item.country_id == 2 ? "美元" : (item.country_id == 3 ? "欧元" : (item.country_id == 4 ? "英镑" : (item.country_id == 5 ? "人民币" : "新加坡元"))));
                        }
                    }
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount);
                    totilpage = totil / onepagecount;
                    exmsg = "";
                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }
                    return list.ToList();
                }
                catch (Exception ex)
                {
                    exmsg = ex.ToString();
                    totil = 0;
                    totilpage = 0;
                    return null;
                }

            }
        }


        /// <summary>
        /// 修改 添加的方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ShopResult Save(base_shop model)
        {
            bool rstNum = false;
            ShopResult result = new ShopResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                #region 判断
                if (model == null)
                {
                    result.success = false;
                    result.Msg = "请填写信息!";
                    return result;
                }

                if (model.platform_id == 0)
                {
                    result.success = false;
                    result.Msg = "请选择平台!";
                    return result;
                }

                if (string.IsNullOrWhiteSpace(model.shop_name))
                {
                    result.success = false;
                    result.Msg = "请填写店铺名称!";
                    return result;
                }

                if (string.IsNullOrWhiteSpace(model.shop_account))
                {
                    result.success = false;
                    result.Msg = "请填写管理账号!";
                    return result;
                }

                if (model.country_id == 0)
                {
                    result.success = false;
                    result.Msg = "请选择店铺币种!";
                    return result;
                }


                if (string.IsNullOrWhiteSpace(model.shop_telephone))
                {
                    result.success = false;
                    result.Msg = "请填写店铺电话!";
                    return result;
                }


                if (string.IsNullOrWhiteSpace(model.shop_zipcode))
                {
                    result.success = false;
                    result.Msg = "请填写店铺邮编!";
                    return result;
                }


                if (string.IsNullOrWhiteSpace(model.shop_address))
                {
                    result.success = false;
                    result.Msg = "请填写店铺地址!";
                    return result;
                }

                if (model.platform_lrish == null)
                {
                    result.success = false;
                    result.Msg = "请填写平台扣点!";
                    return result;
                }
                #endregion

                if (model.shop_id > 0)
                {
                    var list = db.Queryable<base_shop>().InSingle(model.shop_id);
                    if (list == null)
                    {
                        result.success = false;
                        result.Msg = "不存在的店铺信息";
                        return result;
                    }
                    if (list.shop_status.Value)
                    {
                        list.shop_status = false;
                    }
                    else
                    {
                        list.shop_status = true;
                    }
                    list.edit_time = DateTime.Now;
                    list.edit_user_id = LoginUser.Current.user_id;
                    rstNum = db.Update<base_shop>(list);
                }
                else//添加店铺
                {
                    var base_shop = db.Queryable<base_shop>().Where(c => c.del_flag && c.shop_name == model.shop_name).ToList();
                    if (base_shop.Count > 0)
                    {
                        result.success = false;
                        result.Msg = "已存在的店铺信息，操作失败!";
                        return result;
                    }
                    model.create_time = DateTime.Now;
                    model.create_user_id = LoginUser.Current.user_id;
                    model.del_flag = true;
                    model.del_user_id = 0;
                    model.del_time = DateTime.Now;
                    model.edit_time = DateTime.Now;
                    model.edit_user_id = 0;
                    model.remark = model.shop_name;
                    model.settlm_currency = 0;
                    model.shop_status = true;
                    model.LStablename = db.Queryable<base_platform>().Where(s => s.platform_id == model.platform_id).FirstOrDefault().platform_name;
                    var id = db.Insert<base_shop>(model);
                    if (id.ObjToInt() > 0)
                    {
                        rstNum = true;
                    }

                }

                if (rstNum)
                {
                    result.success = true;
                    result.URL = "/Shop/Index";
                    result.Msg = "操作成功";
                    return result;
                }
                else
                {
                    result.success = false;
                    result.Msg = "操作失败";
                    return result;
                }
            }
        }


        /// <summary>
        /// 获取下拉框信息
        /// </summary>
        /// <returns></returns>
        public ShopResult GetPlatformList()
        {
            ShopResult result = new ShopResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var list = db.Queryable<base_platform>().Where(a => a.del_flag).OrderBy("platform_id DESC").ToList();
                    if (list.Count <= 0)
                    {
                        result.success = false;
                        result.Msg = "暂无平台信息!";
                        return result;
                    }
                    var list1 = "<option value=\"0\">请选择...</option>";
                    foreach (var item in list)
                    {
                        list1 += "<option value=\"" + item.platform_id + "\">" + item.platform_name + "</option>";
                    }

                    result.success = true;
                    result.Msg = list1;
                    return result;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.Msg = "获取平台信息失败!";
                    return result;
                }
            }
        }



        /// <summary>
        /// 获取币种下拉框信息
        /// </summary>
        /// <returns></returns>
        public ShopResult GetCountryList()
        {
            ShopResult result = new ShopResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    //var list = db.Queryable<base_platform>().Where(a => a.del_flag).OrderBy("platform_id DESC").ToList();
                    //if (list.Count <= 0)
                    //{
                    //    result.success = false;
                    //    result.Msg = "暂无平台信息!";
                    //    return result;
                    //}
                    var list1 = "<option value=\"0\">请选择...</option>" +
                                "<option value=\"1\">日元</option>" +
                                "<option value=\"2\">美元</option>" +
                                "<option value=\"3\">欧元</option>" +
                                "<option value=\"4\">英镑</option>" +
                                "<option value=\"5\">人民币</option>" +
                                "<option value=\"6\">新加坡元</option>";
                    // foreach (var item in list)
                    //{
                    //  list1 += "<option value=\"" + item.platform_id + "\">" + item.platform_name + "</option>";
                    // }

                    result.success = true;
                    result.Msg = list1;
                    return result;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.Msg = "获取平台信息失败!";
                    return result;
                }
            }
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <returns></returns>
        public ShopResult GetShopList()
        {
            ShopResult result = new ShopResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var list = db.Queryable<base_shop>().Where(a => a.del_flag).OrderBy("shop_id DESC").ToList();
                    if (list.Count <= 0)
                    {
                        result.success = false;
                        result.Msg = "暂无店铺信息!";
                        return result;
                    }
                    var list1 = "<option value=\"0\">请选择...</option>";
                    foreach (var item in list)
                    {
                        list1 += "<option value=\"" + item.shop_id + "\">" + item.shop_name + "</option>";
                    }

                    result.success = true;
                    result.Msg = list1;
                    return result;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.Msg = "获取店铺信息失败!";
                    return result;
                }
            }

        }


        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public base_shop GetShopByID(int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    base_shop aa=db.Queryable<base_shop>().Where(s => s.shop_id == id).FirstOrDefault();
                    return aa;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 更新店铺信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateShop(base_shop model)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    bool isok = db.Update<base_shop>(new { shop_name = model.shop_name, shop_telephone = model.shop_telephone, shop_zipcode=model.shop_zipcode,
                                                           shop_address=model.shop_address
                    }, it => it.shop_id == model.shop_id);
                    return isok;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
