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
    public class QuestionResultsServices
    {
        private static QuestionResultsRepository rep = new QuestionResultsRepository();

        public static List<QuestionResults> SelectAll()
        {
            return SqlHelper.GetList<QuestionResults>(rep.SelectAll());
        }

        public static QuestionResults SelectOne(int questionId, string userId)
        {
            return SqlHelper.GetInfo<QuestionResults>(rep.SelectOne(questionId, userId));
        }

        public static string Insert(QuestionResults entity)
        {
            return rep.Insert(entity);
        }

        public static string Update(QuestionResults entity)
        {
            return rep.Update(entity);
        }

        public static string Delete(int questionId)
        {
            return rep.Delete(questionId);
        }

        public static List<AverageScoreAndIdDeThi> SelectAverageScoreByUser(int questionId, string userId)
        {
            return SqlHelper.GetList<AverageScoreAndIdDeThi>(rep.SelectAverageScoreByUser(questionId, userId));
        }
    }
}