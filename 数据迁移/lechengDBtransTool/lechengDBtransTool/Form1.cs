using DBModel.DBmodel;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lechengDBtransTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            return SKU.Substring(SKU.LastIndexOf("-") + 1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime start=DateTime.Now;
            busi_product prolist = null;
            busi_supplier oldsupp = null;           
            List<busi_productsku> skulist = null;
            List<busi_product> myprolist = null;
            int proid = 0;
            int codeid = 0;
            int suppid = 0;
            using (var db = SugarDao.GetInstance(SugarDao.ConnectionGloablString))//原先数据库
            {
                myprolist = db.Queryable<busi_product>().Where(s => s.IsDel == false).Where(s => s.ProID > 0).OrderBy(s => s.ProID, OrderByType.Desc).ToList();              
            }
            foreach (busi_product myitem in myprolist)
            {
                using (var db = SugarDao.GetInstance(SugarDao.ConnectionGloablString))//原先数据库
                {
                    try
                    {
                        //prolist = db.Queryable<busi_product>().Where(s => s.IsDel == false).Where(s => s.ProID > 0).OrderBy(s => s.ProID).Take(1).First();
                        prolist=myitem;
                        skulist = db.Queryable<busi_productsku>().Where(s => s.IsDel == false).Where(s => s.ProID == prolist.ProID).ToList();
                        if (skulist.Count==0)
                        {
                            continue;
                        }
                        oldsupp = db.Queryable<busi_supplier>().Where(s => s.SupplierID == prolist.SupplierID).FirstOrDefault();
                        if (null == oldsupp)
                        {
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                
                using (var db = SugarDao.GetInstance(SugarDao.ConnectionString))//新数据库
                {
                    try
                    {
                        base_product add = new base_product(); //添加产品
                        add.brand_id = 1;
                        add.create_time = DateTime.Now;
                        add.create_user_id = 1;
                        add.del_flag = true;
                        add.del_time = null;
                        add.del_user_id = 0;
                        add.edit_time = DateTime.Now;
                        add.edit_user_id = 0;
                        add.ex_type = 0;
                        add.pic_url = prolist.PicUrl;  //产品主图
                        add.bgcode = prolist.BCode;
                        add.pre_weight = prolist.SWeight;
                        add.price_us = Convert.ToDecimal(prolist.PriceR);
                        add.price_cn = Convert.ToDecimal(prolist.PriceR);
                        add.prod_class_id = Convert.ToInt32(prolist.CID);
                        add.prod_developer = prolist.DevePeople;
                        add.prod_picpath = ""; 
                        add.prod_property = 1;   //Convert.ToInt32(clasf);
                        add.prod_status = 0;
                        add.prod_style = prolist.Code;
                        add.prod_title = prolist.Name;
                        add.prod_url = prolist.Url;
                        add.real_weight = prolist.SWeight;
                        add.remark = prolist.Remark;
                        add.title_en = "";
                        add.title_jp = "";
                        add.model = prolist.Model;
                        add.size = prolist.Size;
                        proid = db.Insert<base_product>(add).ObjToInt();

                        base_supplier isin=db.Queryable<base_supplier>().Where(s => s.supp_name == oldsupp.Name).FirstOrDefault();
                        if (null==isin)//不存在
                        {
                            base_supplier supp = new base_supplier();//添加供应商,需要先判断这个供应商是否存在系统里
                            supp.create_user_id = 1;
                            supp.del_flag = true;
                            supp.supp_name = oldsupp.Name;
                            supp.supp_url = oldsupp.Url; //供应商店铺地址
                            supp.remark = oldsupp.Desc;
                            supp.supp_code = oldsupp.Code;
                            supp.is_grade = true;
                            supp.purc_mode = 1;
                            supp.purc_priority = 1;
                            supp.purc_sourceid = 1;
                            supp.create_time = DateTime.Now;
                            supp.edit_time = DateTime.Now;
                            supp.del_time = DateTime.Now;
                            suppid = db.Insert<base_supplier>(supp).ObjToInt();
                        }
                      

                        base_prod_supp_rel supp_rel = new base_prod_supp_rel();
                        supp_rel.create_time = DateTime.Now;
                        supp_rel.create_user_id = 1;// LoginUser.Current.user_id;
                        supp_rel.del_flag = true;
                        supp_rel.del_time = DateTime.Now;
                        supp_rel.edit_time = DateTime.Now;
                        supp_rel.edit_user_id = 1;// LoginUser.Current.user_id;
                        supp_rel.lev_purch = 1; //默认全部为1，因为原先的系统只能绑定一个供应商
                        supp_rel.supp_id = suppid;
                        supp_rel.prod_id = proid;
                        db.Insert<base_prod_supp_rel>(supp_rel);

                        if (suppid > 0)//如果第一张表插入正常
                        {
                            base_prod_code procode = new base_prod_code();
                            base_product_imgs proimgs = new base_product_imgs();
                            foreach (var item in skulist)
                            {
                                procode.bar_code = item.Code; //插入SKU表
                                procode.create_time = DateTime.Now;
                                procode.create_user_id = 1;//LoginUser.Current.user_id;
                                procode.del_flag = true;
                                procode.del_time = DateTime.Now;
                                procode.del_user_id = 0;
                                procode.edit_time = DateTime.Now;
                                procode.edit_user_id = 0;
                                procode.isbn_code = "";
                                procode.jcode = "";
                                procode.prod_id = proid;
                                procode.prod_model = Getmodel(item.Code);
                                procode.prod_size = GetSize(item.Code);
                                procode.remark = "";
                                procode.sku_code = item.Code;
                                codeid = db.Insert<base_prod_code>(procode).ObjToInt();

                                proimgs.code_id = codeid;
                                proimgs.create_time = DateTime.Now;
                                proimgs.create_user_id = 1;    // LoginUser.Current.user_id;
                                proimgs.del_flag = true;
                                proimgs.del_time = DateTime.Now;
                                proimgs.del_user_id = 0;
                                proimgs.edit_time = DateTime.Now;
                                proimgs.edit_user_id = 0;
                                proimgs.pic_describe = "";
                                proimgs.pic_url = item.PicUrl;   ///sku图片地址
                                proimgs.prod_id = proid;
                                proimgs.prod_model = Getmodel(item.Code);
                                proimgs.remark = "";
                                db.Insert<base_product_imgs>(proimgs);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            DateTime end = DateTime.Now;
            TimeSpan lasttime = end - start;
            MessageBox.Show("导入" + myprolist .Count+ "个产品到新系统,用时:"+lasttime.Hours+"小时"+lasttime.Minutes+"分"+lasttime.Seconds+"秒");
        }

    }
}
