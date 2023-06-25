using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using soffapp.Models;

namespace soffapp.Controllers
{
    public class OrdenComprasController : Controller
    {
        private readonly SoffDataContext context;

        public OrdenComprasController(SoffDataContext context)
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
            var IdCompra = int.Parse(splitData[splitData.Length - 1]);
            ViewBag.IdCompra = IdCompra;

            var OrdenesCompras = context.OrdenCompras
                .Where(o => o.IdCompra == IdCompra)
                .Join(
                    context.Insumos,
                    orden => orden.IdInsumo,
                    insumo => insumo.IdInsumo,
                    (orden, insumo) => new
                    {
                        insumo.Nombre,
                        orden.Cantidad,
                        orden.PrecioUnitario,
                        orden.Total,
                        orden.IdOrden,
                        orden.IdCompra
                    })
                .ToList();

            ViewBag.Detalles = OrdenesCompras;
            ViewBag.Insumos = await context.Insumos.Select(x => new { x.IdInsumo, x.Nombre }).ToListAsync();
            ViewBag.Proveedores = await context.Proveedors.Select(x => new { x.IdProveedor, x.Nombre }).ToListAsync();
            Tuple<OrdenCompra, Compra> models = new Tuple<OrdenCompra, Compra>(new OrdenCompra(), new Compra());
            return View(models);
        }


        public async Task<IActionResult> Details()
        {
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdCompra = int.Parse(splitData[splitData.Length - 1]);

            var Ordenes = context.OrdenCompras
                .Where(o => o.IdCompra == IdCompra)
                .Join(
                    context.Insumos,
                    orden => orden.IdInsumo,
                    insumo => insumo.IdInsumo,
                    (orden, insumo) => new
                    {
                        insumo.Nombre,
                        orden.Cantidad,
                        orden.PrecioUnitario,
                        orden.Total,
                        orden.IdOrden,
                        orden.IdCompra
                    }).ToList();
            var compra = context.Compras.Where(o => o.IdCompra == IdCompra).Select(x => new { x.IdCompra, x.IdProveedor, x.FechaCompra, x.Total }).ToList();
            ViewBag.Ordenes = Ordenes;
            ViewBag.IdCompra = IdCompra;
            foreach (var item in compra)
            {
                ViewBag.TotalCompra = item.Total;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind(Prefix = "Item1")] OrdenCompra ordenCompra, [Bind(Prefix = "Item2")] Compra compra, Insumo insumo)
        {
            var insummo = context.Insumos.Where(x => x.IdInsumo == ordenCompra.IdInsumo).FirstOrDefault()!;
            ordenCompra.PrecioUnitario = insummo.Precio;
            ordenCompra.Total = insummo.Precio * Int32.Parse(ordenCompra.Cantidad);
            context.Add(ordenCompra);
            context.SaveChanges();

            var insumoo = context.Insumos.FirstOrDefault(i => i.IdInsumo == ordenCompra.IdInsumo);
            if (insumoo != null)
            {
                insumoo.Stock += int.Parse(ordenCompra.Cantidad);
                context.Update(insumoo);
                context.SaveChanges();
            }

            return Redirect($"/OrdenCompras/Create/{ordenCompra.IdCompra}");
        }
        public async Task<IActionResult> Delete(String id, int otroId)
        {
            var orden = await context.OrdenCompras.FindAsync(long.Parse(id));
            if (orden == null)
            {
                return Redirect($"/OrdenCompras/Create/{otroId}");
            }
            else
            {
                context.OrdenCompras.Remove(orden);
                context.SaveChanges();
                return Redirect($"/OrdenCompras/Create/{otroId}");
            }
        }

        public IActionResult ConfirmSale([Bind(Prefix = "Item2")] Compra compra)
        {
            if (!ModelState.IsValid)
            {
                var Compra = context.Compras.Where(x => x.IdCompra == compra.IdCompra).FirstOrDefault()!;
                Compra.IdProveedor = compra.IdProveedor;
                Compra.Total = compra.Total;
                context.Update(Compra);
                context.SaveChanges();
                return Redirect("/Compras");
            }
            else
            {
                return Redirect("/Compras");
            }
        }
    }
}
