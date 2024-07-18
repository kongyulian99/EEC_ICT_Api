using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEC_ICT.Data.Models
{
    public class TestResult
    {
        //public int AnswerId { get; set; }
        public long Id { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public bool Result { get; set; }
        public DateTime TestDate { get; set; }
    }
}