using DBModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface IImportOrder
    {
        List<base_PSKU> GetSKUList(string shopname,string PSKU,string SSKU);     
    }
}
