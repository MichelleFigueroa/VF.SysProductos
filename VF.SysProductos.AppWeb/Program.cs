using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using VF.SysProductos.BL;
using VF.SysProductos.DAL;
using VF.SysProductosVF.SysProductos.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VFSyProductosDBContext>(options =>
{
    var conexionString = builder.Configuration.GetConnectionString("Conn");
    options.UseMySql(conexionString, ServerVersion.AutoDetect(conexionString));
});

// Registro de dependencias
builder.Services.AddScoped<ProductoDAL>();
builder.Services.AddScoped<ProductoBL>();
builder.Services.AddScoped<ProveedorDAL>();
builder.Services.AddScoped<ProveedorBL>();
builder.Services.AddScoped<CompraDAL>();
builder.Services.AddScoped<CompraBL>();

// Agregar servicios de MVC
builder.Services.AddControllersWithViews();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var app = builder.Build();

// Configuración del middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Configurar Rotativa correctamente
Rotativa.AspNetCore.RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
