using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Bacsi
{
    public string MaBacSi { get; set; } = null!;

    public string MaKhoa { get; set; } = null!;

    public string? ChuyenMon { get; set; }

    public virtual ICollection<ChitietKhambenh> ChitietKhambenhMaBacSiKhamNavigations { get; set; } = new List<ChitietKhambenh>();

    public virtual ICollection<ChitietKhambenh> ChitietKhambenhMaBacSiYeuCauNavigations { get; set; } = new List<ChitietKhambenh>();

    public virtual ICollection<Khambenh> Khambenhs { get; set; } = new List<Khambenh>();

    public virtual ThongtinCanhan MaBacSiNavigation { get; set; } = null!;

    public virtual Khoa MaKhoaNavigation { get; set; } = null!;
}
