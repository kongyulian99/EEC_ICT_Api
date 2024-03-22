using System;

namespace EEC_ICT.Data.Models
{
    public class Logs
    {
        public long Id_logs { get; set; }
        public DateTime NgayLogs { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public int TotalRows { get; set; }
    }
}