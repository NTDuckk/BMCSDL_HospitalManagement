using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Phongkham
{
    public string MaPhongKham { get; set; } = null!;

    public string? TenPhongKham { get; set; }

    public string? MaKhoa { get; set; }

    public int? MaDichVu { get; set; }

    public virtual ICollection<ChitietKhambenh> ChitietKhambenhs { get; set; } = new List<ChitietKhambenh>();

    public virtual ICollection<DanhsachBenhnhan> DanhsachBenhnhans { get; set; } = new List<DanhsachBenhnhan>();

    public virtual ICollection<LichLamviec> LichLamviecs { get; set; } = new List<LichLamviec>();

    public virtual Dichvu? MaDichVuNavigation { get; set; }

    public virtual Khoa? MaKhoaNavigation { get; set; }
}
