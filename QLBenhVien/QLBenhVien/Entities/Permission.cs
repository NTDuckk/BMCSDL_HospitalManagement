using System;
using System.Collections.Generic;

namespace QLBenhVien.Entities;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string? PermissionName { get; set; }

    public virtual ICollection<Account> Usernames { get; set; } = new List<Account>();
}
