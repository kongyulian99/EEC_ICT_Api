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
        public IDataReader SelectAll(long idDeThi)
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                new SqlParameter("@lIdDeThi", SqlDbType.Int){ Value = idDeThi},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_CauHoi_SelectAll", parameters.ToArray());
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
                new SqlParameter("@iTopicId", SqlDbType.Int) { Value = entity.TopicId},
                new SqlParameter("@lIdDeThi", SqlDbType.BigInt) { Value = entity.IdDeThi},
                new SqlParameter("@sQuestion", SqlDbType.NVarChar, -1) { Value = entity.Question},
                //new SqlParameter("@sGraphUrl", SqlDbType.NVarChar, 200) { Value = entity.GraphUrl},
                //new SqlParameter("@iCorrectAnswerId", SqlDbType.Int) { Value = entity.CorrectAnswerId},
                new SqlParameter("@sChoices", SqlDbType.NVarChar, -1) { Value = entity.Choices},
                new SqlParameter("@byQuestionType", SqlDbType.TinyInt) { Value = entity.QuestionType},
                new SqlParameter("@fTrongSo", SqlDbType.Float) { Value = entity.TrongSo},
                new SqlParameter("@sNote", SqlDbType.NVarChar, 4000) { Value = entity.Note},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_CauHoi_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string CheckCorrect(DM_DapAnCheckCorrect entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iQuestionId", SqlDbType.Int){Value = entity.QuestionId},
                new SqlParameter("@iAnswerId", SqlDbType.Int){Value = entity.AnswerId},
                new SqlParameter("@bIsCorrect", SqlDbType.Bit) { Direction = ParameterDirection.Output },
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            var data = SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DapAn_CheckCorrect", parameters.ToArray());
            return parameters[2].Value.ToString();
        }

        public string Update(DM_CauHoi entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iQuestionId", SqlDbType.Int){ Value = entity.QuestionId},
                new SqlParameter("@sQuestion", SqlDbType.NVarChar, -1) { Value = entity.Question},
                //new SqlParameter("@sGraphUrl", SqlDbType.NVarChar, 200) { Value = entity.GraphUrl},
                //new SqlParameter("@iCorrectAnswerId", SqlDbType.Int) { Value = entity.CorrectAnswerId},
                new SqlParameter("@sChoices", SqlDbType.NVarChar, -1) { Value = entity.Choices},
                new SqlParameter("@byQuestionType", SqlDbType.TinyInt) { Value = entity.QuestionType},
                new SqlParameter("@iTopicId", SqlDbType.Int) { Value = entity.TopicId},
                new SqlParameter("@lIdDeThi", SqlDbType.BigInt) { Value = entity.IdDeThi},
                new SqlParameter("@sNote", SqlDbType.NVarChar, 4000) { Value = entity.Note},
                new SqlParameter("@fTrongSo", SqlDbType.Float) { Value = entity.TrongSo},
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