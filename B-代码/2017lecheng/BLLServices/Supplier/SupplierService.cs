using DBModel.Common;
using DBModel.DBmodel;
using IBLLService;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Supplier
{
   
        public class SupplierService : ISupplier
        {
            public bool UpdateSupplier(string suppname, string remark, string suppurl, int id)
            {
                using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
                {
                    try
                    {

                        var isok = db.Update<base_supplier>(new { supp_name = suppname, supp_url = suppurl, remark = remark }, it => it.supp_id == id);
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
                        ex.ToString();
                        return false;

                    }
                }
            }

            public bool AddSupplier(base_supplier supp)
            {
                using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
                {
                    try
                    {

                        //db.DisableInsertColumns = new string[] { "edit_time" };//edit_time列将不会插入值
                        supp.edit_time = DateTime.Now;
                        var id = db.Insert<base_supplier>(supp);

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
                        ex.ToString();
                        return false;

                    }
                }
            }

            public List<base_supplier> SearchSupplier(int pagenum, int onepagecount, string suppliername, out int totil, out int totilpage)
            {
                using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
                {
                    try
                    {
                        //db.CommandTimeOut = 5000;//设置超时时间
                        //db.BeginTran();//开启事务                    
                        var comresult = db.Queryable<base_supplier>().Where(s1 => s1.del_flag == true);
                        if (!string.IsNullOrEmpty(suppliername))
                        {
                            comresult = comresult.Where(s1 => s1.supp_name == suppliername);
                        }
                        List<base_supplier> suppliers = comresult.OrderBy(s1 => s1.supp_id)
                       .Skip(onepagecount * (pagenum - 1))
                       .Take(onepagecount).ToList();
                        totil = comresult.Count();
                        totilpage = totil / onepagecount;
                        if (totil % onepagecount > 0)
                        {
                            totilpage++;
                        }
                        return suppliers;
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        //db.RollbackTran();//回滚事务
                        totil = 0;
                        totilpage = 0;
                        return null;
                    }
                }

            }

            public bool DeleteSupplier(int id)
            {
                using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
                {
                    try
                    {

                        var isok = db.Update<base_supplier>(new { del_flag = 1 }, it => it.supp_id == id);
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
                        ex.ToString();
                        return false;

                    }
                }

            }
        }
    
}
