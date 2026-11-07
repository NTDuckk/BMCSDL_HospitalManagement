using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Toathuoc
{
    public int MaToaThuoc { get; set; }

    public int MaKhamBenh { get; set; }

    public DateTime NgayKe { get; set; }

    public virtual ICollection<ChitietToathuoc> ChitietToathuocs { get; set; } = new List<ChitietToathuoc>();

    public virtual Khambenh MaKhamBenhNavigation { get; set; } = null!;
}
