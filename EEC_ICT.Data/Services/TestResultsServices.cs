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
    public class TestResultsServices
    {
        private static TestResultRepository rep = new TestResultRepository();

        public static List<TestResults> SelectAll()
        {
            return SqlHelper.GetList<TestResults>(rep.SelectAll());
        }

        public static TestResults SelectByUser(string userId)
        {
            return SqlHelper.GetInfo<TestResults>(rep.SelectByUser(userId));
        }

        public static string Insert(TestResults entity)
        {
            return rep.Insert(entity);
        }

        public static List<MaxScoreAndIdDeThi> SelectBestScoreByUser(string userId)
        {
            return SqlHelper.GetList<MaxScoreAndIdDeThi>(rep.SelectBestScoreByUser(userId));
        }

        public static List<MinScoreAndIdDeThi> SelectMinScoreByUser(string userId)
        {
            return SqlHelper.GetList<MinScoreAndIdDeThi>(rep.SelectMinScoreByUser(userId));
        }

        public static List<AverageScoreAndIdDeThi> SelectAverageScoreByUser(string userId)
        {
            return SqlHelper.GetList<AverageScoreAndIdDeThi>(rep.SelectAverageScoreByUser(userId));
        }
    }
}