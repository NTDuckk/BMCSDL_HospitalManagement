using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class DongiaDichvu
{
    public int Id { get; set; }

    public int MaDichVu { get; set; }

    public byte[]? DonGia { get; set; }

    public DateOnly NgayApDung { get; set; }

    public virtual Dichvu MaDichVuNavigation { get; set; } = null!;
}
