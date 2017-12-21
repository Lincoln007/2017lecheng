using DBModel.DBmodel;
using Printer.CommHelper;
using Printer.DBmodel;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer.Service
{
    public class CommPrint
    {
        /// <summary>
        /// 得到需要打印的数据
        /// </summary>  20:拣选单,30:包裹号,40:转运单,50:yamato,60:Umail,70:Upacket，后面再增加
        /// <param name="printID">插件id</param>
        /// <param name="Pworktype">打印哪种类型数据</param>
        /// <returns></returns>
        public static List<busi_printwork> GetprintInfo(int printID,int Pworktype)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    List<busi_printwork> list = db.SqlQuery<busi_printwork>("select top 10 * from busi_printwork where p_WorkType=@worktype and p_Status=1 and p_idPoint=@id order by p_Workid asc", new { id = printID, worktype = Pworktype }).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 只取一条
        /// </summary>
        /// <param name="printID"></param>
        /// <param name="Pworktype"></param>
        /// <returns></returns>
        public static busi_printwork GetOneprintInfo(int printID, int Pworktype)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    busi_printwork list = db.SqlQuery<busi_printwork>("select top 1 * from busi_printwork where p_WorkType=@worktype and p_Status=1 and p_idPoint=@id order by p_Workid asc", new { id = printID, worktype = Pworktype }).FirstOrDefault();
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printID"></param>
        /// <param name="Pworktype"></param>
        /// <returns></returns>
        public static List<busi_printwork> GetSelectPackgeList(int printID, int Pworktype,string packgecode)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    List<busi_printwork> list = db.Queryable<busi_printwork>().Where(s => s.p_Status == 1).Where(s => s.p_WorkType == 20).Where(s => s.p_idPoint == printID)
                                               .Where(s => s.data_1 == packgecode).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static bool FinshPrint(int printID, busi_printwork pack)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    DateTime ptime = DateTime.Now;
                    pack.p_idActual = printID;
                    pack.Print_DateTime = ptime;
                    pack.p_Status = 0;
                    bool isok = db.Update<busi_printwork>(pack);
                    return isok;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 打印拣选单打印完包裹设置状态
        /// </summary>
        /// <param name="printID"></param>
        /// <param name="packgecode"></param>
        /// <returns></returns>
        public static bool FinshSelectPrint(int printID, string packgecode)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    bool isok = db.Update<busi_printwork>(new { p_Status = 0, p_idActual = printID }, it => it.data_1 == packgecode);
                    return isok;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 得到打印包裹信息（面单上）
        /// </summary>
        /// <param name="packge"></param>
        /// <returns></returns>
        public static packgeInfoViewModel GetPrintpackge(busi_printwork packge)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    packgeInfoViewModel aa=db.Queryable<busi_custorder>().JoinTable<busi_sendorder>((s1, s2) => s1.order_id == s2.custorder_id)
                        .JoinTable<base_shop>((s1, s3) => s1.shop_id == s3.shop_id)
                        .JoinTable<busi_printwork>((s2, s4) => s2.order_code == s4.data_1)
                        .Where<busi_sendorder>((s1, s2) => s2.order_code == packge.data_1)
                        .Select<busi_sendorder, base_shop, busi_printwork,packgeInfoViewModel>((s1, s2, s3,s4) =>
                        new packgeInfoViewModel()
                        {
                            LastTime=s1.latest_date.ObjToString(),
                            Number=s2.prod_num.ObjToString(),
                            Packgecode=s2.order_code,
                            ShopName=s3.shop_name,
                            workID=s4.p_Workid.ObjToInt()
                        }).FirstOrDefault();
                    return aa;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static Myamato GetPrintyamato(busi_printwork packge)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    yamatoShopname aa = db.Queryable<busi_sendorder>().JoinTable<busi_custorder>((s1, s2) => s1.custorder_id == s2.order_id)
                        .JoinTable<busi_custorder,base_shop>((s1,s2, s3) => s2.shop_id == s3.shop_id)
                        .JoinTable<base_shop,base_platform>((s1,s3, s4) => s3.platform_id == s4.platform_id)
                        .Where<busi_sendorder>(s1=> s1.order_code == packge.data_10)
                        .Select<base_shop, base_platform, yamatoShopname>((s1,s3, s4) =>
                        new yamatoShopname()
                        {
                            shopname=s3.shop_name,
                            platfromname=s4.platform_name,
                            Shopaddress=s3.shop_address,
                            Shopphone=s3.shop_telephone,
                            Shopzip=s3.shop_zipcode,
                            Companyname = ""
                        }).FirstOrDefault();
                    if(null==aa)
                    {
                        throw new Exception("无法得到发货定订单信息");
                    }
                    Myamato pyamato = new Myamato();
                    pyamato.Shopname = aa.shopname;
                    pyamato.Platform = aa.platfromname;
                    pyamato.Shopzip = aa.Shopzip;
                    pyamato.Shopphone = aa.Shopphone;
                    pyamato.Shopaddress = aa.Shopaddress;
                    pyamato.Companyname = "株式会社ジャパンドレス";
                    //--------------------------------------------------
                    pyamato.data_1 = packge.data_1;
                    pyamato.data_2 = packge.data_2;
                    pyamato.data_3 = packge.data_3;
                    pyamato.data_4 = packge.data_4;
                    pyamato.data_5 = packge.data_5;
                    pyamato.data_6 = packge.data_6;
                    pyamato.data_7 = packge.data_7;
                    pyamato.data_8 = packge.data_8;
                    pyamato.data_9 = packge.data_9;
                    pyamato.data_10 = packge.data_10;
                    pyamato.create_DateTime = packge.create_DateTime;
                    pyamato.create_UserID = packge.create_UserID;
                    pyamato.edit_DateTime = packge.edit_DateTime;
                    pyamato.p_idPoint = packge.p_idPoint;
                    pyamato.p_Status = packge.p_Status;
                    pyamato.p_Workid = packge.p_Workid;
                    pyamato.p_WorkRemarks = packge.p_WorkRemarks;
                    pyamato.p_WorkType = packge.p_WorkType;
                    pyamato.Print_DateTime = packge.Print_DateTime;
                    return pyamato;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 得到打印信息
        /// </summary>
        /// <param name="packge"></param>
        /// <returns></returns>
        public static MyUpacket GetPrintUpacket(busi_printwork packge)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    yamatoShopname aa = db.Queryable<busi_sendorder>().JoinTable<busi_custorder>((s1, s2) => s1.custorder_id == s2.order_id)
                        .JoinTable<busi_custorder, base_shop>((s1, s2, s3) => s2.shop_id == s3.shop_id)
                        .JoinTable<base_shop, base_platform>((s1, s3, s4) => s3.platform_id == s4.platform_id)
                        .Where<busi_sendorder>(s1 => s1.order_code == packge.data_10)
                        .Select<base_shop, base_platform, yamatoShopname>((s1, s3, s4) =>
                        new yamatoShopname()
                        {
                            shopname = s3.shop_name,
                            platfromname = s4.platform_name,
                            Shopaddress = s3.shop_address,
                            Shopphone = s3.shop_telephone,
                            Shopzip = s3.shop_zipcode,
                            Companyname = ""
                        }).FirstOrDefault();
                    if (null == aa)
                    {
                        throw new Exception("无法得到发货定订单信息");
                    }
                    MyUpacket pupacket = new MyUpacket();
                    pupacket.Shopname = aa.shopname;
                    pupacket.Platform = aa.platfromname;
                    pupacket.Shopzip = aa.Shopzip;
                    pupacket.Shopphone = aa.Shopphone;
                    pupacket.Shopaddress = aa.Shopaddress;
                    pupacket.Companyname = "株式会社ジャパンドレス";
                    //--------------------------------------------------
                    pupacket.data_1 = packge.data_1;
                    pupacket.data_2 = packge.data_2;
                    pupacket.data_3 = packge.data_3;
                    pupacket.data_4 = packge.data_4;
                    pupacket.data_5 = packge.data_5;
                    pupacket.data_6 = packge.data_6;
                    pupacket.data_7 = packge.data_7;
                    pupacket.data_8 = packge.data_8;
                    pupacket.data_9 = packge.data_9;
                    pupacket.data_10 = packge.data_10;
                    pupacket.create_DateTime = packge.create_DateTime;
                    pupacket.create_UserID = packge.create_UserID;
                    pupacket.edit_DateTime = packge.edit_DateTime;
                    pupacket.p_idPoint = packge.p_idPoint;
                    pupacket.p_Status = packge.p_Status;
                    pupacket.p_Workid = packge.p_Workid;
                    pupacket.p_WorkRemarks = packge.p_WorkRemarks;
                    pupacket.p_WorkType = packge.p_WorkType;
                    pupacket.Print_DateTime = packge.Print_DateTime;
                    return pupacket;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
