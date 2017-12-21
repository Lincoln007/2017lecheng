using DBModel;
using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    public interface IExpress
    {
        List<base_exp_comp> Getpages(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg);

        bool AddExpress(base_exp_comp proclassfy, out int isexit, out int id);

        bool UpdateExpress(string proclassfyname, int id);
        bool DelUpdateExpress(int id);
        int DeleteExpress(int id);
        bool Import(List<base_expresscode> list);
        List<base_expresscode> Searchexp(int express, int pageindex, int isuse, out int totil);

        List<ExpressPackgeModel> GetExpressPackgeList(int expressid, int ispacked, int pageSize, int pageIndex, out int packgecount, out int count,DateTime? starttime,DateTime? endtime);
        List<ExpressPackgeModel> GetExpressPackgeAllList(int expressid, int ispacked, out int count, DateTime? starttime, DateTime? endtime);

        int GetExpressCodeCount(int expressid);
        int UseExpressCode(int expressid, DataTable csvtable);
        int AssociateExpressCode(int expressid, DataTable csvtable);

        bool AssociateDHLExpressCode(string dhlcode, string zhuanyuncode);
    }
}
