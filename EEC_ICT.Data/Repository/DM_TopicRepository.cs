﻿using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class DM_TopicRepository
    {
        public IDataReader SelectAll()
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                //new SqlParameter("@iTopicId", SqlDbType.Int){ Value = topicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_Topic_SelectAll", parameters.ToArray());
            return data;
        }

        public IDataReader SelectOne(int topicId)
        {
            var parameter = new SqlParameter("@iTopicId", SqlDbType.Int) { Value = topicId };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_Topic_SelectOne", parameter);
        }

        public string Insert(DM_Topic entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iTopicId", SqlDbType.Int){ Value = ParameterDirection.Output},
                new SqlParameter("@iIdCha", SqlDbType.Int) { Value = entity.IdCha},
                new SqlParameter("@sTopicName", SqlDbType.NVarChar, 200) { Value = entity.TopicName},
                new SqlParameter("@sNote", SqlDbType.NVarChar, 500) { Value = entity.Note},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_Topic_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string Update(DM_Topic entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iTopicId", SqlDbType.Int){ Value = entity.TopicId},
                new SqlParameter("@iIdCha", SqlDbType.Int) { Value = entity.IdCha},
                new SqlParameter("@sTopicName", SqlDbType.NVarChar, 200) { Value = entity.TopicName},
                new SqlParameter("@sNote", SqlDbType.NVarChar, 500) { Value = entity.Note},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_Topic_Update", parameters.ToArray());
            return entity.TopicId.ToString();
        }

        public string Delete(int iTopicId)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@iTopicId", SqlDbType.Int){ Value = iTopicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_Topic_Delete", parameters.ToArray());
            return iTopicId.ToString();
        }
    }
}