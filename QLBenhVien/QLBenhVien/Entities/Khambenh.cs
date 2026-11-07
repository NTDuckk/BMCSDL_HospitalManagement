using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Khambenh
{
    public int MaKhamBenh { get; set; }

    public string MaBenhNhan { get; set; } = null!;

    public string? MaBacSi { get; set; }

    public DateTime? NgayKham { get; set; }

    public DateTime? NgayTaiKham { get; set; }

    public byte[]? TrieuChung { get; set; }

    public byte[]? ChanDoanCuoiCung { get; set; }

    public virtual ICollection<ChitietKhambenh> ChitietKhambenhs { get; set; } = new List<ChitietKhambenh>();

    public virtual ICollection<DanhsachBenhnhan> DanhsachBenhnhans { get; set; } = new List<DanhsachBenhnhan>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual Bacsi? MaBacSiNavigation { get; set; }

    public virtual Benhnhan MaBenhNhanNavigation { get; set; } = null!;

    public virtual ICollection<Toathuoc> Toathuocs { get; set; } = new List<Toathuoc>();
}
