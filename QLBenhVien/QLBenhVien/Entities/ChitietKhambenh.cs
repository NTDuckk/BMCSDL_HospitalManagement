using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ChitietKhambenh
{
    public int Id { get; set; }

    public int? MaKhamBenh { get; set; }

    public string? MaBacSiYeuCau { get; set; }

    public string? MaBacSiKham { get; set; }

    public string? MaPhongKham { get; set; }

    public string? MaKhoa { get; set; }

    public int? MaDichVu { get; set; }

    public DateTime? ThoiGianKham { get; set; }

    public byte[]? GhiChuBacSiKham { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChitietHoadonKhamchuabenh> ChitietHoadonKhamchuabenhs { get; set; } = new List<ChitietHoadonKhamchuabenh>();

    public virtual Bacsi? MaBacSiKhamNavigation { get; set; }

    public virtual Bacsi? MaBacSiYeuCauNavigation { get; set; }

    public virtual Dichvu? MaDichVuNavigation { get; set; }

    public virtual Khambenh? MaKhamBenhNavigation { get; set; }

    public virtual Khoa? MaKhoaNavigation { get; set; }

    public virtual Phongkham? MaPhongKhamNavigation { get; set; }
}
