using DBModel;
using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices
{
    public class PrintService : IPrint
    {
        public bool AddPrinter(DBModel.DBmodel.base_print pro, out int isexit, out int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    //1.先判断是否存在
                    var singleOrDefault = db.Queryable<base_print>().SingleOrDefault(c => c.p_name == pro.p_name);
                    if (singleOrDefault != null && singleOrDefault.del_flag == 1)//代表已存在数据库且被删除
                    {
                        isexit = 1;
                        id = singleOrDefault.p_id;
                        return false;
                    }
                    if (singleOrDefault != null && singleOrDefault.del_flag == 0)//代表已存在数据库未被删除
                    {
                        isexit = 2;
                        id = singleOrDefault.p_id;
                        return false;
                    }
                    //2.不存在再添加
                    db.DisableInsertColumns = new string[] { "edit_time" };//edit_time列将不会插入值
                    var myid = db.Insert<base_print>(pro);

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

        public List<myprinter> Getpages(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var comresult = db.Queryable<base_print>().Where(s1 => s1.del_flag == 0);
                    List<base_print> productclass = comresult.OrderBy(s1 => s1.p_id)
                   .Skip(onepagecount * (pagenum - 1))
                   .Take(onepagecount).ToList();
                    totil = comresult.Count();
                    totilpage = totil / onepagecount;
                    exmsg = "";
                    if (totil % onepagecount > 0)
                    {
                        totilpage++;
                    }
                    List<myprinter> printers = new List<myprinter>();

                    foreach(var item in productclass)
                    {
                        myprinter aa = new myprinter();
                        aa.Createtime = item.Createtime;
                        aa.del_flag = item.del_flag;
                        aa.Depotname = db.Queryable<base_wh_warehouse>().Where(d => d.wh_id == item.DepotID).FirstOrDefault().wh_name;
                        aa.isonline = item.isonline;
                        aa.p_name = item.p_name;
                        aa.p_id = item.p_id;
                        printers.Add(aa);

                    }
                    return printers;
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

        public int DeletePrinter(int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    //1.先判断此快递类型是否有未使用的单号数据
                    var hascode = db.Queryable<base_print>().Where(s => s.p_id == id).Where(s => s.isonline==1).ToList();
                    if (hascode.Count > 0)
                    {
                        return 2;//代表在线状态不能删除
                    }
                    else
                    {
                        db.DisableInsertColumns = new string[] { "edit_time" };//edit_time列将不会插入值
                        var isok = db.Update<base_print>(new { del_flag = 1 }, it => it.p_id == id);

                        if (id.ObjToInt() > 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }


        public bool DelUpdatePrinter(int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var isok = db.Update<base_print>(new { del_flag = 0 }, it => it.p_id == id);
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
        /// 更新打印插件
        /// </summary>
        /// <param name="proclassfyname"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdatePrinter(string proclassfyname, int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var isok = db.Update<base_print>(new { p_name = proclassfyname }, it => it.p_id == id);
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
        /// 判断包裹号在系统中是否存在
        /// </summary>
        /// <param name="packgecode"></param>
        /// <returns></returns>
        public bool IsPackgeInSys(string packgecode)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    busi_sendorder isok = db.Queryable<busi_sendorder>().Where(s => s.order_code == packgecode).FirstOrDefault();
                    if (null==isok)
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

        /// <summary>
        /// 查询拣选信息
        /// </summary>
        /// <param name="packgecode"></param>
        /// <param name="ispacked">是否已配完</param>
        /// <returns></returns>
        public List<PrintSelect> GetPrintSelectList(string packgecode, int ispacked, int pageSize, int pageIndex,out int packgecount,out int count)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var comresult = db.Queryable<busi_workinfo>().JoinTable<busi_sendorder>((s1, s2) => s1.packid == s2.order_id)
                        .JoinTable<busi_custorder>((s1, s3) => s1.custorder_id == s3.order_id)
                        .JoinTable<base_shop>((s1, s4) => s1.shop_id == s4.shop_id)
                        .JoinTable<base_prod_code>((s1, s5) => s1.prod_code_id == s5.code_id)
                        .Where<busi_workinfo, busi_sendorder>((s1, s2) => s2.del_flag == true)
                        .Where<busi_workinfo, busi_sendorder>((s1, s2) => s2.is_print == false)
                        .Where<busi_workinfo, busi_sendorder>((s1, s2) => s2.prod_num >=1); //多件的显示，单件的不显示
                    if (!string.IsNullOrEmpty(packgecode))
                    {
                        comresult = comresult.Where<busi_sendorder>((s1,s2) => s2.order_code == packgecode);
                    }
                    if (0 == ispacked)//是否已配完
                    {
                        comresult = comresult.Where<busi_sendorder>((s1, s2) => s2.order_tatus == 30);
                    }
                    else if (1 == ispacked)
                    {
                        comresult = comresult.Where<busi_sendorder>((s1, s2) => s2.order_tatus == 40);
                    }
                    count = comresult.Count();
                    packgecount = count / pageSize;
                    if (count % pageSize>0)
                    {
                        packgecount++;
                    }
                    List<PrintSelect> list = comresult.OrderBy(s1=>s1.work_id).Skip(pageSize * (pageIndex - 1)).Take(pageSize)
                        .Select<busi_sendorder, busi_custorder, base_shop, base_prod_code, PrintSelect>((s1, s2, s3, s4, s5) => new PrintSelect()
                        {
                            packgecode=s2.order_code,
                            skucode=s5.sku_code,
                            shopname=s4.shop_name,
                            depotname = "",
                            location="",
                            workid=s1.work_id,
                            skuststus=s1.is_work,
                            ordercode=s3.order_code
                        }).ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 确认打印
        /// </summary>
        /// <returns></returns>
        public bool ComfirmPrint()
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    List<PrintworkViewModel> comresult = db.Queryable<busi_sendorder>().JoinTable<busi_sendorder_detail>((s1, s2) => s1.order_id == s2.order_id)
                         .JoinTable<busi_sendorder_detail,base_prod_code>((s1,s2, s3) => s2.code_id == s3.code_id)
                         .Where(s1 => s1.del_flag == true).Where(s1 => s1.prod_num >= 2).Where(s1 => s1.order_tatus == 40)//40代表配货已完成
                         .Where(s1 => s1.is_print == false)
                         .Select<busi_sendorder_detail, base_prod_code, PrintworkViewModel>((s1, s2, s3) => new PrintworkViewModel()
                         {
                            packID=s1.order_id,
                            packgecode = s1.order_code,
                            Skucode=s3.sku_code,
                            skunum=s2.prod_num.ObjToInt(),
                            count=s1.prod_num
                         }).ToList();
                    //2.往print_work中放数据
                    List<busi_printwork> mylist = new List<busi_printwork>();
                    List<string> packcode = new List<string>();
                    foreach (var item in comresult)
                    {
                        busi_printwork nn = new busi_printwork();
                        nn.p_WorkType = 20;
                        nn.p_Status = 1;
                        nn.p_idPoint = 1;//测试
                        nn.data_1 = item.packgecode;
                        nn.data_2 = item.Skucode;
                        nn.data_3 = item.skunum.ToString();
                        nn.data_4 = "ZZ-ZZ-ZZ";
                        nn.create_DateTime = DateTime.Now;
                        nn.create_UserID = LoginUser.Current.user_id.ObjToInt();
                        mylist.Add(nn);

                    }
                    foreach (var item in comresult)  //插入包裹号打印
                    {
                        if (!packcode.Contains(item.packgecode))
                        {
                            busi_printwork insertinfo = new busi_printwork();
                            insertinfo.p_WorkType = 30;
                            insertinfo.data_1 = item.packgecode;
                            insertinfo.data_4 = DateTime.Now.ToString();
                            insertinfo.p_idPoint = 1; //测试
                            insertinfo.p_Status = 1;
                            insertinfo.data_2 = item.packgecode.Substring(8, 4);
                            insertinfo.data_3 = item.count.ToString();
                            mylist.Add(insertinfo);
                            packcode.Add(item.packgecode);
                        }
                       
                    }
                    bool isok = db.SqlBulkCopy(mylist);//批量插入数据
                    if (isok)
                    {  
                        //3.打印成功之后设置已打印
                        List<long> mm = new List<long>();
                        foreach (var item in comresult)
                        {
                            mm.Add(item.packID);
                        }
                        var array = mm.ToArray();
                        db.Update<busi_sendorder>(new { is_print = true,print_time=DateTime.Now }, it=>array.Contains(it.order_id));
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
