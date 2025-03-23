using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEC_ICT.Data.Models
{
    public class QuestionResults
    {
        public int QuestionId { get; set; }
        public bool Result { get; set; }
        public string UserId { get; set; }

    }

    public class QuestionResultsByTopic
    {
        public int QuestionId { get; set; }
        public bool Result { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string UserId { get; set; }
        public double AverageScore { get; set; }

    }
}