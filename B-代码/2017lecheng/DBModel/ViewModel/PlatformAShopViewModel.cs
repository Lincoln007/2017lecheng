using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel.DBmodel;


namespace DBModel.ViewModel
{
    public class PlatformAShopViewModel
    {
        public List<base_platform> platforms { get; set; }
        public List<base_shop> shops { get; set; }
    }
}
