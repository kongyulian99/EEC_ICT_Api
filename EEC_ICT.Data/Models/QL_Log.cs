using System;

namespace EEC_ICT.Data.Models
{
    public class QL_Log
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime NgayDangNhap { get; set; }
        public DateTime NgayDangXuat { get; set; }
    }
}