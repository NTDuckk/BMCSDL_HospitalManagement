using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Thuoc
{
    public string MaThuoc { get; set; } = null!;

    public string TenThuoc { get; set; } = null!;

    public string? DonViTinh { get; set; }

    public string? ThongTin { get; set; }

    public int SoLuongTon { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual ICollection<ChitietHoadonThuoc> ChitietHoadonThuocs { get; set; } = new List<ChitietHoadonThuoc>();

    public virtual ICollection<DongiaThuoc> DongiaThuocs { get; set; } = new List<DongiaThuoc>();
}
