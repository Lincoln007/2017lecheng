using DBModel.Base;
using DBModel.Common;
using DBModel.DBmodel;
using IBLLService.Base.StorageBin;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Base.StorageBin
{
    public class StorageBinService : IStorageBinService
    {
        /// <summary>
        ///  获取信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="wh_id"></param>
        /// <param name="locat_code"></param>
        /// <returns></returns>
        public List<StorageBinModel> GetStorageBinList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, long? wh_id, string locat_code)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var sql1 = db.Queryable<base_location>()
                         .JoinTable<base_wh_warehouse>((s1, s2) => s1.wh_id == s2.wh_id)
                         .Where("s1.del_flag=1 and s2.del_flag=1")
                         .OrderBy("s1.locat_id DESC");
                    if (!string.IsNullOrWhiteSpace(locat_code))
                    {
                        sql1 = sql1.Where(s1 => s1.locat_code.Contains(locat_code));
                    }
                    if (wh_id > 0)
                    {
                        sql1 = sql1.Where(s1 => s1.wh_id == wh_id);
                    }
                    totil = sql1.Count();
                    var jList = sql1.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                                    .Select<StorageBinModel>("s1.locat_id,s1.wh_id,s1.locat_code,s1.create_time,s2.wh_name")
                                    .ToList();
                    if (jList.Count > 0)
                    {
                        foreach (var item in jList)
                        {
                            var sql = db.Queryable<base_wh_stock>()
                                .Where(" del_flag=1 and location_id=" + item.locat_id + " and wh_id=" + item.wh_id + "")
                                .GroupBy("location_id,wh_id")
                                .Select<StorageBinModel>("location_id,wh_id, SUM(stock_qty) AS stock_qty")
                                .SingleOrDefault();
                            if (sql != null)
                            {
                                item.stock_qty = sql.stock_qty;
                            }
                            else
                            {
                                item.stock_qty = 0;
                            }
                            item.create_timeE = item.create_time.ToString("yyyy-MM-dd");
                        }
                    }

                    totilpage = totil / onepagecount;
                    exmsg = "";
                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }
                    return jList.ToList();
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
        public StorageBinResult Save(base_location model)
        {
            bool rstNum = false;
            StorageBinResult result = new StorageBinResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                #region 判断
                if (model == null)
                {
                    result.success = false;
                    result.Msg = "请填写信息!";
                    return result;
                }

                if (model.wh_id == 0)
                {
                    result.success = false;
                    result.Msg = "请选择仓库!";
                    return result;
                }

                if (string.IsNullOrWhiteSpace(model.locat_code))
                {
                    result.success = false;
                    result.Msg = "请填写库位号!";
                    return result;
                }
                #endregion

                if (model.locat_id > 0)
                {

                }
                else
                {
                    var base_shop = db.Queryable<base_location>().Where(c => c.del_flag && c.locat_code == model.locat_code).ToList();
                    if (base_shop.Count > 0)
                    {
                        result.success = false;
                        result.Msg = "该仓库已存在的库位号，操作失败!";
                        return result;
                    }
                    model.remark = model.locat_code;
                    model.locat_status = true;
                    model.locat_type = 2;
                    model.create_time = DateTime.Now;
                    model.create_user_id = LoginUser.Current.user_id;
                    model.del_flag = true;
                    model.del_user_id = 0;
                    model.del_time = DateTime.Now;
                    model.edit_time = DateTime.Now;
                    model.edit_user_id = 0;
                    var id = db.Insert<base_location>(model);
                    if (id.ObjToInt() > 0)
                    {
                        rstNum = true;
                    }
                }
                if (rstNum)
                {
                    result.success = true;
                    result.URL = "/StorageBin/Index";
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
        /// 获取库存详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="locat_id"></param>
        /// <returns></returns>
        public List<StorageBinModel> GetStorageBinEList(out string exmsg, long? wh_id, long? locat_id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (wh_id == 0 || locat_id == 0)
                {
                    exmsg = "参数错误!";
                    return null;
                }

                try
                {
                    var sql = db.Queryable<base_wh_stock>()
                         .JoinTable<base_wh_warehouse>((s1, s2) => s1.wh_id == s2.wh_id)
                         .JoinTable<base_product>((s1, s3) => s3.prod_id == s1.prod_id)
                         .JoinTable<base_location>((s1, s5) => s1.location_id == s5.locat_id)
                         .JoinTable<base_prod_code>((s1, s6) => s6.code_id == s1.code_id)
                         .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s5.del_flag=1 and s6.del_flag=1 and s1.wh_id=" + wh_id + " and s1.location_id=" + locat_id + "")
                         .GroupBy(" s1.code_id, s1.wh_id, s1.location_id, s6.sku_code, s1.stock_qty, s2.wh_name, s3.prod_title, s5.locat_code")
                         .Select<StorageBinModel>(" s6.sku_code, s1.stock_qty, s2.wh_name, s3.prod_title, s5.locat_code").ToList();
                    exmsg = "";
                    return sql.ToList();
                }
                catch (Exception ex)
                {
                    exmsg = ex.ToString();
                    return null;
                }

            }
        }


        /// <summary>
        /// 获取上下架信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="wh_id"></param>
        /// <param name="sku_code"></param>
        /// <param name="locat_code"></param>
        /// <returns></returns>
        public List<StorageBinModel> GetStorageBinInOutList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, long? wh_id, string locat_code, string sku_code)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var sql = db.Queryable<base_wh_stock>()
                          .JoinTable<base_wh_warehouse>((s1, s2) => s1.wh_id == s2.wh_id)
                          .JoinTable<base_location>((s1, s3) => s3.locat_id == s1.location_id)
                          .JoinTable<base_prod_code>((s1, s4) => s4.code_id == s1.code_id)
                          .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1")
                        // and s3.locat_type=2
                          .OrderBy("s1.code_id DESC");
                    if (!string.IsNullOrWhiteSpace(locat_code))
                    {
                        sql = sql.Where<base_location>((s1, s3) => s3.locat_code.Contains(locat_code));
                    }
                    if (wh_id > 0)
                    {
                        sql = sql.Where(s1 => s1.wh_id == wh_id);
                    }
                    if (!string.IsNullOrWhiteSpace(sku_code))
                    {
                        sql = sql.Where<base_prod_code>((s1, s4) => s4.sku_code.Contains(sku_code));
                    }

                    totil = sql.Count();
                    var jList2 = sql.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                                 .Select<StorageBinModel>("s1.stock_id,s1.stock_qty, s3.locat_code,s2.wh_name ,s4.sku_code ")
                                 .ToList();
                    totilpage = totil / onepagecount;
                    exmsg = "";
                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }
                    return jList2.ToList();
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
        /// 上下架
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public StorageBinResult StockInOut(Guid? id, Decimal count, int state)
        {
            bool rstNum = false;
            bool rstNums = false;
            StorageBinResult result = new StorageBinResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.BeginTran();
                if (string.IsNullOrWhiteSpace(id.ToString()) || state == 0)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "参数错误!";
                    return result;
                }

                if (count <= 0)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "请填写正确的数量!";
                    return result;
                }

                var list = db.Queryable<base_wh_stock>().InSingle(id.Value);
                if (list == null)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "不存在的库存信息,操作失败!";
                    return result;
                }

                if (!list.del_flag)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "已删除的库存信息,操作失败!";
                    return result;
                }

                Decimal stock_qty = list.stock_qty;
                if (state == 1)
                {
                    list.stock_qty = list.stock_qty + count;
                    rstNum = db.Update<base_wh_stock>(list);
                    if (rstNum)
                    {

                        db.Update<base_wh_stock>(new { stock_qty = list.stock_qty }, s => s.stock_id == id);

                        base_wh_stock_inout model = new base_wh_stock_inout();
                        model.add_time = DateTime.Now;
                        model.last_count = list.stock_qty;
                        model.oper_count = count;
                        model.oper_type = state;
                        model.pre_count = stock_qty;
                        model.user_id = LoginUser.Current.user_id;
                        model.user_name = LoginUser.Current.real_name;
                        model.stock_id = id.Value;
                        var issuc = db.Insert<base_wh_stock_inout>(model);
                        if (issuc.ObjToInt() > 0)
                        {
                            rstNums = true;
                        }
                    }
                }
                else if (state == 2)
                {
                    if (stock_qty < count)
                    {
                        db.RollbackTran();
                        result.success = false;
                        result.Msg = "下架数量不得大于商品数量,操作失败!";
                        return result;
                    }

                    list.stock_qty = list.stock_qty - count;
                    rstNum = db.Update<base_wh_stock>(list);
                    if (rstNum)
                    {

                        db.Update<base_wh_stock>(new { stock_qty = list.stock_qty }, s => s.stock_id == id);

                        base_wh_stock_inout model = new base_wh_stock_inout();
                        model.add_time = DateTime.Now;
                        model.last_count = list.stock_qty;
                        model.oper_count = count;
                        model.oper_type = state;
                        model.pre_count = stock_qty;
                        model.user_id = LoginUser.Current.user_id;
                        model.user_name = LoginUser.Current.real_name;
                        model.stock_id = id.Value;
                        var issuc = db.Insert<base_wh_stock_inout>(model);
                        if (issuc.ObjToInt() > 0)
                        {
                            rstNums = true;
                        }
                    }
                }

                if (rstNums && rstNum)
                {
                    db.CommitTran();
                    result.success = true;
                    result.Msg = "操作成功";
                    return result;
                }
                else
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "操作失败";
                    return result;
                }
            }
        }


        /// <summary>
        /// 获取仓库相信
        /// </summary>
        /// <returns></returns>
        public StorageBinResult GetWh_nameList()
        {
            StorageBinResult result = new StorageBinResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var list = db.Queryable<base_wh_warehouse>().Where(a => a.del_flag).OrderBy("wh_id DESC").ToList();
                    if (list.Count <= 0)
                    {
                        result.success = false;
                        result.Msg = "暂无仓库信息!";
                        return result;
                    }
                    var list1 = "<option value=\"0\">请选择...</option>";
                    foreach (var item in list)
                    {
                        list1 += "<option value=\"" + item.wh_id + "\">" + item.wh_name + "</option>";
                    }

                    result.success = true;
                    result.Msg = list1;
                    return result;
                }
                catch (Exception ex)
                {
                    result.success = false;
                    result.Msg = "获取仓库信息失败!";
                    return result;
                }
            }
        }


        public StorageBinResult Save_sku(Int64 wh_id, Int64 locat_id, string sku, int skucount)
        {
            bool rstNum = false;
            bool rstNums = false;
            StorageBinResult result = new StorageBinResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.BeginTran();
                if (wh_id == 0 || locat_id == 0)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "参数错误!";
                    return result;
                }

                if (skucount <= 0)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "请填写正确的数量!";
                    return result;
                }

                if (string.IsNullOrEmpty(sku))
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "请填写sku!";
                    return result;
                }
                var list = db.Queryable<base_prod_code>().Where(a => a.del_flag && a.sku_code == sku).FirstOrDefault();
                if (list == null)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "系统中不存在的sku!";
                    return result;
                }

                var wh_stock = db.Queryable<base_wh_stock>().Where(a => a.del_flag && a.wh_id == wh_id && a.code_id == list.code_id).FirstOrDefault();
                if (wh_stock == null)
                {
                    var _supp_rel = db.Queryable<base_prod_supp_rel>().Where(a => a.del_flag && a.prod_id == list.prod_id).ToList();
                    long suppid = _supp_rel.Find(b => b.lev_purch == _supp_rel.Min(s => s.lev_purch)).supp_id;
                    base_wh_stock _stock = new base_wh_stock();
                    _stock.prod_id = list.prod_id;
                    _stock.occupied_qty = 0;
                    _stock.pallet_id = 0;
                    _stock.purchase_price = 0;
                    _stock.remark = "库存采购";
                    _stock.reserve_qty = 0;
                    _stock.service_life = "";
                    _stock.stock_barcode = "";
                    _stock.stock_class = 3;
                    _stock.stock_code = "1";
                    _stock.stock_qty = skucount;
                    _stock.stock_status = true;
                    _stock.retrieval_time = DateTime.Now;
                    _stock.storage_time = DateTime.Now;
                    _stock.supplier_id = suppid;
                    _stock.using_state = 1;
                    _stock.wh_id = 1;
                    _stock.location_id = locat_id;
                    _stock.locking_qty = 0;
                    _stock.area_id = 0;
                    _stock.asset_class_id = 0;
                    _stock.code_id = list.code_id;
                    _stock.consignor_id = 0;
                    _stock.create_time = DateTime.Now;
                    _stock.create_user_id = LoginUser.Current.user_id;
                    _stock.del_flag = true;
                    _stock.del_user_id = 0;
                    _stock.edit_user_id = 0;
                    _stock.del_time = DateTime.Now;
                    _stock.edit_time = DateTime.Now;
                    _stock.stock_id = Guid.NewGuid();
                    var id = db.Insert<base_wh_stock>(_stock);
                    if (id.ObjToBool())
                    {
                        rstNums = true;
                    }
                }
                else
                {
                    rstNums = db.Update<base_wh_stock>(new { stock_qty = wh_stock.stock_qty + skucount }, s => s.stock_id == wh_stock.stock_id);
                }
                if (rstNums)
                {
                    db.CommitTran();
                    result.success = true;
                    result.Msg = "操作成功";
                    return result;
                }
                else
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "操作失败";
                    return result;
                }
            }

        }

    }
}
