using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEC_ICT.Data.Services
{
    public class DM_TopicServices
    {
        private static DM_TopicRepository rep = new DM_TopicRepository();

        public static List<DM_Topic> SelectAll()
        {
            return SqlHelper.GetList<DM_Topic>(rep.SelectAll());
        }

        public static DM_Topic SelectOne(int questionId)
        {
            return SqlHelper.GetInfo<DM_Topic>(rep.SelectOne(questionId));
        }

        public static string Insert(DM_Topic entity)
        {
            return rep.Insert(entity);
        }

        public static string Update(DM_Topic entity)
        {
            return rep.Update(entity);
        }

        public static string Delete(int questionId)
        {
            return rep.Delete(questionId);
        }
    }
}