using Microsoft.AspNetCore.Mvc;
using QLBenhVien.Entities;
using QLBenhVien.Services;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;
using System.Data;
using X.PagedList.Extensions;

namespace QLBenhVien.Controllers
{
    [Authorize]
    public class DieuPhoiController : Controller
    {
        private readonly DynamicConnectionProvider _connProvider;

        public DieuPhoiController(DynamicConnectionProvider connProvider)
        {
            _connProvider = connProvider;
        }

        public async Task<JsonResult> GetPhongKham(string maKhoa, int maDichVu)
        {
            try
            {
                var connStr = await _connProvider.GetDataConnectionStringAsync();
                using var context = QlbenhVienContextFactory.Create(connStr);

                var phongKhams = await context.ViewPhongKhamDps
                    .FromSqlRaw("EXEC sp_viewPhongKhamDP @maKhoa, @maDichVu",
                        new SqlParameter("@maKhoa", string.IsNullOrWhiteSpace(maKhoa) ? DBNull.Value : maKhoa),
                        new SqlParameter("@maDichVu", maDichVu))
                    .ToListAsync();

                return Json(phongKhams);
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }
        }

        public async Task<IActionResult> Index(int? page)
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var context = QlbenhVienContextFactory.Create(connStr);

            var allDieuPhoi = await context.ViewDieuPhois
                .FromSqlRaw("EXEC sp_DieuPhoi")
                .ToListAsync();

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var pagedList = allDieuPhoi.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
        }

        [HttpPost]
        public async Task<IActionResult> CapNhatMaPhong(int stt, string maPhongKham)
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var connection = new SqlConnection(connStr);
            await connection.OpenAsync();

            using var cmd = new SqlCommand("sp_DieuPhoi_UpdateMaPhong", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@stt", stt);
            cmd.Parameters.AddWithValue("@maPhongKham", maPhongKham);

            await cmd.ExecuteNonQueryAsync();

            TempData["UpdatedStt"] = stt;
            TempData["SelectedPhongKham"] = maPhongKham;

            return RedirectToAction("Index");
        }
    }
}
