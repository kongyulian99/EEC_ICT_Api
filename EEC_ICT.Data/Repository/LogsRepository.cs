using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class LogsRepository
    {
        public IDataReader SelectAll()
        {
            var parameter = new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                "pr_Logs_SelectAll", parameter);
            return dataReader;
        }

        public IDataReader SelectOne(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@iId", SqlDbType.Int) {Value = id},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                "pr_Logs_SelectOneWIdLogic", parameters.ToArray());
            return dataReader;
        }

        public string Insert(Logs entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@lId_logs", SqlDbType.BigInt) { Direction = ParameterDirection.Output },
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) {Value = entity.UserId},
                new SqlParameter("@daNgayLogs", SqlDbType.DateTime) {Value = entity.NgayLogs},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                "pr_Logs_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public long Update(Logs entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@lId_logs", SqlDbType.BigInt) {Value = entity.Id_logs},
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) {Value = entity.UserId},
                new SqlParameter("@daNgayLogs", SqlDbType.DateTime) {Value = entity.NgayLogs},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                "pr_Logs_Update", parameters.ToArray());
            return entity.Id_logs;
        }

        public long Delete(long id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@lId_logs", SqlDbType.BigInt) {Value = id},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                "pr_Logs_Delete", parameters.ToArray());
            return id;
        }

        public IDataReader SelectByFilter(string keyword, DateTime tungay, DateTime denngay, int pageindex, int pagesize)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sKeyword", SqlDbType.NVarChar, 300) {Value = keyword},
                new SqlParameter("@daTuNgay", SqlDbType.DateTime) {Value = tungay},
                new SqlParameter("@daDenNgay", SqlDbType.DateTime) {Value = denngay},
                new SqlParameter("@iPageIndex", SqlDbType.Int) {Value = pageindex},
                new SqlParameter("@iPageSize", SqlDbType.Int) {Value = pagesize},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                "pr_Logs_SelectByFilter", parameters.ToArray());
            return dataReader;
        }
    }
}