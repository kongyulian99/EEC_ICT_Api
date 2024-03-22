using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;
using System;
using System.Collections.Generic;

namespace EEC_ICT.Data.Services
{
    public class LogsServices
    {
        private static LogsRepository rep = new LogsRepository();

        public static List<Logs> SelectAll()
        {
            var dr = rep.SelectAll();
            return SqlHelper.GetList<Logs>(dr);
        }

        public static List<Logs> SelectByFilter(string keyword, DateTime tungay, DateTime denngay, int pageindex, int pagesize)
        {
            var dr = rep.SelectByFilter(keyword, tungay, denngay, pageindex, pagesize);
            return SqlHelper.GetList<Logs>(dr);
        }

        public static Logs SelectOne(int id)
        {
            var dr = rep.SelectOne(id);
            return SqlHelper.GetInfo<Logs>(dr);
        }

        public static string Insert(Logs entity)
        {
            return rep.Insert(entity);
        }

        public static long Update(Logs entity)
        {
            return rep.Update(entity);
        }

        public static long Delete(int id)
        {
            return rep.Delete(id);
        }
    }
}