using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class RefreshTokenRepository
    {
        private string storep = "pr_Tokens_";
        public IDataReader SelectByRefreshToken(string refreshToken)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sRefreshToken", SqlDbType.NVarChar, 256) { Value = refreshToken }
            };

            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectByRefreshToken", parameters.ToArray());

            return dataReader;
        }

        public IDataReader SelectByUserId(string userId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) { Value = userId }
            };

            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"SelectByUserId", parameters.ToArray());

            return dataReader;
        }

        public string Insert(string userId, string refreshToken)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) { Value = userId },
                new SqlParameter("@sRefreshToken", SqlDbType.NVarChar, 256) { Value = refreshToken }
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Insert", parameters.ToArray());

            return refreshToken;
        }

        public string Delete(string refreshtoken)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sRefreshToken", SqlDbType.NVarChar, 128) { Value = refreshtoken }
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Delete", parameters.ToArray());

            return refreshtoken;
        }
    }
}