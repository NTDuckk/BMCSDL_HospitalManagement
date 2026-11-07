using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Taivu
{
    public int Stt { get; set; }

    public int? MaDieuPhoi { get; set; }

    public DateTime? Thoigian { get; set; }

    public string? TinhTrang { get; set; }

    public virtual Dieuphoi? MaDieuPhoiNavigation { get; set; }
}
