using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class RoleRepository
    {
        private string storep = "pr_Roles_";
        public IDataReader SelectByUserId(string userId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = userId}
            };

            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectByUserId", parameters.ToArray());
            return dataReader;
        }

        public IDataReader SelectAll()
        {
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectAll");
            return dataReader;
        }
        public IDataReader SelectAllByPermission(string userId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) {Value = userId}
            };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, storep+"SelectAllByPermission", parameters.ToArray());
        }

        public IDataReader SelectOne(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@iId", SqlDbType.Int) {Value = id}
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectOne", parameters.ToArray());
            return dataReader;
        }
        public int CheckName(int id, string name)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Value", SqlDbType.TinyInt) { Direction = ParameterDirection.Output },
                new SqlParameter("@iId", SqlDbType.Int) {Value = id},
                new SqlParameter("@sName", SqlDbType.NVarChar, 256) {Value = name}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"CheckName", parameters.ToArray());
            return Convert.ToInt32(parameters[0].Value);
        }

        public int Insert(Role entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sName", SqlDbType.NVarChar, 256) {Value = entity.Name},
                new SqlParameter("@sDescription", SqlDbType.NVarChar, 256) {Value = entity.Description},
                new SqlParameter("@iId", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Insert", parameters.ToArray());
            return Convert.ToInt32(parameters[parameters.Count-1].Value);
        }

        public int Update(Role entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@iId", SqlDbType.Int) {Value = entity.Id},
                new SqlParameter("@sName", SqlDbType.NVarChar, 256) {Value = entity.Name},
                new SqlParameter("@sDescription", SqlDbType.NVarChar, 256) {Value = entity.Description}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Update", parameters.ToArray());
            return entity.Id;
        }

        public int Delete(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@iId", SqlDbType.Int) {Value = id}
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Delete", parameters.ToArray());
            return id;
        }
    }
}