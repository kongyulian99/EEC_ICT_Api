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
    public class DM_CauHoiServices
    {
        private static DM_CauHoiRepository rep = new DM_CauHoiRepository();

        public static List<DM_CauHoi> SelectAll()
        {
            return SqlHelper.GetList<DM_CauHoi>(rep.SelectAll());
        }

        public static DM_CauHoi SelectOne(int questionId)
        {
            return SqlHelper.GetInfo<DM_CauHoi>(rep.SelectOne(questionId));
        }

        public static string Insert(DM_CauHoi entity)
        {
            return rep.Insert(entity);
        }

        public static string CheckCorrect(DM_DapAnCheckCorrect entity)
        {
            return rep.CheckCorrect(entity);
        }

        public static string Update(DM_CauHoi entity)
        {
            return rep.Update(entity);
        }

        public static string Delete(int questionId)
        {
            return rep.Delete(questionId);
        }
    }
}