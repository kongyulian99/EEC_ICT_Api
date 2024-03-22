using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;
using System.Collections.Generic;

namespace EEC_ICT.Data.Services
{
    public class RoleServices
    {
        private static RoleRepository rep = new RoleRepository();

        public static List<Role> SelectByUserId(string userId)
        {
            var dr = rep.SelectByUserId(userId);
            return SqlHelper.GetList<Role>(dr);
        }

        public static List<Role> SelectAll()
        {
            var dr = rep.SelectAll();
            return SqlHelper.GetList<Role>(dr);
        }
        public static List<Role> SelectAllByPermission(string id)
        {
            var dr = rep.SelectAllByPermission(id);
            return SqlHelper.GetList<Role>(dr);
        }

        public static Role SelectOne(int id)
        {
            var dr = rep.SelectOne(id);
            return SqlHelper.GetInfo<Role>(dr);
        }
        public static int CheckName(int id, string name)
        {
            return rep.CheckName(id, name);
        }

        public static int Insert(Role entity)
        {
            return rep.Insert(entity);
        }

        public static int Update(Role entity)
        {
            return rep.Update(entity);
        }

        public static int Delete(int id)
        {
            return rep.Delete(id);
        }
    }
}