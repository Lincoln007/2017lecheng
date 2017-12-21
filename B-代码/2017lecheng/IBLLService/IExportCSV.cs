using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface IExportCSV
    {
        bool IsExitZY(string zhuanyuncode);

        bool GetDeclarationDHLExcelUrl(string zhuanyuncode, string fullpath,long express,long shop);
    }
}
