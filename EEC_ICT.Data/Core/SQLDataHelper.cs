using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Core
{
    public class SQLDataHelper
    {
        public static IDataReader ExecuteReader(string connection, string commandText, SqlParameter[] sqlparam)
        {
            try
            {
                var con = new SqlConnection(connection);
                var com = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText
                };
                con.Open();
                if (sqlparam != null)
                {
                    com.Parameters.AddRange(sqlparam);
                }
                return com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (DataException e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public static IDataReader ExecuteReader(string connection, string commandText, SqlParameter[] sqlparam, out SqlCommand comx)
        {
            try
            {
                var con = new SqlConnection(connection);
                var com = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText,
                    CommandTimeout = 200
                };
                con.Open();
                if (sqlparam != null)
                {
                    com.Parameters.AddRange(sqlparam);
                }
                comx = com;
                return com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (DataException e)
            {
                Logger.Error(e);
                comx = null;
                return null;
            }
        }

        public static DataTable GetDataTable(SqlCommand sqlCommand, string connection)
        {
            var con = new SqlConnection(connection);
            try
            {
                sqlCommand.Connection = con;

                SqlDataAdapter myDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet myDataSet = new DataSet();
                myDataAdapter.Fill(myDataSet);
                return myDataSet.Tables[0];
            }
            catch (SqlException myException)
            {
                Logger.Error(myException);
                throw (new Exception(myException.Message));
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        /// <summary>
        /// Trả về datareader với trường hợp CommandType=CommandType.Textr
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commndText"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string connection, string commndText)
        {
            try
            {
                var con = new SqlConnection(connection);
                var com = new SqlCommand
                {
                    CommandText = commndText,
                    CommandType = CommandType.Text,
                    Connection = con
                };
                con.Open();
                return com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (DataException e)
            {
                Logger.Error(e);
                return null;
            }
        }

        #region ExecuteNonQuery

        /// <summary>
        /// ExecuteNonQuery trường hợp sử dụng storeprocedure
        /// </summary>
        /// <param name="connection">Config.Connectstring</param>
        /// <param name="commandText"></param>
        /// <param name="sqlparam">nếu store không có tham số truyền vào null</param>
        /// <returns>số bản ghi ảnh hưởng</returns>
        public static int ExecuteNonQuery(string connection, string commandText, SqlParameter[] sqlparam)
        {
            try
            {
                var con = new SqlConnection(connection);
                using (con)
                {
                    var com = new SqlCommand
                    {
                        CommandText = commandText,
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };
                    con.Open();
                    if (sqlparam != null)
                    {
                        com.Parameters.AddRange(sqlparam);
                    }
                    return com.ExecuteNonQuery();
                }
            }
            catch (DataException e)
            {
                Logger.Error(e);
                return -1;
            }
        }

        public static int ExecuteNonQuery(string connection, string commandText, SqlParameter[] sqlparam, out SqlCommand comx)
        {
            try
            {
                var con = new SqlConnection(connection);
                using (con)
                {
                    var com = new SqlCommand
                    {
                        CommandText = commandText,
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };
                    con.Open();
                    if (sqlparam != null)
                    {
                        com.Parameters.AddRange(sqlparam);
                    }
                    comx = com;
                    return com.ExecuteNonQuery();
                }
            }
            catch (DataException e)
            {
                Logger.Error(e);
                comx = null;
                return -1;
            }
        }

        /// <summary>
        /// Trường hợp sử dụng commandText
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connection, string commandText)
        {
            try
            {
                var con = new SqlConnection(connection);
                using (con)
                {
                    var com = new SqlCommand
                    {
                        CommandText = commandText,
                        CommandType = CommandType.Text,
                        Connection = con
                    };
                    con.Open();
                    return com.ExecuteNonQuery();
                }
            }
            catch (DataException e)
            {
                Logger.Error(e);
                return -1;
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteScalar

        public static object ExecuteScalar(string connection, string commandText, SqlParameter[] sqlparam)
        {
            try
            {
                var con = new SqlConnection(connection);
                using (con)
                {
                    var com = new SqlCommand
                    {
                        CommandText = commandText,
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };
                    con.Open();
                    if (sqlparam != null)
                    {
                        com.Parameters.AddRange(sqlparam);
                    }
                    return com.ExecuteScalar();
                }
            }
            catch (DataException e)
            {
                Logger.Error(e);
                return -1;
            }
        }

        public static object ExecuteScalar(string connection, string commandText)
        {
            try
            {
                var con = new SqlConnection(connection);
                using (con)
                {
                    var com = new SqlCommand
                    {
                        CommandText = commandText,
                        CommandType = CommandType.Text,
                        Connection = con
                    };
                    con.Open();
                    return com.ExecuteScalar();
                }
            }
            catch (DataException e)
            {
                Logger.Error(e);
                return -1;
            }
        }

        #endregion ExecuteScalar

        public static T GetInfo<T>(IDataReader r)
        {
            var builder = DynamicBuilder<T>.CreateBuilder(r);
            var x = default(T);
            if (r != null)
            {
                if (r.Read())
                {
                    x = builder.Build(r);
                }
                r.Close();
                r.Dispose();
                return x;
            }
            return default(T);
        }

        public static List<T> GetList<T>(IDataReader r)
        {
            if (r != null)
            {
                var list = new List<T>();
                var builder = DynamicBuilder<T>.CreateBuilder(r);
                while (r.Read())
                {
                    var x = builder.Build(r);
                    list.Add(x);
                }
                r.Close();
                r.Dispose();
                return list;
            }
            return null;
        }
    }
}