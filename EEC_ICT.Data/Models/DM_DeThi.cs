using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEC_ICT.Data.Models
{
    public class DM_DeThi
    {
        public long IdDeThi { get; set; }
        public string TenDeThi { get; set; }
        public string GhiChu { get; set; }
        public List<DM_CauHoi> ListCauHoi { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UserId { get; set; }
    }
}