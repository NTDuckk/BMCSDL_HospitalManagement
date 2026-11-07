using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewTaiVu
{
    public long? Stt { get; set; }

    public string MaBenhNhan { get; set; } = null!;

    public string HoTenBenhNhan { get; set; } = null!;

    public string TenDichVu { get; set; } = null!;

    public int MaChiTietKham { get; set; }

    public int MaKhamBenh { get; set; }

    public int? MaDichVu { get; set; }
}
