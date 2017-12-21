using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.DBmodel
{
    public class MyPageList
    {
        public object list { get; set; }

        public int pageIndex { get; set; }

        public int pageCount { get; set; }

        public int pageSize { get; set; }

        public int count { get; set; }

    }
}
