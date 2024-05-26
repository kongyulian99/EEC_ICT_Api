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
    public class DM_DapAnServices
    {
        private static DM_DapAnRepository rep = new DM_DapAnRepository();

        public static List<DM_DapAn> SelectAllWQuestionId(int questionId)
        {
            return SqlHelper.GetList<DM_DapAn>(rep.SelectAllWQuestionId(questionId));
        }

        public static string Insert(DM_DapAn entity)
        {
            return rep.Insert(entity);
        }
        public static string CheckCorrect(DM_DapAnCheckCorrect entity)
        {
            return rep.CheckCorrect(entity);
        }

        public static string CheckCorrect(DM_DapAnCheckCorrect entity)
        {
            return rep.CheckCorrect(entity);
        }

        public static string Update(DM_DapAn entity)
        {
            return rep.Update(entity);
        }

        public static string Delete(int questionId)
        {
            return rep.Delete(questionId);
        }
    }
}