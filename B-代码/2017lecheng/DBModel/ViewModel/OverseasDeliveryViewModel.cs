using DBModel.OverseasDelivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class OverseasDeliveryViewModel
    {
        public List<OverseasDeliveryModel> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }
}
