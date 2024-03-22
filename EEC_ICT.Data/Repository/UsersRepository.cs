using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EEC_ICT.Data.Repository
{
    public class UsersRepository
    {
        private string storep = "pr_Users_";
        public IDataReader LogIn(string userName)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserName", SqlDbType.NVarChar, 256) {Value = userName},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, storep+"Login", parameters.ToArray());
        }

        public IDataReader SelectAll()
        {
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, storep+"SelectAll");
        }
        public IDataReader SelectAllByPermission(string userId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar, 128) {Value = userId}
            };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, storep+"SelectAllByPermission", parameters.ToArray());
        }

        public IDataReader SelectByDonViAndNhom(string madonvi, int role, string keyword, int pageindex, int pagesize)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sMaDonVi", SqlDbType.NVarChar, 50) {Value = madonvi},
                new SqlParameter("@iRole", SqlDbType.Int) {Value = role},
                new SqlParameter("@sKeyword", SqlDbType.NVarChar, 500) {Value = keyword},
                new SqlParameter("@iPageIndex", SqlDbType.Int) {Value = pageindex},
                new SqlParameter("@iPageSize", SqlDbType.Int) {Value = pagesize},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, storep + "SelectByDonViAndNhom", parameters.ToArray());
        }

        public IDataReader SelectOne(string id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sId", SqlDbType.NVarChar, 128) {Value = id},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            return SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, storep+"SelectOne", parameters.ToArray());
        }

        public string Insert(User entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = entity.UserId},
                new SqlParameter("@sFullName", SqlDbType.NVarChar, 256) {Value = entity.FullName},
                new SqlParameter("@sAddress", SqlDbType.NVarChar, 256) {Value = entity.Address},
                new SqlParameter("@sAvatar", SqlDbType.VarChar, 256) {Value = entity.Avatar},
                new SqlParameter("@sEmail", SqlDbType.NVarChar, 256) {Value = entity.Email},
                new SqlParameter("@bStatus", SqlDbType.Bit) {Value = entity.Status},
                new SqlParameter("@bGender", SqlDbType.Bit) {Value = entity.Gender},
                new SqlParameter("@daBirthDay", SqlDbType.DateTime) {Value = entity.BirthDay},
                new SqlParameter("@daCreatedDate", SqlDbType.DateTime) {Value = DateTime.Now},
                new SqlParameter("@sPhoneNumber", SqlDbType.NVarChar, 50) {Value = entity.PhoneNumber},
                new SqlParameter("@sUserName", SqlDbType.NVarChar, 256) {Value = entity.UserName},
                new SqlParameter("@sPassword", SqlDbType.NVarChar, 128) {Value = entity.Password},
                 new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Insert", parameters.ToArray());
            return parameters[0].Value.ToString();
        }
        public int CheckUserName(string userId, string username)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Value", SqlDbType.TinyInt) { Direction = ParameterDirection.Output },
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = userId},
                new SqlParameter("@sUserName", SqlDbType.NVarChar, 256) {Value = username}
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"CheckUserName", parameters.ToArray());
            return Convert.ToInt32(parameters[0].Value);
        }
        public string Update(User entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = entity.UserId},
                new SqlParameter("@sFullName", SqlDbType.NVarChar, 256) {Value = entity.FullName},
                new SqlParameter("@sAddress", SqlDbType.NVarChar, 256) {Value = entity.Address},
                new SqlParameter("@sAvatar", SqlDbType.VarChar, 256) {Value = entity.Avatar},
                new SqlParameter("@sEmail", SqlDbType.NVarChar, 256) {Value = entity.Email},
                new SqlParameter("@bStatus", SqlDbType.Bit) {Value = entity.Status},
                new SqlParameter("@bGender", SqlDbType.Bit) {Value = entity.Gender},
                new SqlParameter("@dBirthDay", SqlDbType.DateTime) {Value = entity.BirthDay},
                new SqlParameter("@sPhoneNumber", SqlDbType.NVarChar, 50) {Value = entity.PhoneNumber},
                new SqlParameter("@sUserName", SqlDbType.NVarChar, 256) {Value = entity.UserName},
                //new SqlParameter("@sMaDonVi", SqlDbType.NVarChar, 50) { Value = entity.MaDonVi },
                 new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Update", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string UpdateCommonInfo(User entity)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = entity.UserId},
                new SqlParameter("@sFullName", SqlDbType.NVarChar, 256) {Value = entity.FullName},
                new SqlParameter("@sAddress", SqlDbType.NVarChar, 256) {Value = entity.Address},
                new SqlParameter("@sEmail", SqlDbType.NVarChar, 256) {Value = entity.Email},
                new SqlParameter("@daBirthDay", SqlDbType.DateTime) {Value = entity.BirthDay},
                new SqlParameter("@sPhoneNumber", SqlDbType.NVarChar, 50) {Value = entity.PhoneNumber},
                new SqlParameter("@bGender", SqlDbType.Bit) {Value = entity.Gender},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"UpdateCommonInfo", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public string UpdatePassword(string userId, string password)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = userId},
                new SqlParameter("@sPassword", SqlDbType.NVarChar, 128) {Value = password},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"UpdatePassword", parameters.ToArray());
            return parameters[0].Value.ToString();
        }

        public IDataReader CheckPassword(string userId, string password)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = userId},
                new SqlParameter("@sPassword", SqlDbType.NVarChar, 128) {Value = password},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            var dataReader = SqlHelper.ExecuteReader(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure, storep+"CheckPassword", parameters.ToArray());
            return dataReader;
        }

        public string Delete(string id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sUserId", SqlDbType.NVarChar,128) {Value = id},
                new SqlParameter("@iErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            SqlHelper.ExecuteNonQuery(CommonFunctions.GetConnectionString(), CommandType.StoredProcedure,
                storep+"Delete", parameters.ToArray());
            return id;
        }
    }
}