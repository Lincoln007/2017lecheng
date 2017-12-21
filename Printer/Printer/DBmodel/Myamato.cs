using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Printer.DBmodel
{
    public class Myamato
    {
        /// <summary>
        /// 平台名
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 增加店铺名
        /// </summary>
        public string Shopname { get; set; }

        public string Shopzip { get; set; }
        public string Shopaddress { get; set; }
        public string Shopphone { get; set; }
        public string Companyname { get; set; }
        /// <summary>
        /// Desc:主键 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 p_Workid { get; set; }

        /// <summary>
        /// Desc:打印任务类型 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? p_WorkType { get; set; }

        /// <summary>
        /// Desc:数据1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_1 { get; set; }

        /// <summary>
        /// Desc:数据2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_2 { get; set; }

        /// <summary>
        /// Desc:数据3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_3 { get; set; }

        /// <summary>
        /// Desc:数据4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_4 { get; set; }

        /// <summary>
        /// Desc:数据5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_5 { get; set; }

        /// <summary>
        /// Desc:数据6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_6 { get; set; }

        /// <summary>
        /// Desc:数据7 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_7 { get; set; }

        /// <summary>
        /// Desc:数据8 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_8 { get; set; }

        /// <summary>
        /// Desc:数据9 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_9 { get; set; }

        /// <summary>
        /// Desc:数据10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string data_10 { get; set; }

        /// <summary>
        /// Desc:Base_Print表主键(0:不指定) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? p_idPoint { get; set; }

        /// <summary>
        /// Desc:创建日时 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? create_DateTime { get; set; }

        /// <summary>
        /// Desc:创建用户ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? create_UserID { get; set; }

        /// <summary>
        /// Desc:0:已打印 1:未打印 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? p_Status { get; set; }

        /// <summary>
        /// Desc:打印日时 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? Print_DateTime { get; set; }

        /// <summary>
        /// Desc:编辑日时 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? edit_DateTime { get; set; }

        /// <summary>
        /// Desc:实际打印客户端软件ID 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? p_idActual { get; set; }

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string p_WorkRemarks { get; set; }
    }
}
