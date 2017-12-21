using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PrintToolOfLC.MyModel;
using XMLClass;

namespace PrintToolOfLC.Helper
{

    public class PrintUpacket
    {
        private string ourtelephone = "Tel:050-5803-2394";
        private string ourzipcode = "〒: 332-0027";   //  335-0016
        private string ouraddress = "埼玉県川口市緑町9-35";//"  埼玉県川口市緑町9-35株式会社インパクトフラッシュ（ジャパンドレス）  ---東京都新宿区西新宿3-5-12トーカン新宿第2キャステ";
        private string ouraddress2 = "インパクトフラッシュ（ジャパンドレス）"; //"ール1004号室";
        private string ourname = "株式会社ジャパンドレス";
        private string ourCode = "048-434-5580";    //打印ecohai的账号
        private string SKUS = string.Empty;
        public List<MyUmail> mylist = null;
        public MyUpacket pwork;       //打印信息存储类

        //通过构造函数传递进来
        public PrintUpacket(MyUpacket p, string a = "Tel: 050-5803-2394", string b = "【差出人·返還先】〒: 332-0027", string c = "埼玉県川口市緑町9-35", string d = "株)ジャパンドレス", string e = "0366674879250", string f = "", string g = "")
        {
            this.pwork = p;
            this.ourtelephone = a;
            this.ourzipcode = b;
            this.ouraddress = c;
            this.ouraddress2 = g;
            this.ourname = d;
            this.ourCode = e;
            this.SKUS = f;
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
                bool isexit = xmlfile.IsExitinnertextOfNode("Upacket", "page");
                if (isexit)
                {
                    //设置打印机的名称
                    //pd.PrinterSettings.PrinterName = xmlfile.GetinnertextOfPrintName("Umail", "page");
                }
                else
                {
                    throw new Exception("没有配置Upacket面单！请先配置！");
                }

                DataTable dt = new DataTable("table1");
                dt.Columns.Add(new DataColumn("ZIPCode", typeof(string)));
                dt.Columns.Add(new DataColumn("address1", typeof(string)));
                dt.Columns.Add(new DataColumn("address2", typeof(string)));
                dt.Columns.Add(new DataColumn("address3", typeof(string)));
                dt.Columns.Add(new DataColumn("SKU", typeof(string)));
                dt.Columns.Add(new DataColumn("Companyaddr", typeof(string))); //Companyaddr2
                dt.Columns.Add(new DataColumn("Companyaddr2", typeof(string)));
                dt.Columns.Add(new DataColumn("shopname", typeof(string)));
                dt.Columns.Add(new DataColumn("shoppost", typeof(string)));
                dt.Columns.Add(new DataColumn("shopphone", typeof(string)));
                dt.Columns.Add(new DataColumn("Phone", typeof(string)));
                dt.Columns.Add(new DataColumn("CustumerName", typeof(string)));
                dt.Columns.Add(new DataColumn("ExpressCode", typeof(string)));
                dt.Columns.Add(new DataColumn("packgeinfo", typeof(string)));
                dt.Columns.Add(new DataColumn("MessageForBuyer", typeof(string)));
                dt.Columns.Add(new DataColumn("SendShopname", typeof(string)));
                dt.Columns.Add(new DataColumn("packgecode", typeof(string)));
                DataRow row = dt.NewRow();
                //判断是否存在 - 符号
                if (pwork.Data_4.Contains("-"))
                {
                    row["ZIPCode"] = "〒：" + pwork.Data_4;   //"〒 160-0023";
                }
                else
                {
                    row["ZIPCode"] = "〒：" + pwork.Data_4.Insert(3, "-");
                }
                if (pwork.Data_7.Length > 30)
                {
                    row["address1"] = pwork.Data_7.Substring(0, 15); //"金华金帆街172金字火腿";
                    row["address2"] = pwork.Data_7.Substring(15, 15);//"乐诚网络";
                    row["address3"] = pwork.Data_7.Substring(30);
                }
                else if (pwork.Data_7.Length <= 30 && pwork.Data_7.Length >= 15)
                {
                    row["address1"] = pwork.Data_7.Substring(0, 15); //"金华金帆街172金字火腿";
                    row["address2"] = pwork.Data_7.Substring(15);//"乐诚网络";
                    row["address3"] = "";
                }
                else if (pwork.Data_7.Length < 15)
                {
                    row["address1"] = pwork.Data_7; //"金华金帆街172金字火腿";
                    row["address2"] = "";          //"乐诚网络";
                    row["address3"] = "";
                }

                row["SKU"] = "";
               //--------------2016.11.12修改----------------------------------------------------------------------------
                if (pwork.Shopname.Contains("Anneybaby"))
                {
                    row["shopname"] = "Name:グラチック合同会社  Annebaby";
                    row["shoppost"] = "【差出人·返還先】〒: 175-0082";
                    row["shopphone"] = "TEL: 03-4400-1323";  //,原先03-6904-1807
                    row["Companyaddr"] = "東京都板橋区高島平６丁目２番３号３階";
                    row["Companyaddr2"] = this.ouraddress2;
                }
                else if (pwork.Shopname.Contains("AnneyBaby1"))
                {
                    row["shopname"] = "Name:グラチック合同会社  Annebaby";
                    row["shoppost"] = "【差出人·返還先】〒: 175-0082";
                    row["shopphone"] = "TEL: 03-4400-1323";
                    row["Companyaddr"] = "東京都板橋区高島平6-2-3-3F";
                    row["Companyaddr2"] = this.ouraddress2;
                }
                else if (pwork.Shopname.Contains("AnneyBaby2"))
                {
                    row["shopname"] = "Name:グラチック合同会社  Annebaby";
                    row["shoppost"] = "【差出人·返還先】〒: 175-0082";
                    row["shopphone"] = "TEL: 03-4400-1323";
                    row["Companyaddr"] = "東京都板橋区高島平6-2-3-3F";
                    row["Companyaddr2"] = this.ouraddress2;
                }
                else if (pwork.Shopname.Contains("Lovepocket"))   //|| pwork.Shopname.Contains("usukmart")
                {
                    row["shopname"] = "Name:グラチック合同会社  Lovepocket";
                    row["shoppost"] = "【差出人·返還先】〒: 335-0016";
                    row["shopphone"] = "TEL: 050-5803-2394";
                    row["Companyaddr"] = "埼玉県戸田市下前1-3-4-2F";
                    row["Companyaddr2"] = this.ouraddress2;
                }
                else if (pwork.Shopname.Contains("kjshopper"))
                {
                    row["shopname"] = "Name:グラチック合同会社  the leader";
                    row["shoppost"] = "【差出人·返還先】〒: 335-0016";
                    row["shopphone"] = "TEL: 050-5803-2394";
                    row["Companyaddr"] = "埼玉県戸田市下前1-3-4-2F";
                    row["Companyaddr2"] = this.ouraddress2;
                }
                else if (pwork.Shopname.Contains("jpdress"))
                {
                    row["shopname"] = "Name:グラチック合同会社  LeaderJpdress";
                    row["shoppost"] = "【差出人·返還先】〒: 332-0027";
                    row["shopphone"] = "TEL: 050-3747-6896";
                    row["Companyaddr"] = "埼玉県川口市緑町9-35";
                    row["Companyaddr2"] = this.ouraddress2;
                }
                else if (pwork.Shopname.Contains("abcmart_king"))
                {
                    row["shopname"] = "Name:グラチック合同会社  modernstyle";
                    row["shoppost"] = "【差出人·返還先】〒: 332-0027";
                    row["shopphone"] = "TEL: 050-3747-6896";
                    row["Companyaddr"] = "埼玉県川口市緑町9-35";
                    row["Companyaddr2"] = this.ouraddress2;
                }

                else if (pwork.Shopname.Contains("kinkastar"))
                {
                    row["shopname"] = "Name:グラチック合同会社  grachic";
                    row["shoppost"] = "【差出人·返還先】〒: 332-0027";
                    row["shopphone"] = "TEL: 03-4400-1323";
                    row["Companyaddr"] = "埼玉県川口市緑町9-35";
                    row["Companyaddr2"] = this.ouraddress2;
                }
             
                else
                {
                    row["shopname"] = "Name:株式会社インパクトフラッシュ(ジャパンドレス)JPdress";
                    row["shoppost"] = this.ourzipcode;
                    row["shopphone"] = this.ourtelephone;
                    row["Companyaddr"] = this.ouraddress;
                    row["Companyaddr2"] = this.ouraddress2;
                }
               

                //判断是否存在 - 符号
                if (pwork.Data_6.Contains("-"))
                {
                    row["Phone"] =  pwork.Data_6;  //"〒 160-0023";
                }
                else
                {
                    row["Phone"] = pwork.Data_6.Insert(3, "-").Insert(8, "-");
                }

                row["CustumerName"] =  pwork.Data_5;
                row["ExpressCode"] = pwork.Data_10 ; //快递单号  packgeinfo
                row["packgeinfo"] = pwork.Data_1;
                row["MessageForBuyer"] = null;//pwork.Data_9;
                row["SKU"] = pwork.Data_8;
                row["SendShopname"] = "Shop: " + pwork.Platform + " + " + pwork.Shopname;
                row["packgecode"] = pwork.Data_3;
                dt.Rows.Add(row);
                MyPrinter printer = new MyPrinter();
                printer.ZPLPrintDeviceLabel5(dt, true, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

