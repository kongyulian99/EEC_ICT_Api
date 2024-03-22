using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;

namespace EEC_ICT.Data.Repository
{
    public class FunctionRepository
    {
        private string storep = "pr_Functions_";
        private string storep2 = "pr_CommandInFunctions_";
        public IDataReader SelectAll()
        {
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectAll");
            return dataReader;
        }
        public IDataReader SelectAllWParentId(string ParentId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sParentId", SqlDbType.VarChar, 50) {Value = ParentId}
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectAllWParentId", parameters.ToArray());
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

        public string Insert(Function entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sId", SqlDbType.VarChar, 50) {Value = entity.Id},
                new SqlParameter("@sName", SqlDbType.NVarChar, 256) {Value = entity.Name},
                new SqlParameter("@sUrl", SqlDbType.VarChar, 256) {Value = entity.Url},
                new SqlParameter("@iSortOrder", SqlDbType.Int) {Value = entity.SortOrder},
                new SqlParameter("@sParentId", SqlDbType.VarChar, 50) {Value = entity.ParentId},
                new SqlParameter("@bStatus", SqlDbType.Bit) {Value = entity.Status},
                new SqlParameter("@sIconCss", SqlDbType.VarChar,256) {Value = entity.IconCss}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string Update(Function entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sId", SqlDbType.VarChar,50) {Value = entity.Id},
                new SqlParameter("@sName", SqlDbType.NVarChar, 256) {Value = entity.Name},
                new SqlParameter("@sUrl", SqlDbType.VarChar, 256) {Value = entity.Url},
                new SqlParameter("@iSortOrder", SqlDbType.Int) {Value = entity.SortOrder},
                new SqlParameter("@sParentId", SqlDbType.VarChar, 50) {Value = entity.ParentId},
                new SqlParameter("@bStatus", SqlDbType.Bit) {Value = entity.Status},
                new SqlParameter("@sIconCss", SqlDbType.VarChar,256) {Value = entity.IconCss}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Update", parameters.ToArray());
            return entity.Id;
        }

        public string Delete(string id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sId", SqlDbType.VarChar,50) {Value = id}
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Delete", parameters.ToArray());
            return id;
        }
        public IDataReader GetFunctionWithCommand()
        {
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"GetFunctionWithCommand");
            return dataReader;
        }

        public void PostCommandToFunction(string idCommand, string idFunction)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sFunctionId", SqlDbType.VarChar, 50) {Value = idFunction},
                new SqlParameter("@sCommandId", SqlDbType.VarChar, 50) {Value = idCommand}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep2+"Insert", parameters.ToArray());
        }
        public int CheckFunctionId(string id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Value", SqlDbType.TinyInt) { Direction = ParameterDirection.Output },
                new SqlParameter("@sId", SqlDbType.NVarChar,50) {Value = id},
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"CheckFunctionId", parameters.ToArray());
            return Convert.ToInt32(parameters[0].Value);
        }

        public void DeleteCommandInFunction(string idCommand, string idFunction)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sFunctionId", SqlDbType.VarChar, 50) {Value = idFunction},
                new SqlParameter("@sCommandId", SqlDbType.VarChar, 50) {Value = idCommand}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep2+"Delete", parameters.ToArray());
        }
        public IDataReader SelectAllActivated()
        {
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectAllActivated");
            return dataReader;
        }
    }
}