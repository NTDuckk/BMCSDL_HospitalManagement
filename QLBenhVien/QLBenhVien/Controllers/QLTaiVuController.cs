using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Models;
using QLBenhVien.Services;
using System.Data;
using System.Diagnostics;

namespace QLBenhVien.Controllers
{
    public class QLTaiVuController : Controller
    {
        private readonly DynamicConnectionProvider _connProvider;
        private readonly KeyVaultService _keyVaultService;
        public QLTaiVuController(
            DynamicConnectionProvider connProvider,
            KeyVaultService keyVaultService)
        {
            _connProvider = connProvider;
            _keyVaultService = keyVaultService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ThemDonGiaDV()
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();
            var certName = await _keyVaultService.GetSecretAsync("CertQLTV");

            using var context = QlbenhVienContextFactory.Create(connStr);

            var certParam = new SqlParameter("@certName", certName);

            var list = await context
                .Set<DonGiaDichVuGiaiMa>()
                .FromSqlRaw("EXEC sp_LayDonGiaDichVu @certName", certParam)
                .ToListAsync();

            var dichVuList = list
                .Select(item => new SelectListItem
                {
                    Value = item.MaDichVu.ToString(),
                    Text = $"{item.MaDichVu} - {item.TenDichVu}"
                })
                .DistinctBy(item => item.Value)
                .ToList();

            ViewBag.DichVuList = dichVuList;
            ViewBag.DonGiaJson = System.Text.Json.JsonSerializer.Serialize(list);

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ThemDonGiaDV(DonGiaDichVuGiaiMa model)
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();
            var certName = await _keyVaultService.GetSecretAsync("CertQLTV");
            if (model.DonGia is null || model.DonGia < 0)
            {
                TempData["DonGia_Am"] = "";
                return RedirectToAction("ThemDonGiaDV");
            }
            using (var context = QlbenhVienContextFactory.Create(connStr))
            {
                var donGiaList = await context
                    .Set<DonGiaDichVuGiaiMa>()
                    .FromSqlRaw("EXEC sp_LayDonGiaDichVu @certName", new SqlParameter("@certName", certName))
                    .ToListAsync();

                var existingDates = donGiaList
                    .Where(x => x.NgayApDung.HasValue)
                    .Select(x => x.NgayApDung.Value.Date)
                     .ToList();


                var ngayMoi = model.NgayApDung.Value.Date;

                if (ngayMoi < DateTime.Today)
                {
                    TempData["NgayApDung_KhongPhuHop"] = "";
                    return RedirectToAction("ThemDonGiaDV");
                }

                if (existingDates.Contains(ngayMoi))
                {
                    TempData["NgayApDung_TonTai"] = "";
                    return RedirectToAction("ThemDonGiaDV");
                }
            }


            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_ThemDonGiaDichVu", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@maDichVu", model.MaDichVu);
                    cmd.Parameters.AddWithValue("@donGia", model.DonGia);
                    cmd.Parameters.AddWithValue("@ngayApDung", model.NgayApDung);
                    cmd.Parameters.AddWithValue("@certName", certName);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            TempData["Success_NgayApDung"] = "Thêm đơn giá dịch vụ thành công!";
            return RedirectToAction("ThemDonGiaDV");
        }

        [HttpGet]
        public async Task<IActionResult> XoaDonGiaDV()
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();
            var certName = await _keyVaultService.GetSecretAsync("CertQLTV");

            using var context = QlbenhVienContextFactory.Create(connStr);
            var certParam = new SqlParameter("@certName", certName);

            var list = await context
                .Set<DonGiaDichVuGiaiMa>()
                .FromSqlRaw("EXEC sp_LayDonGiaDichVu @certName", certParam)
                .ToListAsync();

            var dichVuList = list
                .Select(item => new SelectListItem
                {
                    Value = item.MaDichVu.ToString(),
                    Text = $"{item.MaDichVu} - {item.TenDichVu}"
                })
                .DistinctBy(item => item.Value)
                .ToList();

            ViewBag.DichVuList = dichVuList;
            ViewBag.DonGiaJson = System.Text.Json.JsonSerializer.Serialize(list);

            return View();
        }

        [HttpPost]
        [ActionName("XoaDonGiaDV")]
        public async Task<IActionResult> XoaDonGiaDV_Post(int MaDichVu, DateTime NgayApDung, int DonGiaCount)
        {
            Console.WriteLine($"[DEBUG] MaDichVu = {MaDichVu}, NgayApDung = {NgayApDung:yyyy-MM-dd}, Count = {DonGiaCount}");

            if (NgayApDung == DateTime.MinValue)
            {
                TempData["XoaDonGia_Error"] = "";
                return RedirectToAction("XoaDonGiaDV");
            }

            if (DonGiaCount <= 1)
            {
                TempData["XoaDonGia_Error_SL"] = "";
                return RedirectToAction("XoaDonGiaDV");
            }

            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            using var cmd = new SqlCommand("sp_XoaDonGiaDichVu", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@maDichVu", MaDichVu);
            cmd.Parameters.AddWithValue("@ngayApDung", NgayApDung);

            await cmd.ExecuteNonQueryAsync();

            TempData["XoaDonGia_Success"] = "";
            return RedirectToAction("XoaDonGiaDV");
        }


    }
}
