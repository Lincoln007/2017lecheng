using DBModel.Common;
using DBModel.DBmodel;
using IBLLService;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLLServices.Product
{
    public class NewProductService:INewProduct
    {
        /// <summary>
        /// 判断款号是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool PcodeIsExit(string code)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var isok = db.Queryable<base_product>().Where(s => s.prod_style == code).FirstOrDefault();
                    if (isok==null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }


        private string Getmodel(string SKU)
        {
            //string str = "1111aaaa@@@@@@@@bbbb2222";
            Match m = Regex.Match(SKU, @"-([\s\S]*?)-");
            if (m.Success)
            {
                return m.Result("$1");  // 输出aaaa与bbbb之间的字符串
            }
            else
            {
                return "";
            }
        }

        private string GetSize(string SKU)
        {
            return SKU.Substring(SKU.LastIndexOf("-")+1);  
        }
        public bool InsertProdcut(string code, string proname, string bgname, string bgcode, string bgprice, string weight, string price, string remark, string clasf, string profly, string promodel, string prosize, List<string> skulist)
        {
            bool isexit=PcodeIsExit(code);
            if (isexit)
            {
                throw new Exception("该款号已存在");
            }
            int proid = 0;
            int codeid=0;
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                db.CommandTimeOut = 5000;//设置超时时间 5秒
                try
                {
                    db.BeginTran();//开启事务
                    base_product add = new base_product();
                    add.brand_id = 1;
                    add.create_time = DateTime.Now;
                    add.create_user_id = LoginUser.Current.user_id;
                    add.del_flag = true;
                    add.del_time = null;
                    add.del_user_id = 0;
                    add.edit_time = DateTime.Now;
                    add.edit_user_id = 0;
                    add.ex_type = 0;
                    add.pic_url = "";
                    add.bgcode = bgname;
                    add.bgcode = bgcode;
                    add.pre_weight = Convert.ToInt32(weight);
                    add.price_us = Convert.ToDecimal(bgprice);
                    add.price_cn = Convert.ToDecimal(price);
                    add.prod_class_id = Convert.ToInt32(profly);
                    add.prod_developer = LoginUser.Current.real_name;
                    add.prod_picpath = "";
                    add.prod_property = Convert.ToInt32(clasf);
                    add.prod_status = 0;
                    add.prod_style = code;
                    add.prod_title = proname;
                    add.prod_url = "";
                    add.real_weight = Convert.ToInt32(weight);
                    add.remark = remark;
                    add.title_en = "";
                    add.title_jp = "";
                    add.model = promodel;
                    add.size = prosize;
                  
                    proid=db.Insert<base_product>(add).ObjToInt();
                    if (proid > 0)//如果第一张表插入正常
                    {
                        base_prod_code procode = new base_prod_code();
                        base_product_imgs proimgs = new base_product_imgs();
                        foreach(var item in skulist)
                        {
                            procode.bar_code = item; //插入SKU表
                            procode.create_time = DateTime.Now;
                            procode.create_user_id = LoginUser.Current.user_id;
                            procode.del_flag = true;
                            procode.del_time = DateTime.Now;
                            procode.del_user_id = 0;
                            procode.edit_time = DateTime.Now;
                            procode.edit_user_id = 0;
                            procode.isbn_code = "";
                            procode.jcode = "";
                            procode.prod_id = proid;
                            procode.prod_model=Getmodel(item);
                            procode.prod_size = GetSize(item);
                            procode.remark = "";
                            procode.sku_code = item;
                            codeid=db.Insert<base_prod_code>(procode).ObjToInt();

                            proimgs.code_id = codeid;
                            proimgs.create_time = DateTime.Now;
                            proimgs.create_user_id = LoginUser.Current.user_id;
                            proimgs.del_flag = true;
                            proimgs.del_time = DateTime.Now;
                            proimgs.del_user_id = 0;
                            proimgs.edit_time = DateTime.Now;
                            proimgs.edit_user_id = 0;
                            proimgs.pic_describe = "";
                            proimgs.pic_url = "";
                            proimgs.prod_id = proid;
                            proimgs.prod_model = Getmodel(item);
                            proimgs.remark = "";
                            db.Insert<base_product_imgs>(proimgs);

                        }
                        db.CommitTran(); //提交事务 
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    db.RollbackTran(); //回滚事务
                    throw ex;
                }
            }
        }


        public bool UpdateProdcut(string code, string proname, string bgname,string bgcode, string bgprice, string weight, string price, string remark, string profly, string promodel, string prosize, int proid, int prostatus, string purchase_url)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    bool isok = db.Update<base_product>(new {
                        bgname= bgname,
                        bgcode =bgcode,
                        pre_weight =weight.ObjToInt(), 
                        price_us = bgprice.ObjToDecimal(),
                        price_cn =price.ObjToDecimal(),
                        remark=remark,
                        prod_class_id = profly.ObjToInt(),
                        model = promodel,
                        size = prosize,
                        prod_style = code,
                        prod_title = proname,
                        prod_status = prostatus,
                        prod_url = purchase_url
                       }, s => s.prod_id == proid);
                    string [] modellist=promodel.Split(',');
                    foreach(string item in modellist)
                    {
                        base_product_imgs a=db.Queryable<base_product_imgs>().Where(s => s.prod_id == proid).Where(s => s.prod_model == item).FirstOrDefault();
                        if (null==a)
                        { 
                            base_product_imgs proimgs=new base_product_imgs ();
                            proimgs.code_id = 0;
                            proimgs.create_time = DateTime.Now;
                            proimgs.create_user_id = LoginUser.Current.user_id;
                            proimgs.del_flag = false;
                            proimgs.del_time = DateTime.Now;
                            proimgs.del_user_id = 0;
                            proimgs.edit_time = DateTime.Now;
                            proimgs.edit_user_id = 0;
                            proimgs.pic_describe = "";
                            proimgs.pic_url = "";
                            proimgs.prod_id = proid;
                            proimgs.prod_model = item;
                            proimgs.remark = "";
                            db.Insert<base_product_imgs>(proimgs);
                        }
                    }
                    if (isok)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }
    }
}
