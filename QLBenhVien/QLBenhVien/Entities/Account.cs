using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Account
{
    public string Username { get; set; } = null!;

    public byte[]? PasswordHash { get; set; }

    public string? MaNhanVien { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? TypeId { get; set; }

    public string? HasKey { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
