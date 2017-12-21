using DBModel.DBmodel;
using Printer.CommHelper;
using Printer.DBmodel;
using Printer.Service;
using SqlSugarRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Printer
{
    public partial class main : Form
    {
        int Hasprintcount = 0;
        //Queue<busi_printwork> Queuepackge = new Queue<busi_printwork>(); //包裹队列
        Queue<long> Queuepackge = new Queue<long>(); //包裹队列
        Dictionary<long, busi_printwork> PrintListpackge = new Dictionary<long, busi_printwork>();

        //Queue<busi_printwork> Queuezhuanyun = new Queue<busi_printwork>(); //转运队列
        Queue<long> Queuezhuanyun = new Queue<long>(); //转运
        Dictionary<long, busi_printwork> PrintListzhuanyun = new Dictionary<long, busi_printwork>();
        
        Queue<long> Queueyamato = new Queue<long>(); //yamato打印队列包裹号
        Dictionary<long, busi_printwork> PrintListYamato = new Dictionary<long, busi_printwork>();

       // Queue<busi_printwork> Queueupacket = new Queue<busi_printwork>(); //Upacket打印队列
        Queue<long> Queueupacket = new Queue<long>(); //Upacket打印队列
        Dictionary<long, busi_printwork> PrintListUpacket = new Dictionary<long, busi_printwork>();

        //Queue<JXpackge> QueueSelect = new Queue<JXpackge>(); //包裹拣选
        Queue<string> QueueSelect = new Queue<string>(); //包裹拣选
        Dictionary<string, JXpackge> PrintListSelect = new Dictionary<string, JXpackge>();
        public main()
        {
            InitializeComponent();
            StartUp();
            int msg=online(1);
            if (msg==0)
            {
                if (MessageBox.Show("打印插件未在系统中注册或者被删除了!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    Comm.OutSystem();
                }
            }else if(msg==2)
            {
                if (MessageBox.Show("打印插件启动失败!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                    Comm.OutSystem();
                }
            }
        }
        /// <summary>
        /// 得到打印插件ID
        /// </summary>
        /// <returns></returns>
        private static int GetprintID()
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string printID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            return printID.ObjToInt();
        }
        /// <summary>
        /// 判断进程是否唯一
        /// </summary>
        private void StartUp()
        {
            Process currentProcess = Process.GetCurrentProcess();

            foreach (Process item in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (item.Id != currentProcess.Id && (item.StartTime - currentProcess.StartTime).TotalMilliseconds <= 0)
              
                {
                    item.Kill();

                    item.WaitForExit();

                    break;
                }
            }
        }


        /// <summary>
        /// 设置打印插件在线状态
        /// </summary>
        /// <returns></returns>
        private int online(int on) //on=0,离线，on=1,在线
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string ID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            int p_id = Convert.ToInt32(ID);
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    //获取配置的插件信息
                    var obj=db.Queryable<base_print>().Where(s => s.del_flag == 0).Where(s => s.p_id == p_id).FirstOrDefault();
                    if (null==obj)
                    {
                        return 0; //"打印插件未在系统中注册或者被删除了!";
                    }else
                    {
                        var isok = db.Update<base_print>(new { isonline = on }, s => s.p_id == p_id);
                         if (isok)
                         {
                             return 1;//"启动成功";
                         }
                         else
                         {
                             return 2;//"启动失败";
                         }
                    }
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("停止服务后打印机服务无法正常工作，确认停止吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                online(0);
                Comm.OutSystem();
            }
        }

        /// <summary>
        /// 关于我们
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
            MessageBox.Show("检查更新功能等待开发");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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

        private void MyMenu_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("是否要退出插件程序？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                Comm.OutSystem();//退出系统
            }
        }

        /// <summary>
        /// 队列全部打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label9.Text = QueueSelect.Count.ToString(); //队列显示未打印数量
                int p_id = GetprintID();
                if (QueueSelect.Count < 14) //队列中数据小于14个包裹，直接打印
                {
                    int mycount = QueueSelect.Count;
                    for (int m = 0; m < mycount; m++)
                    {
                        string packgecode=QueueSelect.Dequeue(); //出队列
                        JXpackge s = PrintListSelect[packgecode];
                        Printselect.mylist = Printselect.mylist.Union(s.selectlist).ToList<Myselect>();  //放入打印list中 
                        CommPrint.FinshSelectPrint(p_id, s.packgecode);
                        PrintListSelect.Remove(packgecode);
                    }
                    Printselect.Print(false);//打印
                    Printselect.mylist.Clear(); //移除所有元素
                }
                label9.Text = QueueSelect.Count.ToString(); //队列显示未打印数量
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 包裹号扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)//按下回车键
            {
                string packgecode = textBox1.Text.Trim();
                if (string.IsNullOrEmpty(packgecode))
                {
                    MessageBox.Show("请扫描包裹号或者输入正确的包裹号！");
                    textBox1.Focus();
                    return;
                }
                else //包裹号不为空
                { 
                    //1.先判断包裹号是否在系统中存在，且没有取消
                    int isok=CheckSysInfo.CheckPackge(packgecode);
                    if (1 == isok)
                    {
                        Log(packgecode + "包裹在系统中不存在!" + DateTime.Now.ToString());
                        MessageBox.Show("包裹在系统中不存在!");
                        textBox1.Text = "";
                        textBox1.Focus();
                        return;
                    }else if(2==isok)
                    {
                        Log(packgecode + "包裹已取消，请取出!" + DateTime.Now.ToString());
                        MessageBox.Show("包裹已取消，请取出!");
                        textBox1.Text = "";
                        textBox1.Focus();
                    }
                    //2.如果包裹存在系统中，正常,取出包裹的信息
                    List<PackgePrintInfo> packinfolist=CheckSysInfo.GetPckgePrintInfo(packgecode);
                    busi_printwork printinsert = new busi_printwork();
                    StringBuilder skus = new StringBuilder();
                    foreach (var item in packinfolist)
                    {
                        skus = skus.Append(item.skucode+"*"+item.skunum+",");
                    }
                    if (null == packinfolist)
                    {
                        Log("无法获取到包裹信息" + DateTime.Now.ToString());
                        MessageBox.Show("无法获取到包裹信息,打印失败");
                        textBox1.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    if (packinfolist[0].expressid == 0 || string.IsNullOrEmpty(packinfolist[0].ExpCode))
                    {
                        Log("包裹信息不完整,该包裹还未选择发货方式或没单号,无法打印" + DateTime.Now.ToString());
                        MessageBox.Show("包裹信息不完整,该包裹还未选择发货方式或没单号,无法打印");
                        textBox1.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(packinfolist[0].name) || string.IsNullOrEmpty(packinfolist[0].mobile + packinfolist[0].phone))
                    {
                        Log("包裹信息不完整,没有客户名或者电话信息，无法打印" + DateTime.Now.ToString());
                        MessageBox.Show("包裹信息不完整,没有客户名或者电话信息，无法打印");
                        textBox1.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(packinfolist[0].address) || string.IsNullOrEmpty(packinfolist[0].zipcode))
                    {
                        Log("包裹信息不完整,没有客户地址或者邮编信息，无法打印" + DateTime.Now.ToString());
                        MessageBox.Show("包裹信息不完整,没有客户地址或者邮编信息，无法打印");
                        textBox1.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    printinsert.data_10 =  packgecode;
                    printinsert.data_1 = "Scan:" + packgecode;
                    printinsert.data_2 = packinfolist[0].name;//客户名
                    printinsert.data_3 = packinfolist[0].mobile + ","+packinfolist[0].phone;
                    printinsert.data_4 = packinfolist[0].zipcode;
                    printinsert.data_5 = packinfolist[0].address;
                    printinsert.data_6 = packinfolist[0].count.ToString();
                    printinsert.data_7 = skus.ToString();

                    printinsert.data_9 = packinfolist[0].ExpCode;
                    
                    printinsert.p_idPoint=GetprintID();  //得到打印插件ID
                    printinsert.p_Status = 1;
                    printinsert.p_WorkRemarks = "";
                    printinsert.create_DateTime = DateTime.Now;
                    switch (packinfolist[0].expressid)
                    {
                        case 1: //yamato
                            printinsert.data_8 = "yamato";
                            printinsert.p_WorkType = 50;
                            break;
                        case 2: //Upacket
                            printinsert.data_8 = "Upacket";
                            printinsert.p_WorkType = 70;
                            break;
                        default:
                              Log(packgecode + "包裹号,暂时不支持这种快递类型的打印！" + DateTime.Now.ToString());
                              MessageBox.Show("包裹号,暂时不支持这种快递类型的打印!");
                              textBox1.Text = "";
                              textBox1.Focus();
                            break;
                    }
                    bool isprintok=CheckSysInfo.InsertPrintMiandan(printinsert);
                    if (isprintok)
                    {
                        Log("包裹号:"+packgecode +" "+printinsert.data_8 + "面单打印成功!打印时间:" + DateTime.Now.ToString());
                        textBox1.Text = "";
                        textBox1.Focus();
                    }
                    else
                    {
                        MessageBox.Show("包裹号打印失败,请确认！");
                        textBox1.Text = "";
                        textBox1.Focus();
                    }
                   
                }
            }
        }
        /// <summary>
        /// 打印机设置
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
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Load(object sender, EventArgs e)
        {
            Timer updatetime = new Timer();  //时间
            updatetime.Interval = 1000;
            updatetime.Enabled = true;
            updatetime.Tick += new EventHandler(Timer1_Tick);
            updatetime.Start();   //启动计时器 

            Timer selectpack = new Timer(); //拣选单定时器
            selectpack.Interval = 1000;
            selectpack.Enabled = true;
            selectpack.Tick += new EventHandler(Timerselect_Tick);
            selectpack.Start();   //启动计时器 

            Timer packgecode = new Timer();  //包裹定时器
            packgecode.Interval = 1500;
            packgecode.Enabled = true;
            packgecode.Tick += new EventHandler(Timerpackge_Tick);
            packgecode.Start();   //启动计时器  

            Timer zhuanyun = new Timer();  //包裹转运单
            zhuanyun.Interval = 2000;
            zhuanyun.Enabled = true;
            zhuanyun.Tick += new EventHandler(Timerzhuanyun_Tick);
            zhuanyun.Start();   //启动计时器  

            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string isbehind = xmlfile2.ReadNodeInnerText("/Passport/IsBehind[1]");
            if ("1" == isbehind)
            {
                //Timer Umail = new Timer();  //Umail
                //Umail.Interval = 1500;
                //Umail.Enabled = true;
                //Umail.Tick += new EventHandler(TimerUmail_Tick);
                //Umail.Start();   //启动计时器 

                Timer Upacket = new Timer();  //Upacket
                Upacket.Interval = 1500;
                Upacket.Enabled = true;
                Upacket.Tick += new EventHandler(TimerUpacket_Tick);
                Upacket.Start();   //启动计时器  

                Timer yamato = new Timer();  //yamato
                yamato.Interval = 1500;
                yamato.Enabled = true;
                yamato.Tick += new EventHandler(Timeryamato_Tick);
                yamato.Start();   //启动计时器 
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


            }
        }
        /// <summary>
        /// 时间
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object Sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 拣选单打印
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timerselect_Tick(object Sender, EventArgs e)
        {
            
            int worktype = 20;
            int p_id = GetprintID();  //得到打印插件ID
            busi_printwork list = CommPrint.GetOneprintInfo(p_id, worktype);
            if(null!=list) //说明有数据
            {
                List<busi_printwork> allselect=CommPrint.GetSelectPackgeList(p_id, worktype, list.data_1);//取出这个包裹号所有SKU信息
                List<Myselect> myselectlist = new List<Myselect>();
                foreach(var item in allselect)
                {
                    Myselect p = new Myselect();
                    p.ID1 = item.p_Workid.ObjToInt();
                    p.Packgecode = item.data_1;
                    p.Position = item.data_4;
                    p.Number = item.data_3;
                    p.Skus = item.data_2;
                    p.Remarks1 = item.data_5;
                    myselectlist.Add(p);
                }
                JXpackge pack = new JXpackge();
                pack.selectlist = myselectlist;
                pack.packgecode = list.data_1;
                //将此包裹设为中间状态，防止多次取值
                int a = SQLHelper.ExecuteSql("update busi_printwork set p_Status=10 where p_WorkType=20 and data_1=" + "'" + list.data_1 + "'");
                if(a<0)
                {
                    Log("打印拣选设置中间状态出错!包裹号:" + list.data_1);
                    return;
                }
                if (!QueueSelect.Contains(pack.packgecode))
                {
                    QueueSelect.Enqueue(pack.packgecode);
                    PrintListSelect.Add(pack.packgecode,pack);
                }
                label9.Text = QueueSelect.Count.ToString(); //队列显示未打印数量
                #region 满足打印个数条件打印，不然等待队列满足条件，或者人为手动清空队列打印
                try
                {
                    if (QueueSelect.Count == 14) //队列中数据大于14个包裹，直接打印
                    {
                        for (int m = 0; m <14; m++)
                        {
                            //List<JXpackge> ss=QueueSelect.Take<JXpackge>(14);
                            string packgecode=QueueSelect.Dequeue(); //出队列
                            JXpackge s = PrintListSelect[packgecode];
                            Printselect.mylist = Printselect.mylist.Union(s.selectlist).ToList<Myselect>();  //放入打印list中 
                            CommPrint.FinshSelectPrint(p_id, s.packgecode);
                            PrintListSelect.Remove(packgecode);
                        }                        
                        Printselect.Print(false);//打印
                        Printselect.mylist.Clear(); //移除所有元素
                    }
                    label9.Text = QueueSelect.Count.ToString(); //队列显示未打印数量
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }

                #endregion
            }
        }
        /// <summary>
        /// 包裹打印
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timerpackge_Tick(object Sender, EventArgs e)
        {
            int worktype=30;
            int p_id=GetprintID();  //得到打印插件ID
            //1.得到需要打印的包裹号
            List<busi_printwork> list = CommPrint.GetprintInfo(p_id, worktype);
            if(list.Count>0)//有数据要打印
            {
                foreach(var item in list)
                {
                    if(!Queuepackge.Contains(item.p_Workid))
                    {
                        Queuepackge.Enqueue(item.p_Workid);
                        PrintListpackge.Add(item.p_Workid,item);
                    }
                }
            }
            if (PrintListpackge.Count > 0)//队列中有数据
            {
                try
                {
                    long workid = Queuepackge.Peek();
                    busi_printwork mypackge = PrintListpackge[workid]; //取出队列中对象
                    //打印包裹
                    packgeInfoViewModel mymodel = CommPrint.GetPrintpackge(mypackge);
                    if (null == mymodel)
                    {
                        throw new Exception("系统中不存在此店铺名或者此包裹" + mypackge.data_1);
                    }
                    PrintPackge printer = new PrintPackge(mymodel);
                    printer.Print(true);
                    bool isok=CommPrint.FinshPrint(p_id, mypackge);
                    if (!isok)
                    {
                        Log(mypackge.data_1 + "打印失败!");
                    }
                    else
                    {
                        //打印完成清除出队列
                        Queuepackge.Dequeue();
                        PrintListpackge.Remove(workid);
                        Hasprintcount++;
                        label7.Text = Hasprintcount.ToString();
                        Log(mypackge.data_1 + "包裹号已打印。" + DateTime.Now.ToString()); 
                    }                  
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 转运单打印
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timerzhuanyun_Tick(object Sender, EventArgs e)
        {
            int worktype = 40;
            int p_id = GetprintID();  //得到打印插件ID
            //1.得到需要打印的包裹号
            List<busi_printwork> list = CommPrint.GetprintInfo(p_id, worktype);
            if (list.Count > 0)//有数据要打印
            {
                foreach (var item in list)
                {
                    if (!Queuezhuanyun.Contains(item.p_Workid))
                    {
                        Queuezhuanyun.Enqueue(item.p_Workid);
                        PrintListzhuanyun.Add(item.p_Workid,item);
                    }
                }
            }
            if (PrintListzhuanyun.Count > 0)//队列中有数据
            {
                try
                {
                    long workid = Queuezhuanyun.Peek();
                    busi_printwork mypackge = PrintListzhuanyun[workid]; //取出队列中对象
                    zhuanyunInfo zhuanyun = new zhuanyunInfo();
                    zhuanyun.workid = mypackge.p_Workid.ObjToInt();
                    zhuanyun.zhuanyuncode = mypackge.data_1;
                    //打印转运单
                    Printzhuanyun printer = new Printzhuanyun(zhuanyun);
                    printer.Print(true);
                    bool isok = CommPrint.FinshPrint(p_id, mypackge);
                    if (!isok)
                    {
                        Log(mypackge.data_1 + "打印失败!");
                    }
                    else
                    {
                        //打印完成清除出队列
                        Queuezhuanyun.Dequeue();
                        PrintListzhuanyun.Remove(workid);
                        Hasprintcount++;
                        label7.Text = Hasprintcount.ToString();
                        Log(mypackge.data_1 + "转运单已打印。" + DateTime.Now.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Umail打印
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void TimerUmail_Tick(object Sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Upacket打印
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void TimerUpacket_Tick(object Sender, EventArgs e)
        {
            int worktype = 70;
            int p_id = GetprintID();  //得到打印插件ID
            //1.得到需要打印的包裹号
            List<busi_printwork> list = CommPrint.GetprintInfo(p_id, worktype);
            if (list.Count > 0)//有数据要打印
            {
                foreach (var item in list)
                {
                    if (!Queueupacket.Contains(item.p_Workid))
                    {
                        Queueupacket.Enqueue(item.p_Workid);
                        PrintListUpacket.Add(item.p_Workid,item);
                    }
                }
            }
            if (PrintListUpacket.Count > 0)//队列中有数据
            {
                try
                {
                    long workid = Queueupacket.Peek();
                    busi_printwork mypackge = PrintListUpacket[workid]; //取出队列中对象
                    //打印yamato
                    MyUpacket pupacket = CommPrint.GetPrintUpacket(mypackge);
                    if (null == pupacket)
                    {
                        throw new Exception("系统中不存在此店铺名或者此包裹" + mypackge.data_10);
                    }
                    PrintUpacket printer = new PrintUpacket(pupacket);
                    printer.Print(true);
                    bool isok = CommPrint.FinshPrint(p_id, mypackge);
                    if (!isok)
                    {
                        Log(mypackge.data_1 + "打印失败!");
                    }
                    else
                    {
                        //打印完成清除出队列
                        Queueupacket.Dequeue();
                        PrintListUpacket.Remove(workid);
                        Hasprintcount++;
                        label7.Text = Hasprintcount.ToString();
                        Log(mypackge.data_1 + "包裹号已打印。Upacket" + DateTime.Now.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }
        }
        /// <summary>
        /// yamato打印，
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void Timeryamato_Tick(object Sender, EventArgs e)
        {
            int worktype = 50;
            int p_id = GetprintID();  //得到打印插件ID
            //1.得到需要打印的包裹号
            List<busi_printwork> list = CommPrint.GetprintInfo(p_id, worktype);//worktype =50
            if (list.Count > 0)//有数据要打印
            {
                foreach (var item in list)
                {
                    if (!Queueyamato.Contains(item.p_Workid))  //data_10存的是包裹号
                    {
                        Queueyamato.Enqueue(item.p_Workid);
                        PrintListYamato.Add(item.p_Workid, item);
                    }
                }
            }
            if (PrintListYamato.Count > 0)//字典中有数据
            {
                try
                {
                    long workid = Queueyamato.Peek();
                    busi_printwork mypackge = PrintListYamato[workid]; //取出队列中对象
                    //打印yamato
                    Myamato pyamato = CommPrint.GetPrintyamato(mypackge);
                    if (null == pyamato)
                    {
                        throw new Exception("系统中不存在此店铺名或者此包裹" + mypackge.data_10);
                    }
                    PrintYamato printer = new PrintYamato(pyamato);
                    printer.Print(true);                  
                    bool isok = CommPrint.FinshPrint(p_id, mypackge); //设置已打印的状态
                    if (!isok)
                    {
                        Log(mypackge.data_1 + "打印失败!");
                    }
                    else
                    {
                        //打印完成清除出队列
                        Queueyamato.Dequeue();
                        PrintListYamato.Remove(workid);
                        Hasprintcount++;
                        label7.Text = Hasprintcount.ToString();
                        Log(mypackge.data_1 + "包裹号已打印。Yamato" + DateTime.Now.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 打印插件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_DoubleClick(object sender, EventArgs e)
        {
            NewXmlControl xmlfile2 = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            string ID = xmlfile2.ReadNodeInnerText("/Passport/ID[1]");
            using (var db = SugarDao.GetInstance(Getconnstring.Getmyconnstring()))
            {
                try
                {
                    //获取配置的插件信息
                    var obj = db.Queryable<base_print>().Where(s => s.p_id == Convert.ToInt32(ID)).FirstOrDefault();
                    if (null == obj)
                    {
                        MessageBox.Show("此打印插件在系统中不存在");
                        return;
                    }
                    else {
                        MessageBox.Show("打印插件名称:"+obj.p_name,"提示");
                        return;
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
