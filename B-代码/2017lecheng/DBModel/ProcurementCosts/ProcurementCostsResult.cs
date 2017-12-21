using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ProcurementCosts
{
    public class ProcurementCostsResult
    {
        /// <summary>
        /// 返回的地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object DataResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 返回的信息
        /// </summary>
        public string Msg { get; set; }
    }


    public class ProcurementCostsModel
    {

        /// <summary>
        /// Desc:采购单编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string purch_code { get; set; }

        /// <summary>
        /// Desc:货物总金额 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? sum_money { get; set; }


        public decimal? sum_moneyE { get; set; }

        /// <summary>
        /// Desc:创建时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime create_time { get; set; }

        public string create_timeE { get; set; }



        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 create_user_id { get; set; }

        /// <summary>
        /// Desc:员工姓名 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string emp_name { get; set; }

    }
}
