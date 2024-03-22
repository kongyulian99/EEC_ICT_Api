using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;

namespace EEC_ICT.Data.Repository
{
    public class CommandRepository
    {
        private string storep = "pr_Commands_";
        public IDataReader SelectAll()
        {
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectAll");
            return dataReader;
        }

        public IDataReader SelectOne(string id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sId", SqlDbType.VarChar,50) {Value = id}
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectOne", parameters.ToArray());
            return dataReader;
        }

        public string Insert(Command entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sId", SqlDbType.VarChar,50) {Value = entity.Id},
                new SqlParameter("@sName", SqlDbType.NVarChar, 50) {Value = entity.Name},
                new SqlParameter("@bySortOrder", SqlDbType.TinyInt) {Value = entity.SortOrder}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string Update(Command entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sId", SqlDbType.VarChar,50) {Value = entity.Id},
                new SqlParameter("@sName", SqlDbType.NVarChar, 50) {Value = entity.Name},
                new SqlParameter("@bySortOrder", SqlDbType.TinyInt) {Value = entity.SortOrder}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Update", parameters.ToArray());
            return entity.Id;
        }

        public string Delete(string id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sId", SqlDbType.VarChar, 50) {Value = id}
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Delete", parameters.ToArray());
            return id;
        }
        public IDataReader SelectByFunctionId(string functionId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sFunctionId", SqlDbType.VarChar,50) {Value = functionId}
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectByFunctionId", parameters.ToArray());
            return dataReader;
        }
    }
}