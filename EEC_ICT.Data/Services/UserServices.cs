using System.Collections.Generic;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;

namespace EEC_ICT.Data.Services
{
    public class UserServices
    {
        private static UsersRepository rep = new UsersRepository();
        public static User Login(string userName, string password)
        {
            var dr = rep.LogIn(userName);
            User result = null;
            if (dr != null) result = SqlHelper.GetInfo<User>(dr);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result.Password == password ? result :  null;
            }
        }
        public static List<User> SelectAll()
        {
            var dr = rep.SelectAll();
            return SqlHelper.GetList<User>(dr);
        }
        public static List<User> SelectAllByPermission(string id)
        {
            var dr = rep.SelectAllByPermission(id);
            return SqlHelper.GetList<User>(dr);
        }

        public static List<User> SelectByDonViAndNhom(string madonvi, int role, string keyword, int pageindex, int pagesize)
        {
            var dr = rep.SelectByDonViAndNhom(madonvi, role, keyword, pageindex, pagesize);
            return SqlHelper.GetList<User>(dr);
        }

        public static User SelectOne(string id)
        {
            var dr = rep.SelectOne(id);
            return SqlHelper.GetInfo<User>(dr);
        }
        public static int CheckUserName(string id, string username)
        {
            return rep.CheckUserName(id, username);
        }

        public static string Insert(User entity)
        {
            //entity.Password = CommonFunctions.ToMD5(entity.Password);
            return rep.Insert(entity);
        }

        public static string Update(User entity)
        {
            return rep.Update(entity);
        }

        public static string UpdatePassword(string userId, string password)
        {
            //var passwordHash = CommonFunctions.ToMD5(password);
            return rep.UpdatePassword(userId, password);
        }

        public static bool CheckPassword(string userId, string password)
        {
            //var passwordHash = CommonFunctions.ToMD5(password);
            return SqlHelper.GetList<User>(rep.CheckPassword(userId, password)).Count == 1;
        }

        public static string UpdateCommonInfo(User entity)
        {
            return rep.UpdateCommonInfo(entity);
        }


        public static string Delete(string id)
        {
            return rep.Delete(id);
        }
    }
}