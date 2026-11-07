using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewThongTinNhanVien
{
    public string MaNhanVien { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public byte[] LuongCoBan { get; set; } = null!;

    public byte[] PhuCap { get; set; } = null!;
}
