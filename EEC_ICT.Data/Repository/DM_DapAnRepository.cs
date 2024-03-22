using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class DM_DapAnRepository
    {
        public IDataReader SelectAllWQuestionId(int questionId)
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                new SqlParameter("@iQuestionId", SqlDbType.Int){ Value = questionId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DapAn_SelectAllWQuestionId", parameters.ToArray());
            return data;
        }
        public string Insert(DM_DapAn entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iAnswerId", SqlDbType.Int){ Value = ParameterDirection.Output},
                new SqlParameter("@sAnswer", SqlDbType.NVarChar, -1) { Value = entity.Answer},
                new SqlParameter("@iQuestionId", SqlDbType.Int) { Value = entity.QuestionId},
                new SqlParameter("@bIsCorrect", SqlDbType.Bit) { Value = entity.IsCorrect},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DapAn_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string Update(DM_DapAn entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iAnswerId", SqlDbType.Int){ Value = entity.AnswerId},
                new SqlParameter("@sAnswer", SqlDbType.NVarChar, -1) { Value = entity.Answer},
                new SqlParameter("@iQuestionId", SqlDbType.Int) { Value = entity.QuestionId},
                new SqlParameter("@bIsCorrect", SqlDbType.Bit) { Value = entity.IsCorrect},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DapAn_Update", parameters.ToArray());
            return entity.QuestionId.ToString();
        }

        public string Delete(int answerId)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iAnswerId", SqlDbType.Int){ Value = answerId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DapAn_Delete", parameters.ToArray());
            return answerId.ToString();
        }
    }
}