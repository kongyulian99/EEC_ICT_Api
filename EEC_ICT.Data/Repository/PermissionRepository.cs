using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;

namespace EEC_ICT.Data.Repository
{
    public class PermissionRepository
    {
        private string storep = "pr_Permissions_";
        public IDataReader SelectByRoleId(int roleId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@iRoleId", SqlDbType.Int) {Value = roleId}
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectByRoleId", parameters.ToArray());
            return dataReader;
        }
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
        public void Insert(Permission entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@iRoleId", SqlDbType.Int) {Value = entity.RoleId},
                new SqlParameter("@sCommandId", SqlDbType.VarChar, 50) {Value = entity.CommandId},
                new SqlParameter("@sFunctionId", SqlDbType.VarChar, 50) {Value = entity.FunctionId}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Insert", parameters.ToArray());
        }
        public void DeleteByRoleId(int roleId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@iRoleId", SqlDbType.Int ) {Value = roleId}
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"DeleteByRoleId", parameters.ToArray());
        }
        
    }
}