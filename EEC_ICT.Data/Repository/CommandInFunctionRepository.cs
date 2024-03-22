using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;

namespace EEC_ICT.Data.Repository
{
    public class CommandInFunctionRepository
    {
        private string storep = "pr_CommandInFunctions_";
        public IDataReader SelectOne(string commandId, string functionId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sCommandId", SqlDbType.VarChar,50) {Value = commandId},
                new SqlParameter("@sFunctionId", SqlDbType.VarChar,50) { Value = functionId}
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep + "SelectOne", parameters.ToArray());
            return dataReader;
        }
        public void Insert(CommandInFunction entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sCommandId", SqlDbType.VarChar,50) {Value = entity.CommandId},
                new SqlParameter("@sFunctionId", SqlDbType.VarChar, 50) {Value = entity.FunctionId}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Insert", parameters.ToArray());
        }
        public void Delete(CommandInFunction entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sCommandId", SqlDbType.VarChar, 50) {Value = entity.CommandId},
                new SqlParameter("@sFunctionId", SqlDbType.VarChar, 50) {Value = entity.FunctionId}
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Delete", parameters.ToArray());
        }
    }
}