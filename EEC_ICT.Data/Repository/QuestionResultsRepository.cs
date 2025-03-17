using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class QuestionResultsRepository
    {
        public IDataReader SelectAll()
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                //new SqlParameter("@iTopicId", SqlDbType.Int){ Value = topicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_QuestionResults_SelectAll", parameters.ToArray());
            return data;
        }

        public IDataReader SelectOne(int questionId, string userId)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) { Value = userId },
                new SqlParameter("@iQuestionId", SqlDbType.Int) { Value = questionId },
                //new SqlParameter("@iTopicId", SqlDbType.Int){ Value = topicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_QuestionResults_SelectOne", parameters.ToArray());
        }

        public string Insert(QuestionResults entity)
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@iTopicId", SqlDbType.Int){ Value = ParameterDirection.Output},
                //new SqlParameter("@iIdCha", SqlDbType.Int) { Value = entity.IdCha},
                //new SqlParameter("@sTopicName", SqlDbType.NVarChar, 200) { Value = entity.TopicName},
                //new SqlParameter("@sNote", SqlDbType.NVarChar, 500) { Value = entity.Note},
                
                new SqlParameter("@iQuestionId", SqlDbType.Int){ Value = entity.QuestionId},
                new SqlParameter("@bResult", SqlDbType.Bit){ Value = entity.Result},
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) { Value = entity.UserId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_QuestionResults_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string Update(QuestionResults entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iQuestionId", SqlDbType.Int){ Value = entity.QuestionId},
                new SqlParameter("@bResult", SqlDbType.Bit){ Value = entity.Result},
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) { Value = entity.UserId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_QuestionResults_Update", parameters.ToArray());
            return entity.QuestionId.ToString();
        }

        public string Delete(int iTopicId)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iTopicId", SqlDbType.Int){ Value = iTopicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_QuestionResults_Delete", parameters.ToArray());
            return iTopicId.ToString();
        }
    }
}