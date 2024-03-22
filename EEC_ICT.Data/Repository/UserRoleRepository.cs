using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class UserRoleRepository
    {
        private string storep = "pr_UserRoles_";
        public IDataReader SelectOne(string userId, int roleId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = userId},
                new SqlParameter("@iRoleId", SqlDbType.Int) { Value = roleId}
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectOne", parameters.ToArray());
            return dataReader;
        }

        public void Insert(string userId, int roleId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = userId},
                new SqlParameter("@iRoleId", SqlDbType.Int) {Value = roleId}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Insert", parameters.ToArray());
        }

        public void Delete(string userId, int roleId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = userId},
                new SqlParameter("@iRoleId", SqlDbType.Int) {Value = roleId}
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Delete", parameters.ToArray());
        }
    }
}