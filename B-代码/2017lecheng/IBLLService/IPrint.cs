using DBModel.DBmodel;
using DBModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface IPrint
    {
        bool AddPrinter(base_print pro, out int isexit, out int id);
        List<myprinter> Getpages(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg);
        int DeletePrinter(int id);
        bool DelUpdatePrinter(int id);
        bool UpdatePrinter(string proclassfyname, int id);
        bool IsPackgeInSys(string packgecode);
        List<PrintSelect> GetPrintSelectList(string packgecode, int ispacked, int pageSize,  int pageIndex,out int packgecount,out int count);
        bool ComfirmPrint();
    }
}
