using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface ISupplier 
    {
        bool UpdateSupplier(string suppname, string remark, string suppurl, int id);
        bool AddSupplier(base_supplier supp);
        List<base_supplier> SearchSupplier(int pagenum, int onepagecount, string suppliername, out int totil, out int totilpage);
        bool DeleteSupplier(int id);

    }
}
