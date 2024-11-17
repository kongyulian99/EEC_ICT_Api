using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class TestResultRepository
    {
        public IDataReader SelectAll()
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                //new SqlParameter("@iTopicId", SqlDbType.Int){ Value = topicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_SelectAll", parameters.ToArray());
            return data;
        }

        public IDataReader SelectByUser(string userId)
        {
            var parameter = new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) { Value = userId };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_SelectByUser", parameter);
        }

        public string Insert(TestResults entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@lId", SqlDbType.BigInt){ Direction = ParameterDirection.Output},
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) { Value = entity.UserId},
                new SqlParameter("@lIdDeThi", SqlDbType.BigInt) { Value = entity.IdDeThi},
                new SqlParameter("@fScore", SqlDbType.Float) { Value = entity.Score},
                //new SqlParameter("@iCorrectAnswerId", SqlDbType.Int) { Value = entity.CorrectAnswerId},
                new SqlParameter("@daStartTime", SqlDbType.DateTime) { Value = entity.StartTime},
                new SqlParameter("@daEndTime", SqlDbType.DateTime) { Value = entity.EndTime},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        // for dashboard
        public IDataReader SelectBestScoreByUser(string userId)
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                //new SqlParameter("@iTopicId", SqlDbType.Int){ Value = topicId},
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128){ Value = userId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_SelectBestScoreByUser", parameters.ToArray());
            return data;
        }
    }
}