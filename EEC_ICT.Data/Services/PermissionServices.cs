using System.Collections.Generic;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;

namespace EEC_ICT.Data.Services
{
    public class PermissionServices
    {
        private static PermissionRepository rep = new PermissionRepository();

        public static List<Permission> SelectByRoleId(int roleId)
        {
            var dr = rep.SelectByRoleId(roleId);
            return SqlHelper.GetList<Permission>(dr);
        }
        public static List<Permission> SelectByUserId(string userId)
        {
            var dr = rep.SelectByUserId(userId);
            return SqlHelper.GetList<Permission>(dr);
        }
        public static void Insert( Permission permission)
        {
           rep.Insert(permission);
        }
        public static void DeleteWithRoleId(int roleId)
        {
            rep.DeleteByRoleId(roleId);
        }
    }
}