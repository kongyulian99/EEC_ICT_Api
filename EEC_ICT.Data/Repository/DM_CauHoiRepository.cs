using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class DM_CauHoiRepository
    {
        public IDataReader SelectAll()
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                //new SqlParameter("@iTopicId", SqlDbType.Int){ Value = topicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_CauHoi_SelectAll", parameters.ToArray());
            return data;
        }
        public IDataReader SelectAllWTopicIdLogic(int topicId)
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                new SqlParameter("@iTopicId", SqlDbType.Int){ Value = topicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_CauHoi_SelectAllWTopicIdLogic", parameters.ToArray());
            return data;
        }

        public IDataReader SelectOne(int questionId)
        {
            var parameter = new SqlParameter("@iQuestionId", SqlDbType.Int) { Value = questionId };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_CauHoi_SelectOne", parameter);
        }

        public string Insert(DM_CauHoi entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iQuestionId", SqlDbType.Int){ Direction = ParameterDirection.Output},
                new SqlParameter("@sQuestion", SqlDbType.NVarChar, -1) { Value = entity.Question},
                new SqlParameter("@sGraphUrl", SqlDbType.NVarChar, 200) { Value = entity.GraphUrl},
                //new SqlParameter("@iCorrectAnswerId", SqlDbType.Int) { Value = entity.CorrectAnswerId},
                new SqlParameter("@iTopicId", SqlDbType.Int) { Value = entity.TopicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_CauHoi_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string Update(DM_CauHoi entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iQuestionId", SqlDbType.Int){ Value = entity.QuestionId},
                new SqlParameter("@sQuestion", SqlDbType.NVarChar, -1) { Value = entity.Question},
                new SqlParameter("@sGraphUrl", SqlDbType.NVarChar, 200) { Value = entity.GraphUrl},
                //new SqlParameter("@iCorrectAnswerId", SqlDbType.Int) { Value = entity.CorrectAnswerId},
                new SqlParameter("@iTopicId", SqlDbType.Int) { Value = entity.TopicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_CauHoi_Update", parameters.ToArray());
            return entity.QuestionId.ToString();
        }

        public string Delete(int questionId)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iQuestionId", SqlDbType.Int){ Value = questionId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_CauHoi_Delete", parameters.ToArray());
            return questionId.ToString();
        }
    }
}