using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class DanhsachBenhnhan
{
    public string MaBenhNhan { get; set; } = null!;

    public string MaPhongKham { get; set; } = null!;

    public int? MaKhamBenh { get; set; }

    public string? TinhTrang { get; set; }

    public DateTime NgayKham { get; set; }

    public virtual Benhnhan MaBenhNhanNavigation { get; set; } = null!;

    public virtual Khambenh? MaKhamBenhNavigation { get; set; }

    public virtual Phongkham MaPhongKhamNavigation { get; set; } = null!;
}
