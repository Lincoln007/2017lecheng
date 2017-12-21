using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface IDeclaration
    {
       string GetDeclarationExcelUrl(string dhlcode, string zhuanyuncode,string fullpath);
        bool IsExitZY(string zhuanyuncode);
    }
}
