using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Models;

namespace QLBenhVien.Entities;

public partial class QlbenhVienAccountContext : DbContext
{
    public QlbenhVienAccountContext()
    {
    }

    public QlbenhVienAccountContext(DbContextOptions<QlbenhVienAccountContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<ViewQuyen> ViewQuyens { get; set; }

    public virtual DbSet<ViewQuyenTk> ViewQuyenTks { get; set; }

    public virtual DbSet<ViewTaiKhoan> ViewTaiKhoans { get; set; }
    public virtual DbSet<AccountInfo> AccountInfos { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.; Database=QLBenhVien_ACCOUNT; Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountInfo>(entity =>
        {
            entity.HasNoKey(); // Vì là kết quả từ stored procedure
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.TypeID).HasColumnName("typeID");
            entity.Property(e => e.MaNhanVien).HasColumnName("maNhanVien");
            entity.Property(e => e.PermissionID).HasColumnName("permissionID");
        });
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__ACCOUNT__F3DBC57398F06A76");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.HasKey)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("0")
                .HasColumnName("hasKey");
            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maNhanVien");
            entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");
            entity.Property(e => e.TypeId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("typeID");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Usernames)
                .UsingEntity<Dictionary<string, object>>(
                    "AccountPermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ACCOUNT_P__permi__10566F31"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ACCOUNT_P__usern__0F624AF8"),
                    j =>
                    {
                        j.HasKey("Username", "PermissionId").HasName("PK__ACCOUNT___2E59D664FFDEFED5");
                        j.ToTable("ACCOUNT_PERMISSION");
                        j.IndexerProperty<string>("Username")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("username");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("permissionID");
                    });
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__PERMISSI__D821317C3EC7179B");

            entity.ToTable("PERMISSION");

            entity.HasIndex(e => e.PermissionName, "UQ__PERMISSI__70661EFCA9DA8547").IsUnique();

            entity.Property(e => e.PermissionId).HasColumnName("permissionID");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .HasColumnName("permissionName");
        });

        modelBuilder.Entity<ViewQuyen>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewQuyen");

            entity.Property(e => e.PermissionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("permissionID");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .HasColumnName("permissionName");
        });

        modelBuilder.Entity<ViewQuyenTk>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewQuyenTK");

            entity.Property(e => e.PermissionId).HasColumnName("permissionID");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .HasColumnName("permissionName");
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<ViewTaiKhoan>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewTaiKhoan");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.HasKey)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("hasKey");
            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maNhanVien");
            entity.Property(e => e.TypeId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("typeID");
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
