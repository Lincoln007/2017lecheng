using DBModel.GoodsReceived;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class GoodsReceivedViewModel
    {
        public List<GoodsReceivedModel> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }

        public string express_info { get; set; }
    }
}
