using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using PrintToolOfLC.config;
using XMLClass;
using DALPrint;
using ModelPrint;
using PrintToolOfLC.Helper;
using PrintToolOfLC.MyModel;
using System.IO;

namespace PrintToolOfLC
{
    public partial class main : Form
    {
        // bool is_regedit = false;
        int count = 0;
        int waitcount = 0;
        Dictionary<int, Mypackge> jadePrintList = new Dictionary<int, Mypackge>();
        Queue<int> keys = new Queue<int>();
        Dictionary<int, Myselect> jadePrintListSelect = new Dictionary<int, Myselect>();
        Queue<int> keysSelect = new Queue<int>();
        //-----------------------------2015-12.18增加-----------------------------------------------------
        Dictionary<int, MyUmail> jadePrintListUmail = new Dictionary<int, MyUmail>();
        Queue<int> keysUmail = new Queue<int>();
        Dictionary<int, MyUpacket> jadePrintListUpacket = new Dictionary<int, MyUpacket>();
        Queue<int> keysUpacket = new Queue<int>();
        Dictionary<int, MyYamato> jadePrintListYamato = new Dictionary<int, MyYamato>();
        Queue<int> keysYamato = new Queue<int>();
        Dictionary<int, MyEcohai> jadePrintListEcohai = new Dictionary<int, MyEcohai>();
        Queue<int> keysEcohai = new Queue<int>();
        Dictionary<int, Myzhuanyun> jadePrintListzhuanyun = new Dictionary<int, Myzhuanyun>();
        Queue<int> keyszhuanyun = new Queue<int>();
        //------------------------------------------------------------------------------------

        Queue<string> countpackde = new Queue<string>();
        public main()
        {

            InitializeComponent();
            StartUp();  
        }
        /// <summary>
        /// 判断进程是否唯一
        /// </summary>
        private void StartUp()
        {
            Process currentProcess = Process.GetCurrentProcess();

            foreach (Process item in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (item.Id != currentProcess.Id &&
                (item.StartTime - currentProcess.StartTime).TotalMilliseconds <= 0)
                {
                    item.Kill();

                    item.WaitForExit();

                    break;
                }
            }
        }  

        /// <summary>
        /// 关于我们暂时链接到百度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.baidu.com/");
        }
        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("当前程序是最新版本！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            return;
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("停止服务后打印机服务无法正常工作，确认停止吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                Comm.OutSystem();
            }
        }

        /// <summary>
        /// 查看日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filename = DateTime.Now.ToString("yyyyMMdd");
            Comm.IsExitFile(filename);
            //Comm.AddMyLog("测试信息！",filename);
            Comm.ShowLog(filename);
        }
        /// <summary>
        /// 增加日志
        /// </summary>
        /// <param name="content"></param>
        public void Log(string content)
        {
            string filename = DateTime.Now.ToString("yyyyMMdd");
            string log = string.Format("{0}\t{1:yyyy-MM-dd HH:mm:ss}\r\n", content, DateTime.Now);
            lblog.Items.Insert(0, content);
            Comm.AddMyLog(content, filename);
        }
        /// <summary>
        /// 队列显示
        /// </summary>
        /// <param name="content"></param>
        public void Waitcount(int count)
        {

            //lblog.Items.Insert(0, content);
            label9.Text = count.ToString();

        }
        /// <summary>
        /// 打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Log(DateTime.Now.ToString());
            printconfig config = new printconfig();
            //显示模态对话框
            config.ShowDialog();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Load(object sender, EventArgs e)
        {

            label7.Text = count.ToString();
            label9.Text = waitcount.ToString();

            //判断文件是否存在，不存在创建一个
            NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            //1.读取配置文件
            bool ispass = xmlfile.ReadXMLfileOfPassport("/Passport/pass[1]");
            string myID = xmlfile.ReadNodeInnerText("/Passport/ID[1]");
            List<string> macdress = GetMacAddressByDos2();
            StringBuilder br = new StringBuilder();
            br.Append(macdress[0].ToString());

            string cmdstr = "select * from Base_Print  where is_Lock=0 and is_Del=0 and p_SerialNum like '%" + br.ToString() + "%'";
            if( (!ispass) || !SQLHelper.Exists(cmdstr))
            {
                FM_Regedit regedit = new FM_Regedit();
                regedit.ShowDialog();
                label10.Text = "程序未注册功能暂时无法使用！";
                linkLabel1.Visible = false;
                linkLabel2.Visible = false;
                linkLabel3.Visible = false;
                linkLabel4.Visible = false;
                MessageBox.Show("程序未注册功能暂时无法使用,请联系程序提供商！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("程序启动成功！但未注册！" + DateTime.Now.ToString());
            }
            else
            {
                label10.Text = "程序已注册成功！";
                Log("程序启动成功！" + DateTime.Now.ToString());
            }
            bool ispass2 = xmlfile.ReadXMLfileOfPassport("/Passport/pass[1]");
            if (ispass2)
            {
                label10.Text = "程序已注册成功！";

            }
            else
            {
                label10.Text = "程序未注册功能暂时无法使用！";
                MessageBox.Show("程序未注册功能暂时无法使用，请联系程序提供商！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           

            //2.更新程序判断
            Timer selectpack = new Timer(); //拣选单定时器
            selectpack.Interval = 1000;
            selectpack.Enabled = true;
            selectpack.Tick += new EventHandler(Timerselect_Tick);
            selectpack.Start();   //启动计时器  
            //-----------------------------------20151217增加代码----------------------------------------------------------
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string isbehind = xmlfile2.ReadNodeInnerText("/Passport/IsBehind[1]");
            if ("1" == isbehind)
            {
                Timer Umail = new Timer();  //Umail
                Umail.Interval = 1500;
                Umail.Enabled = true;
                Umail.Tick += new EventHandler(TimerUmail_Tick);
                Umail.Start();   //启动计时器 

                Timer Upacket = new Timer();  //Umail
                Upacket.Interval = 1500;
                Upacket.Enabled = true;
                Upacket.Tick += new EventHandler(TimerUpacket_Tick);
                Upacket.Start();   //启动计时器  

                Timer yamato = new Timer();  //yamato
                yamato.Interval = 1500;
                yamato.Enabled = true;
                yamato.Tick += new EventHandler(Timeryamato_Tick);
                yamato.Start();   //启动计时器 

                Timer Ecohai = new Timer();  //Ecohai
                Ecohai.Interval = 2000;
                Ecohai.Enabled = true;
                Ecohai.Tick += new EventHandler(TimerEcohai_Tick);
                Ecohai.Start();   //启动计时器 
            }
            else //如果没有配置则用扫描枪，启用扫描输入框
            {
                textBox1.ReadOnly = false;

                Timer Umail = new Timer();  //Umail
                Umail.Interval = 1500;
                Umail.Enabled = true;
                Umail.Tick += new EventHandler(TimerUmail_Tick);
                Umail.Start();   //启动计时器 

                Timer Upacket = new Timer();  //Umail
                Upacket.Interval = 1500;
                Upacket.Enabled = true;
                Upacket.Tick += new EventHandler(TimerUpacket_Tick);
                Upacket.Start();   //启动计时器  

                Timer yamato = new Timer();  //yamato
                yamato.Interval = 1500;
                yamato.Enabled = true;
                yamato.Tick += new EventHandler(Timeryamato_Tick);
                yamato.Start();   //启动计时器 

                Timer Ecohai = new Timer();  //Ecohai
                Ecohai.Interval = 2000;
                Ecohai.Enabled = true;
                Ecohai.Tick += new EventHandler(TimerEcohai_Tick);
                Ecohai.Start();   //启动计时器 
            
            }
           

            //---------------------------------------------------------------------------------------------
            Timer packgecode = new Timer();  //包裹定时器
            packgecode.Interval = 1500;
            packgecode.Enabled = true;
            packgecode.Tick += new EventHandler(Timerpackge_Tick);
            packgecode.Start();   //启动计时器  

            Timer zhuanyun = new Timer();  //包裹转运
            zhuanyun.Interval = 1500;
            zhuanyun.Enabled = true;
            zhuanyun.Tick += new EventHandler(Timerzhuanyun_Tick);
            zhuanyun.Start();   //启动计时器  

            label4.Text = Comm.AssemblyVersion;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(Timer1_Tick);
            timer1.Start();   //启动计时器  
            //再次读取配置文件
            //NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
          
            
            this.textBox1.Focus();
        }

        /// <summary>
        /// 打印Umail
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void TimerUmail_Tick(object Sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            // Set the caption to the current time.
            string cmd = "select top 10 * from Print_Work where p_WorkType=60 and p_Status=1 and p_idPoint=" + printID + "order by p_Workid asc";
            DataSet ds = SQLHelper.Query(cmd);
            string cmd2 = string.Empty;
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //啥也不干
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var p = new MyUmail();
                    p.P_Workid = int.Parse(dr["p_Workid"].ToString());
                    p.Data_1 = dr["data_1"].ToString();
                    p.Data_2 = dr["data_2"].ToString();
                    p.Data_3 = dr["data_3"].ToString();
                    p.Data_4 = dr["data_4"].ToString();
                    p.Data_5 = dr["data_5"].ToString();
                    p.Data_6 = dr["data_6"].ToString();
                    p.Data_7 = dr["data_7"].ToString();
                    p.Data_8 = dr["data_8"].ToString();
                    p.Data_9 = dr["data_9"].ToString();
                    p.Data_10 = dr["data_10"].ToString();
                    p.Create_DateTime = dr["create_DateTime"].ToString();
                    p.Create_UserID = Convert.ToInt32(dr["create_UserID"].ToString());
                    p.Edit_DateTime = dr["edit_DateTime"].ToString();
                    p.P_idActual = Convert.ToInt32(dr["p_idActual"].ToString());
                    p.P_idPoint = Convert.ToInt32(dr["p_idPoint"].ToString()); 
                    p.P_Status = Convert.ToInt32(dr["p_Status"].ToString());
                    p.P_WorkRemarks = dr["p_WorkRemarks"].ToString();
                    p.P_WorkType = Convert.ToInt32(dr["p_WorkType"].ToString());
                    p.Print_DateTime1 = dr["Print_DateTime"].ToString();

                    cmd2 = string.Format("select c.ShopName,c.Platform from busi_orderpackge a join busi_order b on a.OrderID=b.OrderID   join busi_shop c on c.ShopID=b.ShopID where a.PackgeCode='{0}'", dr["data_3"].ToString());
                    DataTable mid = SQLHelper.Query(cmd2).Tables[0];
                    if (mid == null || mid.Rows.Count <= 0)
                    {
                        //throw new Exception("不存在店铺名！请确认");
                        //p.Shopname = "JPdress";
                        //p.Platform = "4";
                        if (!string.IsNullOrEmpty(dr["data_9"].ToString()))
                        {
                            p.Shopname = dr["data_9"].ToString();
                            p.Platform = "import";
                        }
                    }
                    else
                    {
                        p.Shopname = mid.Rows[0]["ShopName"].ToString();
                        p.Platform = mid.Rows[0]["Platform"].ToString();
                    }

                    if (!keysUmail.Contains(p.P_Workid))
                    {
                        keysUmail.Enqueue(p.P_Workid);
                        jadePrintListUmail.Add(p.P_Workid, p);
                    }

                }
                if (jadePrintListUmail.Count > 0)
                {
                    try
                    {
                        int id = keysUmail.Peek();
                        MyUmail printpack = jadePrintListUmail[id];
                        PrintUmail pack = new PrintUmail(printpack);
                        pack.Print(false);
                        keysUmail.Dequeue();
                        jadePrintListUmail.Remove(id);
                        Log(printpack.Data_3 + "包裹已打印打印模式一,发货方式为：Umail," + DateTime.Now.ToString());
                        SQLHelper.ExecuteSql("update Print_Work set p_Status=0,p_idActual=" + printID + ",Print_DateTime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where p_Workid=" + printpack.P_Workid);  
                    }
                    catch (Exception ex)
                    {
                        Log(ex.ToString());

                    }
                }
                waitcount = waitcount + 1;
                label7.Text = waitcount.ToString();
            }


            //Log("包裹号定时器启动" + DateTime.Now.ToString() + "当前队列待打数目：" + jadePrintList.Count.ToString());
        }


        /// <summary>
        /// 打印Upacket
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void TimerUpacket_Tick(object Sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            // Set the caption to the current time.
            string cmd = "select top 10 * from Print_Work where p_WorkType=70 and p_Status=1 and p_idPoint=" + printID + "order by p_Workid asc";
            DataSet ds = SQLHelper.Query(cmd);
            string cmd2 = string.Empty;
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //啥也不干
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var p = new MyUpacket();
                    p.P_Workid = int.Parse(dr["p_Workid"].ToString());
                    p.Data_1 = dr["data_1"].ToString();
                    p.Data_2 = dr["data_2"].ToString();
                    p.Data_3 = dr["data_3"].ToString();
                    p.Data_4 = dr["data_4"].ToString();
                    p.Data_5 = dr["data_5"].ToString();
                    p.Data_6 = dr["data_6"].ToString();
                    p.Data_7 = dr["data_7"].ToString();
                    p.Data_8 = dr["data_8"].ToString();
                    p.Data_9 = dr["data_9"].ToString();
                    p.Data_10 = dr["data_10"].ToString();
                    p.Create_DateTime = dr["create_DateTime"].ToString();
                    p.Create_UserID = Convert.ToInt32(dr["create_UserID"].ToString());
                    p.Edit_DateTime = dr["edit_DateTime"].ToString();
                    p.P_idActual = Convert.ToInt32(dr["p_idActual"].ToString());
                    p.P_idPoint = Convert.ToInt32(dr["p_idPoint"].ToString());
                    p.P_Status = Convert.ToInt32(dr["p_Status"].ToString());
                    p.P_WorkRemarks = dr["p_WorkRemarks"].ToString();
                    p.P_WorkType = Convert.ToInt32(dr["p_WorkType"].ToString());
                    p.Print_DateTime1 = dr["Print_DateTime"].ToString();

                    cmd2 = string.Format("select c.ShopName,c.Platform from busi_orderpackge a join busi_order b on a.OrderID=b.OrderID   join busi_shop c on c.ShopID=b.ShopID where a.PackgeCode='{0}'", dr["data_3"].ToString());
                    DataTable mid = SQLHelper.Query(cmd2).Tables[0];
                    if (mid == null || mid.Rows.Count <= 0)
                    {
                        //throw new Exception("不存在店铺名！请确认");
                        //p.Shopname = "JPdress";
                        //p.Platform = "4";
                        if (!string.IsNullOrEmpty(dr["data_9"].ToString()))
                        {
                            p.Shopname = dr["data_9"].ToString();
                            p.Platform = "import";
                        }
                    }
                    else
                    {
                        p.Shopname = mid.Rows[0]["ShopName"].ToString();
                        p.Platform = mid.Rows[0]["Platform"].ToString();
                    }

                    if (!keysUpacket.Contains(p.P_Workid))
                    {
                        keysUpacket.Enqueue(p.P_Workid);
                        jadePrintListUpacket.Add(p.P_Workid, p);
                    }

                }
                if (jadePrintListUpacket.Count > 0)
                {
                    try
                    {
                        int id = keysUpacket.Peek();
                        MyUpacket printpack = jadePrintListUpacket[id];
                        PrintUpacket pack = new PrintUpacket(printpack);
                        pack.Print(false);
                        keysUpacket.Dequeue();
                        jadePrintListUpacket.Remove(id);
                        Log(printpack.Data_3 + "包裹已打印打印模式一,发货方式为：Upacket," + DateTime.Now.ToString());
                        SQLHelper.ExecuteSql("update Print_Work set p_Status=0,p_idActual=" + printID + ",Print_DateTime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where p_Workid=" + printpack.P_Workid);
                    }
                    catch (Exception ex)
                    {
                        Log(ex.ToString());

                    }
                }
                waitcount = waitcount + 1;
                label7.Text = waitcount.ToString();
            }


            //Log("包裹号定时器启动" + DateTime.Now.ToString() + "当前队列待打数目：" + jadePrintList.Count.ToString());
        }


        /// <summary>
        /// 打印Yamato
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timeryamato_Tick(object Sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            // Set the caption to the current time.
            string cmd = "select top 10 * from Print_Work where p_WorkType=50 and p_Status=1 and p_idPoint=" + printID + " order by p_Workid asc";
            DataSet ds = SQLHelper.Query(cmd);
            string cmd2 = string.Empty;
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //啥也不干
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var p = new MyYamato();
                    p.P_Workid = int.Parse(dr["p_Workid"].ToString());
                    p.Data_1 = dr["data_1"].ToString();
                    p.Data_2 = dr["data_2"].ToString();
                    p.Data_3 = dr["data_3"].ToString();
                    p.Data_4 = dr["data_4"].ToString();
                    p.Data_5 = dr["data_5"].ToString();
                    p.Data_6 = dr["data_6"].ToString();
                    p.Data_7 = dr["data_7"].ToString();
                    p.Data_8 = dr["data_8"].ToString();
                    p.Data_9 = dr["data_9"].ToString();
                    p.Data_10 = dr["data_10"].ToString();
                    p.Create_DateTime = dr["create_DateTime"].ToString();
                    p.Create_UserID = Convert.ToInt32(dr["create_UserID"].ToString());
                    p.Edit_DateTime = dr["edit_DateTime"].ToString();
                    p.P_idActual = Convert.ToInt32(dr["p_idActual"].ToString());
                    p.P_idPoint = Convert.ToInt32(dr["p_idPoint"].ToString());
                    p.P_Status = Convert.ToInt32(dr["p_Status"].ToString());
                    p.P_WorkRemarks = dr["p_WorkRemarks"].ToString();
                    p.P_WorkType = Convert.ToInt32(dr["p_WorkType"].ToString());
                    p.Print_DateTime1 = dr["Print_DateTime"].ToString();

                    cmd2 = string.Format("select c.ShopName,c.Platform from busi_orderpackge a join busi_order b on a.OrderID=b.OrderID   join busi_shop c on c.ShopID=b.ShopID where a.PackgeCode='{0}'", dr["data_3"].ToString());
                    DataTable mid = SQLHelper.Query(cmd2).Tables[0];
                    if (mid == null || mid.Rows.Count <= 0)
                    {
                        //throw new Exception("不存在店铺名！请确认"); 2017-01-16修改
                        //p.Shopname = "JPdress";
                        //p.Platform = "4";
                        if (!string.IsNullOrEmpty(dr["data_9"].ToString()))
                        {
                            p.Shopname = dr["data_9"].ToString();
                            p.Platform = "import";
                        }
                    }
                    else
                    {
                        p.Shopname = mid.Rows[0]["ShopName"].ToString();
                        p.Platform = mid.Rows[0]["Platform"].ToString();
                    }
                  
                    if (!keysYamato.Contains(p.P_Workid))
                    {
                        keysYamato.Enqueue(p.P_Workid);
                        jadePrintListYamato.Add(p.P_Workid, p);
                    }

                }
                if (jadePrintListYamato.Count > 0)
                {
                    try
                    {
                        int id = keysYamato.Peek();
                        MyYamato printpack = jadePrintListYamato[id];
                        PrintYamto pack = new PrintYamto(printpack);
                        pack.Print(false);
                        keysYamato.Dequeue();
                        jadePrintListYamato.Remove(id);
                        Log(printpack.Data_3 + "包裹已打印打印模式一,发货方式为：Yamato," + DateTime.Now.ToString());
                        SQLHelper.ExecuteSql("update Print_Work set p_Status=0,p_idActual=" + printID + ",Print_DateTime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where p_Workid=" + printpack.P_Workid);
                    }
                    catch(Exception ex)
                    {
                        Log(ex.ToString());
                    }
                }
                waitcount = waitcount + 1;
                label7.Text = waitcount.ToString();
            }


            //Log("包裹号定时器启动" + DateTime.Now.ToString() + "当前队列待打数目：" + jadePrintList.Count.ToString());
        }

        /// <summary>
        /// 打印ECOHAI
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void TimerEcohai_Tick(object Sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            // Set the caption to the current time.
            string cmd = "select top 10 * from Print_Work where p_WorkType=80 and p_Status=1 and p_idPoint=" + printID + "order by p_Workid asc";
            DataSet ds = SQLHelper.Query(cmd);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //啥也不干
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var p = new MyEcohai();
                    p.P_Workid = int.Parse(dr["p_Workid"].ToString());
                    p.Data_1 = dr["data_1"].ToString();
                    p.Data_2 = dr["data_2"].ToString();
                    p.Data_3 = dr["data_3"].ToString();
                    p.Data_4 = dr["data_4"].ToString();
                    p.Data_5 = dr["data_5"].ToString();
                    p.Data_6 = dr["data_6"].ToString();
                    p.Data_7 = dr["data_7"].ToString();
                    p.Data_8 = dr["data_8"].ToString();
                    p.Data_9 = dr["data_9"].ToString();
                    p.Data_10 = dr["data_10"].ToString();
                    p.Create_DateTime = dr["create_DateTime"].ToString();
                    p.Create_UserID = Convert.ToInt32(dr["create_UserID"].ToString());
                    p.Edit_DateTime = dr["edit_DateTime"].ToString();
                    p.P_idActual = Convert.ToInt32(dr["p_idActual"].ToString());
                    p.P_idPoint = Convert.ToInt32(dr["p_idPoint"].ToString());
                    p.P_Status = Convert.ToInt32(dr["p_Status"].ToString());
                    p.P_WorkRemarks = dr["p_WorkRemarks"].ToString();
                    p.P_WorkType = Convert.ToInt32(dr["p_WorkType"].ToString());
                    p.Print_DateTime1 = dr["Print_DateTime"].ToString();

                    if (!keysEcohai.Contains(p.P_Workid))
                    {
                        keysEcohai.Enqueue(p.P_Workid);
                        jadePrintListEcohai.Add(p.P_Workid, p);
                    }

                }
                if (jadePrintListEcohai.Count > 0)
                {
                    try
                    {
                        int id = keysEcohai.Peek();
                        MyEcohai printpack = jadePrintListEcohai[id];
                        PrintECOHai pack = new PrintECOHai(printpack);
                        pack.Print(false);
                        keysEcohai.Dequeue();
                        jadePrintListEcohai.Remove(id);
                        Log(printpack.Data_3 + "包裹已打印打印模式一,发货方式为：Ecohai," + DateTime.Now.ToString());
                        SQLHelper.ExecuteSql("update Print_Work set p_Status=0,p_idActual=" + printID + ",Print_DateTime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where p_Workid=" + printpack.P_Workid);
                    }
                    catch (Exception ex)
                    {
                        Log(ex.ToString());
                    }
                }
                waitcount = waitcount + 1;
                label7.Text = waitcount.ToString();
            }


            //Log("包裹号定时器启动" + DateTime.Now.ToString() + "当前队列待打数目：" + jadePrintList.Count.ToString());
        }
        /// <summary>
        /// 定时器执行函数Timerselect_Tick
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object Sender, EventArgs e)
        {
            // Set the caption to the current time.

            label3.Text = DateTime.Now.ToString();
        }
        /// <summary>
        /// 打印拣选单
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timerselect_Tick(object Sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            // Set the caption to the current time.
            string cmd = "select top 1 * from Print_Work where p_WorkType=20 and p_Status=1 and p_idPoint=" + printID + "order by p_Workid asc";
            DataSet ds = SQLHelper.Query(cmd);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //啥也不干
                return;
            }
            else
            {
                string packgecode = ds.Tables[0].Rows[0]["data_1"].ToString();
                DataSet ds2 = SQLHelper.Query("select * from Print_Work where p_WorkType=20 and p_Status=1 and data_1=" + packgecode);
                if (ds2.Tables[0].Rows.Count <= 0)
                {

                }
                else
                {
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        
                        var p = new Myselect();
                        p.ID1 = int.Parse(dr["p_Workid"].ToString());
                        p.Packgecode = dr["data_1"].ToString();
                        p.Position = dr["data_4"].ToString();
                        p.Number = dr["data_3"].ToString();
                        p.Skus = dr["data_2"].ToString();
                        p.Remarks1 = dr["data_5"].ToString();
                        if (!keysSelect.Contains(p.ID1))
                        {
                            keysSelect.Enqueue(p.ID1);         //进队列
                            jadePrintListSelect.Add(p.ID1, p);  //进字典
                        }
                        SQLHelper.ExecuteSql("update Print_Work set p_Status=10 where p_WorkType=20 and data_1=" + "'" + packgecode + "'");

                    }
                    foreach (var item in jadePrintListSelect)
                    {
                        if (!countpackde.Contains(item.Value.Packgecode))
                        {
                            countpackde.Enqueue(item.Value.Packgecode);
                        }
                    }
                    label9.Text = countpackde.Count.ToString(); //队列显示未打印数量
                    //满足14个包裹号打印
                    #region 满足14个包裹号打印
                    if (countpackde.Count >= 14)         //如果满足14个包裹需要拣货则打印，不满足等待
                    {
                        int[] contain = new int[keysSelect.Count];
                        int count = keysSelect.Count;
                        keysSelect.CopyTo(contain, 0);
                        try
                        {
                            for (int i = 0; i < count; i++)
                            {
                                int id = contain[i];
                                Myselect printpack = jadePrintListSelect[id];
                                Printselect pack = new Printselect(printpack);
                                //int a=Printselect.mylist.Count;
                                keysSelect.Dequeue();
                                jadePrintListSelect.Remove(id);
                                SQLHelper.ExecuteSql("update Print_Work set p_Status=0, p_idActual= " + printID + ", Print_DateTime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'  where p_Workid=" + id.ToString());
                                countpackde.Clear();
                            }
                            Printselect.Print(false);
                            Printselect.mylist.Clear();                         //打印完一批清空队列
                            label9.Text = jadePrintListSelect.Count.ToString();   //队列显示未打印数量
                        }
                        catch (Exception ex)
                        {
                            Log(ex.ToString());
                        }

                    } 
                    #endregion
                
                }
              
            }
            //Log("拣选单定时器启动" + DateTime.Now.ToString());
        }
        /// <summary>
        /// 打印包裹号
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timerpackge_Tick(object Sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            // Set the caption to the current time.
            string cmd = "select top 10 * from Print_Work where p_WorkType=30 and p_Status=1 and p_idPoint=" + printID + "order by p_Workid asc";
            DataSet ds = SQLHelper.Query(cmd);
            string shopname = string.Empty;
            string cmd2 = string.Empty;
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //啥也不干
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var p = new Mypackge();
                    p.Id = int.Parse(dr["p_Workid"].ToString());
                    p.Packgecode = dr["data_1"].ToString();
                    p.Number = dr["data_3"].ToString();
                    cmd2 = string.Format("select c.ShopName from busi_orderpackge a join busi_order b on a.OrderID=b.OrderID   join busi_shop c on c.ShopID=b.ShopID where a.PackgeCode='{0}'", dr["data_1"].ToString());
                    DataTable res=SQLHelper.Query(cmd2).Tables[0];
                    if(res==null || res.Rows.Count<=0)
                    {
                        throw new Exception("不存在此店铺名！");
                    }else
                    {
                        p.ShopName = res.Rows[0]["ShopName"].ToString();
                    }
                    
                    p.Time = (Convert.ToDateTime(dr["data_4"].ToString()).AddDays(5)).ToString();
                    if (!keys.Contains(p.Id))
                    {
                        keys.Enqueue(p.Id);
                        jadePrintList.Add(p.Id, p);
                    }

                }
                if (jadePrintList.Count > 0)
                {
                    try
                    {
                        int id = keys.Peek();
                        Mypackge printpack = jadePrintList[id];
                        Printpackge pack = new Printpackge(printpack);
                        pack.Print(true);
                        keys.Dequeue();
                        jadePrintList.Remove(id);
                        Log(printpack.Packgecode + "包裹号已打印。" + DateTime.Now.ToString());
                        SQLHelper.ExecuteSql("update Print_Work set p_Status=0,p_idActual= " + printID + ", Print_DateTime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where p_Workid=" + id.ToString());
                    }
                    catch (Exception ex)
                    {
                        Log(ex.ToString());
                    }
                }
                waitcount = waitcount + 1;
                label7.Text = waitcount.ToString();
            }

          
            //Log("包裹号定时器启动" + DateTime.Now.ToString() + "当前队列待打数目：" + jadePrintList.Count.ToString());
        }


        /// <summary>
        /// 打印转运单号
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timerzhuanyun_Tick(object Sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            // Set the caption to the current time.
            string cmd = "select top 10 * from Print_Work where p_WorkType=40 and p_Status=1 and p_idPoint=" + printID + "order by p_Workid asc";
            DataSet ds = SQLHelper.Query(cmd);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //啥也不干
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var p = new Myzhuanyun();
                    p.Id = int.Parse(dr["p_Workid"].ToString());
                    p.Packgecode = dr["data_1"].ToString();

                    if (!keyszhuanyun.Contains(p.Id))
                    {
                        keyszhuanyun.Enqueue(p.Id);
                        jadePrintListzhuanyun.Add(p.Id, p);
                    }

                }
                if (jadePrintListzhuanyun.Count > 0)
                {
                    try
                    {
                        int id = keyszhuanyun.Peek();
                        Myzhuanyun printpack = jadePrintListzhuanyun[id];
                        Printzhuanyun pack = new Printzhuanyun(printpack);
                        pack.Print(true);
                        keyszhuanyun.Dequeue();
                        jadePrintListzhuanyun.Remove(id);
                        Log(printpack.Packgecode + "转运单号已打印。" + DateTime.Now.ToString());
                        SQLHelper.ExecuteSql("update Print_Work set p_Status=0,p_idActual= " + printID + ", Print_DateTime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where p_Workid=" + id.ToString());
                    }
                    catch (Exception ex)
                    {
                        Log(ex.ToString());
                    }
                }
                waitcount = waitcount + 1;
                label7.Text = waitcount.ToString();
            }


            //Log("包裹号定时器启动" + DateTime.Now.ToString() + "当前队列待打数目：" + jadePrintList.Count.ToString());
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //当用户点击窗体右上角X按钮或(Alt + F4)时 发生 
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.ShowInTaskbar = false;
                this.notifyIcon1.Icon = this.Icon;
                this.Hide();
            }
        }
        /// <summary>
        /// 打印机插件的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_DoubleClick(object sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
             string ispass2 = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            string time = xmlfile2.ReadNodeInnerText("/Passport/time[1]");
           
            DataTable table=SQLHelper.Query("select b.warehouse_name from Base_Print a join Base_WareHours b on b.warehouse_no=a.warehouse_no where a.p_id=" + Convert.ToInt32(ispass2)).Tables[0];
            string depotID = table.Rows[0]["warehouse_name"].ToString();
           
            MessageBox.Show("当前打印机插件的ID：" + ispass2 + "\r\n打印机插件所属仓库：" + depotID + "\r\n注册时间：" + time + "", "插件信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 单击右下角图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MyMenu.Show();
            }

            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }
        /// <summary>
        /// 单击菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyMenu_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("是否要退出插件程序？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                Comm.OutSystem();//退出系统
            }

        }
        /// <summary>
        /// 扫描入口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 窗口激活时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Activated(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }
        List<MyYamato> Constlist = new List<MyYamato>();
        PrintYamto yamato = null;
        /// <summary>
        /// 判断是否按下了Enter键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                string isdel = string.Empty;
                string expressid = string.Empty;
                string ExpCode = string.Empty;         
                string zipcode = string.Empty;
                string mobile = string.Empty;
                string phone = string.Empty;
                string address = string.Empty;
                string name = string.Empty;
                string count = string.Empty;
                string packge = textBox1.Text.Trim();
                if (string.IsNullOrEmpty(packge))
                {
                    Log(textBox1.Text+ "包裹已经取消,请取出！" + DateTime.Now.ToString());
                    textBox1.Text = "";
                    return;
                }
                string cmd = string.Format(@"select IsDel,ExpressID from busi_orderpackge where PackgeCode='{0}'", packge);
                string cmd2 = string.Format(@"select b.ZipCode,b.Address,b.Mobile,b.Phone,b.Name,a.ExpCode,c.Code,c.Num,a.Num as count from busi_orderpackge a join 
                                             busi_orderaddress b on a.OrderID=b.OrderID 
                                             join busi_orderproduct c on c.PackID=a.PackID
                                             where a.PackgeCode='{0}'",packge);
                string cmd3 = string.Empty;
                DataSet re = SQLHelper.Query(cmd);
                if (null == re || re.Tables[0].Rows.Count==0)
                {
                    Log(packge + "包裹在系统中不存在，请确认！" + DateTime.Now.ToString());
                    textBox1.Text = "";
                    return;
                }
                else
                {
                    isdel = re.Tables[0].Rows[0]["IsDel"].ToString();
                    expressid = re.Tables[0].Rows[0]["ExpressID"].ToString();
                    
                    if ("True" ==isdel)
                    {
                        Log(packge + "包裹已经取消,请取出！" + DateTime.Now.ToString());
                        textBox1.Text = "";
                        return;
                    }
                    DataSet result = SQLHelper.Query(cmd2);
                    if (null == result || result.Tables.Count == 0)
                    {
                        Log(packge + "包裹在系统中找不到此客户信息,请确认！" + DateTime.Now.ToString());
                        textBox1.Text = "";
                        return;
                    }
                    string skus = string.Empty;
                    ExpCode = result.Tables[0].Rows[0]["ExpCode"].ToString();
                    zipcode = result.Tables[0].Rows[0]["ZipCode"].ToString();
                    mobile = result.Tables[0].Rows[0]["Mobile"].ToString();
                    phone = result.Tables[0].Rows[0]["Phone"].ToString();
                    address = result.Tables[0].Rows[0]["Address"].ToString();
                    name = result.Tables[0].Rows[0]["Name"].ToString();
                    count=result.Tables[0].Rows[0]["count"].ToString();
                    foreach(DataRow dr in result.Tables[0].Rows)
                    {
                        skus =dr["Code"].ToString()+ "*"+dr["Num"].ToString()+",";
                    }
                    if (string.IsNullOrEmpty(zipcode))
                    {
                        Log(packge+"包裹没有邮编，请确认！");
                        return;
                    }
                    if (string.IsNullOrEmpty(mobile) && string.IsNullOrEmpty(phone))
                    {
                        Log(packge + "包裹没有电话或者手机号码，请确认！");
                        return;
                    }
                    if (string.IsNullOrEmpty(address))
                    {
                        Log(packge + "包裹没有地址信息，请确认！");
                        return;
                    }
                    if (string.IsNullOrEmpty(name))
                    {
                        Log(packge + "包裹没有客户名，请确认！");
                        return;
                    }
                    if (string.IsNullOrEmpty(ExpCode))
                    {
                        Log(packge + "包裹没有关联单号，请确认！");
                        return;
                    }
                    if ("9" == expressid ) //Umail
                    {
                         cmd3 = string.Format(@"insert into Print_Work (p_WorkType,data_1,data_2,data_3,data_4,data_5,data_6,data_7,data_8,data_9,data_10,
                                            p_idPoint,create_DateTime,create_UserID,p_Status,Print_DateTime,edit_DateTime,p_idActual,p_WorkRemarks) 
                                            values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}',{14},'{15}','{16}',{17},'{18}')",60,"Scan:"+packge,
                                            count,packge,zipcode,name,phone+","+mobile,address,skus,"",ExpCode,3,DateTime.Now.ToString(),"110",1,"","",3,"");
                    }
                    else if ("10" == expressid) //Upacket
                    {
                        cmd3 = string.Format(@"insert into Print_Work (p_WorkType,data_1,data_2,data_3,data_4,data_5,data_6,data_7,data_8,data_9,data_10,
                                            p_idPoint,create_DateTime,create_UserID,p_Status,Print_DateTime,edit_DateTime,p_idActual,p_WorkRemarks) 
                                            values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}',{14},'{15}','{16}',{17},'{18}')", 70, "Scan:" + packge,
                                           count, packge, zipcode, name, phone + "," + mobile, address, skus, "", ExpCode, 3, DateTime.Now.ToString(), "110", 1, "", "", 3, "");
                    }
                    else if ("12" == expressid) //Yamato
                    {
                        cmd3 = string.Format(@"insert into Print_Work (p_WorkType,data_1,data_2,data_3,data_4,data_5,data_6,data_7,data_8,data_9,data_10,
                                            p_idPoint,create_DateTime,create_UserID,p_Status,Print_DateTime,edit_DateTime,p_idActual,p_WorkRemarks) 
                                            values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}',{14},'{15}','{16}',{17},'{18}')", 50, zipcode,address,packge,skus,
                                            phone + "," + mobile, name, ExpCode, "Yamato", "", "", 3, DateTime.Now.ToString(), "110", 1, "", "", 3, "");
                    }
                    else
                    {
                        Log(packge + "包裹号,暂时不支持这种快递类型的打印！" + DateTime.Now.ToString());
                        textBox1.Text = "";
                        return;
                    }
                    int a=SQLHelper.ExecuteSql(cmd3);
                    if(0==a)
                    {
                        Log(packge + "包裹号打印失败,请检查！" + DateTime.Now.ToString());
                        textBox1.Text = "";
                        return;
                    }


                }
                textBox1.Text = "";
            }
            
        }


        /// <summary>  
        /// 通过DOS命令获得MAC地址  
        /// </summary>  
        /// <returns></returns>  
        public List<string> GetMacAddressByDos2()
        {
            List<string> macdresses = new List<string>();
            string macAddress = "";
            Process p = null;
            StreamReader reader = null;
            try
            {
                ProcessStartInfo start = new ProcessStartInfo("cmd.exe");

                start.FileName = "ipconfig";
                start.Arguments = "/all";

                start.CreateNoWindow = true;

                start.RedirectStandardOutput = true;

                start.RedirectStandardInput = true;

                start.UseShellExecute = false;

                p = Process.Start(start);

                reader = p.StandardOutput;

                string line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    if (line.ToLower().IndexOf("physical address") > 0 || line.ToLower().IndexOf("物理地址") > 0)
                    {
                        int index = line.IndexOf(":");
                        index += 2;
                        macAddress = line.Substring(index);
                        macdresses.Add(macAddress);
                        //macAddress = macAddress.Replace('-', ':');
                        //break;
                    }
                    line = reader.ReadLine();
                }
            }
            catch
            {

            }
            finally
            {
                if (p != null)
                {
                    p.WaitForExit();
                    p.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return macdresses;
        }
        /// <summary>
        /// 队列中的数据全部打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            if (countpackde.Count < 14 && countpackde.Count>0)
            {
                int[] contain = new int[keysSelect.Count];
                int count = keysSelect.Count;
                keysSelect.CopyTo(contain, 0);
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        int id = contain[i];
                        Myselect printpack = jadePrintListSelect[id];
                        Printselect pack = new Printselect(printpack);
                        //int a=Printselect.mylist.Count;
                        keysSelect.Dequeue();
                        jadePrintListSelect.Remove(id);
                        SQLHelper.ExecuteSql("update Print_Work set p_Status=0, p_idActual= " + printID + ", Print_DateTime='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'  where p_Workid=" + id.ToString());
                        countpackde.Clear();

                        //update Print_Work set p_Status=0, p_idActual= " + printID + ", Print_DateTime='" + DateTime.Now.ToString() + "'  where p_Workid=" + id.ToString()

                    }

                    Printselect.Print(false);
                    Printselect.mylist.Clear();                   //打印完一批清空队列
                    label9.Text = jadePrintListSelect.Count.ToString(); //队列显示未打印数量
                }
                catch
                {

                }

            }
            else if (countpackde.Count >= 14)
            {
                MessageBox.Show("队列中的数据大于14条请等待自动打印完成少于14条数据再打印剩余数据！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
                    
            }
            else if (countpackde.Count==0)
            {
                MessageBox.Show("队列中没有数据无法打印！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }
    }
}
