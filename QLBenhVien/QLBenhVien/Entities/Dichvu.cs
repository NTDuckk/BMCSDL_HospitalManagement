using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Dichvu
{
    public int MaDichVu { get; set; }

    public string TenDichVu { get; set; } = null!;

    public string? TypeId { get; set; }

    public virtual ICollection<ChitietKhambenh> ChitietKhambenhs { get; set; } = new List<ChitietKhambenh>();

    public virtual ICollection<DongiaDichvu> DongiaDichvus { get; set; } = new List<DongiaDichvu>();

    public virtual ICollection<Phongkham> Phongkhams { get; set; } = new List<Phongkham>();
}
