using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewTaiKhoan
{
    public string Username { get; set; } = null!;

    public string? MaNhanVien { get; set; }

    public string? TypeId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? HasKey { get; set; }
}
