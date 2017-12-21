using System;
using System.Linq;
using System.Text;

namespace DBModel.DBmodel
{
    public class busi_class
    {
        
        /// <summary>
        /// Desc:分类id 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int CID {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int ParentID {get;set;}

        /// <summary>
        /// Desc:分类名称 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string Name {get;set;}

        /// <summary>
        /// Desc:图标 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Icon {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public int Sort {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:((0)) 
        /// Nullable:False 
        /// </summary>
        public Boolean IsLast {get;set;}

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
