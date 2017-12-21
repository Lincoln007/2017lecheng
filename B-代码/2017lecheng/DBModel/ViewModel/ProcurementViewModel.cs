using DBModel.DBmodel;
using DBModel.Procurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class ProcurementViewModel
    {
        public List<ProcurementModel> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }


    public class ProcurementViewModelE
    {
        public List<ProcurementModelE> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }

}
