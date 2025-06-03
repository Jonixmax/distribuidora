using distribuidora.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace distribuidora.Controllers
{
    public class AdministradorController : Controller

    {
        private readonly AppDbContext _context;
        public AdministradorController(AppDbContext context)
        {
            _context = context;
        }

        
        public ActionResult Index()
        {
            var pedidos = _context.Pedidos
              .OrderByDescending(p => p.Fecha)
              .ToList();
            var productos = _context.Productos.ToList();
            ViewBag.Productos = _context.Productos.ToList();

            return View(pedidos);


        }





        [HttpPost]
        public IActionResult CambiarEstado(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            // Cambiar estado entre "Pendiente" y "Entregado"
            pedido.Estado = pedido.Estado == "Pendiente" ? "Entregado" : "Pendiente";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }








        [HttpPost]
        public IActionResult AgregarProducto(string Nombre, string Descripcion, decimal Precio, int Stock)
        {
            var producto = new Producto
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                Precio = Precio,
                Stock = Stock
            };
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ActualizarStock(int IdProducto, int Cantidad)
        {
            var producto = _context.Productos.Find(IdProducto);
            if (producto != null)
            {
                producto.Stock += Cantidad;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }





        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario nuevoUsuario)
        {
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();
            TempData["Mensaje"] = "Usuario registrado correctamente";
            return RedirectToAction("Index");
        }

















    }





}
