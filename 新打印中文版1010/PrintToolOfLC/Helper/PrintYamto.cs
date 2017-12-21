using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelPrint;
using System.Drawing.Printing;
using System.Windows.Forms;
using BarcodeLib;
using System.Drawing;
using ZPLPrinter;
using System.Data;
using PrintToolOfLC.MyModel;
using XMLClass;
using PrintToolOfLC;

namespace PrintToolOfLC.Helper
{
    class PrintYamto
    {
        private string ourtelephone = "Tel: 050-5803-2394";
        private string ourzipcode = "〒: 332-0027";
        private string ouraddress = "埼玉県川口市緑町9-35";
        private string ourname = "株式会社ジャパンドレス";
        private string ourCode = "0366674879250";
        private string SKUS = string.Empty;
        public List<MyYamato> mylist = null;
        public MyYamato pwork;  //打印信息存储类
        //通过构造函数传递进来
        public PrintYamto(MyYamato p, string a = "Tel: 050-5803-2394", string b = "〒: 332-0027", string c = "埼玉県川口市緑町9-35", string d = "株式会社ジャパンドレス", string e = "050-5803-2394", string f = "")
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
                //1.获取打印机插件是否设置了打印拣选单的面单
                NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "Passport");
                bool isexit = xmlfile.IsExitinnertextOfNode("Yamato", "page");
                if (isexit)
                {
                    //设置打印机的名称
                    //pd.PrinterSettings.PrinterName = xmlfile.GetinnertextOfPrintName("Umail", "page");
                }
                else
                {
                    throw new Exception("没有配置Yamato面单！请先配置！");
                }

                DataTable dt = new DataTable("table1");
                dt.Columns.Add(new DataColumn("ZIPCode", typeof(string)));
                dt.Columns.Add(new DataColumn("address1", typeof(string)));
                dt.Columns.Add(new DataColumn("address2", typeof(string)));
                dt.Columns.Add(new DataColumn("address3", typeof(string)));
                dt.Columns.Add(new DataColumn("SKU", typeof(string)));
                dt.Columns.Add(new DataColumn("YamtoCompany1", typeof(string)));
                dt.Columns.Add(new DataColumn("YamtoCompany2", typeof(string)));
                dt.Columns.Add(new DataColumn("YamtoCompany3", typeof(string)));
                dt.Columns.Add(new DataColumn("YamtoCompany4", typeof(string)));
                dt.Columns.Add(new DataColumn("Phone", typeof(string)));
                dt.Columns.Add(new DataColumn("CustumerName", typeof(string)));
                dt.Columns.Add(new DataColumn("ExpressCode", typeof(string)));
                dt.Columns.Add(new DataColumn("MessageForBuyer", typeof(string)));
                dt.Columns.Add(new DataColumn("SendShopname", typeof(string)));
                dt.Columns.Add(new DataColumn("packgecode", typeof(string)));
                DataRow row = dt.NewRow();
                //判断是否存在 - 符号
                if (string.IsNullOrEmpty(pwork.Data_1))
                {
                    new main().Log(pwork.Data_3 + "包裹号不存在邮编，请确认!");
                    //Log(DateTime.Now.ToString());
                    return ;   //"〒 160-0023";
                }
                if (pwork.Data_1.Contains("-"))
                {
                    row["ZIPCode"] = "〒 " + pwork.Data_1;   //"〒 160-0023";
                }
                else
                {
                    row["ZIPCode"] = "〒 " + pwork.Data_1.Insert(3,"-");
                }
                if (string.IsNullOrEmpty(pwork.Data_2))
                {
                    new main().Log(pwork.Data_3 + "包裹号不存在地址，请确认!");
                    //Log(DateTime.Now.ToString());
                    return;   //"〒 160-0023";
                }
                if (pwork.Data_2.Length > 30)
                {
                    row["address1"] = pwork.Data_2.Substring(0, 9); //"金华金帆街172金字火腿";
                    row["address2"] = pwork.Data_2.Substring(9,21);    //"乐诚网络";
                    row["address3"] = pwork.Data_2.Substring(30);
                }
                else if (pwork.Data_2.Length <= 30 || pwork.Data_2.Length >= 9)
                {
                    row["address1"] = pwork.Data_2.Substring(0, 9); //"金华金帆街172金字火腿";
                    row["address2"] = pwork.Data_2.Substring(9);//"乐诚网络";
                    row["address3"] = "";
                }
                else if (pwork.Data_2.Length<9)
                {
                    row["address1"] = pwork.Data_2; //"金华金帆街172金字火腿";
                    row["address2"] = "";          //"乐诚网络";
                    row["address3"] = "";
                }

                row["SKU"] = pwork.Data_4;
                row["YamtoCompany1"] = "ヤマト運輸株式会社";
                row["YamtoCompany2"] = "北東京物流システム支店";//,西新宿3丁目センター
                row["YamtoCompany3"] = "この荷物が郵便物ではありません　030-600";  //036-292
                row["YamtoCompany4"] ="お問い合せ先　フリーダイヤル　0120-11-8010";
                //判断是否存在 - 符号
                if (pwork.Data_5.Contains("-"))
                {
                    row["Phone"] = pwork.Data_5;  //"〒 160-0023";   MessageForBuyer
                }
                else
                {
                    row["Phone"] = pwork.Data_5.Insert(3, "-").Insert(8,"-");
                }
                
                row["CustumerName"] = pwork.Data_6;
                row["ExpressCode"] = pwork.Data_7;
                row["MessageForBuyer"] = null;//pwork.Data_9;
                row["SendShopname"] = "Shop: "+pwork.Platform+" + "+pwork.Shopname;
                row["packgecode"] = pwork.Data_3;
                dt.Rows.Add(row);
                MyPrinter printer = new MyPrinter();
                printer.ZPLPrintDeviceLabel(dt,true, 1);
                dt.Clear();
                pwork = null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// 直接打印,是否显示打印对话框
        /// </summary>
        /// <param name="p_ShowPrintDialog"></param>
        public void PrintA4(bool p_ShowPrintDialog)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                //1.获取打印机插件是否设置了打印拣选单的面单
                NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "Passport");
                bool isexit = xmlfile.IsExitinnertextOfNode("Yamato", "page");
                if (isexit)
                {
                    //设置打印机的名称
                    pd.PrinterSettings.PrinterName = xmlfile.GetinnertextOfPrintName("Yamato", "page");
                }
                else
                {
                    throw new Exception("没有配置拣货单面单！请先配置！");
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
            Point zipcode = new Point(70, 45);
            Point address = new Point(70, 60);
            Point SKUS = new Point(70, 110);
            Point phone = new Point(70, 140);
            Point name = new Point(70, 155);
            Point barcode = new Point(70, 173);
            Point barcodenub = new Point(135, 216);
            Point yamadre1 = new Point(200, 240);
            Point yamadre2 = new Point(58, 250);
            string yamatoaddr1 = "西新宿3丁目センター\r\n";
            string yamatoaddr2 = "この荷物が郵便物ではありません　036-292\r\nお問い合せ先　フリーダイヤル　0120-11-8010";
            for (int j = 1; j <=mylist.Count; j++)
            {
                switch (j)
                { 
                    case 1:
                        //客户信息
                        e.Graphics.DrawString("〒" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, zipcode);
                        e.Graphics.DrawString(pwork.Data_2, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, address);
                        e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, SKUS);
                        e.Graphics.DrawString(pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 8f), System.Drawing.Brushes.Black, phone);
                        e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, name);

                        //打印条码  201510270066
                        Barcode bar = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
                        var Image = bar.Encode(TYPE.Codabar, "a" + pwork.Data_7 + "a", 280, 40);       //生成条码图像

                        e.Graphics.DrawImage(Image, barcode);
                        e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, barcodenub);
                        e.Graphics.DrawString(yamatoaddr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, yamadre1);
                        e.Graphics.DrawString(yamatoaddr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, yamadre2);
                        break;
                    case 2:
                        //客户信息
                        e.Graphics.DrawString("〒" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(zipcode.X+350,zipcode.Y));
                        e.Graphics.DrawString(pwork.Data_2, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(address.X + 350, address.Y));
                        e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new Point(SKUS.X + 350, SKUS.Y));
                        e.Graphics.DrawString(pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 8f), System.Drawing.Brushes.Black, new Point(phone.X + 350, phone.Y));
                        e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(name.X + 350, name.Y));

                        //打印条码  201510270066
                        Barcode bar2 = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
                        var Image2 = bar2.Encode(TYPE.Codabar, "a" + pwork.Data_7 + "a", 280, 40);       //生成条码图像

                        e.Graphics.DrawImage(Image2, new Point(barcode.X + 350, barcode.Y));
                        e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(barcodenub.X + 350, barcodenub.Y));
                        e.Graphics.DrawString(yamatoaddr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre1.X + 350, yamadre1.Y));
                        e.Graphics.DrawString(yamatoaddr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre2.X + 350, yamadre2.Y));
                        break;
                    case 3:
                        //客户信息
                        e.Graphics.DrawString("〒" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(zipcode.X, zipcode.Y+282));
                        e.Graphics.DrawString(pwork.Data_2, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(address.X, address.Y + 282));
                        e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new Point(SKUS.X, SKUS.Y + 282));
                        e.Graphics.DrawString(pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 8f), System.Drawing.Brushes.Black, new Point(phone.X, phone.Y + 282));
                        e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(name.X, name.Y + 282));

                        //打印条码  201510270066
                        Barcode bar3 = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
                        var Image3 = bar3.Encode(TYPE.Codabar, "a" + pwork.Data_7 + "a", 280, 40);       //生成条码图像

                        e.Graphics.DrawImage(Image3, new Point(barcode.X, barcode.Y + 282));
                        e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(barcodenub.X, barcodenub.Y + 282));
                        e.Graphics.DrawString(yamatoaddr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre1.X, yamadre1.Y + 282));
                        e.Graphics.DrawString(yamatoaddr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre2.X, yamadre2.Y + 282));
                        break;
                    case 4:
                        //客户信息
                        e.Graphics.DrawString("〒" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(zipcode.X + 350, zipcode.Y + 282));
                        e.Graphics.DrawString(pwork.Data_2, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(address.X + 350, address.Y + 282));
                        e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new Point(SKUS.X + 350, SKUS.Y + 282));
                        e.Graphics.DrawString(pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 8f), System.Drawing.Brushes.Black, new Point(phone.X + 350, phone.Y + 282));
                        e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(name.X + 350, name.Y + 282));

                        //打印条码  201510270066
                        Barcode bar4 = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
                        var Image4 = bar4.Encode(TYPE.Codabar, "a" + pwork.Data_7 + "a", 280, 40);       //生成条码图像

                        e.Graphics.DrawImage(Image4, new Point(barcode.X + 350, barcode.Y + 282));
                        e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(barcodenub.X + 350, barcodenub.Y + 282));
                        e.Graphics.DrawString(yamatoaddr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre1.X + 350, yamadre1.Y + 282));
                        e.Graphics.DrawString(yamatoaddr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre2.X + 350, yamadre2.Y + 282));
                        break;
                    case 5:
                        //客户信息
                        e.Graphics.DrawString("〒" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(zipcode.X , zipcode.Y + 564));
                        e.Graphics.DrawString(pwork.Data_2, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(address.X, address.Y + 564));
                        e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new Point(SKUS.X, SKUS.Y + 564));
                        e.Graphics.DrawString(pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 8f), System.Drawing.Brushes.Black, new Point(phone.X, phone.Y + 564));
                        e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(name.X, name.Y + 564));

                        //打印条码  201510270066
                        Barcode bar5 = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
                        var Image5 = bar5.Encode(TYPE.Codabar, "a" + pwork.Data_7 + "a", 280, 40);       //生成条码图像

                        e.Graphics.DrawImage(Image5, new Point(barcode.X, barcode.Y + 564));
                        e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(barcodenub.X, barcodenub.Y + 564));
                        e.Graphics.DrawString(yamatoaddr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre1.X, yamadre1.Y + 564));
                        e.Graphics.DrawString(yamatoaddr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre2.X, yamadre2.Y + 564));
                        break;
                    case 6:
                        //客户信息
                        e.Graphics.DrawString("〒" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(zipcode.X + 350, zipcode.Y + 564));
                        e.Graphics.DrawString(pwork.Data_2, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(address.X + 350, address.Y + 564));
                        e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new Point(SKUS.X + 350, SKUS.Y + 564));
                        e.Graphics.DrawString(pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 8f), System.Drawing.Brushes.Black, new Point(phone.X + 350, phone.Y + 564));
                        e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(name.X + 350, name.Y + 564));

                        //打印条码  201510270066
                        Barcode bar6 = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
                        var Image6 = bar6.Encode(TYPE.Codabar, "a" + pwork.Data_7 + "a", 280, 40);       //生成条码图像

                        e.Graphics.DrawImage(Image6, new Point(barcode.X + 350, barcode.Y + 564));
                        e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(barcodenub.X + 350, barcodenub.Y + 564));
                        e.Graphics.DrawString(yamatoaddr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre1.X + 350, yamadre1.Y + 564));
                        e.Graphics.DrawString(yamatoaddr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre2.X + 350, yamadre2.Y + 564));
                        break;
                    case 7:
                        //客户信息
                        e.Graphics.DrawString("〒" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(zipcode.X, zipcode.Y + 846));
                        e.Graphics.DrawString(pwork.Data_2, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(address.X, address.Y + 846));
                        e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new Point(SKUS.X, SKUS.Y + 846));
                        e.Graphics.DrawString(pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 8f), System.Drawing.Brushes.Black, new Point(phone.X, phone.Y + 846));
                        e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(name.X, name.Y + 846));

                        //打印条码  201510270066
                        Barcode bar7 = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
                        var Image7 = bar7.Encode(TYPE.Codabar, "a" + pwork.Data_7 + "a", 280, 40);       //生成条码图像

                        e.Graphics.DrawImage(Image7, new Point(barcode.X, barcode.Y + 846));
                        e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(barcodenub.X, barcodenub.Y + 846));
                        e.Graphics.DrawString(yamatoaddr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre1.X, yamadre1.Y + 846));
                        e.Graphics.DrawString(yamatoaddr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre2.X, yamadre2.Y + 846));
                        break;
                    case 8:
                        //客户信息
                        e.Graphics.DrawString("〒" + pwork.Data_1.Insert(3, "-"), new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(zipcode.X + 350, zipcode.Y + 846));
                        e.Graphics.DrawString(pwork.Data_2, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(address.X + 350, address.Y + 846));
                        e.Graphics.DrawString(pwork.Data_4, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new Point(SKUS.X + 350, SKUS.Y + 846));
                        e.Graphics.DrawString(pwork.Data_5.Insert(2, "-").Insert(6, "-"), new System.Drawing.Font("黑体", 8f), System.Drawing.Brushes.Black, new Point(phone.X + 350, phone.Y + 846));
                        e.Graphics.DrawString(pwork.Data_6, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(name.X + 350, name.Y + 846));

                        //打印条码  201510270066
                        Barcode bar8 = new Barcode();                               //new Barcode(tiaoma, TYPE.CODE128
                        var Image8 = bar8.Encode(TYPE.Codabar, "a" + pwork.Data_7 + "a", 280, 40);       //生成条码图像

                        e.Graphics.DrawImage(Image8, new Point(barcode.X + 350, barcode.Y + 846));
                        e.Graphics.DrawString("a" + pwork.Data_7 + "a", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new Point(barcodenub.X + 350, barcodenub.Y + 846));
                        e.Graphics.DrawString(yamatoaddr1, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre1.X + 350, yamadre1.Y + 846));
                        e.Graphics.DrawString(yamatoaddr2, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new Point(yamadre2.X + 350, yamadre2.Y + 846));
                        break;
                }      
            }
          
        }
    }
}
