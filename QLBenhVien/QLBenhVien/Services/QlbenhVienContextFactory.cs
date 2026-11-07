using Microsoft.EntityFrameworkCore;
using QLBenhVien.Entities;

namespace QLBenhVien.Services
{
    public static class QlbenhVienContextFactory
    {
        public static QlbenhVienContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QlbenhVienContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new QlbenhVienContext(optionsBuilder.Options);
        }
    }
}
