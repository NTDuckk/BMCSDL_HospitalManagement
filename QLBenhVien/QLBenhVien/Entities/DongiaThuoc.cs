using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class DongiaThuoc
{
    public int Id { get; set; }

    public string MaThuoc { get; set; } = null!;

    public byte[]? DonGia { get; set; }

    public DateOnly NgayApDung { get; set; }

    public virtual Thuoc MaThuocNavigation { get; set; } = null!;
}
