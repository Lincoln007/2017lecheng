using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PrintToolOfLC.MyModel;
using XMLClass;

namespace PrintToolOfLC.Helper
{
    class Printzhuanyun
    {
        public Myzhuanyun packge;

        public Printzhuanyun(Myzhuanyun pack)
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
                bool isexit = xmlfile.IsExitinnertextOfNode("转运单", "page");
                if (isexit)
                {
                    //设置打印机的名称
                    //pd.PrinterSettings.PrinterName = xmlfile.GetinnertextOfPrintName("Umail", "page");
                }
                else
                {
                    throw new Exception("没有配置转运单面单！请先配置！");
                }
                DataTable dt = new DataTable("table1");
                dt.Columns.Add(new DataColumn("ID", typeof(string)));
                dt.Columns.Add(new DataColumn("packgecode", typeof(string)));
                dt.Columns.Add(new DataColumn("number", typeof(string)));
                dt.Columns.Add(new DataColumn("time", typeof(string)));

                DataRow row = dt.NewRow();

                row["ID"] = packge.Id;
                row["packgecode"] = packge.Packgecode;
               
                dt.Rows.Add(row);
                MyPrinter printer = new MyPrinter();
                printer.ZPLPrintDeviceLabel4(dt, true, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
