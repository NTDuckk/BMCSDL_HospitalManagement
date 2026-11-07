using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Khoa
{
    public string MaKhoa { get; set; } = null!;

    public string TenKhoa { get; set; } = null!;

    public virtual ICollection<Bacsi> Bacsis { get; set; } = new List<Bacsi>();

    public virtual ICollection<ChitietKhambenh> ChitietKhambenhs { get; set; } = new List<ChitietKhambenh>();

    public virtual ICollection<Phongkham> Phongkhams { get; set; } = new List<Phongkham>();
}
