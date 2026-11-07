using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Entities;
using QLBenhVien.Models;
using QLBenhVien.Services;
using System.Data;
using System.Text;

namespace QLBenhVien.Controllers
{
    [Authorize]
    public class AccountManagerController : Controller
    {
        private readonly DynamicConnectionProvider _connProvider;
        private readonly KeyVaultService _keyVaultService;

        public AccountManagerController(
            DynamicConnectionProvider connProvider,
            KeyVaultService keyVaultService)
        {
            _connProvider = connProvider;
            _keyVaultService = keyVaultService;
        }


        public async Task<IActionResult> Index()
        {
            var connStr = await _connProvider.GetAccountConnectionStringAsync();
            using var context = QlbenhVienAccountContextFactory.Create(connStr);

            var accounts = await context.ViewTaiKhoans
                .FromSqlRaw("EXEC sp_ViewTaiKhoan")
                .ToListAsync();

            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var connStr = await _connProvider.GetDataConnectionStringAsync();
            using var context = QlbenhVienContextFactory.Create(connStr);


            var maNhanViens = context.MaNhanViens
                .FromSqlRaw("EXEC sp_LayMaNV")
                .AsEnumerable()
                .Select(x => x.MaNhanVienId)
                .ToList();

            ViewBag.MaNhanVienList = new SelectList(maNhanViens);

            ViewBag.LoaiTaiKhoanList = new SelectList(new List<SelectListItem>
{
    new SelectListItem { Value = "1", Text = "Bác sĩ" },
    new SelectListItem { Value = "2", Text = "Quản lý nhân sự" },
    new SelectListItem { Value = "3", Text = "Nhân viên điều phối" },
    new SelectListItem { Value = "4", Text = "Nhân viên tài vụ" },
    new SelectListItem { Value = "5", Text = "Nhân viên bán thuốc" }
}, "Value", "Text");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountCreate model)
        {
            if (!ModelState.IsValid)
            {
                // Gọi lại hàm Get để nạp dropdown nếu có lỗi nhập
                return await Create();
            }

            var connStr = await _connProvider.GetAccountConnectionStringAsync();
            using var connection = new SqlConnection(connStr);
            await connection.OpenAsync();

            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(model.Password);
            var passwordHash = sha256.ComputeHash(passwordBytes);

            using var command = new SqlCommand("sp_ThemTaiKhoanNV", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@username", model.Username);
            command.Parameters.Add("@passwordHash", SqlDbType.VarBinary, -1).Value = passwordHash;
            command.Parameters.AddWithValue("@maNhanVien", model.MaNhanVien);
            command.Parameters.AddWithValue("@typeID", model.TypeID);

            try
            {
                await command.ExecuteNonQueryAsync();
                TempData["SuccessMessage"] = "Tao tai khoan thanh cong.";
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                TempData["ErrorMessage"] = "Loi khi tao tai khoan: " + ex.Message;
                return await Create();
            }

        }
        [HttpGet]
        public async Task<IActionResult> AddKey()
        {
            var connStr = await _connProvider.GetAccountConnectionStringAsync();
            using var context = QlbenhVienAccountContextFactory.Create(connStr);

            var accountsWithoutKey = await context.ViewTaiKhoans
                .FromSqlRaw("EXEC sp_CheckKeyAccount")
                .ToListAsync();

            var usernames = accountsWithoutKey
                .Select(x => x.Username)
                .Distinct()
                .ToList();

            ViewBag.UsernameList = usernames;
            ViewBag.Accounts = accountsWithoutKey;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddKey(string username, string secretKey)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(secretKey))
            {
                TempData["Error"] = "Vui lòng nhập đầy đủ thông tin.";
                return RedirectToAction("AddKey");
            }

            try
            {
                string secretName = $"{username}-key";
                await _keyVaultService.SetSecretAsync(secretName, secretKey);
                string certName = await _keyVaultService.GetSecretAsync("CertificateBS");

                using var conn = new SqlConnection(await _connProvider.GetAccountConnectionStringAsync());
                await conn.OpenAsync();

                using var cmd = new SqlCommand("sp_CapNhatKeyTaiKhoan", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@keyValue", secretKey);
                cmd.Parameters.AddWithValue("@BSCert", certName);

                await cmd.ExecuteNonQueryAsync();

                TempData["Success"] = "Luu khoa thanh cong.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Loi khi luu khoa: " + ex.Message;
            }

            return RedirectToAction("AddKey");
        }


    }
}
