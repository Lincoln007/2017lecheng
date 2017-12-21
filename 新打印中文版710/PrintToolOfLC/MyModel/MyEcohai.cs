using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PrintToolOfLC.MyModel
{
    public class MyEcohai
    {
        public MyEcohai(DataSet ds)
        {
            Data_1 = ds.Tables[0].Rows[0]["data_1"].ToString();
            Data_2 = ds.Tables[0].Rows[0]["data_2"].ToString();
            Data_3 = ds.Tables[0].Rows[0]["data_3"].ToString();
            Data_4 = ds.Tables[0].Rows[0]["data_4"].ToString();
            Data_5 = ds.Tables[0].Rows[0]["data_5"].ToString();
            Data_6 = ds.Tables[0].Rows[0]["data_6"].ToString();
            Data_7 = ds.Tables[0].Rows[0]["data_7"].ToString();
            Data_8 = ds.Tables[0].Rows[0]["data_8"].ToString();
            Data_9 = ds.Tables[0].Rows[0]["data_9"].ToString();
            Data_10 = ds.Tables[0].Rows[0]["data_10"].ToString();
            Create_DateTime = ds.Tables[0].Rows[0]["create_DateTime"].ToString();
            Create_UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["create_UserID"].ToString());
            Edit_DateTime = ds.Tables[0].Rows[0]["edit_DateTime"].ToString();
            P_idActual = Convert.ToInt32(ds.Tables[0].Rows[0]["p_idActual"].ToString());
            P_idPoint = Convert.ToInt32(ds.Tables[0].Rows[0]["p_idPoint"].ToString());
            P_Status = Convert.ToInt32(ds.Tables[0].Rows[0]["p_Status"].ToString());
            P_Workid = Convert.ToInt32(ds.Tables[0].Rows[0]["p_Workid"].ToString());
            P_WorkRemarks = ds.Tables[0].Rows[0]["p_WorkRemarks"].ToString();
            P_WorkType = Convert.ToInt32(ds.Tables[0].Rows[0]["p_WorkType"].ToString());
            Print_DateTime1 = ds.Tables[0].Rows[0]["Print_DateTime"].ToString();

        }
        public MyEcohai()
        {
        }

        private int p_Workid;
        /// <summary>
        /// 表ID
        /// </summary>
        public int P_Workid
        {
            get { return p_Workid; }
            set { p_Workid = value; }
        }
        private int p_WorkType;
        /// <summary>
        /// 打印类型
        /// </summary>
        public int P_WorkType
        {
            get { return p_WorkType; }
            set { p_WorkType = value; }
        }
        private string data_1;
        /// <summary>
        /// 收件人邮编
        /// </summary>
        public string Data_1
        {
            get { return data_1; }
            set { data_1 = value; }
        }
        private string data_2;
        /// <summary>
        /// 收件人地址
        /// </summary>
        public string Data_2
        {
            get { return data_2; }
            set { data_2 = value; }
        }
        private string data_3;
        /// <summary>
        /// 包裹号
        /// </summary>
        public string Data_3
        {
            get { return data_3; }
            set { data_3 = value; }
        }
        private string data_4;
        /// <summary>
        /// SKU信息
        /// </summary>
        public string Data_4
        {
            get { return data_4; }
            set { data_4 = value; }
        }
        private string data_5;
        /// <summary>
        /// 收件人电话号码
        /// </summary>
        public string Data_5
        {
            get { return data_5; }
            set { data_5 = value; }
        }
        private string data_6;
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string Data_6
        {
            get { return data_6; }
            set { data_6 = value; }
        }
        private string data_7;
        /// <summary>
        /// 快递单号及条码
        /// </summary>
        public string Data_7
        {
            get { return data_7; }
            set { data_7 = value; }
        }
        private string data_8;
        /// <summary>
        /// 暂时空着
        /// </summary>
        public string Data_8
        {
            get { return data_8; }
            set { data_8 = value; }
        }
        private string data_9;
        /// <summary>
        /// 暂时空着
        /// </summary>
        public string Data_9
        {
            get { return data_9; }
            set { data_9 = value; }
        }
        private string data_10;
        /// <summary>
        /// 暂时空着
        /// </summary>
        public string Data_10
        {
            get { return data_10; }
            set { data_10 = value; }
        }

        private int p_idPoint;
        /// <summary>
        /// 指定打印客户端软件ID
        /// </summary>
        public int P_idPoint
        {
            get { return p_idPoint; }
            set { p_idPoint = value; }
        }
        private int p_idActual;
        /// <summary>
        /// 实际打印的打印客户端软件ID
        /// </summary>
        public int P_idActual
        {
            get { return p_idActual; }
            set { p_idActual = value; }
        }
        private string create_DateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public string Create_DateTime
        {
            get { return create_DateTime; }
            set { create_DateTime = value; }
        }
        private int create_UserID;
        /// <summary>
        /// 创建者ID
        /// </summary>
        public int Create_UserID
        {
            get { return create_UserID; }
            set { create_UserID = value; }
        }
        private int p_Status;
        /// <summary>
        /// 状态是否打印
        /// </summary>
        public int P_Status
        {
            get { return p_Status; }
            set { p_Status = value; }
        }
        private string Print_DateTime;
        /// <summary>
        /// 打印时间
        /// </summary>
        public string Print_DateTime1
        {
            get { return Print_DateTime; }
            set { Print_DateTime = value; }
        }

        private string edit_DateTime;
        /// <summary>
        /// 编辑时间
        /// </summary>
        public string Edit_DateTime
        {
            get { return edit_DateTime; }
            set { edit_DateTime = value; }
        }

        private string p_WorkRemarks;
        /// <summary>
        /// 备注
        /// </summary>
        public string P_WorkRemarks
        {
            get { return p_WorkRemarks; }
            set { p_WorkRemarks = value; }
        }

    }
}
