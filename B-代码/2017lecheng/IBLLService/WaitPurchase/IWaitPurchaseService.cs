using DBModel.DBmodel;
using DBModel.WaitPurchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.WaitPurchase
{
    public interface IWaitPurchaseService
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        /// 
        List<WaitPurchaseModel> GetWaitPurchaseList(int pagenum, int onepagecount, out int totil, out int totilpage, out string exmsg);


        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        busi_purchase GetInfoByID(long? id);


        List<WaitPurchaseModelE> GetWaitPurchaseEList(out string exmsg, Int64? purch_id);


        WaitPurchaseResult Save(busi_purchase model);


        WaitPurchaseResult isLocked(busi_purchase model);
    }
}
