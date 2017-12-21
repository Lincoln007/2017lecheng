using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelPrint;
using System.Data;

namespace DALPrint
{
    public class D_Print
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="m_print"></param>
        /// <returns></returns>
        public List<M_print> IsExit(string P_RegCode)
        {

            List<M_print> list = new List<M_print>();
            string sbSelect = string.Empty;
            sbSelect = string.Format("select * from Base_Print where p_RegCode like '{0}' ", P_RegCode);


            try
            {
                DataSet ds = SQLHelper.Query(sbSelect);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        M_print user = new M_print();
                        user.P_id = Convert.ToInt32(dr["p_id"]);
                        user.P_RegCode = dr["P_RegCode"].ToString();
                        
                        list.Add(user);
                    }
                    return list;
                }else
                {
                   return null;
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }


        }
           
    }


}
