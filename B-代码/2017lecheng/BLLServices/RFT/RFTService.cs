using DBModel.Common;
using DBModel.DBmodel;
using DBModel.ViewModel;
using IBLLService.RFT;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.RFT
{
    public class RFTService : IRFT
    {
        private static string conn = "server=127.0.0.1;uid=sa;pwd=jhqn89808980;database=20170717Newlecheng";// "server=122.226.76.100,5433;uid=sa;pwd=123!@#QAZ;database=Newlecheng";

        public DBModel.DBmodel.base_users CheckLogin(string userid, string password)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    base_users platlist = db.Queryable<base_users>().Where(s => s.user_id == userid.ObjToInt()).Where(s => s.user_pwd == password).FirstOrDefault();
                    return platlist;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 判断SKU是否在系统中存在 
        /// </summary>
        /// <param name="SKUcode"></param>
        /// <returns></returns>
        public bool IsSKUInSys(string SKUcode)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {

                    base_prod_code sku = db.Queryable<base_prod_code>().Where(s => s.sku_code == SKUcode).FirstOrDefault();
                    if (sku == null)
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
        /// 正常配货上架,店铺优先，包裹优先全部包含在内
        /// </summary>
        /// <param name="SKUcode"></param>
        /// <returns></returns>
        public RFTDistrView Distrbuilt(string SKUcode,int shopID,string packcode)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    var depotID = db.Queryable<base_wh_warehouse>().Where(s => s.wh_name == "金华仓").FirstOrDefault();
                    if (null == depotID)
                    {
                        throw new Exception("金华仓不存在");
                    }
                    var locationID = db.Queryable<base_location>().Where(s => s.wh_id == depotID.wh_id && s.locat_code == "zz-zz-zz").FirstOrDefault();
                    if (null == locationID)
                    {
                        throw new Exception("金华仓库位zz-zz-zz不存在");
                    }
                    long codeid = db.Queryable<base_prod_code>().Where(s => s.sku_code == SKUcode).FirstOrDefault().code_id;//得到skuid
                    var stock = db.Queryable<base_wh_stock>().Where(s => s.location_id == locationID.locat_id && s.code_id == codeid).FirstOrDefault();
                    if (stock==null || 0 >= stock.stock_qty)
                    {
                        throw new Exception("金华仓临时库库存为零");
                    }

                    string[] list=null;
                    if(!string.IsNullOrEmpty(packcode))
                    {
                        list = db.SqlQuery<string[]>(@"select top 1 a.order_code,a.latest_date,b.work_id,b.packid,b.shop_id,b.prod_code_id,c.prod_num,c.order_code,a.order_id from busi_custorder a right join 
                                                    busi_workinfo b on a.order_id=b.custorder_id left join busi_sendorder c on c.order_id=b.packid 
                                                    where b.del_flag=1 and b.is_work=0 and b.islock=0 and c.del_flag=1 and b.prod_code_id=@codeid and c.order_code=@packcode order by  
                                                    a.order_date ,c.prod_num,b.create_time", new { codeid = codeid, packcode = packcode }).SingleOrDefault();
                    }
                    else if (0 != shopID)//店铺优先
                    {
                        list = db.SqlQuery<string[]>(@"select top 1 a.order_code,a.latest_date,b.work_id,b.packid,b.shop_id,b.prod_code_id,c.prod_num,c.order_code,a.order_id from busi_custorder a right join 
                                                    busi_workinfo b on a.order_id=b.custorder_id left join busi_sendorder c on c.order_id=b.packid 
                                                    where b.del_flag=1 and b.is_work=0 and b.islock=0 and c.del_flag=1 and b.prod_code_id=@codeid and a.shop_id=@shopID order by  
                                                    a.order_date ,c.prod_num,b.create_time", new { codeid = codeid, shopID = shopID }).SingleOrDefault();
                    }
                    else 
                    {
                        list = db.SqlQuery<string[]>(@"select top 1 a.order_code,a.latest_date,b.work_id,b.packid,b.shop_id,b.prod_code_id,c.prod_num,c.order_code,a.order_id from busi_custorder a right join 
                                                    busi_workinfo b on a.order_id=b.custorder_id left join busi_sendorder c on c.order_id=b.packid 
                                                    where b.del_flag=1 and b.is_work=0 and b.islock=0 and c.del_flag=1 and b.prod_code_id=@codeid order by  
                                                    a.order_date ,c.prod_num,b.create_time", new { codeid = codeid }).SingleOrDefault();
                    }
                    if (list==null) //无可配包裹
                    {
                        return null; 
                    }
                    //得到包裹号
                    int packid=list[3].ObjToInt();
                    //得到包裹已配数量
                    int Ispackpacknum=db.SqlQuery<int>(@"select count(*) as num from busi_workinfo where is_work=1 and packid=@packid",new {packid=packid}).SingleOrDefault();
                    //得到订单号
                    string ordercode = list[0].ToString();
                    //得到包裹号
                    string packgecode = list[7].ToString();
                    //得到包裹SKU总数
                    int packnum=list[6].ObjToInt();
                    //得到店铺ID
                    int shopid=list[4].ObjToInt();
                    //得到订单中包裹数目
                    int cusorderid=list[8].ObjToInt();                  
                    int packges=db.Queryable<busi_sendorder>().Where(s => s.custorder_id == cusorderid).ToList().Count;

                    string shopname=db.Queryable<base_shop>().Where(s => s.shop_id == shopid).FirstOrDefault().shop_name;
                    int workid = list[2].ObjToInt();
                    RFTDistrView dis = new RFTDistrView();
                    dis.Ispackpacknum = Ispackpacknum;
                    dis.ordercode = ordercode;
                    dis.skucode = SKUcode;
                    dis.packgecode = packgecode;
                    dis.packnum = packnum;
                    dis.shopname = shopname;
                    dis.workid = workid;
                    dis.packges = packges;
                    dis.packid = packid;
                    dis.orderid = cusorderid;
                    if (packnum > 1)
                    {
                        dis.info = "可拆包";
                    }
                    else
                    {
                        dis.info = "可包装";
                    }
                    db.Update<busi_workinfo>(new { islock = 1 }, s => s.work_id == dis.workid); //锁定这条配货信息
                    return dis;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 店铺优先配货上架
        /// </summary>
        /// <param name="skucode"></param>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public object ShopDistrbuilt(string skucode, int? shopid)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    long codeid = db.Queryable<base_prod_code>().Where(s => s.sku_code == skucode).FirstOrDefault().code_id;  //得到skuid
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 包裹号优先配货
        /// </summary>
        /// <param name="skucode"></param>
        /// <param name="packgecode"></param>
        /// <returns></returns>
        public object PDistrbuilt(string skucode, string packgecode)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }




        /// <summary>
        /// 确认配货
        /// </summary>
        /// <param name="peihuoinfo"></param>
        /// <param name="Userinfo"></param>
        /// <returns></returns>
        public bool ConfirmDistruit(RFTDistrView peihuoinfo, base_users Userinfo)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    //1.先设置work_info中已配货的状态
                    bool isok = db.Update<busi_workinfo>(new { is_work = 1, work_type = 2, work_time = DateTime.Now, edit_user_id = Userinfo.user_id, islock = 0 }, it => it.work_id == peihuoinfo.workid);
                    if (isok)
                    {                       
                        if (peihuoinfo.info == "可包装")//单件的，需要打印
                        {
                            db.Update<busi_sendorder>(new { order_tatus = 40 }, it => it.order_id ==peihuoinfo.packid);
                            //插入打印数据,只是包裹号
                            busi_printwork insertinfo = new busi_printwork(); 
                            insertinfo.p_WorkType = 30;
                            insertinfo.data_1 = peihuoinfo.packgecode;
                            insertinfo.data_4 = DateTime.Now.ToString();
                            insertinfo.p_idPoint = 1; //测试
                            insertinfo.p_Status = 1;
                            insertinfo.data_2 = peihuoinfo.packgecode.Substring(8,4);
                            insertinfo.data_3 = peihuoinfo.packnum.ToString();
                            db.Insert<busi_printwork>(insertinfo);

                            //-------------------20170720减库存--------------------------------------
                            var depotID = db.Queryable<base_wh_warehouse>().Where(s => s.wh_name == "金华仓").FirstOrDefault();
                            if (null == depotID)
                            {
                                throw new Exception("金华仓不存在");
                            }
                            var locationID = db.Queryable<base_location>().Where(s => s.wh_id == depotID.wh_id && s.locat_code == "zz-zz-zz").FirstOrDefault();
                            if (null == locationID)
                            {
                                throw new Exception("金华仓库位zz-zz-zz不存在");
                            }
                            var skuid = db.Queryable<base_prod_code>().Where(s => s.sku_code == peihuoinfo.skucode).FirstOrDefault();
                            var stock = db.Queryable<base_wh_stock>().Where(s => s.location_id == locationID.locat_id && s.code_id == skuid.code_id).FirstOrDefault();
                            if (stock == null || 0 >= stock.stock_qty)
                            {
                                throw new Exception("金华仓临时库库存为零");
                            }
                            stock.stock_qty--;
                            db.Update<base_wh_stock>(stock);
                            //---------------------------------------------------------------------
                        }
                        else if (peihuoinfo.info == "可拆包")//多件的在拣选中打印(上架)
                        {
                            //2.再判断此包裹是否已配齐，如果配齐，设置状态，如果不配齐，设置对应状态（如果是单件的，直接打印，多件的不打印）
                            int Ispackpacknum = db.SqlQuery<int>(@"select count(*) as num from busi_workinfo where is_work=1 and packid=@packid", new { packid = peihuoinfo.packid }).SingleOrDefault();
                            if (peihuoinfo.packnum ==Ispackpacknum)//此处判断包裹是否已配齐
                            {
                                db.Update<busi_sendorder>(new { order_tatus = 40 }, it => it.order_id == peihuoinfo.packid); //如果相等，配齐
                            }
                            else {
                                db.Update<busi_sendorder>(new { order_tatus = 30 }, it => it.order_id == peihuoinfo.packid); //如果小于，设置包裹部分配货
                            }                           
                        }
                        //3.判断订单是否已配齐
                        int packges=db.Queryable<busi_sendorder>().Where(s => s.custorder_id == peihuoinfo.orderid).Where(s => s.del_flag == true).ToList().Count;
                        //已配货数量
                        int ispackingnum = db.Queryable<busi_sendorder>().Where(s => s.custorder_id == peihuoinfo.orderid).Where(s => s.del_flag == true).Where(s => s.order_tatus >= 40).ToList().Count;
                        if (packges == ispackingnum)
                        {
                            db.Update<busi_custorder>(new { order_status=40 }, it => it.order_id == peihuoinfo.orderid);
                        }
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
        /// 生成拆分后的包裹号
        /// </summary>
        private static readonly object Locker = new object();
        private static long GetPackCode()
        {
            lock (Locker)  //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                long lastpack = 0;
                string date = DateTime.Now.ToString("yyyyMMdd") + "0001";  //得到当天第一个包裹号       
                using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
                {
                    busi_sendorder send = db.SqlQuery<busi_sendorder>("select * from busi_sendorder where order_code=(select MAX(order_code) from  busi_sendorder)").FirstOrDefault();
                    if (send == null)
                    {
                        return Convert.ToInt64(date);
                    }
                    if (Convert.ToInt64(send.order_code) >= Convert.ToInt64(date))
                    {
                        lastpack = Convert.ToInt64(send.order_code) + 1;
                        return lastpack;
                    }
                    else
                    {
                        return Convert.ToInt64(date);
                    }
                }
            }
        }

        /// <summary>
        /// 拆分包裹（虚拟拆分并没有真正拆分掉）
        /// </summary>
        /// <param name="peihuoinfo"></param>
        /// <returns></returns>
        public RFTDistrView Dispartpackge(RFTDistrView peihuoinfo)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    int packges = db.Queryable<busi_sendorder>().Where(s => s.custorder_id == peihuoinfo.orderid).ToList().Count;//得到订单包裹数
                    string npackgecode= GetPackCode().ToString();//新的包裹号
                    peihuoinfo.packgecode = npackgecode; //新增包裹号
                    peihuoinfo.info = "可包装";
                    peihuoinfo.packges = packges;//原先包裹数量
                    return peihuoinfo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 拆分包裹确认配货
        /// </summary>
        /// <param name="peihuoinfo"></param>
        /// <param name="Userinfo"></param>
        /// <returns></returns>
        public bool DispartConfirmDistruit(RFTDistrView peihuoinfo, base_users Userinfo)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                db.CommandTimeOut = 3000;//设置超时时间
                try
                {
                    db.BeginTran();
                    //1.先设置work_info中已配货的状态
                    bool isok = db.Update<busi_workinfo>(new { is_work = 1, work_type = 2, work_time = DateTime.Now, edit_user_id = Userinfo.user_id, islock = 0 }, it => it.work_id == peihuoinfo.workid);
                    if (isok)
                    {
                        if (peihuoinfo.info == "可包装")//单件的，需要打印
                        {
                            //1.先拆分包裹,先得到原先的包裹号
                            busi_sendorder oldpack = db.Queryable<busi_sendorder>().Where(s => s.order_id == peihuoinfo.packid).FirstOrDefault();
                            if (peihuoinfo.packgecode!=GetPackCode().ToString())//再次取新的包裹号，如果不相等说明刚才的包裹号被其他地方占用了
                            {
                                throw new Exception("拆分后的包裹号被占用请重新拆分");
                            }
                            long oldpackid = oldpack.order_id;
                            oldpack.prod_num--;//减少原先包裹SKU数量
                            db.Update<busi_sendorder>(oldpack);//先更新原先包裹SKU数量信息

                            oldpack.order_code = peihuoinfo.packgecode;
                            oldpack.order_id = 0;
                            oldpack.prod_num = 1;
                            oldpack.order_tatus = 40;  //包裹配货完成
                            int npackid=db.Insert<busi_sendorder>(oldpack).ObjToInt(); //插入新包裹
                            
                            //判断这个包裹中是否买了多个相同的SKU
                            long codeid = db.Queryable<base_prod_code>().Where(s => s.sku_code == peihuoinfo.skucode).FirstOrDefault().code_id;//得到skuid
                            busi_sendorder_detail oldsenddetail = db.Queryable<busi_sendorder_detail>().Where(s1 => s1.order_id == peihuoinfo.packid).Where(s1 => s1.code_id == codeid).FirstOrDefault();
                            long senddetailid = 0;
                            if (Convert.ToInt32(oldsenddetail.prod_num)> 1) //原包裹相同SKU购买多件
                            {
                                oldsenddetail.prod_num--;
                                db.Update<busi_sendorder_detail>(new { prod_num = oldsenddetail.prod_num }, it => it.detail_id == oldsenddetail.detail_id);
                                oldsenddetail.detail_id = 0;
                                oldsenddetail.order_id = npackid;
                                oldsenddetail.prod_num = 1;
                                senddetailid = db.Insert<busi_sendorder_detail>(oldsenddetail).ObjToInt();
                            }
                            else if (Convert.ToInt32(oldsenddetail.prod_num)==1)//只买了一个，直接更新这条数据
                            {
                                senddetailid = oldsenddetail.detail_id;
                                db.Update<busi_sendorder_detail>(new { order_id = npackid }, it => it.detail_id == oldsenddetail.detail_id);
                            }
                            db.Update<busi_workinfo>(new { packid = npackid, sendorder_detail_id = senddetailid,islock=0 }, it => it.work_id == peihuoinfo.workid);
                            //==================20171023添加，如果上架过，最后一个拆分包裹，检查原包裹的状态并做相应的更新===================
                            //1.判断已上架的数量和包裹SKU数量是否相等
                            int skunum=db.Queryable<busi_sendorder>().Where(s => s.order_id == oldpackid).FirstOrDefault().prod_num;
                            var worklist=db.Queryable<busi_workinfo>().Where(s => s.packid == oldpackid  && s.is_work==true && s.del_flag==true).ToList();
                            if (skunum == worklist.Count)
                            {
                                db.Update<busi_sendorder>(new { order_tatus = 40 }, it => it.order_id == peihuoinfo.packid);//原先包裹更新状态,全部配货
                            }
                            else
                            {
                                db.Update<busi_sendorder>(new { order_tatus = 30 }, it => it.order_id == peihuoinfo.packid);//原先包裹更新状态，部分配货
                            }                            
                            //================================================================================================
                            //插入打印数据
                            //插入打印数据,只是包裹号
                            busi_printwork insertinfo = new busi_printwork();
                            insertinfo.p_WorkType = 30;
                            insertinfo.data_1 = peihuoinfo.packgecode;
                            insertinfo.data_4 = DateTime.Now.ToString();
                            insertinfo.p_idPoint = 1; //测试
                            insertinfo.p_Status = 1;
                            insertinfo.data_2 = peihuoinfo.packgecode.Substring(8, 4);
                            insertinfo.data_3 = peihuoinfo.packnum.ToString();
                            db.Insert<busi_printwork>(insertinfo);

                            //-------------------20170720减库存--------------------------------------
                            var depotID = db.Queryable<base_wh_warehouse>().Where(s => s.wh_name == "金华仓").FirstOrDefault();
                            if (null == depotID)
                            {
                                throw new Exception("金华仓不存在");
                            }
                            var locationID = db.Queryable<base_location>().Where(s => s.wh_id == depotID.wh_id && s.locat_code == "zz-zz-zz").FirstOrDefault();
                            if (null == locationID)
                            {
                                throw new Exception("金华仓库位zz-zz-zz不存在");
                            }
                            var skuid = db.Queryable<base_prod_code>().Where(s => s.sku_code == peihuoinfo.skucode).FirstOrDefault();
                            var stock = db.Queryable<base_wh_stock>().Where(s => s.location_id == locationID.locat_id && s.code_id == skuid.code_id).FirstOrDefault();
                            if (0 >= stock.stock_qty)
                            {
                                throw new Exception("金华仓临时库库存为零");
                            }
                            stock.stock_qty--;
                            db.Update<base_wh_stock>(stock);
                            //---------------------------------------------------------------------------
                            //3.得到这个订单的所有包裹数目
                            int packges = db.Queryable<busi_sendorder>().Where(s => s.custorder_id == peihuoinfo.orderid).Where(s => s.del_flag == true).ToList().Count;
                            //已配货数量
                            int ispackingnum = db.Queryable<busi_sendorder>().Where(s => s.custorder_id == peihuoinfo.orderid).Where(s => s.del_flag == true).Where(s => s.order_tatus >= 40).ToList().Count;
                            if (packges == ispackingnum)
                            {
                                db.Update<busi_custorder>(new { order_status = 40 }, it => it.order_id == peihuoinfo.orderid);
                            }
                        }
                        db.CommitTran();
                        return true;
                    }
                    else
                    {
                        db.RollbackTran();
                        return false;
                    }
                    
                }
                catch (Exception ex)
                {
                    db.RollbackTran();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 取消配货
        /// </summary>
        /// <param name="peihuoinfo"></param>
        /// <returns></returns>
        public bool CancelDistrubilt(RFTDistrView peihuoinfo)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    bool isok = db.Update<busi_workinfo>(new { islock = 0 }, s => s.work_id == peihuoinfo.workid);

                    return isok;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 得到所有的快递类型  Isjpexpress
        /// </summary>
        /// <returns></returns>
        public List<base_exp_comp> GetAllExpress()
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    List<base_exp_comp> list = db.Queryable<base_exp_comp>().Where(s => s.del_flag == true).Where(s => s.express_status == true).Where(s => s.Isjpexpress == true).ToList();

                    return list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 确定包裹快递类型
        /// </summary>
        /// <param name="packgecode"></param>
        /// <param name="expid"></param>
        /// <returns></returns>
        public bool SetExpress(string packgecode, int expid)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                   var busi_sendorder= db.Queryable<busi_sendorder>().Where(s1 => s1.order_code == packgecode).FirstOrDefault();
                   if (null == busi_sendorder)
                   {
                       return false;
                   }
                   else
                   {
                       bool isok=db.Update<busi_sendorder>(new { express_id = expid }, s => s.order_id == busi_sendorder.order_id);
                       return isok;
                   }
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 打印转运单
        /// </summary>
        /// <param name="zhuanyuncode"></param>
        /// <returns></returns>
        public bool PrintzhuanyunCode(string zhuanyuncode)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    busi_transfer inserttran = new busi_transfer();
                    inserttran.tran_status = 1;
                    inserttran.create_time = DateTime.Now;
                    inserttran.create_user_id = 1;
                    inserttran.del_flag = true;
                    inserttran.remark = "";
                    inserttran.edit_time = DateTime.Now;
                    inserttran.tran_code = zhuanyuncode;
                    inserttran.tran_count = 0;
                    db.Insert<busi_transfer>(inserttran);

                    busi_printwork insertinfo = new busi_printwork();
                    insertinfo.p_WorkType = 40;
                    insertinfo.data_1 = zhuanyuncode;
                    insertinfo.data_4 = DateTime.Now.ToString();
                    insertinfo.p_idPoint = 1; //测试
                    insertinfo.p_Status = 1;
                    var isok=db.Insert<busi_printwork>(insertinfo).ObjToInt();
                    if (isok > 0)
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
        /// 生成拆分后的包裹号
        /// </summary>
        private static readonly object Lockerzhuanyun = new object();
        public long CreatezhuanyunCode()
        {
            lock (Lockerzhuanyun)  //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                long lastpack = 0;
                string date = DateTime.Now.ToString("yyyyMMdd") + "001";  //得到当天第一个包裹号       
                using (var db = SugarDao.GetInstance(conn))
                {
                    busi_transfer send = db.SqlQuery<busi_transfer>("select * from busi_transfer where tran_code=(select MAX(tran_code) from  busi_transfer)").FirstOrDefault();
                    if (send == null)//表为空时
                    {
                        return Convert.ToInt64(date);
                    }
                    if (Convert.ToInt64(send.tran_code) >= Convert.ToInt64(date))
                    {
                        lastpack = Convert.ToInt64(send.tran_code) + 1;
                        return lastpack;
                    }
                    else
                    {
                        return Convert.ToInt64(date);
                    }                    
                }
            }
        }


        public bool IszhuanyunCodeInSys(string zhuanyuncode)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    busi_transfer sku = db.Queryable<busi_transfer>().Where(s => s.del_flag==true).Where(s => s.tran_code == zhuanyuncode).FirstOrDefault();
                    if (sku == null)
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
        /// 判断是否已转运
        /// </summary>
        /// <param name="zhuanyuncode"></param>
        /// <returns></returns>
        public bool IszhuanyunCodeing(string zhuanyuncode)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    busi_transfer sku = db.Queryable<busi_transfer>().Where(s => s.del_flag == true).Where(s => s.tran_status == 1).Where(s => s.tran_code == zhuanyuncode).FirstOrDefault();
                    if (sku == null)
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
        /// 装箱
        /// </summary>
        /// <param name="zhuanyuncode"></param>
        /// <param name="packgecode"></param>
        /// <returns></returns>
        public bool ConfirmPutInBox(string zhuanyuncode, string packgecode)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    busi_transfer tran=db.Queryable<busi_transfer>().Where(s => s.tran_code == zhuanyuncode).FirstOrDefault();
                    if (null == tran)
                    {
                        throw new Exception("转运单号不存在!");
                    }
                    else
                    {
                        //1.判断包裹是否配货完毕
                        busi_sendorder send = db.Queryable<busi_sendorder>().Where(s => s.order_code==packgecode).FirstOrDefault();
                        if (null == send)
                        {
                            throw new Exception("不存在此包裹");
                        }
                        busi_sendorder send2=db.Queryable<busi_sendorder>().Where(s => s.order_tatus >= 40).Where(s => s.order_code==packgecode).FirstOrDefault();
                        if(null==send2)
                        {
                            throw new Exception("包裹未配货完毕不能装箱");
                        }
                        busi_sendorder send3 = db.Queryable<busi_sendorder>().Where(s => s.express_id>0).Where(s => s.order_code == packgecode).FirstOrDefault();
                        if (null == send3)
                        {
                            throw new Exception("请先选择快递再装箱!");
                        }
                        busi_sendorder send4 = db.Queryable<busi_sendorder>().Where(s => s.tran_id ==0).Where(s => s.order_code==packgecode).FirstOrDefault();
                        if (null == send4)
                        {
                            throw new Exception("包裹已装箱,请先出箱!");
                        }
                        db.Update<busi_sendorder>(new { tran_id = tran.tran_id, order_tatus=80 }, it => it.order_code == packgecode);//更新已转运状态
                        db.Update<busi_custorder>(new { order_status = 80 }, it => it.order_id==send.custorder_id);
                        tran.tran_count++;
                        bool isok2 = db.Update<busi_transfer>(tran);//更新转运单中包裹数量
                        return isok2;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool ConfirmOutofBox(string zhuanyuncode, string packgecode)
        {
            using (var db = SugarDao.GetInstance(conn))
            {
                try
                {
                    busi_transfer tran = db.Queryable<busi_transfer>().Where(s => s.tran_code == zhuanyuncode).FirstOrDefault();
                    if (null == tran)
                    {
                        throw new Exception("转运单号不存在!");
                    }
                    else
                    {
                        //1.判断包裹是否配货完毕
                        busi_sendorder send = db.Queryable<busi_sendorder>().Where(s => s.order_code == packgecode).FirstOrDefault();
                        if (null == send)
                        {
                            throw new Exception("不存在此包裹");
                        }
                        busi_sendorder send2 = db.Queryable<busi_sendorder>().Where(s => s.order_tatus >= 40).Where(s => s.order_code == packgecode).FirstOrDefault();
                        if (null == send2)
                        {
                            throw new Exception("包裹未配货完毕不能出箱");
                        }
                        busi_sendorder send3 = db.Queryable<busi_sendorder>().Where(s => s.tran_id > 0).Where(s => s.order_code == packgecode).FirstOrDefault();
                        if (null == send3)
                        {
                            throw new Exception("包裹未装箱,请先装箱!");
                        }
                        db.Update<busi_sendorder>(new { tran_id = 0 }, it => it.order_code == packgecode);
                        tran.tran_count--;
                        bool isok2 = db.Update<busi_transfer>(tran);//更新转运单中包裹数量
                        return isok2;
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
