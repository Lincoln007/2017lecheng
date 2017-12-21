using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZPLPrinter;
//---------------------



using System.Runtime.InteropServices;
using System.IO;


namespace PrintToolOfLC.Helper
{
    using System = global::System;
    //using ThoughtWorks.QRCode.Codec;
    using System.Drawing;
    using System.Drawing.Printing;
using XMLClass;
        
    
    public class MyPrinter
    {
        #region DLL声明
        //ZPL
        [DllImport(@"FNTHEX32.DLL", CharSet = CharSet.Ansi)]
        public static extern int GETFONTHEX(
                          string chnstr,
                          string fontname,
                          string chnname,
                          int orient,
                          int height,
                          int width,
                          int bold,
                          int italic,
                          StringBuilder param1);
        //EPL
        [DllImport(@"Eltronp.dll", CharSet = CharSet.Ansi)]
        public static extern int PrintHZ(int Lpt, //0：LPT1，1 LPT2
                                         int x,
                                         int y,
                                         string HZBuf,
                                         string FontName,
                                         int FontSize,
                                         int FontStyle);
        #endregion


           //1.获取打印机插件是否设置了打印拣选单的面单
                NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "Passport");
               
        /// <summary>
        /// 将字符汉字转化为十六进制的代码
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textId"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public string TextToHex(string text, string textId, int height)
        {
            StringBuilder hexBuilder = new StringBuilder(4 * 1024);
            int subStrCount = 0;
            subStrCount = GETFONTHEX(text, "Arial", textId, 0, height, 0, 1, 0, hexBuilder); //返回长度
            return hexBuilder.ToString().Substring(0, subStrCount);
        }
        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="printerName"></param>
        /// <param name="labels"></param>
        private void ZPLPrintLabelsWithHexText(string printerName, MyLabel[] labels)
        {
            string labelIdCmd = string.Empty;
            string labelContentCmd = string.Empty;
            foreach (MyLabel label in labels)
            {
                labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                labelContentCmd += label.Text;
            }
            string content = labelContentCmd
                + "^XA^LH0,0^PR2,2^MD20^FO0,0"
                + labelIdCmd
                + "^PQ1,0,1,Y^XZ";
            RawPrinterHelper.SendStringToPrinter(printerName, content);
        }
        /// <summary>
        /// 打印标签正式
        /// </summary>
        /// <param name="printerName"></param>
        /// <param name="labels"></param>
        /// <param name="height"></param>
        private void ZPLPrintLabels(string printerName, MyLabel[] labels, int height)
        {
            string labelIdCmd = string.Empty;
            string labelContentCmd = string.Empty;
            string headTitle = string.Empty;
            string barcodeNo = string.Empty;
            foreach (MyLabel label in labels)
            {
                
               
                if (label.Id == "ExpressCode")//快递单号
                {
                    barcodeNo += "^FDMA,YF"+label.Text+"^FS";
                }
                else if (label.Id == "OurZipCode")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "Ourtelephone")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "MessageForBuyer")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "MessageForBuyer1")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "MResponsible")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "Phone")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "SKU")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if(label.Id == "YamtoCompany1")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "YamtoCompany2")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "YamtoCompany3")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "YamtoCompany4")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "packgecode2")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 60);
                }
                else if (label.Id == "packgecode3")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 60);
                }
                else
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, height);
                }

            }
            #region 打印具有格式的小票
            string content = labelContentCmd  //画图代码
                + "^XA^LH0,0^PR2,2^MD20^FO0,0"
                //+ headTitle
                //+ "^FO20,80^GB560,0,3^FS"
                + labelIdCmd  //Label位置信息
                + "^FT180,420^BY3,2.4,50^BKN,N,80,Y,N,a,a"  //BKN,N,150,Y,N,A,A  代表条码
                + barcodeNo
                + "^PQ1,0,1,Y^XZ";
            bool isprintOK=RawPrinterHelper.SendStringToPrinter(printerName, content);
            if(!isprintOK)
            {
                throw new Exception("打印失败！");
            }
            
            #endregion
        }


        /// <summary>
        /// 打印标签正式
        /// </summary>
        /// <param name="printerName"></param>
        /// <param name="labels"></param>
        /// <param name="height"></param>
        private void ZPLPrintLabels2(string printerName, MyLabel[] labels, int height)
        {
            string labelIdCmd = string.Empty;
            string labelContentCmd = string.Empty;
            string headTitle = string.Empty;
            string barcodeNo = string.Empty;
            foreach (MyLabel label in labels)
            {


                if (label.Id == "packgecode")//快递单号
                {
                    barcodeNo += "^FDMA,YF" + label.Text + "^FS";
                }
                else if (label.Id == "pack" || label.Id == "pack2")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 45);
                }
                else if (label.Id == "time")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "number")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, height);
                }
                else if (label.Id == "shopname")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, height);
                }

            }
            #region 打印具有格式的小票
            string content = labelContentCmd  //画图代码
                + "^XA^LH0,0^PR2,2^MD20^FO0,0"
                //+ headTitle
                //+ "^FO20,80^GB560,0,3^FS"
                + labelIdCmd  //Label位置信息
                + "^FT85,130^BY2,2.0,50^BKN,N,80,Y,N,a,a"  //BKN,N,150,Y,N,A,A  代表条码
                + barcodeNo
                + "^PQ1,0,1,Y^XZ";
            bool isprintOK = RawPrinterHelper.SendStringToPrinter(printerName, content);
            if (!isprintOK)
            {
                throw new Exception("打印失败！");
            }

            #endregion
        }


        /// <summary>
        /// 打印标签正式(打印Umail)
        /// </summary>
        /// <param name="printerName"></param>
        /// <param name="labels"></param>
        /// <param name="height"></param>
        private void ZPLPrintLabels4(string printerName, MyLabel[] labels, int height)
        {
            string labelIdCmd = string.Empty;
            string labelContentCmd = string.Empty;
            string headTitle = string.Empty;
            string barcodeNo = string.Empty;
            foreach (MyLabel label in labels)
            {
                if (label.Id == "ExpressCode")//快递单号
                {
                    barcodeNo += "^FDMA,YF" + label.Text + "^FS";
                }
                else if (label.Id == "packgeinfo")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "Companyaddr")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "Companyaddr2")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "shopname")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "shoppost")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "shopphone")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "SKU")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "image1")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "packgecode" || label.Id == "packgecode2")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 60);
                }
                else
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 35);
                }



            }
            #region 打印具有格式的小票
            string content = labelContentCmd  //画图代码
                + "^XA^LH0,0^PR2,2^MD20^FO0,0"
                //+ headTitle
                //+ "^FO20,80^GB560,0,3^FS"
                + labelIdCmd  //Label位置信息
                //+ "^FT180,420^BY3,2.4,50^BKN,N,80,Y,N,a,a"  //BKN,N,150,Y,N,A,A  代表条码
                //+ barcodeNo
                + "^FO80,120 ^GB145,150,3^FS"                                    //^GC150,5,B^FS 圆
                + "^FO80,150 ^GB145,2,3^FS"
                + "^FO80,200 ^GB145,2,3^FS"
                //+ "^BY6,1.5,30"
                //+ "^FT120,530^BY3,2.4,50^BKN,N,80,Y,N,a,a"  //BKN,N,150,Y,N,A,A  代表条码
                //+ barcodeNo
                + "^PQ1,0,1,Y^XZ";
            bool isprintOK = RawPrinterHelper.SendStringToPrinter(printerName, content);
            if (!isprintOK)
            {
                throw new Exception("打印失败！");
            }

            #endregion
        }

        /// <summary>
        /// 打印标签正式(打印Upacket)
        /// </summary>
        /// <param name="printerName"></param>
        /// <param name="labels"></param>
        /// <param name="height"></param>
        private void ZPLPrintLabels3(string printerName, MyLabel[] labels, int height)
        {
            string labelIdCmd = string.Empty;
            string labelContentCmd = string.Empty;
            string headTitle = string.Empty;
            string barcodeNo = string.Empty;
            foreach (MyLabel label in labels)
            {
                if (label.Id == "ExpressCode")//快递单号
                {
                    barcodeNo += "^FDMA,YF" + label.Text + "^FS";
                }
                else if (label.Id == "packgeinfo")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "Companyaddr")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "Companyaddr2")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "shopname")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "shoppost")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "shopphone")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "SKU")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 25);
                }
                else if (label.Id == "image1")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 30);
                }
                else if (label.Id == "packgecode" || label.Id == "packgecode2")
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 60);
                }
                else
                {
                    labelIdCmd += "^FT" + label.XPos.ToString() + "," + label.YPos.ToString() + "^XG" + label.Id + ",1,1^FS";
                    labelContentCmd += TextToHex(label.Text, label.Id, 35);
                }
            
            

            }
            #region 打印具有格式的小票
            string content = labelContentCmd  //画图代码
                + "^XA^LH0,0^PR2,2^MD20^FO0,0"
                //+ headTitle
                //+ "^FO20,80^GB560,0,3^FS"
                + labelIdCmd  //Label位置信息
                //+ "^FT180,420^BY3,2.4,50^BKN,N,80,Y,N,a,a"  //BKN,N,150,Y,N,A,A  代表条码
                //+ barcodeNo
                + "^FO80,120 ^GB145,150,3^FS"                                    //^GC150,5,B^FS 圆
                + "^FO80,150 ^GB145,2,3^FS"
                + "^FO80,200 ^GB145,2,3^FS"
                + "^BY6,1.5,25"  //原先的是30
                + "^FT140,380^BY3,2.4,30^BKN,N,80,Y,N,a,a"  //BKN,N,150,Y,N,A,A  代表条码
                + "^FD" + barcodeNo + "^FS"
                + "^PQ1,0,1,Y^XZ";
            bool isprintOK = RawPrinterHelper.SendStringToPrinter(printerName, content);
            if (!isprintOK)
            {
                throw new Exception("打印失败！");
            }

            #endregion
        }
        /// <summary>
        /// 实际调用 的打印标签函数(打印yamato)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isRequireTextToHex"></param>
        /// <param name="copies"></param>
        public void ZPLPrintDeviceLabel(DataTable dt, bool isRequireTextToHex, int copies)
        {
            string printname=string.Empty;
            bool isexit = xmlfile.IsExitinnertextOfNode("Yamato", "page");
            if (isexit)
            {
                //设置打印机的名称
                printname = xmlfile.GetinnertextOfPrintName("Yamato", "page");
            }
            else
            {
                throw new Exception("没有配置Yamato面单！请先配置！");
            }
            MyLabel label = null;
            for (int i = 0; i < copies; i++)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<MyLabel> labelList = new List<MyLabel>();
                   
                    #region 打印标签数据
                    label = new MyLabel();
                    label.Id = "MessageForBuyer";
                    if (!string.IsNullOrEmpty(row["MessageForBuyer"].ToString()))
                    {
                        label.Text = row["MessageForBuyer"].ToString().Substring(0, 22);
                    }
                    else
                    {
                        label.Text = "";
                    }
                    label.XPos = 130;
                    label.YPos = 50;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "MessageForBuyer1";
                    if (!string.IsNullOrEmpty(row["MessageForBuyer"].ToString()))
                    {
                        label.Text = row["MessageForBuyer"].ToString().Substring(22);
                    }
                    else
                    {
                        label.Text = "";
                    }
                    label.XPos = 130;
                    label.YPos = 70;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "ZIPCode";
                    label.Text = row["ZIPCode"].ToString();
                    label.XPos = 130;
                    label.YPos = 100;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address1";
                    label.Text = row["address1"].ToString();
                    label.XPos = 130;
                    label.YPos = 130;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address2";
                    label.Text = row["address2"].ToString();
                    label.XPos = 130;
                    label.YPos = 160;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address3";
                    label.Text = row["address3"].ToString();
                    label.XPos = 130;
                    label.YPos = 190;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "SKU";
                    label.Text = row["SKU"].ToString();
                    label.XPos = 130;
                    label.YPos = 220;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "YamtoCompany1";
                    label.Text = row["YamtoCompany1"].ToString();
                    label.XPos = 130;
                    label.YPos = 500;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "YamtoCompany2";
                    label.Text = row["YamtoCompany2"].ToString();
                    label.XPos = 380;
                    label.YPos = 500;  //500
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "YamtoCompany3";
                    label.Text = row["YamtoCompany3"].ToString();
                    label.XPos = 130;
                    label.YPos = 520;  //520
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "YamtoCompany4";
                    label.Text = row["YamtoCompany4"].ToString();
                    label.XPos = 130;
                    label.YPos = 540;
                    labelList.Add(label);


                    label = new MyLabel();
                    label.Id = "packgecode";
                    label.Text = row["packgecode"].ToString();
                    label.XPos = 130;
                    label.YPos = 610;
                    labelList.Add(label);


                    label = new MyLabel();
                    label.Id = "SendShopname";
                    if (row["SendShopname"].ToString().Contains("dad_store"))
                    {
                        label.Text = "D-store";
                    }
                    else
                    {
                        label.Text = row["SendShopname"].ToString();
                    }
                    label.XPos = 130;
                    label.YPos = 570;
                    labelList.Add(label);
//-------------------------------------20161109增加代码----------------------------------------
                    if (row["SendShopname"].ToString().Contains("Anneybaby"))
                    {

                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 175-0082";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 03-4400-1323";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:東京都板橋区高島平６丁目２番３号３階";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }
                    else if (row["SendShopname"].ToString().Contains("AnneyBaby1"))
                    {
                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 175-0082";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 03-4400-1323";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:東京都板橋区高島平６丁目２番３号３階";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }
                    else if (row["SendShopname"].ToString().Contains("AnneyBaby2"))
                    {
                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 175-0082";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 03-4400-1323";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:東京都板橋区高島平６丁目２番３号３階";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }
                    else if (row["SendShopname"].ToString().Contains("Lovepocket"))
                    {
                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 335-0016";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 050-5803-2394";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:埼玉県戸田市下前1-3-4-2F";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }
                    else if (row["SendShopname"].ToString().Contains("kjshopper"))
                    {
                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 335-0016";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 050-5803-2394";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:埼玉県戸田市下前1-3-4-2F";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }
                    else if (row["SendShopname"].ToString().Contains("abcmart_king"))
                    {
                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 332-0027";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 050-3747-6896";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:埼玉県川口市緑町9-35";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }
                    else if (row["SendShopname"].ToString().Contains("jpdress"))
                    {
                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 332-0027";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 050-3747-6896";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:埼玉県川口市緑町9-35";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }
                    else if (row["SendShopname"].ToString().Contains("kinkastar"))
                    {
                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 332-0027";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 03-4400-1323";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:埼玉県川口市緑町9-35";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }
                    else //if (!(row["SendShopname"].ToString().Contains("Anney")))
                    {
                        label = new MyLabel();
                        label.Id = "OurZipCode";
                        label.Text = "〒: 332-0027";
                        label.XPos = 130;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "Ourtelephone";
                        label.Text = "TEL: 03-4400-1323";
                        label.XPos = 350;
                        label.YPos = 650;
                        labelList.Add(label);

                        label = new MyLabel();
                        label.Id = "OurCompannyAddress";
                        label.Text = "From:東京都板橋区高島平６丁目２番３号３階";
                        label.XPos = 130;
                        label.YPos = 680;
                        labelList.Add(label);
                    }

 //------------------------------------------------------------------------------------------------------
                    label = new MyLabel();
                    label.Id = "packgecode2";
                    if (row["packgecode"].ToString()=="packgecode")
                    {
                        label.Text = "Packge";
                    }else
                    {
                        label.Text = row["packgecode"].ToString().Substring(8,4);
                    }
                    
                    label.XPos = 550;
                    label.YPos = 610;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "packgecode3";
                    if (row["packgecode"].ToString() == "packgecode")
                    {
                        label.Text = "";
                    }
                    else
                    {
                        label.Text = row["packgecode"].ToString().Substring(6, 2);
                    }

                    label.XPos = 460;
                    label.YPos = 610;
                    labelList.Add(label);


                    label = new MyLabel();
                    label.Id = "Phone";
                    label.Text = row["Phone"].ToString();
                    label.XPos = 130;
                    label.YPos = 290;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "CustumerName";
                    label.Text = row["CustumerName"].ToString();
                    label.XPos = 130;
                    label.YPos = 330;
                    labelList.Add(label); 

                    label = new MyLabel();
                    label.Id = "ExpressCode";
                    label.Text = row["ExpressCode"].ToString();
                    label.XPos = 230;
                    label.YPos = 280;
                    labelList.Add(label); 
                    #endregion
                  

                    if (isRequireTextToHex)
                    {
                        ZPLPrintLabels(printname, labelList.ToArray(), 35);
                    }
                    else
                    {
                        ZPLPrintLabelsWithHexText(printname, labelList.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 实际调用 的打印标签函数(打印包裹号)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isRequireTextToHex"></param>
        /// <param name="copies"></param>
        public void ZPLPrintDeviceLabel2(DataTable dt, bool isRequireTextToHex, int copies)
        {
            string printname = string.Empty;
            bool isexit = xmlfile.IsExitinnertextOfNode("包裹号", "page");
            if (isexit)
            {
                //设置打印机的名称
                printname = xmlfile.GetinnertextOfPrintName("包裹号", "page");
            }
            else
            {
                throw new Exception("没有配置包裹号面单！请先配置！");
            }
            MyLabel label = null;
            for (int i = 0; i < copies; i++)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<MyLabel> labelList = new List<MyLabel>();
                   
                    #region 打印标签数据
                    label = new MyLabel();
                    label.Id = "pack";
                    label.Text = row["packgecode"].ToString().Substring(row["packgecode"].ToString().Length-4,4);
                    label.XPos = 270;  //270
                    label.YPos = 40;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "pack2";
                    label.Text = row["packgecode"].ToString().Substring(row["packgecode"].ToString().Length - 6, 2);
                    label.XPos = 200; //400
                    label.YPos = 40;
                    labelList.Add(label);


                    label = new MyLabel();
                    label.Id = "packgecode";
                    label.Text = row["packgecode"].ToString();
                    label.XPos = 230;
                    label.YPos = 130;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "number";
                    label.Text = row["number"].ToString();
                    label.XPos = 80; //280;
                    label.YPos = 40;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "time";
                    label.Text = "LastTime: "+row["time"].ToString();  //2016.3.4修改;
                    label.XPos = 80;
                    label.YPos = 190;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "shopname";
                    label.Text = row["shopname"].ToString();
                    label.XPos = 450;
                    label.YPos = 190;
                    labelList.Add(label);
                    #endregion

                    if (isRequireTextToHex)
                    {
                        ZPLPrintLabels2(printname, labelList.ToArray(), 35);
                    }
                    else
                    {
                        ZPLPrintLabelsWithHexText(printname, labelList.ToArray());
                    }
                }
            }
        }


        /// <summary>
        /// 实际调用 的打印标签函数(打印Umail)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isRequireTextToHex"></param>
        /// <param name="copies"></param>
        public void ZPLPrintDeviceLabel3(DataTable dt, bool isRequireTextToHex, int copies)
        {
            string printname = string.Empty;
            bool isexit = xmlfile.IsExitinnertextOfNode("Umail", "page");
            if (isexit)
            {
                //设置打印机的名称
                printname = xmlfile.GetinnertextOfPrintName("Umail", "page");
            }
            else
            {
                throw new Exception("没有配置Umail面单！请先配置！");
            }
            MyLabel label = null;
            for (int i = 0; i < copies; i++)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<MyLabel> labelList = new List<MyLabel>();

                    #region 打印标签数据
                    label = new MyLabel();
                    label.Id = "MessageForBuyer";
                    if (!string.IsNullOrEmpty(row["MessageForBuyer"].ToString()))
                    {
                        label.Text = row["MessageForBuyer"].ToString().Substring(0, 22);
                    }
                    else
                    {
                        label.Text = "";
                    }
                    label.XPos = 80;
                    label.YPos = 50;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "MessageForBuyer1";
                    if (!string.IsNullOrEmpty(row["MessageForBuyer"].ToString()))
                    {
                        label.Text = row["MessageForBuyer"].ToString().Substring(22);
                    }
                    else
                    {
                        label.Text = "";
                    }
                    label.XPos = 80;
                    label.YPos = 75;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "packgeinfo";
                    label.Text = row["packgeinfo"].ToString();
                    label.XPos = 80;
                    label.YPos = 100;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "ZIPCode";
                    label.Text = row["ZIPCode"].ToString();
                    label.XPos = 250;
                    label.YPos = 140;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "CustumerName";
                    label.Text = row["CustumerName"].ToString();
                    label.XPos = 250;
                    label.YPos = 300;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "Phone";
                    label.Text = row["Phone"].ToString();
                    label.XPos = 250;
                    label.YPos = 340;
                    labelList.Add(label);

                    //label = new MyLabel();
                    //label.Id = "headaddr";
                    //label.Text = "Address:";
                    //label.XPos = 250;
                    //label.YPos = 340;
                    //labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address1";
                    label.Text = row["address1"].ToString();
                    label.XPos = 250;
                    label.YPos = 180;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address2";
                    label.Text = row["address2"].ToString();
                    label.XPos = 250;
                    label.YPos = 220;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address3";
                    label.Text = row["address3"].ToString();
                    label.XPos = 250;
                    label.YPos = 260;
                    labelList.Add(label);

                    //画线
                    label = new MyLabel();
                    label.Id = "line";
                    label.Text = "-------------------------------------------------------------------------";
                    label.XPos = 30;
                    label.YPos = 430;
                    labelList.Add(label);
                    //-------------------------20160126周二添加的------------------------------------------------------
                    //label = new MyLabel();
                    //label.Id = "newline";
                    //label.Text = "------";
                    //label.XPos = 400;
                    //label.YPos = 100;
                    //labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "image1";
                    label.Text = "料金後納";
                    label.XPos = 100;
                    label.YPos = 190;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "image2";
                    label.Text = "ゆうメール";
                    label.XPos = 70;
                    label.YPos = 310;
                    labelList.Add(label);
                    //----------------------------------------------------------------------------------
                    label = new MyLabel();
                    label.Id = "shoppost";
                    label.Text = row["shoppost"].ToString();
                    label.XPos = 80;
                    label.YPos = 480;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "Companyaddr";
                    label.Text = row["Companyaddr"].ToString();
                    label.XPos = 80;
                    label.YPos = 510;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "Companyaddr2";
                    label.Text = row["Companyaddr2"].ToString();
                    label.XPos = 200;
                    label.YPos = 540;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "shopname";
                    label.Text = row["shopname"].ToString();
                    label.XPos = 80;
                    label.YPos = 540;
                    labelList.Add(label);



                    label = new MyLabel();
                    label.Id = "shopphone";
                    label.Text = row["shopphone"].ToString();
                    label.XPos = 80;
                    label.YPos = 570;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "SendShopname";
                    label.Text = row["SendShopname"].ToString();
                    label.XPos = 80;
                    label.YPos = 770;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "packgecode";
                    if (row["packgecode"].ToString() == "packgecode")
                    {
                        label.Text = "Pack";
                    }
                    else
                    {
                        label.Text = row["packgecode"].ToString().Substring(8, 4);
                    }
                    label.XPos = 570;
                    label.YPos = 780;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "packgecode2";
                    if (row["packgecode"].ToString() == "packgecode")
                    {
                        label.Text = "Pa";
                    }
                    else
                    {
                        label.Text = row["packgecode"].ToString().Substring(6, 2);
                    }
                    label.XPos = 480;
                    label.YPos = 780;
                    labelList.Add(label);


                    label = new MyLabel();
                    label.Id = "ExpressCode";
                    label.Text = row["ExpressCode"].ToString();
                    label.XPos = 120;
                    label.YPos = 680;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "SKU";
                    label.Text = row["SKU"].ToString();
                    label.XPos = 250;
                    label.YPos = 380;
                    labelList.Add(label);

                    //label = new MyLabel();
                    //label.Id = "ExpressCode";
                    //label.Text = row["ExpressCode"].ToString();
                    //label.XPos = 230;
                    //label.YPos = 280;
                    //labelList.Add(label);


                    #endregion


                    if (isRequireTextToHex)
                    {
                        ZPLPrintLabels4(printname, labelList.ToArray(), 35);
                        labelList.Clear();
                    }
                    else
                    {
                        ZPLPrintLabelsWithHexText(printname, labelList.ToArray());
                        labelList.Clear();
                    }
                }
            }
           
        }


        /// <summary>
        /// 实际调用 的打印标签函数(打印UPacket)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isRequireTextToHex"></param>
        /// <param name="copies"></param>
        public void ZPLPrintDeviceLabel5(DataTable dt, bool isRequireTextToHex, int copies)
        {
            string printname = string.Empty;
            bool isexit = xmlfile.IsExitinnertextOfNode("Upacket", "page");
            if (isexit)
            {
                //设置打印机的名称
                printname = xmlfile.GetinnertextOfPrintName("Upacket", "page");
            }
            else
            {
                throw new Exception("没有配置Upacket面单！请先配置！");
            }
            MyLabel label = null;
            for (int i = 0; i < copies; i++)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<MyLabel> labelList = new List<MyLabel>();

                    #region 打印标签数据
                    label = new MyLabel();
                    label.Id = "MessageForBuyer";
                    if (!string.IsNullOrEmpty(row["MessageForBuyer"].ToString()))
                    {
                        label.Text = row["MessageForBuyer"].ToString().Substring(0, 22);
                    }
                    else
                    {
                        label.Text = "";
                    }
                    label.XPos = 80;
                    label.YPos = 50;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "MessageForBuyer1";
                    if (!string.IsNullOrEmpty(row["MessageForBuyer"].ToString()))
                    {
                        label.Text = row["MessageForBuyer"].ToString().Substring(22);
                    }
                    else
                    {
                        label.Text = "";
                    }
                    label.XPos = 80;
                    label.YPos = 75;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "packgeinfo";
                    label.Text = row["packgeinfo"].ToString();
                    label.XPos = 80;
                    label.YPos = 40;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "ZIPCode";
                    label.Text = row["ZIPCode"].ToString();
                    label.XPos = 250;
                    label.YPos = 80;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "CustumerName";
                    label.Text = row["CustumerName"].ToString();
                    label.XPos = 250;
                    label.YPos = 220;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "Phone";
                    label.Text = row["Phone"].ToString();
                    label.XPos = 250;
                    label.YPos = 250;
                    labelList.Add(label);

                    //label = new MyLabel();
                    //label.Id = "headaddr";
                    //label.Text = "Address:";
                    //label.XPos = 250;
                    //label.YPos = 340;
                    //labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address1";
                    label.Text = row["address1"].ToString();
                    label.XPos = 250;
                    label.YPos = 120;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address2";
                    label.Text = row["address2"].ToString();
                    label.XPos = 250;
                    label.YPos = 160;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "address3";
                    label.Text = row["address3"].ToString();
                    label.XPos = 250;
                    label.YPos = 180;
                    labelList.Add(label);

                    //画线
                    label = new MyLabel();
                    label.Id = "line";
                    label.Text = "-------------------------------------------------------------------------";
                    label.XPos = 30;
                    label.YPos = 440;
                    labelList.Add(label);
                    //-------------------------20160126周二添加的------------------------------------------------------
                    //label = new MyLabel();
                    //label.Id = "newline";
                    //label.Text = "------";
                    //label.XPos = 400;
                    //label.YPos = 100;
                    //labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "image1";
                    label.Text = "料金後納";
                    label.XPos = 100;
                    label.YPos = 150;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "image2";
                    label.Text = "ゆうパケット";
                    label.XPos = 60;
                    label.YPos = 250;
                    labelList.Add(label);
                    
                    //-------------------------------------20161214增加代码----------------------------------------
                   
                    //---------------------------------------End----------------------------------------
                    label = new MyLabel();
                    label.Id = "shoppost";
                    label.Text = row["shoppost"].ToString();
                    label.XPos = 80;
                    label.YPos = 460;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "Companyaddr";
                    label.Text = row["Companyaddr"].ToString();
                    label.XPos = 80;
                    label.YPos = 490;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "Companyaddr2";
                    label.Text = row["Companyaddr2"].ToString();
                    label.XPos = 200;
                    label.YPos = 520;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "SendShopname";
                    label.Text = row["SendShopname"].ToString();
                    label.XPos = 80;
                    label.YPos = 680;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "shopname";
                    label.Text = row["shopname"].ToString();
                    label.XPos = 80;
                    label.YPos = 520;
                    labelList.Add(label);

                  

                    label = new MyLabel();
                    label.Id = "shopphone";
                    label.Text = row["shopphone"].ToString();
                    label.XPos = 80;
                    label.YPos = 550;
                    labelList.Add(label);

                   

                    label = new MyLabel();
                    label.Id = "packgecode";
                    if (row["packgecode"].ToString() == "packgecode")
                    {
                        label.Text = "Packge";
                    }
                    else
                    {
                        label.Text = row["packgecode"].ToString().Substring(8, 4);
                    }
                    label.XPos = 570;
                    label.YPos = 550;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "packgecode2";
                    if (row["packgecode"].ToString() == "packgecode")
                    {
                        label.Text = "Pack";
                    }
                    else
                    {
                        label.Text = row["packgecode"].ToString().Substring(6, 2);
                    }
                    label.XPos = 510;
                    label.YPos = 550;
                    labelList.Add(label);


                    label = new MyLabel();
                    label.Id = "ExpressCode";
                    label.Text = row["ExpressCode"].ToString();
                    label.XPos = 120;
                    label.YPos = 490;
                    labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "SKU";
                    label.Text = row["SKU"].ToString();
                    label.XPos = 250;
                    label.YPos = 280;
                    labelList.Add(label);

                    //label = new MyLabel();
                    //label.Id = "ExpressCode";
                    //label.Text = row["ExpressCode"].ToString();
                    //label.XPos = 230;
                    //label.YPos = 280;
                    //labelList.Add(label);

                  
                    #endregion


                    if (isRequireTextToHex)
                    {
                        ZPLPrintLabels3(printname, labelList.ToArray(), 35);
                        labelList.Clear();
                    }
                    else
                    {
                        ZPLPrintLabelsWithHexText(printname, labelList.ToArray());
                        labelList.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// 实际调用 的打印标签函数(打印转运单)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isRequireTextToHex"></param>
        /// <param name="copies"></param>
        public void ZPLPrintDeviceLabel4(DataTable dt, bool isRequireTextToHex, int copies)
        {
            string printname = string.Empty;
            bool isexit = xmlfile.IsExitinnertextOfNode("包裹号", "page");
            if (isexit)
            {
                //设置打印机的名称
                printname = xmlfile.GetinnertextOfPrintName("包裹号", "page");
            }
            else
            {
                throw new Exception("没有配置包裹号面单！请先配置！");
            }
            MyLabel label = null;
            for (int i = 0; i < copies; i++)
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<MyLabel> labelList = new List<MyLabel>();
                    label = new MyLabel();
                    #region 打印标签数据
                    //label.Id = "pack";
                    //label.Text = row["packgecode"].ToString().Substring(row["packgecode"].ToString().Length - 4, 4);
                    //label.XPos = 470;
                    //label.YPos = 40;
                    //labelList.Add(label);

                    label = new MyLabel();
                    label.Id = "packgecode";
                    label.Text = row["packgecode"].ToString();
                    label.XPos = 230;
                    label.YPos = 130;
                    labelList.Add(label);

                    //label = new MyLabel();
                    //label.Id = "number";
                    //label.Text = row["number"].ToString();
                    //label.XPos = 280;
                    //label.YPos = 40;
                    //labelList.Add(label);

                    //label = new MyLabel();
                    //label.Id = "time";
                    //label.Text = DateTime.Now.ToString();//row["time"].ToString();
                    //label.XPos = 330;
                    //label.YPos = 190;
                    //labelList.Add(label);
                    #endregion

                    if (isRequireTextToHex)
                    {
                        ZPLPrintLabels2(printname, labelList.ToArray(), 35);
                    }
                    else
                    {
                        ZPLPrintLabelsWithHexText(printname, labelList.ToArray());
                    }
                }
            }
        }
    }
}
