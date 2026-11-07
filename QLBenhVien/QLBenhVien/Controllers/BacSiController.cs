using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Entities;
using QLBenhVien.Models;
using QLBenhVien.Services;
using System.Security.Claims;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure.Security.KeyVault.Secrets;
using Azure;

namespace QLBenhVien.Controllers
{
    [Authorize]
    public class BacSiController : Controller
    {
        private readonly DynamicConnectionProvider _connProvider;
        private readonly KeyVaultService _keyVaultService;

        public BacSiController(
            DynamicConnectionProvider connProvider,
            KeyVaultService keyVaultService)
        {
            _connProvider = connProvider;
            _keyVaultService = keyVaultService;
        }


        public async Task<IActionResult> Index()
        {
            var maPhongKham = User.FindFirst("MaPhongKham")?.Value;

            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var context = QlbenhVienContextFactory.Create(connStr);

            var param = new SqlParameter("@maPhongKham", maPhongKham);
            var result = await context.ViewBenhNhanTrongNgays
                .FromSqlRaw("EXEC sp_LayBenhNhanDauTienTheoPhong @maPhongKham", param)
                .ToListAsync();

            var benhNhan = result.FirstOrDefault();

            if (benhNhan == null)
            {
                ViewBag.KhongConBenhNhan = true;
                ViewBag.DanhSachDichVu = new SelectList(new List<string>(), "", "");
                return View();
            }


            var maKhamBenh = benhNhan.MaKhamBenh.ToString();

            var identity = (ClaimsIdentity)User.Identity!;
            identity.AddClaim(new Claim("MaKhamBenh", maKhamBenh));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );
            var danhSachDichVu = await LayDichVuXetNghiem();
            ViewBag.DanhSachDichVu = new SelectList(danhSachDichVu, "MaDichVu", "TenDichVu");

            return View(benhNhan);
        }

        [HttpPost]
        public async Task<IActionResult> LuuThongTinKham(IFormCollection form)
        {
            string trieuChung = form["TrieuChung"];
            string chanDoan = form["ChanDoanCuoiCung"];

            if (string.IsNullOrWhiteSpace(trieuChung) || string.IsNullOrWhiteSpace(chanDoan))
            {
                return Json(new { success = false, message = "Vui lòng nhập đủ thông tin." });
            }

            var maKhamBenhClaim = User.FindFirst("MaKhamBenh")?.Value;
            if (string.IsNullOrWhiteSpace(maKhamBenhClaim) || !int.TryParse(maKhamBenhClaim, out int maKhamBenh))
            {
                return Json(new { success = false, message = "Không tìm thấy mã khám bệnh." });
            }

            string username = User.Identity?.Name ?? "";
            string secretName = $"{username}-key";

            string ma, BSCert;
            try
            {
                ma = await _keyVaultService.GetSecretAsync(secretName);
                BSCert = await _keyVaultService.GetSecretAsync("CertificateBS");

                if (string.IsNullOrWhiteSpace(ma))
                {
                    return Json(new
                    {
                        success = false,
                        message = $"Chưa có khóa được tạo cho người dùng '{username}'. Vui lòng tạo trước khi tiếp tục."
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi khi truy cập Key Vault: {ex.Message}" });
            }

            try
            {
                var connStr = await _connProvider.GetDataConnectionStringAsync();
                using var context = new SqlConnection(connStr);
                await context.OpenAsync();

                using var command = new SqlCommand("sp_UpdateKhamBenh_MaHoa", context);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ma", ma);
                command.Parameters.AddWithValue("@BSCert", BSCert);
                command.Parameters.AddWithValue("@maKhamBenh", maKhamBenh);
                command.Parameters.AddWithValue("@trieuChung", trieuChung);
                command.Parameters.AddWithValue("@chanDoanCuoiCung", chanDoan);

                await command.ExecuteNonQueryAsync();
                return Json(new { success = true, message = "Khám bệnh thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi cập nhật thông tin khám: " + ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> LuuToaThuoc(IFormCollection form)
        {
            var maKhamBenhClaim = User.FindFirst("MaKhamBenh")?.Value;
            if (string.IsNullOrWhiteSpace(maKhamBenhClaim) || !int.TryParse(maKhamBenhClaim, out int maKhamBenh))
            {
                return Json(new { success = false, message = "Không tìm thấy mã khám bệnh hợp lệ." });
            }

            string username = User.Identity?.Name ?? "";
            string secretName = $"{username}-key";

            string ma, BSCert;
            try
            {
                ma = await _keyVaultService.GetSecretAsync(secretName);
                BSCert = await _keyVaultService.GetSecretAsync("CertificateBS");

                if (string.IsNullOrWhiteSpace(ma))
                {
                    return Json(new { success = false, message = $"Chưa có khóa được tạo cho người dùng '{username}'." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi khi truy cập Key Vault: {ex.Message}" });
            }

            List<string> tenThuocList = new();
            List<string> soLuongList = new();
            List<string> lieuDungList = new();
            List<string> ghiChuList = new();

            foreach (var key in form.Keys)
            {
                if (key.StartsWith("TenThuoc_"))
                {
                    var index = key.Split('_')[1];
                    string ten = form[key];
                    string sl = form[$"SoLuong_{index}"];
                    string lieu = form[$"LieuDung_{index}"];
                    string ghiChu = form[$"GhiChu_{index}"];

                    if (string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(sl))
                        continue;

                    tenThuocList.Add(ten.Trim());
                    soLuongList.Add(sl.Trim());
                    lieuDungList.Add(lieu?.Trim() ?? "");
                    ghiChuList.Add(ghiChu?.Trim() ?? "");
                }
            }

            if (tenThuocList.Count == 0)
            {
                return Json(new { success = false, message = "Chưa có thuốc nào được nhập." });
            }

            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var context = new SqlConnection(connStr);
            await context.OpenAsync();

            using var command = new SqlCommand("sp_ThemToaThuoc_MaHoa", context)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ma", ma);
            command.Parameters.AddWithValue("@BSCert", BSCert);
            command.Parameters.AddWithValue("@maKhamBenh", maKhamBenh);
            command.Parameters.AddWithValue("@tenThuocList", string.Join(",", tenThuocList));
            command.Parameters.AddWithValue("@soLuongList", string.Join(",", soLuongList));
            command.Parameters.AddWithValue("@lieuDungList", string.Join(",", lieuDungList));
            command.Parameters.AddWithValue("@ghiChuList", string.Join(",", ghiChuList));

            try
            {
                await command.ExecuteNonQueryAsync();
                return Json(new { success = true, message = "Kê toa thuốc thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi lưu toa thuốc: " + ex.Message });
            }
        }


        private async Task<List<ViewXetNghiemB>> LayDichVuXetNghiem()
        {
            var result = new List<ViewXetNghiemB>();

            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            using var cmd = new SqlCommand("sp_LayDanhSachXetNghiem", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new ViewXetNghiemB
                {
                    MaDichVu = Convert.ToInt32(reader["maDichVu"]),
                    TenDichVu = reader["tenDichVu"].ToString()
                });
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> LuuXetNghiem(IFormCollection form)
        {
            var maKhamBenhClaim = User.FindFirst("MaKhamBenh")?.Value;
            if (string.IsNullOrWhiteSpace(maKhamBenhClaim) || !int.TryParse(maKhamBenhClaim, out int maKhamBenh))
            {
                return Json(new { success = false, message = "Không tìm thấy mã khám bệnh." });
            }

            string maBSYeuCau = User.FindFirst("MaNhanVien")?.Value;
            if (string.IsNullOrWhiteSpace(maBSYeuCau))
            {
                return Json(new { success = false, message = "Không tìm thấy mã bác sĩ." });
            }

            string username = User.Identity?.Name ?? "";
            string secretName = $"{username}-key";

            string ghiChu = form["GhiChuXet"];
            string maDichVuStr = form["MaDichVu"];

            if (string.IsNullOrWhiteSpace(maDichVuStr) || !int.TryParse(maDichVuStr, out int maDichVu))
            {
                return Json(new { success = false, message = "Bạn phải chọn dịch vụ xét nghiệm." });
            }

            if (string.IsNullOrWhiteSpace(ghiChu))
            {
                return Json(new { success = false, message = "Bạn phải nhập ghi chú." });
            }

            string ma;
            string BSCert;


            ma = await _keyVaultService.GetSecretAsync(secretName);
            BSCert = await _keyVaultService.GetSecretAsync("CertificateBS");
            if (string.IsNullOrWhiteSpace(ma))
            {
                return Json(new
                {
                    success = false,
                    message = $"Chưa có khóa được tạo cho người dùng '{username}'. Vui lòng tạo trước khi tiếp tục."
                });
            }

            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            using var cmd = new SqlCommand("sp_ThemChiTietKhamBenh", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@maKhamBenh", maKhamBenh);
            cmd.Parameters.AddWithValue("@maBSYeuCau", maBSYeuCau);
            cmd.Parameters.AddWithValue("@maDichVu", maDichVu);
            cmd.Parameters.AddWithValue("@ghiChu", ghiChu ?? "");
            cmd.Parameters.AddWithValue("@ma", ma);
            cmd.Parameters.AddWithValue("@BSCert", BSCert);

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return Json(new { success = true, message = "Thêm thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

    }
}
