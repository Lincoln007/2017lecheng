using DBModel.Common;
using DBModel.DBmodel;
using IBLLService.Purchase;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Purchase
{
    public class PurchaseService : IPurchaseService
    {
        /// <summary>
        /// 添加库存采购信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ComResult AddPurchaseService(string sku, int num, int supId)
        {
            ComResult com = new ComResult();
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                //验证SKU是否存在
                var procodeModel = db.Queryable<base_prod_code>().Where(a => a.sku_code == sku).FirstOrDefault();
                if (procodeModel == null)
                {
                    com.State = 0;
                    com.Msg = "输入的SKU不存在请重新输入";
                    return com;
                }
                if (num <= 0)
                {
                    com.State = 0;
                    com.Msg = "采购数量不能小于等于0";
                    return com;
                }
                //验证供应商是否存在
                var supModel = db.Queryable<base_supplier>().Where(a => a.del_flag).InSingle(supId);
                if (supModel == null)
                {
                    com.State = 0;
                    com.Msg = "输入的供应商不存在请重新输入";
                    return com;
                }

                var list = db.Queryable<base_product>().Where(s => s.del_flag).InSingle(procodeModel.prod_id);
                DateTime time = DateTime.Now.AddDays(-3);
                var issuc = false;
                int id = 0;
                int ids = 0;
                var ss = db.Queryable<busi_purchase>().Where(s => s.del_flag && s.create_time >= time && s.purch_status == 1 && s.supp_id == supId && s.purch_type == 2).FirstOrDefault();
                if (ss != null)
                {
                    issuc = db.Update<busi_purchase>(new
                    {
                        edit_time = DateTime.Now,
                        edit_user_id = LoginUser.Current.user_id,
                        purch_numb = ss.purch_numb + num,
                        sum_money = ss.sum_money + list.price_cn * num,
                        purch_sum = list.price_cn * num + ss.purch_sum,
                    }, s1 => s1.purch_id == ss.purch_id); // 批量修改订单表 状态                                                       
                }
                else
                {
                    busi_purchase pur = new busi_purchase();
                    pur.abnormal_remark = "";
                    pur.create_time = DateTime.Now;
                    pur.create_user_id = LoginUser.Current.user_id;
                    pur.del_flag = true;
                    pur.del_time = DateTime.Now;
                    pur.supp_id = supId;
                    pur.del_user_id = 0;
                    pur.edit_time = DateTime.Now;
                    pur.edit_user_id = 0;
                    pur.purch_categories = 0;
                    pur.purch_code = GetPurchaseCode();
                    pur.purch_numb = num;
                    pur.purch_remark = "库存采购";
                    pur.purch_status = 1;
                    pur.purch_type = 2;
                    pur.remark = "库存采购";
                    pur.sum_freight = 0;
                    pur.sum_money = list.price_cn * num;
                    pur.purch_sum = list.price_cn * num;
                    pur.express_id = 0;
                    pur.express_code = null;
                    pur.express_name = null;
                    pur.OrderCode = null;
                    pur.isLocked = false;
                    pur.Locked_userid = 0;
                    var obj = db.Insert<busi_purchase>(pur);  // 插入采购表
                    id = obj.ObjToInt();
                }
                if (issuc || id > 0)
                {
                    busi_purchasedetail purchasedetail = new busi_purchasedetail();
                    purchasedetail.code_id = procodeModel.code_id;
                    purchasedetail.create_time = DateTime.Now;
                    purchasedetail.create_user_id = LoginUser.Current.user_id;
                    purchasedetail.cust_send_billcode = "";
                    purchasedetail.custorderdetail_id = 0;
                    purchasedetail.del_flag = true;
                    purchasedetail.del_time = DateTime.Now;
                    purchasedetail.del_user_id = 0;
                    purchasedetail.edit_time = DateTime.Now;
                    purchasedetail.edit_user_id = 0;
                    purchasedetail.err_count = 0;
                    purchasedetail.is_cancel = true;
                    purchasedetail.lack_count = 0;
                    purchasedetail.prod_id = procodeModel.code_id;
                    purchasedetail.purch_count = num;
                    purchasedetail.purch_id = id;
                    purchasedetail.purch_rice = list.price_cn;
                    purchasedetail.purch_status = 1;
                    purchasedetail.purch_type = 2;
                    purchasedetail.purch_url = supModel.supp_url;
                    purchasedetail.remark = "库存采购";
                    purchasedetail.send_detail_id = 0;
                    purchasedetail.shop_id = 0;
                    purchasedetail.supp_id = supId;
                    purchasedetail.wh_id = 1;
                    var objs = db.Insert<busi_purchasedetail>(purchasedetail);
                    ids = objs.ObjToInt();
                }
                if (ids > 0)
                {
                    com.State = 1;
                    com.Msg = "操作成功";
                }
                else
                {
                    com.State = 0;
                    com.Msg = "操作失败";
                }
                return com;
            }
        }


        /// <summary>
        /// 获取新增采购编号
        /// </summary>
        /// <returns></returns>
        public string GetPurchaseCode()
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:MM");
            Random rd = new Random();
            int num = rd.Next(999999);
            string purchasecode = "C" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + num.ToString();
            return purchasecode;
        }
    }
}
