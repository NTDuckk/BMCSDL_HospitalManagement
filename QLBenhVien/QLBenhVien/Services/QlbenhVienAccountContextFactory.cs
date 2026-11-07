using Microsoft.EntityFrameworkCore;
using QLBenhVien.Entities;

public static class QlbenhVienAccountContextFactory
{
    public static QlbenhVienAccountContext Create(string connStr)
    {
        var optionsBuilder = new DbContextOptionsBuilder<QlbenhVienAccountContext>();
        optionsBuilder.UseSqlServer(connStr);
        return new QlbenhVienAccountContext(optionsBuilder.Options);
    }
}
