using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelPrint
{
    public class M_print
    {
        //ID
        private int p_id;
        /// <summary>
        /// 记录的ID
        /// </summary>
        public int P_id
        {
            get { return p_id; }
            set { p_id = value; }
        }
        
        private string p_SerialNum = string.Empty;
        /// <summary>
        /// 序列号
        /// </summary>
        public string P_SerialNum
        {
            get { return p_SerialNum; }
            set { p_SerialNum = value; }
        }
       
        private string p_RegCode = string.Empty;
       /// <summary>
       /// 注册授权码
       /// </summary>
        public string P_RegCode
        {
            get { return p_RegCode; }
            set { p_RegCode = value; }
        }
        private int is_Lock;

        /// <summary>
        /// 是否锁定
        /// </summary>
        public int Is_Lock
        {
            get { return is_Lock; }
            set { is_Lock = value; }
        }
        private DateTime create_DateTime = DateTime.Now;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Create_DateTime
        {
            get { return create_DateTime; }
            set { create_DateTime = value; }
        }
        private int create_UserID;

        /// <summary>
        /// 创建信息用户ID
        /// </summary>
        public int Create_UserID
        {
            get { return create_UserID; }
            set { create_UserID = value; }
        }
        private int is_Del;
        /// <summary>
        /// 是否删除
        /// </summary>
        public int Is_Del
        {
            get { return is_Del; }
            set { is_Del = value; }
        }
        private DateTime del_DateTime = DateTime.Now;
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime Del_DateTime
        {
            get { return del_DateTime; }
            set { del_DateTime = value; }
        }
        private int del_UserID;
        /// <summary>
        /// 删除人ID
        /// </summary>
        public int Del_UserID
        {
            get { return del_UserID; }
            set { del_UserID = value; }
        }
        private string p_Remarks = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string P_Remarks
        {
            get { return p_Remarks; }
            set { p_Remarks = value; }
        }

    }
}
