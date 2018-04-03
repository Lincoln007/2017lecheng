using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer.CommHelper
{
    public static class Getconnstring
    {
        public static string Getmyconnstring()
        {
            //1.先判断XML文件是否存在，如果不存在则创建一个
            NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "PrintList");
            return xmlfile.Readconnstring();
        }
        public static string GetPrintInfo()
        {
            //1.先判断XML文件是否存在，如果不存在则创建一个
            NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//regedit.xml", false, "Passport");
            return xmlfile.Readconnstring();
        }

    }
}
