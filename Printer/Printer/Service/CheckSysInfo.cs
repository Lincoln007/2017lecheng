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
    /// <summary>
    /// 沟通系统和打印插件的数据信息
    /// </summary>
    public class CheckSysInfo
    {
        /// <summary>
        /// 判断扫描的包裹号在系统中是否存在且未删除
        /// </summary>
        /// <param name="packgecode"></param>
        /// <returns></returns>
        public static int CheckPackge(string packgecode)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    busi_sendorder packge = db.Queryable<busi_sendorder>().Where(s => s.order_code == packgecode).FirstOrDefault();
                    if (null == packge)
                    {
                        return 1;//代表包裹不存在系统中
                    }
                    else 
                    {
                        busi_sendorder packge2 = db.Queryable<busi_sendorder>().Where(s => s.order_code == packgecode).Where(s => s.del_flag == true).FirstOrDefault();
                        if (null == packge2)
                        {
                            return 2; //代表包裹被删除
                        }
                        else
                        {
                            return 3; //包裹存在，正常
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static List<PackgePrintInfo> GetPckgePrintInfo(string packgecode)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    List<PackgePrintInfo> packinfo=db.Queryable<busi_sendorder_detail>().JoinTable<busi_sendorder>((s1, s2) => s1.order_id == s2.order_id)
                           .JoinTable<base_prod_code>((s1, s3) => s1.code_id == s3.code_id)
                           .Where<busi_sendorder>((s1, s2) => s2.order_code == packgecode)
                           .Select<busi_sendorder, base_prod_code, PackgePrintInfo>((s1, s2, s3) =>
                            new PackgePrintInfo()
                            {
                                expressid = s2.express_id,
                                ExpCode = s2.exp_code,
                                zipcode = s2.receive_zip,
                                mobile = s2.receive_mobile,
                                phone = s2.receive_phone,
                                address = s2.receive_address,
                                name = s2.receive_name,
                                count = s2.prod_num,
                                skucode = s3.sku_code,
                                skunum = s1.prod_num.ObjToInt()
                            }).ToList();

                    return packinfo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static bool InsertPrintMiandan(busi_printwork pwork)
        {
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    var isok = db.Insert<busi_printwork>(pwork);
                    if (isok.ObjToInt() > 0)
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
