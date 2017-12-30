using DBModel;
using DBModel.Common;
using DBModel.DBmodel;
using IBLLService;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices
{
    public class ExpressService : IExpress
    {
        public List<base_exp_comp> Getpages(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var comresult = db.Queryable<base_exp_comp>().Where(s1 => s1.del_flag==true);
                    List<base_exp_comp> productclass = comresult.OrderBy(s1 => s1.express_id)
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


        public bool AddExpress(base_exp_comp proclassfy, out int isexit, out int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    //1.先判断是否存在
                    var singleOrDefault = db.Queryable<base_exp_comp>().SingleOrDefault(c => c.express_name == proclassfy.express_name);
                    if (singleOrDefault != null && singleOrDefault.del_flag == false)//代表已存在数据库且被删除
                    {
                        isexit = 1;
                        id = singleOrDefault.express_id;
                        return false;
                    }
                    if (singleOrDefault != null && singleOrDefault.del_flag == true)//代表已存在数据库未被删除
                    {
                        isexit = 2;
                        id = singleOrDefault.express_id;
                        return false;
                    }
                    //2.不存在再添加
                    db.DisableInsertColumns = new string[] { "edit_time" };//edit_time列将不会插入值
                    var myid = db.Insert<base_exp_comp>(proclassfy);

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


        public bool UpdateExpress(string proclassfyname, int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var isok = db.Update<base_exp_comp>(new { express_name = proclassfyname }, it => it.express_id == id);
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


        public int DeleteExpress(int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    //1.先判断此快递类型是否有未使用的单号数据
                    var hascode = db.Queryable<base_expresscode>().Where(s => s.express_id == id).Where(s => s.isuse == false).ToList();
                    if (hascode.Count > 0)
                    {
                        return 2;//代表有单号不能删除
                    }
                    else
                    {
                        db.DisableInsertColumns = new string[] { "edit_time" };//edit_time列将不会插入值
                        var isok = db.Update<base_exp_comp>(new { del_flag = true }, it => it.express_id == id);

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


        public bool Import(List<base_expresscode> list)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    bool isok = db.SqlBulkCopy(list);
                    return isok;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }


        public List<base_expresscode> Searchexp(int express, int pageindex, int isuse, out int totil)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var comresult = db.Queryable<base_expresscode>().Where(s => s.isuse == isuse.ObjToBool()).Where(s => s.express_id == express);
                    totil = comresult.ToList().Count;
                    List<base_expresscode> result = comresult.OrderBy(s => s.Eid).Skip(20 * (pageindex - 1))
                   .Take(20).ToList();

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }


        public bool DelUpdateExpress(int id)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var isok = db.Update<base_exp_comp>(new { del_flag = false }, it => it.express_id == id);
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
        /// 导出所有选择的快递类型数据
        /// </summary>
        /// <param name="expressid"></param>
        /// <param name="ispacked"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ExpressPackgeModel> GetExpressPackgeAllList(int expressid, int ispacked, out int count, DateTime? starttime, DateTime? endtime)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var sql = db.Queryable<busi_sendorder>().JoinTable<busi_custorder>((s1, s2) => s1.custorder_id == s2.order_id)
                        .JoinTable<busi_custorder, base_shop>((s1, s2, s3) => s2.shop_id == s3.shop_id)
                        .Where<busi_sendorder>(s1 => s1.is_export == ispacked.ObjToBool())
                        .Where<busi_sendorder>(s1 => s1.order_tatus == 40) //以配货
                        .Where<busi_sendorder>(s1 => s1.express_id == expressid);
                    if (1 == ispacked)//查询已导出的
                    {
                        sql = sql.Where<busi_sendorder>(s1 => s1.edit_time >= starttime).Where<busi_sendorder>(s1 => s1.edit_time <= endtime);
                    }
                    count = sql.Count();
                    List<ExpressPackgeModel> list = sql.Select<base_shop, ExpressPackgeModel>((s1, s3) => new ExpressPackgeModel()
                          {
                              packid = s1.order_id.ObjToInt(),
                              sendtime = DateTime.Now,
                              packgecode = s1.order_code,
                              custname = s1.receive_name,
                              custaddress1 = s1.receive_address,
                              custaddress2 = "",
                              custphone = s1.receive_phone,
                              custmobile = s1.receive_mobile,
                              custzip = s1.receive_zip,
                              shopaddress = s3.shop_address,
                              shopzip = s3.shop_zipcode,
                              shopname = s3.shop_name,
                              shopphone = s3.shop_telephone,
                              sku = "",
                              sku1 = "",
                              sku2 = "",
                              sendtype = 3,//3是yamato  0 是宅急便
                              daihao = "",
                              mangagedaihao = "",
                              senddaihao = "",
                              custmangagedaihao = s1.order_code
                          }).ToList();
                    foreach (var item in list)
                    {
                        StringBuilder aa = new StringBuilder();
                        var detaillist = db.Queryable<busi_sendorder_detail>()
                            .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                            .Where(s1 => s1.order_id == item.packid).Select<base_prod_code, SkuAndNum>((s1, s2) => new SkuAndNum()
                            {
                                skucode = s2.sku_code,
                                num = s1.prod_num.ObjToInt()
                            }).ToList();
                        foreach (var it in detaillist)
                        {
                            aa.Append(it.skucode + "*" + it.num + ",");
                        }
                        item.sku = aa.ToString();
                        item.sku1 = aa.ToString();
                        item.sendtype = 1; //默认全部为1
                        item.daihao = "048430747501";
                        item.mangagedaihao = "11";
                        item.senddaihao = "1";
                        //设置导出的包裹号未已导出，并且用编辑时间代表导出的时间，来方便再次导出已导出过的数据
                        db.Update<busi_sendorder>(new { is_export = true, edit_time = DateTime.Now }, it => it.order_id == item.packid);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }
        /// <summary>
        /// 查询选择的需要导出快递类型数据
        /// </summary>
        /// <param name="expressid"></param>
        /// <param name="ispacked"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="packgecount"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ExpressPackgeModel> GetExpressPackgeList(int expressid, int ispacked, int pageSize, int pageIndex, out int packgecount, out int count, DateTime? starttime, DateTime? endtime)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    var sql = db.Queryable<busi_sendorder>().JoinTable<busi_custorder>((s1, s2) => s1.custorder_id == s2.order_id)
                        .JoinTable<busi_custorder, base_shop>((s1, s2, s3) => s2.shop_id == s3.shop_id)
                        .Where<busi_sendorder>(s1 => s1.is_export == ispacked.ObjToBool())
                        .Where<busi_sendorder>(s1 => s1.express_id == expressid);
                    if (1 == ispacked)//查询已导出的
                    {
                        sql = sql.Where<busi_sendorder>(s1 => s1.edit_time >= starttime).Where<busi_sendorder>(s1 => s1.edit_time <= endtime);
                    }
                    count = sql.Count();
                    packgecount = count / pageSize;
                    if (count % pageSize > 0)
                    {
                        packgecount++;
                    }
                    List<ExpressPackgeModel> list = sql.OrderBy(s1 => s1.order_id).Skip(pageSize * (pageIndex - 1)).Take(pageSize)
                          .Select<base_shop, ExpressPackgeModel>((s1, s3) => new ExpressPackgeModel()
                          {
                              packid = s1.order_id.ObjToInt(),
                              sendtime = DateTime.Now,
                              packgecode = s1.order_code,
                              custname = s1.receive_name,
                              custaddress1 = s1.receive_address,
                              custaddress2 = "",
                              custphone = s1.receive_phone,
                              custmobile = s1.receive_mobile,
                              custzip = s1.receive_zip,
                              shopaddress = s3.shop_name,
                              shopzip = s3.shop_zipcode,
                              shopname = s3.shop_name,
                              shopphone = s3.shop_telephone,
                              sku = "",
                              sku1 = "",
                              sku2 = "",
                              sendtype = 1,
                              daihao = "",
                              mangagedaihao = "",
                              senddaihao = "",
                              custmangagedaihao = s1.order_code
                          }).ToList();
                    foreach (var item in list)
                    {
                        StringBuilder aa = new StringBuilder();
                        var detaillist = db.Queryable<busi_sendorder_detail>()
                            .JoinTable<base_prod_code>((s1, s2) => s1.code_id == s2.code_id)
                            .Where(s1 => s1.order_id == item.packid).Select<base_prod_code, SkuAndNum>((s1, s2) => new SkuAndNum()
                            {
                                skucode = s2.sku_code,
                                num = s1.prod_num.ObjToInt()
                            }).ToList();
                        foreach (var it in detaillist)
                        {
                            aa.Append(it.skucode + "*" + it.num + ",");
                        }
                        item.sku = aa.ToString();
                        item.sku1 = aa.ToString();
                        item.sendtype = 1; //默认全部为1
                        item.daihao = "048430747501";
                        item.mangagedaihao = "11";
                        item.senddaihao = "1";
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }


        public int GetExpressCodeCount(int expressid)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    int count = db.Queryable<base_expresscode>().Where(s => s.express_id == expressid).Where(s => s.isuse == false).ToList().Count;
                    return count;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 使用快递单号
        /// </summary>
        /// <param name="expressid"></param>
        /// <param name="csvtable"></param>
        /// <returns></returns>
        public int UseExpressCode(int expressid, System.Data.DataTable csvtable)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                int mycount = 0;
                db.CommandTimeOut = 3000;//设置超时时间
                try
                {
                    db.BeginTran();//开启事务
                    //string sql = string.Format(@"", expressid);
                    foreach (DataRow item in csvtable.Rows)
                    {
                        base_expresscode Eid = db.SqlQuery<base_expresscode>("select top 1 * from base_expresscode where isuse=0 and express_id=@expressid order by Eid", new { expressid = expressid }).FirstOrDefault();
                        string mypackge = item["包裹号"].ToString();
                        db.Update<base_expresscode>(new { isuse = true, Usetime = DateTime.Now, packgecode = mypackge }, it => it.Eid == Eid.Eid);
                        bool isok = db.Update<busi_sendorder>(new { exp_code = Eid.Expresscode }, it => it.order_code == mypackge);//如果包裹号不存在系统中也没事
                        if (isok)
                        {
                            mycount++;
                        }
                    }
                    db.CommitTran();//提交事务
                    return mycount;
                }
                catch (Exception ex)
                {
                    db.RollbackTran();//回滚事务
                    throw ex;
                }
            }
        }


        public int AssociateExpressCode(int expressid, DataTable csvtable)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                int mycount = 0;
                db.CommandTimeOut = 3000;  //设置超时时间
                try
                {
                    db.BeginTran();//开启事务
                    //string sql = string.Format(@"", expressid);
                    foreach (DataRow item in csvtable.Rows)
                    {
                        //base_expresscode Eid = db.SqlQuery<base_expresscode>("select top 1 * from base_expresscode where isuse=0 and express_id=@expressid order by Eid", new { expressid = expressid }).FirstOrDefault();
                        string mypackge = item["包裹号"].ToString();
                        string expresscode = item["快递单号"].ToString();
                        busi_sendorder aa = db.Queryable<busi_sendorder>().Where(s => s.order_code == mypackge).FirstOrDefault();
                        if (null == aa)
                        {
                            throw new Exception(mypackge + "此包裹号在系统中不存在,请确认");

                        }
                        else if (expressid != aa.express_id)
                        {
                            throw new Exception("导入的快递类型和原先选择的不一致,请确认");
                        }
                        else
                        {
                            bool isok = db.Update<busi_sendorder>(new { exp_code = expresscode }, it => it.order_code == mypackge);//如果包裹号不存在系统中也没事
                            if (isok)
                            {
                                mycount++;
                            }
                        }
                    }
                    db.CommitTran();//提交事务
                    return mycount;
                }
                catch (Exception ex)
                {
                    db.RollbackTran();//回滚事务
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 关联DHL单号和转运单号
        /// </summary>
        /// <param name="dhlcode"></param>
        /// <param name="zhuanyuncode"></param>
        /// <returns></returns>
        public bool AssociateDHLExpressCode(string dhlcode, string zhuanyuncode)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    busi_transfer tran = db.Queryable<busi_transfer>().Where(s => s.tran_code == zhuanyuncode).FirstOrDefault();
                    if (null==tran)
                    {
                        throw new Exception("不存在这个转运单号");
                    }
                    //指定列更新
                    bool isok = db.Update<busi_transfer>(new { express_code = dhlcode }, it => it.tran_code == zhuanyuncode); //只更新name列                 
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
