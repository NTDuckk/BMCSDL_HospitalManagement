using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Thanhvien
{
    public int Id { get; set; }

    public int? Luong { get; set; }

    public byte[]? EncryptLuong { get; set; }
}
