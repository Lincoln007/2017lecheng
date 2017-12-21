using DBModel.DBmodel;
using DBModel.OrderQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class OrderQueryViewModel
    {
        public List<OrderQueryModel> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }


    public class OrderQueryViewModelE
    {
        public List<OrderQueryModelE> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }


    public class OrderQueryViewModelEE
    {
        public List<busi_sendorder> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }

}
