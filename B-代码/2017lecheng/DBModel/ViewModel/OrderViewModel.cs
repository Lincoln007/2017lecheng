﻿using DBModel.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class OrderViewModel
    {
        public List<OrderWorkE> supplist { get; set; }
        public string totil { get; set; }
        public string totilcount { get; set; }
    }  
}
