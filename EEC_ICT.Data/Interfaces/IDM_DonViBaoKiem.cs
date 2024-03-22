using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEC_ICT.Data.Interfaces
{
    interface IDM_DonViBaoKiem
    {
        string MaDonViBaoKiem { get; set; }
        string TenDonViBaoKiem { get; set; }
        string DiaChi { get; set; }
        string NguoiDaiDien { get; set; }
        string GhiChu { get; set; }
    }
}
