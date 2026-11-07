using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Dieuphoi
{
    public int Stt { get; set; }

    public string? MaBenhNhan { get; set; }

    public string? MaPhongKham { get; set; }

    public string? MaBacSi { get; set; }

    public int? MaDichVu { get; set; }

    public string? MaKhoa { get; set; }

    public DateTime? Thoigian { get; set; }

    public virtual Bacsi? MaBacSiNavigation { get; set; }

    public virtual Benhnhan? MaBenhNhanNavigation { get; set; }

    public virtual Dichvu? MaDichVuNavigation { get; set; }

    public virtual Khoa? MaKhoaNavigation { get; set; }

    public virtual Phongkham? MaPhongKhamNavigation { get; set; }

    public virtual ICollection<Taivu> Taivus { get; set; } = new List<Taivu>();
}
