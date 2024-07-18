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
    public class TestResultServices
    {
        private static TestResultRepository rep = new TestResultRepository();

        public static List<TestResult> SelectAll()
        {
            return SqlHelper.GetList<TestResult>(rep.SelectAll());
        }

        public static TestResult SelectOne(int questionId)
        {
            return SqlHelper.GetInfo<TestResult>(rep.SelectOne(questionId));
        }

        public static string Insert(TestResult entity)
        {
            return rep.Insert(entity);
        }
    }
}