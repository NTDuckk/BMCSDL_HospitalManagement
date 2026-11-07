using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewDonGiaDichVu
{
    public int MaDichVu { get; set; }

    public string TenDichVu { get; set; } = null!;

    public byte[]? DonGia { get; set; }

    public DateOnly? NgayApDung { get; set; }
}
