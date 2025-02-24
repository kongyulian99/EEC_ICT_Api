using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEC_ICT.Data.Models
{
    public class DM_CauHoi
    {
        public int QuestionId { get; set; }
        public long IdDeThi { get; set; }
        public string Question { get; set; }
        public string Choices { get; set; }
        
        //public int CorrectAnswerId { get; set; }
        public int TopicId { get; set; }
        public string Note { get; set; }
        public QuestionType QuestionType { get; set; }
        public float TrongSo { get; set; }
        //public List<D> ChoiceList { get; set; }
        //public List<DM_DapAn> ChoiceList_Delete { get; set; }
    }

    public class MultipleChoiceModel
    {
        public int? Id { get; set; }
        public bool Check { get; set; }
    }

    public class FillInBlankModel
    {
        public int? Id { get; set; }
        public string Answer { get; set; }
    }

    //public enum QuestionType
    //{
    //    MULTIPLE_CHOICE = 1,
    //    FILL_IN_BLANK = 2,
    //    MATCHING = 3,
    //}
}