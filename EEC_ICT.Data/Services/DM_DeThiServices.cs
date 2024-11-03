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
    public class DM_DeThiServices
    {
        private static DM_DeThiRepository rep = new DM_DeThiRepository();

        public static List<DM_DeThi> SelectAll()
        {
            return SqlHelper.GetList<DM_DeThi>(rep.SelectAll());
        }

        public static DM_DeThi SelectOne(int questionId)
        {
            return SqlHelper.GetInfo<DM_DeThi>(rep.SelectOne(questionId));
        }

        public static string Insert(DM_DeThi entity)
        {
            return rep.Insert(entity);
        }

        //public static string CheckCorrect(DM_DapAnCheckCorrect entity)
        //{
        //    return rep.CheckCorrect(entity);
        //}

        public static string Update(DM_DeThi entity)
        {
            return rep.Update(entity);
        }

        public static string Delete(int questionId)
        {
            return rep.Delete(questionId);
        }
    }
}