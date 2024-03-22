using System.Collections.Generic;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;

namespace EEC_ICT.Data.Services
{
    public class CommandServices
    {
        private static CommandRepository rep = new CommandRepository();

        public static List<Command> SelectAll()
        {
            var dr = rep.SelectAll();
            return SqlHelper.GetList<Command>(dr);
        }

        public static Command SelectOne(string id)
        {
            var dr = rep.SelectOne(id);
            return SqlHelper.GetInfo<Command>(dr);
        }

        public static string Insert(Command entity)
        {
            return rep.Insert(entity);
        }

        public static string Update(Command entity)
        {
            return rep.Update(entity);
        }

        public static string Delete(string id)
        {
            return rep.Delete(id);
        }
        public static List<Command> SelectByFunctionId(string functionId)
        {
            var dr = rep.SelectByFunctionId(functionId);
            return SqlHelper.GetList<Command>(dr);
        }
    }
}