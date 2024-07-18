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
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResult_SelectAll", parameters.ToArray());
            return data;
        }

        public IDataReader SelectOne(int questionId)
        {
            var parameter = new SqlParameter("@iQuestionId", SqlDbType.Int) { Value = questionId };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResult_SelectOne", parameter);
        }

        public string Insert(TestResult entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@lId", SqlDbType.BigInt){ Direction = ParameterDirection.Output},
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) { Value = entity.UserId},
                new SqlParameter("@iQuestionId", SqlDbType.Int) { Value = entity.QuestionId},
                new SqlParameter("@bResult", SqlDbType.Bit) { Value = entity.Result},
                //new SqlParameter("@iCorrectAnswerId", SqlDbType.Int) { Value = entity.CorrectAnswerId},
                new SqlParameter("@daTestDate", SqlDbType.DateTime) { Value = entity.TestDate},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }
    }
}