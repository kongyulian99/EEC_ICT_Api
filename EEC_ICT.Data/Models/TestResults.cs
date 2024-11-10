using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEC_ICT.Data.Models
{
    public class TestResults
    {
        //public int AnswerId { get; set; }
        public long Id { get; set; }
        public string UserId { get; set; }
        public long IdDeThi { get; set; }
        public float Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}