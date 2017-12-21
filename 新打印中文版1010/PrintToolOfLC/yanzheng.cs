using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing.Printing;
using PrintToolOfLC.Helper;
using ModelPrint;
using XMLClass;

namespace PrintToolOfLC
{
   

    public partial class FM_Regedit : Form
    {
        public FM_Regedit()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 认证序列号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
           
            string p_regcode = textBox1.Text.Trim();
            //1.验证数据库授权码
            if (string.IsNullOrEmpty(p_regcode))
            {
                MessageBox.Show("请先输入注册授权码,若没有请联系程序提供商！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            DALPrint.D_Print help = new DALPrint.D_Print();
            List<M_print> list = help.IsExit(p_regcode);
            if (null == list || list.Count <= 0)
            {
                MessageBox.Show("注册授权码不正确！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                
                NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
                //更新配置文件
                xmlfile.UpdateNode("/Passport/pass[1]", "up");  //list.First().P_id
                xmlfile.UpdateNode("/Passport/ID[1]", list.First().P_id.ToString());
                xmlfile.UpdateNode("/Passport/time[1]", list.First().Del_DateTime.ToString());
                MessageBox.Show("注册成功！请重启打印插件！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
               
            
            }
           


        }
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FM_Regedit_Load(object sender, EventArgs e)
        {
            //获取安装电脑的所有MAC地址
            string mac = string.Empty;

            List<string> macdress = GetMacAddressByDos();//获取所有的MAC地址
            for (int i = 0; i < macdress.Count; i++)
            {

                listBox1.Items.Add(macdress[i].ToString());

            }
          

            //获取打印机列表
            //listBox1.Dock = DockStyle.Fill;
            //foreach (String fPrinterName in LocalPrinter.GetLocalPrinters())
            //{
            //    listBox1.Items.Add(fPrinterName);
            
            //}
              
            //this.Controls.Add(fListBox);

        }


        /// <summary>  
        /// 通过DOS命令获得MAC地址  
        /// </summary>  
        /// <returns></returns>  
        public List<string> GetMacAddressByDos()
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
        /// 关闭窗口按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FM_Regedit_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                //Comm.OutSystem();
            
        }


    }
}
