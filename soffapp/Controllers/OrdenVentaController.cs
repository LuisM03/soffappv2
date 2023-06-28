using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using soffapp.Models;

namespace soffapp.Controllers
{
    public class OrdenVentaController : Controller
    {
        private readonly SoffDataContext context;

        public OrdenVentaController(SoffDataContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {   
                return View();   
        }

        public async Task<IActionResult> Create(int id)
        {
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdVenta = int.Parse(splitData[splitData.Length - 1]);
            ViewBag.IdVenta = IdVenta;

            var OrdenesVentas = context.OrdenVenta
                .Where(o => o.IdVenta == IdVenta)
                .Join(
                    context.Productos,
                    orden => orden.IdProducto,
                    producto => producto.IdProducto,
                    (orden, producto) => new
                    {
                        producto.Nombre,
                        orden.Cantidad,
                        orden.PrecioUnitario,
                        orden.Total,
                        orden.IdOrden,
                        orden.IdVenta
                    }).ToList();

            ViewBag.Detalles = OrdenesVentas;
            ViewBag.Productos = await context.Productos.Select(x => new { x.IdProducto, x.Nombre }).ToListAsync();
            Tuple<OrdenVentum, Ventum> models = new Tuple<OrdenVentum, Ventum>(new OrdenVentum(), new Ventum());
            return View(models);
        }

        public async Task<IActionResult> Details()
        {
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdVenta = int.Parse(splitData[splitData.Length - 1]);

            var Ordenes = context.OrdenVenta
                .Where(o => o.IdVenta == IdVenta)
                .Join(
                    context.Productos,
                    orden => orden.IdProducto,
                    producto => producto.IdProducto,
                    (orden, producto) => new
                    {
                        producto.Nombre,
                        orden.Cantidad,
                        orden.PrecioUnitario,
                        orden.Total,
                        orden.IdOrden,
                        orden.IdVenta
                    }).ToList();
            var venta = context.Venta.Where(o => o.IdVenta == IdVenta).Select(x => new {x.IdVenta, x.TipoVenta, x.FechaVenta, x.Total}).ToList();
            ViewBag.Ordenes = Ordenes;
            ViewBag.IdVenta = IdVenta;
            foreach (var item in venta)
            {
                ViewBag.TotalVenta = item.Total;
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(Prefix = "Item1")] OrdenVentum ordenVenta, [Bind(Prefix = "Item2")] Ventum venta, AsociacionProducto asociacion)
        {

            if (ordenVenta.IdProducto != null && ordenVenta.Cantidad != 0)
            {
                var producto = context.Productos.Where(x => x.IdProducto == ordenVenta.IdProducto).FirstOrDefault()!;
                ordenVenta.PrecioUnitario = producto.Precio;
                ordenVenta.Total = producto.Precio * ordenVenta.Cantidad;
                context.Add(ordenVenta);
                context.SaveChanges();

                return Redirect($"/OrdenVenta/Create/{ordenVenta.IdVenta}");
            }
            else
            {
                ViewBag.Message = "Debe ingresar un producto";
                ViewBag.CantidadError = "Debe por lo menos pedir un producto";
                return Redirect($"/OrdenVenta/Create/{ordenVenta.IdVenta}");
            }
        }
        public async Task<IActionResult> Delete(String id, int otroId)
        {
            var orden = await context.OrdenVenta.FindAsync(long.Parse(id));
            if(orden == null)
            {
                return Redirect($"/OrdenVenta/Create/{otroId}");
            }else
            {
                context.OrdenVenta.Remove(orden);
                context.SaveChanges();
                return Redirect($"/OrdenVenta/Create/{otroId}");
            }
        }

        public IActionResult ConfirmSale([Bind(Prefix = "Item2")] Ventum venta)
        {
            var Venta = context.Venta.Where(x => x.IdVenta == venta.IdVenta).FirstOrDefault()!;
            if (ModelState.IsValid)
            {
                if (venta.Total == 0)
                {
                    return Redirect($"/OrdenVenta/Create/{venta.IdVenta}");
                }
                Venta.TipoVenta = venta.TipoVenta;
                Venta.Metodo = venta.Metodo;
                Venta.Total = venta.Total;
                Venta.TipoVenta = venta.TipoVenta;
                context.Update(Venta);
                context.SaveChanges();
                return Redirect("/Ventas");
            }
            else
            {
                return View();
            }
        }
    }
}
