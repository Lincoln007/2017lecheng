using CommHelper;
using DBModel.Common;
using DBModel.DBmodel;
using DBModel.MaterialReceipt;
using IBLLService.MaterialReceipt;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.MaterialReceipt
{
    public class MaterialReceiptService : IMaterialReceiptService
    {
        /// <summary>
        /// 获取待收货信息
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <param name="express_code"></param>
        /// <returns></returns>
        public List<MaterialReceiptModel> GetMaterialReceiptList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg, string express_code)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var getwhere = db.Queryable<busi_purchase>()
                            .JoinTable<base_users>((s1, s2) => s1.create_user_id == s2.user_id)
                            .JoinTable<base_exp_comp>((s1, s3) => s1.express_id == s3.express_id)
                            .JoinTable<base_supplier>((s1, s4) => s1.supp_id == s4.supp_id)
                            .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1 and s1.purch_status=3")
                            .OrderBy("s1.purch_id DESC");
                    if (!string.IsNullOrWhiteSpace(express_code))
                    {
                        getwhere = getwhere.Where(s1 => s1.express_code.Contains(express_code));
                    }

                    totil = getwhere.Count();
                    var list = getwhere.Skip(onepagecount * (pagenum - 1)).Take(onepagecount)
                      .Select<base_users, base_exp_comp, base_supplier, MaterialReceiptModel>((s1, s2, s3, s4) => new MaterialReceiptModel
                      {
                          purch_id = s1.purch_id,
                          purch_code = s1.purch_code,
                          purchase_time = s1.create_time,
                          purch_status = s1.purch_status,
                          emp_name = s2.user_name,
                          express_code = s1.express_code,
                          express_coded = s3.express_coded,
                          express_name = s1.express_name,
                          purch_type = s1.purch_type,
                          supp_name = s4.supp_name,
                          OrderCode = s1.OrderCode,
                          isLocked = s1.isLocked,
                          Locked_userid = s1.Locked_userid,
                          express_id = s1.express_id,
                      }).ToList();
                    if (totil > 0)
                    {
                        foreach (var item in list)
                        {
                            if (item.purchase_time == null)
                            {
                                item.purchase_timeE = "";
                            }
                            else
                            {
                                item.purchase_timeE = item.purchase_time.Value.ToString("yyyy-MM-dd");
                            }
                            item.purch_statusE = item.purch_status == 1 ? "初始" : (item.purch_status == 2 ? "已采购" : (item.purch_status == 3 ? "待收货" : (item.purch_status == 4 ? "已全部到货" : "")));
                            item.purch_typeE = item.purch_type == 1 ? "订单采购" : (item.purch_type == 2 ? "库存采购" : "");
                            item.OrderCodeE = item.OrderCode == null ? "" : item.OrderCode;
                            //if (item.isLocked)
                            //{
                            //    var info = db.Queryable<base_users>().InSingle(item.Locked_userid);
                            //    if (info != null)
                            //    {
                            //        item.isLockedE = "已锁定&nbsp;&nbsp;(" + info.user_name + ")";
                            //    }
                            //    else
                            //    {
                            //        item.isLockedE = "已锁定&nbsp;&nbsp;" + "()";
                            //    }

                            //}
                            //else
                            //{
                            //    item.isLockedE = "未锁定";
                            //}
                        }
                    }
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
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public busi_purchase GetInfoByID(long? id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (!id.HasValue)
                {
                    return new busi_purchase();
                }
                try
                {
                    var list = db.Queryable<busi_purchase>().Where(s => s.del_flag).InSingle(id.Value);
                    if (list == null)
                    {
                        return new busi_purchase();
                    }
                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据采购ID获取待收货详情
        /// </summary>
        /// <param name="exmsg"></param>
        /// <param name="purch_id"></param>
        /// <returns></returns>
        public List<MaterialReceiptModelE> GetMaterialReceiptEList(out string exmsg, Int64? purch_id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                if (purch_id == 0)
                {
                    exmsg = "参数错误";
                    return null;
                }
                try
                {

                    var getwhere = db.Queryable<busi_purchasedetail>()
                       .JoinTable<busi_purchase>((s1, s2) => s1.purch_id == s2.purch_id)
                       .JoinTable<base_prod_code>((s1, s3) => s1.code_id == s3.code_id)
                       .JoinTable<base_supplier>((s1, s4) => s1.supp_id == s4.supp_id)
                       .JoinTable<base_prod_code, base_product>((s1, s3, s5) => s3.prod_id == s5.prod_id)
                       .JoinTable<base_product_imgs>((s1, s6) => s1.code_id == s6.code_id)
                       .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1 and s5.del_flag=1 and s6.del_flag=1 and s1.purch_id=" + purch_id + " and s1.purch_status=3")
                       .GroupBy("s1.code_id, s1.supp_id,s3.sku_code,s4.supp_name,s5.prod_url,s5.prod_id,s6.pic_url,s5.prod_id, s5.prod_title,s4.supp_url")
                       .Select<MaterialReceiptModelE>("s1.code_id, s1.supp_id,s3.sku_code,s4.supp_name,s5.prod_url,SUM(s1.purch_count) as purch_count,s6.pic_url,s5.prod_id, s5.prod_title,s4.supp_url")
                       .ToList();
                    if (getwhere.Count > 0)
                    {
                        foreach (var item in getwhere)
                        {
                            List<MaterialReceiptModelEE> getwhere2 = new List<MaterialReceiptModelEE>();
                            var getwhere1 = db.Queryable<busi_purchasedetail>()
                           .Where("del_flag=1 and purch_id=" + purch_id + " and code_id=" + item.code_id + " and supp_id=" + item.supp_id + "  and purch_status=3").ToList();
                            if (getwhere1.Count > 0)
                            {
                                List<Int64> ss = new List<Int64>();
                                foreach (var item1 in getwhere1)
                                {
                                    ss.Add(item1.send_detail_id);
                                }
                                getwhere2 = db.Queryable<busi_sendorder_detail>()
                                            .JoinTable<busi_sendorder>((s1, s2) => s1.order_id == s2.order_id)
                                            .JoinTable<busi_sendorder, busi_custorder>((s1, s2, s3) => s2.custorder_id == s3.order_id)
                                            .JoinTable<busi_custorder, base_shop>((s1, s3, s4) => s3.shop_id == s4.shop_id)
                                            .Where("s1.del_flag=1 and s2.del_flag=1 and s3.del_flag=1 and s4.del_flag=1")
                                            .Where(s1 => ss.Contains(s1.detail_id))
                                            .Select<MaterialReceiptModelEE>("s4.shop_name,s2.order_code,s1.prod_num,s2.order_tatus").ToList();
                                //if (getwhere2.Count > 0)
                                //{
                                //    foreach (var item2 in getwhere2)
                                //    {
                                //        //40.已配货;50.已拣选;60.已包装;70.已发货;80.已转运;90.已再入库 
                                //        item2.order_tatusE = item2.order_tatus == 40 ? "已配货" : (item2.order_tatus == 50 ? "已拣选" :
                                //        (item2.order_tatus == 60 ? "已包装" : (item2.order_tatus == 70 ? "已发货" :
                                //        (item2.order_tatus == 80 ? "已转运" : (item2.order_tatus == 90 ? "已再入库" : "")))));
                                //    }
                                //}
                            }
                            item.details = getwhere2;
                        }
                    }
                    exmsg = "";
                    return getwhere.ToList();
                }
                catch (Exception ex)
                {
                    exmsg = ex.ToString();
                    return null;
                }
            }
        }

        /// <summary>
        /// 采购收货
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lists"></param>
        /// <param name="purch_type"></param>
        /// <returns></returns>
        public MaterialReceiptResult Save(busi_purchase model, List<MaterialReceiptSaveModel> lists, int? purch_type)
        {
            bool rstNum = false;
            bool rstNums = false;
            bool rstNumss = false;
            MaterialReceiptResult result = new MaterialReceiptResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.BeginTran();
                    #region 判断
                    if (model == null)
                    {
                        result.success = false;
                        result.Msg = "请填写信息!";
                        return result;
                    }

                    if (model.purch_id == 0)
                    {
                        result.success = false;
                        result.Msg = "参数错误!";
                        return result;
                    }

                    if (!purch_type.HasValue || purch_type < 0)
                    {
                        result.success = false;
                        result.Msg = "参数错误!";
                        return result;
                    }
                    #endregion
                    int suc = 0;

                    var list = db.Queryable<busi_purchase>().Where(s => s.del_flag).InSingle(model.purch_id);
                    if (list == null)
                    {
                        result.success = false;
                        result.Msg = "不存在的采购信息";
                        return result;
                    }

                    if (list.purch_status == 4)
                    {
                        result.success = false;
                        result.Msg = "该单号已收货,请勿重复操作!";
                        return result;
                    }
                    list.edit_time = DateTime.Now;
                    list.purch_status = 4;
                    list.edit_user_id = 0;
                    rstNum = db.Update<busi_purchase>(list);
                    if (rstNum)
                    {
                        rstNums = db.Update<busi_purchasedetail>(new { purch_status = 4 }, a => a.purch_id == model.purch_id && a.purch_status == 3);
                        if (lists.Count > 0)
                        {
                            if (purch_type == 1)
                            {
                                foreach (var item in lists)
                                {
                                    var stock = db.Queryable<base_wh_stock>().Where(s => s.del_flag && s.code_id == item.code_id && s.prod_id == item.prod_id && s.location_id == 1 && s.wh_id == 1).FirstOrDefault();
                                    if (stock != null)
                                    {
                                        Decimal oldwh_stock = stock.stock_qty;
                                        Decimal newwh_stock = oldwh_stock + item.prod_num;
                                        rstNumss = db.Update<base_wh_stock>(new { stock_qty = newwh_stock }, s => s.stock_id == stock.stock_id);
                                        if (rstNumss)
                                        {
                                            suc += 1;
                                        }
                                    }
                                    else
                                    {
                                        var wh_warehouse = db.Queryable<base_wh_warehouse>().Where(s => s.del_flag).InSingle(1);
                                        if (wh_warehouse != null)
                                        {
                                            base_wh_stock wh_stock = new base_wh_stock();
                                            wh_stock.prod_id = item.prod_id;
                                            wh_stock.occupied_qty = 0;
                                            wh_stock.pallet_id = 0;
                                            wh_stock.purchase_price = 0;
                                            wh_stock.remark = "采购收货";
                                            wh_stock.reserve_qty = 0;
                                            wh_stock.service_life = "";
                                            wh_stock.stock_barcode = "";
                                            wh_stock.stock_class = 3;
                                            wh_stock.stock_code = "1";
                                            wh_stock.stock_qty = item.prod_num;
                                            wh_stock.stock_status = true;
                                            wh_stock.retrieval_time = DateTime.Now;
                                            wh_stock.storage_time = DateTime.Now;
                                            wh_stock.supplier_id = item.supp_id;
                                            wh_stock.using_state = 1;
                                            wh_stock.wh_id = 1;
                                            wh_stock.location_id = 1;
                                            wh_stock.locking_qty = 0;
                                            wh_stock.area_id = 0;
                                            wh_stock.asset_class_id = 0;
                                            wh_stock.code_id = item.code_id;
                                            wh_stock.consignor_id = 0;
                                            wh_stock.create_time = DateTime.Now;
                                            wh_stock.create_user_id = LoginUser.Current.user_id;
                                            wh_stock.del_flag = true;
                                            wh_stock.del_user_id = 0;
                                            wh_stock.edit_user_id = 0;
                                            wh_stock.del_time = DateTime.Now;
                                            wh_stock.edit_time = DateTime.Now;
                                            wh_stock.stock_id = Guid.NewGuid();
                                            var id = db.Insert<base_wh_stock>(wh_stock);
                                            if (id.ObjToBool())
                                            {
                                                suc += 1;
                                            }
                                        }
                                        else
                                        {
                                            db.RollbackTran();
                                            result.success = false;
                                            result.Msg = "操作失败！ 请先添加<span style=\"color:red;\">金华仓</span>！";
                                            return result;
                                        }

                                    }
                                }
                            }
                            else if (purch_type == 2)
                            {
                                foreach (var item in lists)
                                {
                                    var stock = db.Queryable<base_wh_stock>().Where(s => s.del_flag && s.code_id == item.code_id && s.prod_id == item.prod_id && s.location_id != 1 && s.wh_id == 1).FirstOrDefault();
                                    if (stock != null)
                                    {
                                        Decimal oldwh_stock = stock.stock_qty;
                                        Decimal newwh_stock = oldwh_stock + item.prod_num;
                                        if (newwh_stock >= 0)
                                        {
                                            rstNumss = db.Update<base_wh_stock>(new { stock_qty = newwh_stock }, s => s.stock_id == stock.stock_id);
                                            if (rstNumss)
                                            {
                                                suc += 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var location = db.Queryable<base_location>().Where(s => s.del_flag && s.wh_id == 1 && s.locat_type == 2).FirstOrDefault();
                                        if (location != null)
                                        {
                                            base_wh_stock wh_stock = new base_wh_stock();
                                            wh_stock.prod_id = item.prod_id;
                                            wh_stock.occupied_qty = 0;
                                            wh_stock.pallet_id = 0;
                                            wh_stock.purchase_price = 0;
                                            wh_stock.remark = "库存采购";
                                            wh_stock.reserve_qty = 0;
                                            wh_stock.service_life = "";
                                            wh_stock.stock_barcode = "";
                                            wh_stock.stock_class = 3;
                                            wh_stock.stock_code = "1";
                                            wh_stock.stock_qty = item.prod_num;
                                            wh_stock.stock_status = true;
                                            wh_stock.retrieval_time = DateTime.Now;
                                            wh_stock.storage_time = DateTime.Now;
                                            wh_stock.supplier_id = item.supp_id;
                                            wh_stock.using_state = 1;
                                            wh_stock.wh_id = 1;
                                            wh_stock.location_id = location.locat_id;
                                            wh_stock.locking_qty = 0;
                                            wh_stock.area_id = 0;
                                            wh_stock.asset_class_id = 0;
                                            wh_stock.code_id = item.code_id;
                                            wh_stock.consignor_id = 0;
                                            wh_stock.create_time = DateTime.Now;
                                            wh_stock.create_user_id = LoginUser.Current.user_id;
                                            wh_stock.del_flag = true;
                                            wh_stock.del_user_id = 0;
                                            wh_stock.edit_user_id = 0;
                                            wh_stock.del_time = DateTime.Now;
                                            wh_stock.edit_time = DateTime.Now;
                                            wh_stock.stock_id = Guid.NewGuid();
                                            var id = db.Insert<base_wh_stock>(wh_stock);
                                            if (id.ObjToBool())
                                            {
                                                suc += 1;
                                            }
                                        }
                                        else
                                        {
                                            db.RollbackTran();
                                            result.success = false;
                                            result.Msg = "<span style=\"color:red;\">金华仓</span>暂无库位,请先添加<span style=\"color:red;\">金华仓</span>库位，在进行操作！";
                                            return result;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (suc == lists.Count)
                    {
                        db.CommitTran();
                        result.success = true;
                        // result.URL = "/MaterialReceipt/IndexE?id=" + model.purch_id + "";
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
                catch (Exception)
                {
                    db.RollbackTran();
                    throw;
                }
            }
        }


        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="purch_id"></param>
        /// <param name="express_id"></param>
        /// <param name="express_code"></param>
        /// <param name="express_name"></param>
        /// <param name="OrderCode"></param>
        /// <returns></returns>
        public MaterialReceiptResult Modify(Int64? purch_id, Int64? express_id, string express_code, string express_name, string OrderCode)
        {
            bool rstNum = false;
            MaterialReceiptResult result = new MaterialReceiptResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.BeginTran();
                var list = db.Queryable<busi_purchase>().Where(s => s.del_flag).InSingle(purch_id.Value);
                if (list == null)
                {
                    db.RollbackTran();
                    result.success = false;
                    result.Msg = "无效的采购信息,操作失败!";
                    return result;
                }

                rstNum = db.Update<busi_purchase>(new { purch_status = 3, express_id = express_id, express_code = express_code, express_name = express_name, OrderCode = OrderCode }, a => a.purch_id == purch_id && a.purch_status == 3);

                if (rstNum)
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
