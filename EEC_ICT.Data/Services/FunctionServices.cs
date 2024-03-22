using System.Collections.Generic;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;

namespace EEC_ICT.Data.Services
{
    public class FunctionServices
    {
        private static FunctionRepository rep = new FunctionRepository();

        public static List<Function> SelectAll()
        {
            var dr = rep.SelectAll();
            return SqlHelper.GetList<Function>(dr);
        }

        public static List<Function> SelectAllActivated()
        {
            var dr = rep.SelectAllActivated();
            return SqlHelper.GetList<Function>(dr);
        }

        public static Function SelectOne(string id)
        {
            var dr = rep.SelectOne(id);
            return SqlHelper.GetInfo<Function>(dr);
        }

        public static string Insert(Function entity)
        {
            return rep.Insert(entity);
        }

        public static string Update(Function entity)
        {
            return rep.Update(entity);
        }

        public static string Delete(string id)
        {
            return rep.Delete(id);
        }

        public static List<FunctionWCommand> GetFunctionWithCommand()
        {
            var dr = rep.GetFunctionWithCommand();
            return SqlHelper.GetList<FunctionWCommand>(dr);
        }
        public static void PostCommandToFunction( string idCommand, string idFunction)
        {
            rep.PostCommandToFunction(idCommand, idFunction);
        }
        public static int CheckFunctionId(string id)
        {
            return rep.CheckFunctionId(id);
        }
        public static void DeleteCommandInFunction(string idCommand, string idFunction)
        {
            rep.DeleteCommandInFunction(idCommand, idFunction);
        }
        public static List<Function> SelectAllWParentId(string id)
        {
            var dr = rep.SelectAllWParentId(id);
            return SqlHelper.GetList<Function>(dr);
        }
    }
}