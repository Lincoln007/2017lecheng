using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface INewProduct
    {
         bool PcodeIsExit(string code);
         bool InsertProdcut(string code, string proname, string bgname,string bgcode, string bgprice, string weight, string price, string remark, string clasf, string profly, string promodel, string prosize, List<string> skulist);
         bool UpdateProdcut(string code, string proname, string bgname,string bgcode, string bgprice, string weight, string price, string remark, string clasf, string promodel, string prosize, int proid, int prostatus,string purchase_url);
      
    }
}
