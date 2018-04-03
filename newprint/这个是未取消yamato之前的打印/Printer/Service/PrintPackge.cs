using Printer.CommHelper;
using Printer.DBmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Printer.Service
{
    /// <summary>
    /// 包裹号打印类
    /// </summary>
    public class PrintPackge
    {

        public packgeInfoViewModel packge;

        public PrintPackge(packgeInfoViewModel pack)
        {
            packge = pack;
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
                bool isexit = xmlfile.IsExitinnertextOfNode("包裹号", "page");
                if (isexit)
                {
                    //设置打印机的名称
                    //pd.PrinterSettings.PrinterName = xmlfile.GetinnertextOfPrintName("Umail", "page");
                }
                else
                {
                    throw new Exception("没有配置包裹号面单！请先配置！");
                }

                DataTable dt = new DataTable("table1");
                dt.Columns.Add(new DataColumn("ID", typeof(string)));
                dt.Columns.Add(new DataColumn("packgecode", typeof(string)));
                dt.Columns.Add(new DataColumn("number", typeof(string)));
                dt.Columns.Add(new DataColumn("time", typeof(string)));
                dt.Columns.Add(new DataColumn("shopname", typeof(string)));

                DataRow row = dt.NewRow();

                row["ID"] = packge.workID;
                row["packgecode"] = packge.Packgecode;
                row["number"] = packge.Number;
                row["time"] = packge.LastTime;
                row["shopname"] = packge.ShopName;
                dt.Rows.Add(row);
                PrintCenter printer = new PrintCenter();
                printer.ZPLPrintPackge(dt, true, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
