using DBModel.DBmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class ExpressViewModel
    {
        public List<base_exp_comp> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }
}
