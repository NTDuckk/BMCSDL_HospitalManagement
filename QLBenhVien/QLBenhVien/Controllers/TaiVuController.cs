using Microsoft.AspNetCore.Mvc;
using QLBenhVien.Entities;
using QLBenhVien.Services;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using X.PagedList.Extensions;
using System.Data;

namespace QLBenhVien.Controllers
{
    [Authorize]
    public class TaiVuController : Controller
    {
        private readonly DynamicConnectionProvider _connProvider;
        private readonly KeyVaultService _keyVaultService;
        public TaiVuController(
            DynamicConnectionProvider connProvider,
            KeyVaultService keyVaultService)
        {
            _connProvider = connProvider;
            _keyVaultService = keyVaultService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var context = QlbenhVienContextFactory.Create(connStr);

            var allRecords = await context.ViewTaiVus
                .FromSqlRaw("EXEC sp_viewTaiVu")
                .ToListAsync();

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var pagedList = allRecords.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
        }

        [HttpPost]
        public async Task<IActionResult> ThanhToan(int maKhamBenh)
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();

            var maNhanVien = User.Claims.FirstOrDefault(c => c.Type == "MaNhanVien")?.Value;
         
            var certName = await _keyVaultService.GetSecretAsync("CertQLTV");

            using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

     
            using (var cmd = new SqlCommand("sp_TaoHoaDonKCB", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maKhamBenh", maKhamBenh);
                cmd.Parameters.AddWithValue("@nhanVienThu", maNhanVien);
                cmd.Parameters.AddWithValue("@certName", certName); 
                await cmd.ExecuteNonQueryAsync();
            }

            using var context = QlbenhVienContextFactory.Create(connStr);
            var chiTietHoaDon = await context.ViewChiTietHoaDonTaiVus
                .FromSqlRaw("EXEC sp_XemChiTietHoaDonTheoKhamBenh @maKhamBenh, @certName",
                    new SqlParameter("@maKhamBenh", maKhamBenh),
                    new SqlParameter("@certName", certName))
                .ToListAsync();


            var hoaDonModel = new HoaDon
            {
                MaHoaDon = chiTietHoaDon.FirstOrDefault()?.MaHoaDon ?? 0
            };

            ViewBag.ChiTietHoaDon = chiTietHoaDon;
            ViewBag.TotalAmount = chiTietHoaDon.Sum(x => x.DonGia);

            return View("ThanhToan", hoaDonModel);
        }


        [HttpPost]
        public async Task<IActionResult> XacNhanThanhToan(HoaDon model)
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();

            try
            {
                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();

                using var cmd = new SqlCommand("sp_CapNhatThanhToanHoaDon", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@maHoaDon", model.MaHoaDon);
                cmd.Parameters.Add(new SqlParameter("@soTienNhan", SqlDbType.Decimal)
                {
                    Precision = 15,
                    Scale = 2,
                    Value = model.SoTienNhan
                });
                cmd.Parameters.Add(new SqlParameter("@soTienThoi", SqlDbType.Decimal)
                {
                    Precision = 15,
                    Scale = 2,
                    Value = model.SoTienThoi
                });

                await cmd.ExecuteNonQueryAsync();
                TempData["Success_ThanhToanHDKCB"] = "Thanh toán thành công.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
