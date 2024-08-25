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

        public static TestResults SelectOne(int questionId)
        {
            return SqlHelper.GetInfo<TestResults>(rep.SelectOne(questionId));
        }

        public static string Insert(TestResults entity)
        {
            return rep.Insert(entity);
        }
    }
}