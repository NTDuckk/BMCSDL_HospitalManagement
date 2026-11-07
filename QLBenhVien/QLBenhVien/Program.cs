using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QLBenhVien.Entities;
using QLBenhVien.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<QlbenhVienContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("QLBenhVien")));

//builder.Services.AddDbContext<QlbenhVienAccountContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("QLBenhVien_ACCOUNT")));

builder.Services.AddSingleton<KeyVaultService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";   
        options.LogoutPath = "/Account/Logout";  
        options.AccessDeniedPath = "/Account/AccessDenied"; 
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DynamicConnectionProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
