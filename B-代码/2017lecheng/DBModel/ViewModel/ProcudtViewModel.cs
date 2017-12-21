using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.ViewModel
{
    public class ProcudtViewModel
    {
        /// <summary>
        /// Desc:商品id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Int64 prod_id { get; set; }


        /// <summary>
        /// Desc:商品主图片地址 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string pic_url { get; set; }

        /// <summary>
        /// Desc:参考价格（RMB） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public decimal? price_cn { get; set; }

       
     

        /// <summary>
        /// Desc:0未指定，1普货，2仿货 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int prod_property { get; set; }

      

        /// <summary>
        /// Desc:是否上架（0待审核，1销售中，2已下架，3回收站） 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int prod_status { get; set; }

        /// <summary>
        /// Desc:标题(商品名称) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_title { get; set; }

        /// <summary>
        /// Desc:款号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_style { get; set; }


        /// <summary>
        /// Desc:开发人 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string prod_developer { get; set; }

        /// <summary>
        /// Desc:创建时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime create_time { get; set; }



        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remark { get; set; }
    }
}
