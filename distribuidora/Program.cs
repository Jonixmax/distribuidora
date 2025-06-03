using distribuidora.Models.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ?? 1. Registrar el contexto de base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("distribuidoraContext")));

// ?? 2. Agregar servicios necesarios
builder.Services.AddControllersWithViews();

// ? Agregar el servicio de sesiones
builder.Services.AddSession();  // <-- Añadir esta línea

var app = builder.Build();

// ?? 3. Configurar el middleware HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ? Activar sesiones (importante: antes de Authorization)
app.UseSession();  // <-- Añadir esta línea

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
