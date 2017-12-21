using DBModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class StockViewModel
    {
        public List<StockModel> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }
}
