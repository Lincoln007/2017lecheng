using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BarcodeLib;
using System.Drawing.Printing;
using System.Windows.Forms;
using ModelPrint;
using PrintToolOfLC.MyModel;
using XMLClass;

namespace PrintToolOfLC.Helper
{
    /// <summary>
    /// 打印ECOHAI的类
    /// </summary>
    public class PrintECOHai
    {
        private string ourtelephone = "Tel: 03-6667-4879";
        private string ourzipcode = "〒: 160-0023";
        private string ouraddress = "東京都新宿区西新宿3-5-12トーカン新宿\r\n第2キャステール1004号室";
        private string ourname = "株式会社ジャパンドレス";
        private string ourCode = "0366674879250";
        private string SKUS = string.Empty;

        public MyEcohai pwork;  //打印信息存储类
        //通过构造函数传递进来
        public PrintECOHai(MyEcohai p, string a = "Tel: 03-6667-4879", string b = "〒: 160-0023", string c = "東京都新宿区西新宿3-5-12トーカン新宿\r\n第2キャステール1004号室", string d = "株式会社ジャパンドレス", string e = "0366674879250", string f = "")
        {
            pwork = p;
            ourtelephone = a;
            ourzipcode = b;
            ouraddress = c;
            ourname = d;
            ourCode = e;
            SKUS = f;
        }
        /// <summary>
        /// 直接打印,是否显示打印对话框
        /// </summary>
        /// <param name="p_ShowPrintDialog"></param>
        public void Print(bool p_ShowPrintDialog)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                //1.获取打印机插件是否设置了打印拣选单的面单
                NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "Passport");
                bool isexit = xmlfile.IsExitinnertextOfNode("ECOHAI", "page");
                if (isexit)
                {
                    //设置打印机的名称
                    pd.PrinterSettings.PrinterName = xmlfile.GetinnertextOfPrintName("ECOHAI", "page");
                }
                else
                {
                    throw new Exception("没有配置ECOhai面单！请先配置！");
                }
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                if (p_ShowPrintDialog)
                {
                    PrintDialog pdlg = new PrintDialog();
                    pdlg.Document = pd;
                    DialogResult res = pdlg.ShowDialog();
                    if (res == DialogResult.OK)
                        pd.Print();
                }
                else
                {
                    pd.Print();
                }
            }
            finally
            {


            }
        }

         /// <summary>
        /// 打印页面的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            string addr1 = string.Empty;
            string addr2 = string.Empty;
            if (pwork.Data_2.Length >= 25)
            {
                addr1 = pwork.Data_2.Substring(0, 25);
                addr2 = pwork.Data_2.Substring(25);
            }
            else if (pwork.Data_2.Length<25)
            {
                addr1 = pwork.Data_2;
                addr2 ="";
            }
            string time = DateTime.Now.ToString("yyyyMMdd hhmmss");
            //打印条码  201510270066
            Barcode bar = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
           
            var Image = bar.Encode(TYPE.Codabar, "a"+pwork.Data_7+"a",230,40);       //生成条码图像
          
            System.Drawing.Point pt = new System.Drawing.Point(70, 15);   //打印开始点
            e.Graphics.DrawImage(Image, pt);
            System.Drawing.Point pt2 = new System.Drawing.Point(470, 245);   //打印开始点
            e.Graphics.DrawImage(Image, pt2);
            System.Drawing.Point pt3 = new System.Drawing.Point(470, 495);   //打印开始点
            e.Graphics.DrawImage(Image, pt3);
            e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(135f, 55f));
            e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(535f, 285f));
            e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(535f, 535f));
            //条码时间
            e.Graphics.DrawString(time, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(580f, 235f));               
            //打印字符串店铺名代号
            e.Graphics.DrawString("B", new System.Drawing.Font("黑体", 15f), System.Drawing.Brushes.Black, new System.Drawing.PointF(45f, 30f));
            e.Graphics.DrawString("B", new System.Drawing.Font("黑体", 15f), System.Drawing.Brushes.Black, new System.Drawing.PointF(365f, 250f));
            e.Graphics.DrawString("B", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(560f, 310f));  
            
            //面单快递单号
            e.Graphics.DrawString(pwork.Data_7, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(80f, 90f));
            e.Graphics.DrawString(pwork.Data_7, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(420f, 310f));
            //客户信息
            e.Graphics.DrawString("Tel: "+pwork.Data_5.Insert(2,"-").Insert(6,"-"), new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(40f, 115f));
            e.Graphics.DrawString("〒:" + pwork.Data_1.Insert(3,"-"), new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(160f, 115f));
            e.Graphics.DrawString(addr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(40f, 125f));
            e.Graphics.DrawString(addr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(40f, 135f));
            e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(40f, 170f));
            //公司信息
            e.Graphics.DrawString(ourtelephone, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(40f, 205f));
            e.Graphics.DrawString(ourzipcode, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(160f, 205f));
            e.Graphics.DrawString(ouraddress, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(40f, 215f));
            e.Graphics.DrawString(ourname, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(40f, 255f));
            //打印公司代号
            e.Graphics.DrawString(ourCode, new System.Drawing.Font("黑体", 12f), System.Drawing.Brushes.Black, new System.Drawing.PointF(175f, 330f));
            e.Graphics.DrawString(ourCode, new System.Drawing.Font("黑体", 12f), System.Drawing.Brushes.Black, new System.Drawing.PointF(350f, 500f));
            //-------------------------------------------另外两段客户信息------------------------------------------------------------------------------------------------
            //客户信息
            e.Graphics.DrawString("Tel: " + pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 12f));
            e.Graphics.DrawString("〒:" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(500f, 12f));
            e.Graphics.DrawString(addr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 22f));
            e.Graphics.DrawString(addr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 32f));
            e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 67f));
            //公司信息
            e.Graphics.DrawString(ourtelephone, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 102f));
            e.Graphics.DrawString(ourzipcode, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(500f, 102f));
            e.Graphics.DrawString(ouraddress, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 112f));
            e.Graphics.DrawString(ourname, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 152f));

            //客户信息
            e.Graphics.DrawString("Tel: " + pwork.Data_5.Insert(2, "-"), new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 330f));
            e.Graphics.DrawString("〒:" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(500f, 330f));
            e.Graphics.DrawString(addr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 340f));
            e.Graphics.DrawString(addr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 350f));
            e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 385f));
            //公司信息
            e.Graphics.DrawString(ourtelephone, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 415f));
            e.Graphics.DrawString(ourzipcode, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(500f, 415f));
            e.Graphics.DrawString(ouraddress, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 425f));
            e.Graphics.DrawString(ourname, new System.Drawing.Font("黑体", 6f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 465f));

            //------------------------------------------------SKU信息------------------------------------------------------------------------------------------------
            e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(40f, 280f));
            e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(380f, 175f)); 
            //---------------------------------------------------------------------------------------------------------------------------------------------

        }
        
    }
}
