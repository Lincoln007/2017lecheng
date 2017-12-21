using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ViewModel;
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
    public class ProductService : IProduct
    {

       public bool UpdateSKU(int codeID, string skucode)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    bool isok=db.Update<base_prod_code>(new { sku_code = skucode }, it => it.code_id == codeID); //只更新name列
                    if (isok)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        
        }
        /// <summary>
        /// 得到所有产品分类
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="exmsg"></param>
        /// <returns></returns>
        public List<base_productclass> Getpages(int pagenum, int onepagecount, out int totil, out int totilpage,out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var comresult = db.Queryable<base_productclass>().Where(s1 => s1.del_flag == true);
                    List<base_productclass> productclass = comresult.OrderBy(s1 => s1.prod_class_id)
                   .Skip(onepagecount * (pagenum - 1))
                   .Take(onepagecount).ToList();
                    totil = comresult.Count();
                    totilpage = totil / onepagecount;
                    exmsg = "";
                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }
                    return productclass;
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
        /// 根据产品ID获得产品的详细信息和SKU
        /// </summary>
        /// <param name="proid"></param>
        /// <returns></returns>
        public ProductDetail GetProductDetail(int proid)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    base_product pro=db.Queryable<base_product>().Where(s => s.prod_id == proid).FirstOrDefault();
                    List<base_prod_code> proskus = db.Queryable<base_prod_code>().Where(s1 => s1.prod_id == proid).ToList();
                    List<img_model> proimgs = db.Queryable<base_product_imgs>().Where(s2 => s2.prod_id == proid).GroupBy(s2 => s2.prod_model)
                                .GroupBy(s2 => s2.pic_url)
                                .Select<img_model>("prod_model,pic_url").ToList();
                    List<SupplierModel> prosupp = db.Queryable<base_prod_supp_rel>().JoinTable<base_supplier>((s1, s2) => s1.supp_id == s2.supp_id)
                        .Where(s1 => s1.prod_id == proid)
                        .Select<base_supplier, SupplierModel>((s1, s2) => new SupplierModel()
                        {
                            rel_id=s1.rel_id,
                            supp_id=s1.supp_id,
                            suppliername=s2.supp_name,
                            prod_id=s1.prod_id,
                            lev_purch=s1.lev_purch
                        }).ToList();
                    //List<base_prod_supp_rel> prosupp = db.Queryable<base_prod_supp_rel>().Where(s3 => s3.prod_id == proid).ToList();
                    ProductDetail prodetail = new ProductDetail();
                    prodetail.pro = pro;
                    prodetail.proimgs = proimgs;
                    prodetail.proskus = proskus;
                    prodetail.prosupp = prosupp;
                    return prodetail;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }   
        }
        /// <summary>
        /// 得到产品列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<ProcudtViewModel> Getprolist(int pagenum, int onepagecount, out int totil, out int totilpage, int status,string prostyle, string proskucode, string prodevelpoer)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var comresult = db.Queryable<base_product>().JoinTable<base_productclass>((s1, s2) => s1.prod_class_id == s2.prod_class_id)
                        //.JoinTable<base_prod_code>((s1, s3) => s1.prod_id == s3.prod_id,JoinType.Inner)
                                .Where<base_product>(s1 => s1.del_flag == true).Where<base_product>(s1 => s1.prod_status == status);
                    if (!string.IsNullOrEmpty(proskucode)) //如果SKU不为空
                    {
                        long myproid = db.Queryable<base_prod_code>().Where(s3 => s3.sku_code == proskucode).FirstOrDefault().prod_id;
                        comresult = comresult.Where<base_product>(s1 => s1.prod_id == myproid);
                    }
                
                    if (!string.IsNullOrEmpty(prostyle))
                    {
                        comresult = comresult.Where<base_product>(s1 => s1.prod_style == prostyle);
                    }
                  
                    if (!string.IsNullOrEmpty(prodevelpoer))
                    {
                        comresult = comresult.Where<base_product>(s1 => s1.prod_developer == prodevelpoer);
                    }
                    totil = comresult.Count();
                    totilpage = totil / onepagecount;

                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }

                    List<ProcudtViewModel> productclass = comresult.OrderBy(s1 => s1.prod_id, OrderByType.Desc)
                   .Skip(onepagecount * (pagenum - 1))
                   .Take(onepagecount).Select<base_productclass, ProcudtViewModel>((s1, s2) => new ProcudtViewModel
                   {
                       prod_id=s1.prod_id,
                       pic_url=s1.pic_url,
                       price_cn=s1.price_cn,
                       prod_property = s1.prod_property,//Enum.GetName(typeof(Productstyle), Convert.ToInt32(s1.prod_property)), 
                       prod_status = s1.prod_status,//Enum.GetName(typeof(ProductStatus), Convert.ToInt32(s1.prod_status)), //
                       prod_title=s1.prod_title,
                       prod_style=s1.prod_style,
                       prod_developer=s1.prod_developer,
                       create_time=s1.create_time,
                       remark=s1.remark

                   }).ToList();
                 
                    return productclass;
                }
                catch (Exception ex)
                {                   
                    totil = 0;
                    totilpage = 0;
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 更新产品分类
        /// </summary>
        /// <param name="proclassfyname"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateProclassfy(string proclassfyname, int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var isok = db.Update<base_productclass>(new { class_name = proclassfyname }, it => it.prod_class_id ==id);
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


        public bool AddProclassfy(base_productclass proclassfyname,out int isexit,out int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    //1.先判断是否存在
                    var singleOrDefault = db.Queryable<base_productclass>().SingleOrDefault(c => c.class_name == proclassfyname.class_name);
                    if (singleOrDefault != null && singleOrDefault.del_flag==false)//代表已存在数据库且被删除
                    {
                        isexit = 1;
                        id = singleOrDefault.prod_class_id;
                        return false;
                    }
                    if (singleOrDefault != null && singleOrDefault.del_flag == true)//代表已存在数据库未被删除
                    {
                        isexit = 2;
                        id = singleOrDefault.prod_class_id;
                        return false;
                    }
                    //2.不存在再添加
                    db.DisableInsertColumns = new string[] { "edit_time" };//edit_time列将不会插入值
                    var myid = db.Insert<base_productclass>(proclassfyname);

                    if (myid.ObjToInt() > 0)
                    {
                        isexit = 0;
                        id = myid.ObjToInt();
                        return true;
                    }
                    else
                    {
                        isexit = 0;
                        id = myid.ObjToInt();
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }


        public bool DeleteProClassfy(int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    db.DisableInsertColumns = new string[] { "edit_time" };//edit_time列将不会插入值
                    var isok = db.Update<base_productclass>(new { del_flag = false }, it => it.prod_class_id == id);

                    if (id.ObjToInt() > 0)
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


        public bool UpdateProclassfyDel(string proclassfyname, int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var isok = db.Update<base_productclass>(new { del_flag = true }, it => it.prod_class_id == id);
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

        /// <summary>
        /// 设置款号的图片
        /// </summary>
        /// <param name="fFullDir"></param>
        /// <returns></returns>
        public bool SetProImg(string fFullDir, int proID, string model)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {

                    var isok = db.Update<base_product_imgs>(new { pic_url = fFullDir }, it => it.prod_id==proID && it.prod_model==model);
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


        public bool SetProMainImg(string fFullDir, int proID)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {

                    var isok = db.Update<base_product>(new { pic_url = fFullDir }, it => it.prod_id == proID );
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


        public base_prod_supp_rel AddProductSupplier(string supplier, int proid)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    base_supplier isin=db.Queryable<base_supplier>().Where(s => s.supp_name == supplier).FirstOrDefault();
                    if (null == isin)
                    {
                        throw new Exception("此供应商不存在系统中");
                    }
                    //判断是否绑定过
                    base_prod_supp_rel isinsupp=db.Queryable<base_prod_supp_rel>().Where(s => s.prod_id == proid).Where(s => s.supp_id == isin.supp_id).FirstOrDefault();
                    if (null!=isinsupp)
                    {
                        throw new Exception("已经绑定过此供应商不能重复绑定");
                    }
                    int levl = 1;
                    base_prod_supp_rel isexit = db.SqlQuery<base_prod_supp_rel>("select top 1 * from base_prod_supp_rel where prod_id=@prod_id order by lev_purch desc", new { prod_id = proid }).FirstOrDefault();
                    if (null == isexit)
                    {
                        levl = 1;
                    }
                    else
                    {
                        levl = isexit.lev_purch.ObjToInt()+1;
                    }
                    base_prod_supp_rel newsupp = new base_prod_supp_rel();
                    newsupp.create_time = DateTime.Now;
                    newsupp.create_user_id = LoginUser.Current.user_id;
                    newsupp.del_flag = true;
                    newsupp.del_time = DateTime.Now;
                    newsupp.edit_time = DateTime.Now;
                    newsupp.edit_user_id = LoginUser.Current.user_id;
                    newsupp.lev_purch = (Byte?)levl;
                    newsupp.supp_id = isin.supp_id;
                    newsupp.prod_id = proid;
                    var isok = db.Insert<base_prod_supp_rel>(newsupp);
                    newsupp.rel_id = isok.ObjToInt();
                    return newsupp;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool DeleteProductSupplier(int relid, int proid)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    bool isok = db.Delete<base_prod_supp_rel>(s => s.rel_id == relid);

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
