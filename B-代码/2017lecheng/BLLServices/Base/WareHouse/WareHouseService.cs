using DBModel.Base;
using DBModel.Common;
using DBModel.DBmodel;
using IBLLService.Base.WareHouse;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Base.WareHouse
{
    public class WareHouseService : IWareHouseService
    {

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public List<base_wh_warehouse> GetWareHouseList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<base_wh_warehouse>()
                                   .Where(a => a.del_flag);
                    totil = getwhere.Count();
                    var list = getwhere.OrderBy("wh_id DESC")
                                   .Skip(onepagecount * (pagenum - 1))
                                   .Take(onepagecount).ToList();
                    totilpage = totil / onepagecount;
                    exmsg = "";
                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }
                    return list;
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
        public WareHouseResult Save(base_wh_warehouse model)
        {
            bool rstNum = false;
            bool rstNums = false;
            WareHouseResult result = new WareHouseResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (string.IsNullOrWhiteSpace(model.wh_name))
                {
                    result.success = false;
                    result.Msg = "请填写仓库名称!";
                    return result;
                }

                if (model.wh_id > 0)
                {
                    var list = db.Queryable<base_wh_warehouse>().Where(s => s.del_flag).InSingle(model.wh_id);
                    if (list == null)
                    {
                        result.success = false;
                        result.Msg = "不存在的仓库信息";
                        return result;
                    }
                    if (list.wh_id == 1)
                    {
                        result.success = false;
                        result.Msg = "" + list.wh_name + "您无权修改，操作失败！";
                        return result;
                    }
                    var base_platform = db.Queryable<base_wh_warehouse>().Where(c => c.del_flag && c.wh_name == model.wh_name && c.wh_id != model.wh_id).ToList();
                    if (base_platform.Count > 0)
                    {
                        result.success = false;
                        result.Msg = "已存在的仓库信息，操作失败!";
                        return result;
                    }
                    list.edit_time = DateTime.Now;
                    list.edit_user_id = LoginUser.Current.user_id;
                    list.wh_name = model.wh_name;
                    rstNum = db.Update<base_wh_warehouse>(list);
                    rstNums = true;

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(model.wh_address))
                    {
                        result.success = false;
                        result.Msg = "请填写仓库地址!";
                        return result;
                    }

                    if (string.IsNullOrWhiteSpace(model.wh_code))
                    {
                        result.success = false;
                        result.Msg = "请填写仓库邮编!";
                        return result;
                    }

                    if (string.IsNullOrWhiteSpace(model.wh_phone))
                    {
                        result.success = false;
                        result.Msg = "请填写仓库电话!";
                        return result;
                    }

                    var base_platform = db.Queryable<base_wh_warehouse>().Where(c => c.del_flag && c.wh_name == model.wh_name).ToList();
                    if (base_platform.Count > 0)
                    {
                        result.success = false;
                        result.Msg = "已存在的平台信息，操作失败!";
                        return result;
                    }
                    model.net_no = 0;
                    model.wh_alarm_qty = 0;
                    model.wh_lowlimit_qty = 0;
                    model.wh_uplimit_qty = 0;
                    model.create_time = DateTime.Now;
                    model.create_user_id = LoginUser.Current.user_id;
                    model.del_flag = true;
                    model.del_time = DateTime.Now;
                    model.del_user_id = 0;
                    model.edit_time = DateTime.Now;
                    model.edit_user_id = 0;
                    model.remark = model.wh_name;
                    model.wh_status = true;
                    var id = db.Insert<base_wh_warehouse>(model);
                    if (id.ObjToInt() > 0)
                    {
                        rstNum = true;
                        base_location location = new base_location();
                        location.create_time = DateTime.Now;
                        location.create_user_id = LoginUser.Current.user_id;
                        location.del_flag = true;
                        location.del_time = DateTime.Now;
                        location.del_user_id = 0;
                        location.edit_time = DateTime.Now;
                        location.edit_user_id = 0;
                        location.locat_code = "zz-zz-zz";
                        location.locat_status = true;
                        location.locat_type = 1;
                        location.remark = "临时库位";
                        location.wh_id = id.ObjToInt();
                        var location_id = db.Insert<base_location>(location);
                        if (location_id.ObjToInt() > 0)
                        {
                            rstNums = true;
                        }
                    }
                }

                if (rstNum && rstNums)
                {
                    result.success = true;
                    result.URL = "/WareHouse/Index";
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
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WareHouseResult Del(long? id)
        {
            bool rstNum = false;
            WareHouseResult result = new WareHouseResult();
            if (id == 0)
            {
                result.success = false;
                result.Msg = "参数错误";
                return result;
            }
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var model = db.Queryable<base_wh_warehouse>().InSingle(id.Value);
                if (model == null)
                {
                    result.success = false;
                    result.Msg = "不存在的平台信息";
                    return result;
                }
                if (model.wh_id == 1)
                {
                    result.success = false;
                    result.Msg = "" + model.wh_name + "您无权删除，操作失败！";
                    return result;
                }
                model.del_time = DateTime.Now;
                model.del_user_id = LoginUser.Current.user_id;
                model.del_flag = false;
                rstNum = db.Update<base_wh_warehouse>(model);
                if (rstNum)
                {
                    result.success = true;
                    result.URL = "/WareHouse/Index";
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


    }
}
