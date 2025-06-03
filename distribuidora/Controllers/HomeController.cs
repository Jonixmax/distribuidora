using System.Diagnostics;
using distribuidora.Models;
using distribuidora.Models.DB;
using Microsoft.AspNetCore.Mvc;

namespace distribuidora.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        public IActionResult Login(string correo, string contrasena, string tipoUsuario)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u =>
                u.Email == correo &&
                u.Contraseña == contrasena &&
                u.Rol == tipoUsuario);

            if (usuario != null)
            {
                // Guardar datos en sesión
                HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
                HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
                HttpContext.Session.SetString("RolUsuario", usuario.Rol);
                HttpContext.Session.SetString("EmailUsuario", usuario.Email);


                // Aquí puedes guardar en sesión si deseas
                if (tipoUsuario == "cliente")
                {
                    return RedirectToAction("Index", "Cliente");
                }
                else if (tipoUsuario == "administrador")
                {
                    return RedirectToAction("Index", "Administrador");
                }
            }

            ViewBag.MensajeError = "Credenciales incorrectas.";
            return View("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
