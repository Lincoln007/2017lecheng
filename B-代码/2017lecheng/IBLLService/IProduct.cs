using DBModel.DBmodel;
using DBModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface IProduct //UpdateProclassfyDel 
    {
        List<base_productclass> Getpages(int pagenum, int onepagecount, out int totil, out int totilpage,out string exmsg);
        bool UpdateProclassfy(string proclassfyname, int id);
        bool UpdateProclassfyDel(string proclassfyname, int id);
        bool AddProclassfy(base_productclass proclassfy, out int isexit, out int id);
        bool DeleteProClassfy(int id);
        List<ProcudtViewModel> Getprolist(int pagenum, int onepagecount, out int totil, out int totilpage, int status,string prostyle, string proskucode, string prodevelpoer);
        ProductDetail GetProductDetail(int proid);
        bool UpdateSKU(int codeID, string skucode);
        bool SetProImg(string fFullDir,int proID, string model);
        bool SetProMainImg(string fFullDir, int proID);
        base_prod_supp_rel AddProductSupplier(string supplier, int proid);//
        bool DeleteProductSupplier(int supplier, int proid);
    }
}
