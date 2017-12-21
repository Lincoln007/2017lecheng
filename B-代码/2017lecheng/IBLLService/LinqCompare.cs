using DBModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLLService
{
    //比较接口，点击IEqualityComparer下面的蓝色线，加载接口方法
    public class LinqCompare : IEqualityComparer<ProcudtViewModel>
    {

        #region IEqualityComparer<Card> 成员
        //值想同的时候返回真
        public bool Equals(ProcudtViewModel x, ProcudtViewModel y)
        {
            if (x.prod_id == y.prod_id)
            {
                return true;
            }
            else
            {
                return false;
            }
            //方法固有的
            throw new NotImplementedException();
        }

        public int GetHashCode(ProcudtViewModel obj)
        {
            return 0;
            throw new NotImplementedException();
        }

        #endregion
    }
}
