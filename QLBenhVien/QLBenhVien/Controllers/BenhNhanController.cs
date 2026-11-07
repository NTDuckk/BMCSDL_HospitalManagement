using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Entities;
using QLBenhVien.Models;
using QLBenhVien.Services;

namespace QLBenhVien.Controllers
{
    public class BenhNhanController : Controller
    {
        private readonly DynamicConnectionProvider _connProvider;

        public BenhNhanController(DynamicConnectionProvider connProvider)
        {
            _connProvider = connProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string connStr = await _connProvider.GetDataConnectionStringAsync();
            using var context = QlbenhVienContextFactory.Create(connStr);

            var khoaList = await context.ViewKhoas
                .FromSqlRaw("EXEC sp_viewKhoa")
                .ToListAsync();

            ViewBag.KhoaList = new SelectList(khoaList, "MaKhoa", "TenKhoa");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BenhNhan model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string connStr = await _connProvider.GetDataConnectionStringAsync();
            using var context = QlbenhVienContextFactory.Create(connStr);

            await context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ThemBenhNhan @hoTen, @gioiTinh, @chieuCao, @canNang, @namSinh, @diaChi, @soDienThoai, @maKhoa",
                new SqlParameter("@hoTen", model.HoTen),
                new SqlParameter("@gioiTinh", model.GioiTinh ?? (object)DBNull.Value),
                new SqlParameter("@chieuCao", model.ChieuCao ?? (object)DBNull.Value),
                new SqlParameter("@canNang", model.CanNang ?? (object)DBNull.Value),
                new SqlParameter("@namSinh", model.NamSinh),
                new SqlParameter("@diaChi", model.DiaChi),
                new SqlParameter("@soDienThoai", model.SoDienThoai),
                new SqlParameter("@maKhoa", model.MaKhoa ?? (object)DBNull.Value)
            );

            return RedirectToAction("Create", new { success = true });
        }
    }
}
