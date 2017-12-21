using DBModel.Base;
using DBModel.Common;
using DBModel.DBmodel;
using IBLLService.Base.Platform;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Base.Platform
{
    public class PlatformService : IPlatformService
    {

        /// <summary>
        /// 获取平台列表信息
        /// </summary>
        /// <returns></returns>
        public List<base_platform> GetPlatformList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<base_platform>()
                                   .Where(a => a.del_flag);
                    totil = getwhere.Count();
                    var list = getwhere.OrderBy("platform_id DESC")
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
        public PlatformResult Save(base_platform model)
        {
            bool rstNum = false;
            PlatformResult result = new PlatformResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (string.IsNullOrWhiteSpace(model.platform_name))
                {
                    result.success = false;
                    result.Msg = "请填写平台名称!";
                    return result;
                }

                if (model.platform_id > 0)
                {
                    var list = db.Queryable<base_platform>().InSingle(model.platform_id);
                    if (list == null)
                    {
                        result.success = false;
                        result.Msg = "不存在的平台信息";
                        return result;
                    }


                    var base_platform = db.Queryable<base_platform>().Where(c => c.del_flag && c.platform_name == model.platform_name && c.platform_id != model.platform_id).ToList();
                    if (base_platform.Count > 0)
                    {
                        result.success = false;
                        result.Msg = "已存在的平台信息，操作失败!";
                        return result;
                    }
                    list.edit_time = DateTime.Now;
                    list.edit_user_id = LoginUser.Current.user_id;
                    list.platform_name = model.platform_name;
                    rstNum = db.Update<base_platform>(list);
                }
                else
                {
                    var base_platform = db.Queryable<base_platform>().Where(c => c.del_flag && c.platform_name == model.platform_name).ToList();
                    if (base_platform.Count > 0)
                    {
                        result.success = false;
                        result.Msg = "已存在的平台信息，操作失败!";
                        return result;
                    }
                    model.create_time = DateTime.Now;
                    model.create_user_id = LoginUser.Current.user_id;
                    model.del_flag = true;
                    model.del_time = DateTime.Now;
                    model.del_user_id = 0;
                    model.edit_time = DateTime.Now;
                    model.edit_user_id = 0;
                    model.remark = model.platform_name;
                    var id = db.Insert<base_platform>(model);
                    if (id.ObjToInt() > 0)
                    {
                        rstNum = true;
                    }
                }

                if (rstNum)
                {
                    result.success = true;
                    result.URL = "/Platform/Index";
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
        public PlatformResult Del(long? id)
        {
            bool rstNum = false;
            PlatformResult result = new PlatformResult();
            if (id == 0)
            {
                result.success = false;
                result.Msg = "参数错误";
                return result;
            }
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                var model = db.Queryable<base_platform>().InSingle(id.Value);
                if (model == null)
                {
                    result.success = false;
                    result.Msg = "不存在的平台信息";
                    return result;
                }
                model.del_time = DateTime.Now;
                model.del_user_id = LoginUser.Current.user_id;
                model.del_flag = false;
                rstNum = db.Update<base_platform>(model);
                if (rstNum)
                {
                    result.success = true;
                    result.URL = "/Platform/Index";
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
