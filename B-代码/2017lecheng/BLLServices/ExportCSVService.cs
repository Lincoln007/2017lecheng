using DBModel.Common;
using DBModel.DBmodel;
using IBLLService;
using SqlSugarRepository;
using STO.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices
{
    public class ExportCSVService : IExportCSV
    {
        public bool GetDeclarationDHLExcelUrl(string zhuanyuncode, string fullpath, long express, long shop)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    DataTable list = db.Queryable<busi_sendorder>().JoinTable<busi_transfer>((s1, s2) => s1.tran_id == s2.tran_id)
                        .JoinTable<busi_sendorder, busi_custorder>((s1,s3) => s1.custorder_id == s3.order_id)
                        .JoinTable<busi_sendorder, base_exp_comp>((s1, s4) => s1.express_id == s4.express_id)
                        .JoinTable<busi_sendorder, busi_custorder,base_shop>((s1,s3, s5) => s3.shop_id == s5.shop_id)
                        .Where<busi_transfer>((s1, s2) => s2.tran_code== zhuanyuncode.Trim())
                        .Where<base_exp_comp>((s1,s4)=>s4.express_id==express)
                        .Where<base_shop>((s1,s5)=>s5.shop_id==shop)
                        .Select("s3.custorder_code,s4.express_name ,s1.exp_code").ToDataTable();
                    //将datetable放入excel中
                    string error = string.Empty;
                    bool isok=ExcelHelper.DataTableToExcel(list, fullpath, out error);
                    return isok;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool IsExitZY(string zhuanyuncode)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    busi_transfer a = db.Queryable<busi_transfer>().Where(s => s.tran_code == zhuanyuncode).FirstOrDefault();
                    if (null == a)
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
    }
}
