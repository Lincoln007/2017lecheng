using Printer.CommHelper;
using Printer.DBmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Printer.Service
{
    public class PrintUpacket
    {
        public List<MyUpacket> mylist = null;
        public MyUpacket pwork;       //打印信息存储类

        //通过构造函数传递进来
        public PrintUpacket(MyUpacket p)
        {
            pwork = p;
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
                if (pwork.data_4.Contains("-"))
                {
                    row["ZIPCode"] = "〒：" + pwork.data_4;   //"〒 160-0023";
                }
                else
                {
                    row["ZIPCode"] = "〒：" + pwork.data_4.Insert(3, "-");
                }
                if (pwork.data_5.Length > 30)
                {
                    row["address1"] = pwork.data_5.Substring(0, 15); //"金华金帆街172金字火腿";
                    row["address2"] = pwork.data_5.Substring(15, 15);//"乐诚网络";
                    row["address3"] = pwork.data_5.Substring(30);
                }
                else if (pwork.data_5.Length <= 30 && pwork.data_5.Length >= 15)
                {
                    row["address1"] = pwork.data_5.Substring(0, 15); //"金华金帆街172金字火腿";
                    row["address2"] = pwork.data_5.Substring(15);//"乐诚网络";
                    row["address3"] = "";
                }
                else if (pwork.data_5.Length < 15)
                {
                    row["address1"] = pwork.data_5; //"金华金帆街172金字火腿";
                    row["address2"] = "";          //"乐诚网络";
                    row["address3"] = "";
                }
                row["SKU"] = "";
                //判断是否存在 - 符号
                if (pwork.data_3.Contains("-"))
                {
                    row["Phone"] = pwork.data_3;  //"〒 160-0023";
                }
                else
                {
                    row["Phone"] = pwork.data_3.Insert(3, "-").Insert(8, "-");
                }
                row["CustumerName"] = pwork.data_2;
                row["ExpressCode"] = pwork.data_9; 
                row["packgeinfo"] = pwork.data_1;
                row["MessageForBuyer"] = null;   
                row["SKU"] = pwork.data_7;
                row["SendShopname"] = "Shop: " + pwork.Shopname; // + pwork.Platform + " + "
                row["packgecode"] = pwork.data_10;
                row["shopname"] = "Name: "+pwork.Shopname;
                row["shoppost"] = "[差出人返還先]  〒：" + pwork.Shopzip;
                row["shopphone"] = "TEL: "+pwork.Shopphone;
                row["Companyaddr"] = pwork.Shopaddress;
                row["Companyaddr2"] ="";
                dt.Rows.Add(row);
                PrintCenter printer = new PrintCenter();
                printer.ZPLPrintUpacket3CM(dt, true, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
