using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_supplier
    {
        
        /// <summary>
        /// Desc:供应商id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int SupplierID {get;set;}

        /// <summary>
        /// Desc:编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Code {get;set;}

        /// <summary>
        /// Desc:类型 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Type {get;set;}

        /// <summary>
        /// Desc:采购方式 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Mode {get;set;}

        /// <summary>
        /// Desc:名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Name {get;set;}

        /// <summary>
        /// Desc:网店地址 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Url {get;set;}

        /// <summary>
        /// Desc:优质供应商标识 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean IsGrade {get;set;}

        /// <summary>
        /// Desc:备注 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Desc {get;set;}

        /// <summary>
        /// Desc:是否删除 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean IsDel {get;set;}

        /// <summary>
        /// Desc:添加时间 
        /// Default:(getdate()) 
        /// Nullable:False 
        /// </summary>
        public DateTime AppTime {get;set;}

    }
}
