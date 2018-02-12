using DBModel.DBmodel;
using DBModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService.RFT
{
    public interface IRFT   //
    {
       base_users CheckLogin(string userid,string password);
       bool IsSKUInSys(string SKUcode);
       RFTDistrView Distrbuilt(string SKUcode, int shopID, string packgecode);
       object ShopDistrbuilt(string skucode, int? shopid);
       object PDistrbuilt(string skucode, string packgecode);
       bool ConfirmDistruit(RFTDistrView peihuoinfo, base_users Userinfo);
       bool DispartConfirmDistruit(RFTDistrView peihuoinfo, base_users Userinfo);
       RFTDistrView Dispartpackge(RFTDistrView peihuoinfo);

       bool CancelDistrubilt(RFTDistrView peihuoinfo);
       List<base_exp_comp> GetAllExpress();
       bool SetExpress(string packgecode,int expid);
       bool PrintzhuanyunCode(string zhuanyuncode);
        bool PrintpackgeCode(string packgecode);

        long CreatezhuanyunCode();
       bool IszhuanyunCodeInSys(string zhuanyuncode);
       bool IszhuanyunCodeing(string zhuanyuncode);
       bool ConfirmPutInBox(string zhuanyuncode, string packgecode);

       bool ConfirmOutofBox(string zhuanyuncode, string packgecode);  
    }
}
