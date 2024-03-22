namespace EEC_ICT.Data.Interfaces
{
    public interface IDM_DiaDiemKiemTra
    {
        string MaDiaDiemKiemTra { get; set; }
        string TenDiaDiemKiemTra { get; set; }
        string DiaChi { get; set; }
        int ViTri { get; set; }
        string NguoiDaiDien { get; set; }
        string GhiChu { get; set; }
    }
}