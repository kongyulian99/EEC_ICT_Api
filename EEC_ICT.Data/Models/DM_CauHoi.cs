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
        public int QuestionType { get; set; }
        //public List<D> ChoiceList { get; set; }
        //public List<DM_DapAn> ChoiceList_Delete { get; set; }
    }
}