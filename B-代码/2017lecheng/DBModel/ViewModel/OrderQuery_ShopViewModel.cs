using DBModel.OrderQuery_Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class OrderQuery_ShopViewModel
    {

        public List<OrderQuery_ShopModel> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }

    }


    public class OrderQuery_ShopViewModelE
    {

        public List<OrderQuery_ShopModelE> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }

    }
}
