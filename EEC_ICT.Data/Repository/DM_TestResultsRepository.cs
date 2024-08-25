using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class TestResultsRepository
    {
        public IDataReader SelectAll()
        {
            var parameters = new List<SqlParameter>()
            {
                //new SqlParameter("@sFilter", SqlDbType.NVarChar, 300){ Value = filter},
                //new SqlParameter("@iTestResultsId", SqlDbType.Int){ Value = TestResultsId},
                new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
            };
            var data = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_SelectAll", parameters.ToArray());
            return data;
        }

        public IDataReader SelectOne(int TestResultsId)
        {
            var parameter = new SqlParameter("@iTestResultsId", SqlDbType.Int) { Value = TestResultsId };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_SelectOne", parameter);
        }

        //public string Insert(TestResults entity)
        //{
        //    var parameters = new List<SqlParameter>()
        //    {
        //        new SqlParameter("@iTestResultsId", SqlDbType.Int){ Value = ParameterDirection.Output},
        //        new SqlParameter("@sTestResultsName", SqlDbType.NVarChar, 200) { Value = entity.TestResultsName},
        //        new SqlParameter("@sNote", SqlDbType.NVarChar, 500) { Value = entity.Note},
        //        new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
        //    };
        //    SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_Insert", parameters.ToArray());
        //    return parameters[0].Value.ToString();
        //}

        //public string Update(TestResults entity)
        //{
        //    var parameters = new List<SqlParameter>()
        //    {
        //        new SqlParameter("@iTestResultsId", SqlDbType.Int){ Value = entity.TestResultsId},
        //        new SqlParameter("@sTestResultsName", SqlDbType.NVarChar, 200) { Value = entity.TestResultsName},
        //        new SqlParameter("@sNote", SqlDbType.NVarChar, 500) { Value = entity.Note},
        //        new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
        //    };
        //    SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_Update", parameters.ToArray());
        //    return entity.TestResultsId.ToString();
        //}

        //public string Delete(int iTestResultsId)
        //{
        //    var parameters = new List<SqlParameter>()
        //    {
        //        new SqlParameter("@iTestResultsId", SqlDbType.Int){ Value = iTestResultsId},
        //        new SqlParameter("@iErrorCode", SqlDbType.Int) {Direction = ParameterDirection.Output}
        //    };
        //    SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, "pr_TestResults_Delete", parameters.ToArray());
        //    return iTestResultsId.ToString();
        //}
    }
}