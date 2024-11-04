using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class DM_DeThiRepository
    {
        public IDataReader SelectAll()
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                //new SqlParameter("@iTopicId", SqlDbType.Int){ Value = topicId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DeThi_SelectAll", parameters.ToArray());
            return data;
        }

        public IDataReader SelectOne(int idDeThi)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@lIdDeThi", SqlDbType.BigInt){ Value = idDeThi},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DeThi_SelectOne", parameters.ToArray());
        }

        public string Insert(DM_DeThi entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@lIdDeThi", SqlDbType.BigInt){ Direction = ParameterDirection.Output},
                new SqlParameter("@sTenDeThi", SqlDbType.NVarChar, 250) { Value = entity.TenDeThi},
                new SqlParameter("@sGhiChu", SqlDbType.NVarChar, 500) { Value = entity.GhiChu},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DeThi_Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string Update(DM_DeThi entity)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@lIdDeThi", SqlDbType.Int){ Value = entity.IdDeThi},
                new SqlParameter("@sTenDeThi", SqlDbType.NVarChar, 250) { Value = entity.TenDeThi},
                new SqlParameter("@sGhiChu", SqlDbType.NVarChar, 500) { Value = entity.GhiChu},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DeThi_Update", parameters.ToArray());
            return entity.IdDeThi.ToString();
        }

        public string Delete(int idDeThi)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@lIdDeThi", SqlDbType.Int){ Value = idDeThi},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_DM_DeThi_Delete", parameters.ToArray());
            return idDeThi.ToString();
        }
    }
}