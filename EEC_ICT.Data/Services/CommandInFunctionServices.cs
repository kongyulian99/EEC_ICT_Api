using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;

namespace EEC_ICT.Data.Services
{
    public class CommandInFunctionServices
    {
        private static CommandInFunctionRepository rep = new CommandInFunctionRepository();

        public static Command SelectOne(string commandId, string functionId)
        {
            var dr = rep.SelectOne(commandId, functionId);
            return SqlHelper.GetInfo<Command>(dr);
        }

        public static void Insert(CommandInFunction commandInFunction)
        {
            rep.Insert(commandInFunction);
        }
        public static void Delete(CommandInFunction commandInFunction)
        {
            rep.Delete(commandInFunction);
        }
    }
}