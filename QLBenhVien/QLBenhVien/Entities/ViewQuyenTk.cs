using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class ViewQuyenTk
{
    public string Username { get; set; } = null!;

    public int? PermissionId { get; set; }

    public string? PermissionName { get; set; }
}
