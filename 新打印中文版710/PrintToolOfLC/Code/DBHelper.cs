using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
 


    public  class DBHelper
    {

        private static SqlConnection connection;
        private static OleDbConnection oleConn;
        private  string connString;
        private static string oleconnString="";

        public DBHelper(String connstr)
        {
            connString = connstr;
			
        } 

        static DBHelper()
        {

           
			// connString = ConfigurationManager.ConnectionStrings["crm"].ConnectionString;
			//connString = "Data Source=211.149.164.141;database=Lecheng;uid=jhqn;pwd=jhqn_2014";
            //oleconnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\StuInfo.accdb;Persist Security Info=True";
            // oleconnString=@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\juntang.liu\Desktop\StuInfo\App_Data\StuInfo.accdb;Persist Security Info=True"; 
        }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public  SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(connString);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }

        public static OleDbConnection OleConn
        {
            get
            {
                if (oleConn == null)
                {
                    oleConn = new OleDbConnection(oleconnString);
                    oleConn.Open();
                }
                else if (oleConn.State == System.Data.ConnectionState.Closed)
                {
                    oleConn.Open();
                }
                else if (oleConn.State == System.Data.ConnectionState.Broken)
                {
                    oleConn.Close();
                    oleConn.Open();
                }
                return oleConn;
            }
        }


        /// <summary>
        /// 过滤SQL注入
        /// </summary>
        /// <returns></returns>
        public  static string FilterSql(string str)
        {
            //str = str.Replace("'", "''");
            //str = str.Replace("<", "&lt;");
            //str = str.Replace(">", "&gt;");
            return str;
        }

        ///<summary>
        ///SQL注入过滤 
        ///</summary>
        ///<param name="InText">要过滤的字符串 </param>
        ///<returns>如果参数存在不安全字符，则返回true </returns>
        public static bool SqlFilter(string InText)
        {
            string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|cmd";
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + "") > -1) || (InText.ToLower().IndexOf("" + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 获取command对象
        /// </summary>
        /// <param name="safeSql">sql</param>
        /// <returns></returns>
        public  SqlCommand GetCommand(string safeSql)
        {

            SqlCommand cmd = new SqlCommand(FilterSql(safeSql), Connection);
            return cmd;
        }

        public static OleDbCommand GetOleCommand(string safeSql)
        {
            OleDbCommand cmd = new OleDbCommand(FilterSql(safeSql), OleConn);
            return cmd;
        }


        /// <summary>
        /// 获取command对象
        /// </summary>
        /// <param name="safeSql">sql</param>
        /// <param name="values">参数|参数数组</param>
        /// <returns></returns>
        public  SqlCommand GetCommand(string safeSql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(FilterSql(safeSql), Connection);

            cmd.Parameters.AddRange(values);

            return cmd;
        }

        public static OleDbCommand GetOleCommand(string safeSql, params OleDbParameter[] values)
        {
            OleDbCommand cmd = new OleDbCommand(FilterSql(safeSql), OleConn);

            cmd.Parameters.AddRange(values);

            return cmd;
        }


        /// <summary>
        /// 对数据库增删改操作
        /// </summary>
        /// <param name="safeSql">sql</param>
        /// <returns></returns>
        public  int ExecuteCommand(string safeSql)
        {
            int result = GetCommand(safeSql).ExecuteNonQuery();
            return result;
        }

        public static int ExecuteOleCommand(string safeSql)
        {
            int result = GetOleCommand(safeSql).ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 对数据库增删改操作
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="values">参数</param>
        /// <returns></returns>
        public  int ExecuteCommand(string sql, params SqlParameter[] values)
        {
            int result = GetCommand(sql, values).ExecuteNonQuery();
            return result;
        }

        public static int ExecuteOleCommand(string sql, params OleDbParameter[] values)
        {
            int result = GetOleCommand(sql, values).ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 返回第一行第一列数据
        /// </summary>
        /// <param name="safeSql">sql</param>
        /// <returns></returns>
		public string GetScalar(string safeSql)
        {
            object result = GetCommand(safeSql).ExecuteScalar();
			if (result!=null)
			{
				return result.ToString();
			}
			return string.Empty;
        }

        public static int GetOleScalar(string safeSql)
        {
            int result = (int)GetOleCommand(safeSql).ExecuteScalar();
            return result;
        }

        /// <summary>
        /// 返回第一行第一列数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values">参数</param>
        /// <returns></returns>
        public  object GetScalar(string safeSql, params SqlParameter[] values)
        {

            object result = GetCommand(safeSql, values).ExecuteScalar();
            return result;
        }

        public static object GetOleScalar(string safeSql, params OleDbParameter[] values)
        {

            int result = GetOleCommand(safeSql, values).ExecuteNonQuery();
            return result;
        }


        /// <summary>
        /// 获取DataReader对象
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public  SqlDataReader GetReader(string safeSql)
        {
            SqlDataReader reader = GetCommand(safeSql).ExecuteReader();
            return reader;
        }

        public static OleDbDataReader GetOleReader(string safeSql)
        {
            OleDbDataReader reader = GetOleCommand(safeSql).ExecuteReader();
            return reader;
        }

        public  SqlDataReader GetReader(string safeSql, params SqlParameter[] values)
        {
            SqlDataReader reader = GetCommand(safeSql, values).ExecuteReader();
            return reader;
        }

        public static OleDbDataReader GetOleReader(string safeSql, params OleDbParameter[] values)
        {
            OleDbDataReader reader = GetOleCommand(safeSql, values).ExecuteReader();
            return reader;
        }

        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public  DataSet GetDataSet(string safeSql)
        {
            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(GetCommand(safeSql));
            da.Fill(ds);
            return ds;
        }

        public static DataSet GetOleDataSet(string safeSql)
        {
            DataSet ds = new DataSet();

            OleDbDataAdapter da = new OleDbDataAdapter(GetOleCommand(safeSql));
            da.Fill(ds);
            return ds;
        }


        public  DataSet GetDataSet(string sql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(GetCommand(sql, values));
            da.Fill(ds);
            return ds;
        }

        public static DataSet GetOleDataSet(string sql, params OleDbParameter[] values)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(GetOleCommand(sql, values));
            da.Fill(ds);
            return ds;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="values">参数数组</param>
        /// <returns>数据集</returns>
        public  DataSet ExecuteProcedure(string procName, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = GetCommand(procName, values);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
           
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public  SqlDataReader ExecuteProcedure(string procName)
        {

            SqlCommand cmd = GetCommand(procName);
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd.ExecuteReader();
        }


		public static DBHelper Create(string connstring)
		{
			var model = new DBHelper(connstring);
			connection = new SqlConnection(connstring);
			return model;
		}

	

    }





