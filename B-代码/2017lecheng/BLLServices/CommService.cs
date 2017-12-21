using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ViewModel;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices
{
    public class CommService
    {
        /// <summary>
        /// 得到所有平台
        /// </summary>
        /// <returns></returns>
       public static List<base_platform> GetPlatformList()
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    List<base_platform> platlist = db.Queryable<base_platform>().ToList();
                    return platlist;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
       /// <summary>
       /// 得到所有快递公司
       /// </summary>
       /// <returns></returns>
       public static List<base_exp_comp> GetExpress()
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   List<base_exp_comp> platlist = db.Queryable<base_exp_comp>().Where(s => s.express_status == true).Where(s => s.del_flag == true).ToList();
                   return platlist;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }

       /// <summary>
       /// 得到所有商品分类
       /// </summary>
       /// <returns></returns>
       public static List<base_productclass> GetProclsflyList()
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   List<base_productclass> platlist = db.Queryable<base_productclass>().ToList();
                   return platlist;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }

        /// <summary>
        /// 得到所有店铺
        /// </summary>
        /// <returns></returns>
       public static List<base_shop> GetShopList()
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    List<base_shop> shoplist = db.Queryable<base_shop>().ToList();
                    return shoplist;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 得到所有仓库
        /// </summary>
        /// <returns></returns>
       public static List<base_wh_warehouse> GetDepotList()
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   List<base_wh_warehouse> shoplist = db.Queryable<base_wh_warehouse>().Where(s => s.del_flag == true).Where(s => s.wh_status == true).ToList();
                   return shoplist;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }
       /// <summary>
       /// 根据平台获得店铺名列表
       /// </summary>
       /// <returns></returns>
       public static List<base_shop> GetShopList(string platforID)
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   //var plat=db.Queryable<base_platform>().Where(a1 => a1.platform_name == platformname).FirstOrDefault();
                   List<base_shop> shoplist = db.Queryable<base_shop>().Where(s1 => s1.platform_id == Convert.ToInt32(platforID)).ToList();
                   return shoplist;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }
        /// <summary>
       /// 删除配置SKU 
        /// </summary>
        /// <returns></returns>
       public static bool DeletePSKU(int id)
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   //删除根据主键
                   bool isok=db.Delete<base_PSKU, int>(id);
                   return isok;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }

        /// <summary>
        /// 根据ID获取快递名称
        /// </summary>
        /// <returns></returns>
        public static string GetExpressByID(int expressid)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    string expressname=db.Queryable<base_exp_comp>().Where(s => s.express_id == expressid).FirstOrDefault().express_name;
                    return expressname;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 添加SKU配置信息
        /// </summary>
        /// <param name="PSKU"></param>
        /// <param name="SSKU"></param>
        /// <param name="GSKU"></param>
        /// <returns></returns>
        public static bool AddPSKU(string PSKU, string SSKU, string GSKU,int shopID)
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                    if (!string.IsNullOrEmpty(GSKU))
                    {
                        var GSKUList = db.Queryable<base_PSKU>().Where(c => c.GSKU == GSKU && c.ShopID == shopID).ToList();
                        if (GSKUList.Count >= 1)
                        {
                            throw new Exception("过滤SKU已经存在！");
                        }
                    }                
                   if (!string.IsNullOrEmpty(SSKU) && !string.IsNullOrEmpty(PSKU))
                   {
                       var PSKUList = db.Queryable<base_PSKU>().Where(c => c.PSKU == PSKU).Where(c => c.SSKU == SSKU).ToList();
                       if (PSKUList.Count >= 1)
                       {
                           throw new Exception("系统SKU或者平台SKU已经存在！");
                       }
                   }
                  
                  //1.判断SSKU在系统中是否存在
                   if (!string.IsNullOrEmpty(SSKU))
                   {
                       var sku = db.Queryable<base_prod_code>().Where(s => s.sku_code == SSKU).ToList();
                       if (sku.Count == 0)
                       {
                           throw new Exception("系统中不存在此SKU！，" + SSKU);
                       }
                   }
                  
                   //2.所有条件全部满足之后添加
                   base_PSKU NPSKU = new base_PSKU();
                   NPSKU.PSKU = PSKU;
                   NPSKU.SSKU = SSKU;
                   NPSKU.ShopID = shopID;
                   NPSKU.GSKU = GSKU;
                   var id=db.Insert<base_PSKU>(NPSKU);
                   if(id.ObjToInt()>0)
                   {
                       return true;
                   }else
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
        /// 得到配置SKU列表
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="onepagecount"></param>
        /// <param name="shopID"></param>
        /// <param name="totil"></param>
        /// <param name="totilpage"></param>
        /// <returns></returns>
       public static List<base_NPSKU> GetPSKUListByshopID(int pagenum, int onepagecount, int shopID, out int totil, out int totilpage)
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   var comresult = db.Queryable<base_PSKU>().JoinTable<base_shop>((s1, s2) => s1.ShopID == s2.shop_id).Where<base_PSKU>(s1 => s1.ShopID == shopID);
                   totil = comresult.Count();
                   totilpage = totil / onepagecount;
                   if (totil % onepagecount > 0)
                   {
                       totilpage++;
                   }
                   List<base_NPSKU> platlist = comresult.OrderBy(s1 => s1.ID)
                       .Skip(onepagecount * (pagenum - 1))
                       .Take(onepagecount)
                          .Select<base_shop, base_NPSKU>((s1, s2) => new base_NPSKU()
                          {
                             ID=s1.ID,                          
                             ShopName=s2.shop_name,
                             PSKU=s1.PSKU,
                             SSKU=s1.SSKU,
                             GSKU=s1.GSKU
                          }).ToList();
                 
                   return platlist;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }
       public static List<GSKU> GetGSKUListByshopID(int shopID)
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   List<GSKU> platlist = db.Queryable<base_PSKU>().Where(s1 => s1.ShopID == shopID).Select<GSKU>((s1) => new GSKU()
                       {
                           gsku = s1.GSKU
                       }).ToList();
                 
                   return platlist;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }

       public static string GetCSVcolByshopID(int shopID)
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   var platlist = db.Queryable<base_shop>().Where(s1 => s1.shop_id == shopID).FirstOrDefault();

                   return platlist.Csv_insert;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }
        /// <summary>
        /// 根据shopID获得实体
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
       public static base_shop GetLStableByshopID(int shopID)
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               try
               {
                   var platlist = db.Queryable<base_shop>().Where(s1 => s1.shop_id == shopID).FirstOrDefault();

                   return platlist;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }
       public static string GetPlatNameByshopID(int shopID)
       {
           using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
           {
               string platformname=string.Empty;
               try
               {
                   var result=db.Queryable<base_platform>().JoinTable<base_shop>((s1, s2) => s1.platform_id == s2.platform_id)
                       .Where<base_shop>((s1, s2) => s2.shop_id == shopID).FirstOrDefault();

                   platformname = result.platform_name;
                   return platformname;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
       }
    }
}
