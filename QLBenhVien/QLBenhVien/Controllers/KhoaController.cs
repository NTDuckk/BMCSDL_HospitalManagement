using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Entities;
using QLBenhVien.Services;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using X.PagedList.Extensions;

namespace QLBenhVien.Controllers
{
    [Authorize]
    public class KhoaController : Controller
    {
        //private readonly DynamicConnectionProvider _connProvider;

        //public KhoaController(DynamicConnectionProvider connProvider)
        //{
        //    _connProvider = connProvider;
        //}

        //public async Task<IActionResult> Index(int? page)
        //{
        //    var connStr = await _connProvider.GetDataConnectionStringAsync();
        //    using var context = QlbenhVienContextFactory.Create(connStr);

        //    var allKhoas = await context.Khoas
        //        .FromSqlRaw("EXEC sp_XemKhoa")
        //        .ToListAsync();

        //    int pageSize = 10;
        //    int pageNumber = page ?? 1;

        //    var pagedList = allKhoas.ToPagedList(pageNumber, pageSize);
        //    return View(pagedList);
        //}
    }
}
