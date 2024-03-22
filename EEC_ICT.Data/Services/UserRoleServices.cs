using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;

namespace EEC_ICT.Data.Services
{
    public class UserRoleServices
    {
        private static UserRoleRepository rep = new UserRoleRepository();

        public static UserRole SelectOne(UserRole userRole)
        {
            var dr = rep.SelectOne(userRole.UserId, userRole.RoleId);
            return SqlHelper.GetInfo<UserRole>(dr);
        }

        public static void Insert(string userId, int roleId)
        {
            rep.Insert(userId,roleId);
        }

        public static void Delete(string userId, int roleId)
        {
            rep.Delete(userId, roleId);
        }
    }
}