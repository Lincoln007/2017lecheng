using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
   public class WaitPurchaseViewModel
    {
       public List<WaitPurchaseModel> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }


   public class WaitPurchaseViewModelE
   {
       public List<WaitPurchaseModelE> supplist { get; set; }
       public string totil { get; set; }
       public string totilcount { get; set; }
   }
}
