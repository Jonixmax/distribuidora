using distribuidora.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace distribuidora.Controllers
{
    public class ClienteController : Controller
    {


        private readonly AppDbContext _context;
        public ClienteController(AppDbContext context)
        {
            _context = context;
        }


        public ActionResult Index()
        {

            int? idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            if (idUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var productos = _context.Productos
                .Where(p => p.Stock > 0)
                .ToList();

            var pedidos = _context.Pedidos
                .Where(p => p.IdUsuario == idUsuario)
                .OrderByDescending(p => p.Fecha)
                .ToList();

            ViewBag.Productos = productos;
            ViewBag.Pedidos = pedidos;

            return View();
        }







        [HttpPost]
        public IActionResult RealizarPedido(Dictionary<int, int> cantidades)
        {
            int? idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            if (idUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Filtrar productos con cantidad válida
            var productosSeleccionados = cantidades
                .Where(kvp => kvp.Value > 0)
                .ToList();

            if (!productosSeleccionados.Any())
            {
                TempData["Error"] = "Debe seleccionar al menos un producto con cantidad válida.";
                return RedirectToAction("Index");
            }

            // Crear el pedido
            var pedido = new Pedido
            {
                IdUsuario = idUsuario.Value,
                Fecha = DateTime.Now,
                Estado = "Pendiente"
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            foreach (var item in productosSeleccionados)
            {
                var producto = _context.Productos.FirstOrDefault(p => p.Id == item.Key);
                if (producto != null && producto.Stock >= item.Value)
                {
                    var detalle = new DetallesPedido
                    {
                        IdPedido = pedido.Id,
                        IdProducto = producto.Id,
                        Cantidad = item.Value,
                        PrecioUnitario = producto.Precio
                    };

                    producto.Stock -= item.Value;

                    _context.DetallesPedidos.Add(detalle);
                }
            }

            _context.SaveChanges();
            TempData["Exito"] = "¡Pedido realizado con éxito!";
            return RedirectToAction("Index");
        }








    }
}
