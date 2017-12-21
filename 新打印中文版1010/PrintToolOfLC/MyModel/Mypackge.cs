using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintToolOfLC.MyModel
{
    public class Mypackge
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string packgecode;

        public string Packgecode
        {
            get { return packgecode; }
            set { packgecode = value; }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        private string shopname;

        /// <summary>
        /// 3.5号增加
        /// </summary>
        public string ShopName
        {
            get { return shopname; }
            set { shopname = value; }
        }

    }
}
