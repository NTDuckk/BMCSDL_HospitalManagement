using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ThongtinPhongkham
{
    public int SoThuTu { get; set; }

    public string? MaPhongKham { get; set; }

    public string? MaBenhNhan { get; set; }

    public string? TinhTrang { get; set; }

    public virtual Benhnhan? MaBenhNhanNavigation { get; set; }

    public virtual Phongkham? MaPhongKhamNavigation { get; set; }
}
