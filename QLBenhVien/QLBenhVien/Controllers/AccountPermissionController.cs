using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Entities;
using QLBenhVien.Services;
using System.Data;

namespace QLBenhVien.Controllers
{
    [Authorize]
    public class AccountPermissionController : Controller
    {
        private readonly DynamicConnectionProvider _connProvider;

        public AccountPermissionController(DynamicConnectionProvider connProvider)
        {
            _connProvider = connProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string selectedUser = "")
        {
            var connStr = await _connProvider.GetAccountConnectionStringAsync();
            using var context = QlbenhVienAccountContextFactory.Create(connStr);

            var allPermissions = await context.ViewQuyenTks
                .FromSqlRaw("EXEC sp_LayQuyenTK")
                .ToListAsync();

            var usernames = allPermissions
                .Select(p => p.Username)
                .Distinct()
                .ToList();

            ViewBag.UsernameList = new SelectList(usernames, selectedUser);

            var selectedPermissions = allPermissions
                .Where(p => p.Username == selectedUser)
                .Select(p => $"{p.PermissionId} - {p.PermissionName}")
                .ToList();

            ViewBag.PermissionDetails = string.Join("\n", selectedPermissions);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var connStr = await _connProvider.GetAccountConnectionStringAsync();
            using var context = QlbenhVienAccountContextFactory.Create(connStr);

            var allQuyens = await context.ViewQuyens
                .FromSqlRaw("EXEC sp_LayQuyen")
                .ToListAsync();

            var allQuyenTKs = await context.ViewQuyenTks
                .FromSqlRaw("EXEC sp_LayQuyenTK")
                .ToListAsync();

            var usernames = allQuyenTKs
                .Select(x => x.Username)
                .Distinct()
                .ToList();

            ViewBag.AllQuyens = allQuyens;
            ViewBag.AllQuyenTks = allQuyenTKs;
            ViewBag.UsernameList = usernames;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string username, int permissionId)
        {
            if (string.IsNullOrEmpty(username) || permissionId <= 0)
            {
                TempData["Error_AccountPermission"] = "Vui lòng chọn tài khoản và quyền để gán.";
                return RedirectToAction("Create");
            }

            var connStr = await _connProvider.GetAccountConnectionStringAsync();
            using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            using var cmd = new SqlCommand("sp_GanQuyen", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@permissionId", permissionId);

            try
            {
                await cmd.ExecuteNonQueryAsync();
                TempData["Success_AccountPermission"] = "Gan quyen thanh cong.";
            }
            catch (SqlException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Create");
        }
        // Controller
        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var connStr = await _connProvider.GetAccountConnectionStringAsync();
            using var context = QlbenhVienAccountContextFactory.Create(connStr);

            var allQuyenTKs = await context.ViewQuyenTks
                .FromSqlRaw("EXEC sp_LayQuyenTK")
                .ToListAsync();

            var usernames = allQuyenTKs
                .Select(x => x.Username)
                .Distinct()
                .ToList();

            ViewBag.AllQuyenTks = allQuyenTKs;
            ViewBag.UsernameList = usernames;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string username, int permissionId)
        {
            if (string.IsNullOrEmpty(username) || permissionId <= 0)
            {
                TempData["Error"] = "Vui long chon tai khoan va quyen can xoa.";
                return RedirectToAction("Delete");
            }

            var connStr = await _connProvider.GetAccountConnectionStringAsync();
            using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            using var cmd = new SqlCommand("sp_XoaQuyen", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@permissionId", permissionId);

            try
            {
                await cmd.ExecuteNonQueryAsync();
                TempData["Success"] = "Xoa quyen thanh cong.";
            }
            catch (SqlException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Delete");
        }


    }
}
