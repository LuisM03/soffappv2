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
                    }).ToList();

            ViewBag.Detalles = OrdenesCompras;
            ViewBag.Insumos = await context.Insumos.Select(x => new { x.IdInsumo, x.Nombre }).ToListAsync();
            Tuple<OrdenCompra, Compra> models = new Tuple<OrdenCompra, Compra>(new OrdenCompra(), new Compra());
            return View(models);
        }

        [HttpPost]
        public IActionResult Create([Bind(Prefix = "Item1")] OrdenCompra ordenCompra, [Bind(Prefix = "Item2")] Compra compra, Insumo insumo)
        {
            var insummo = context.Insumos.Where(x => x.IdInsumo == ordenCompra.IdInsumo).FirstOrDefault()!;
            ordenCompra.PrecioUnitario = insummo.Precio;
            ordenCompra.Total = insummo.Precio * Int32.Parse(ordenCompra.Cantidad);
            context.Add(ordenCompra);
            context.SaveChanges();

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
            if (ModelState.IsValid)
            {
                var Compra = context.Compras.Where(x => x.IdCompra == compra.IdCompra).FirstOrDefault()!;
                Compra.IdProveedor = compra.IdProveedor;
                Compra.Total = compra.IdCompra;
                context.Update(Compra);
                context.SaveChanges();
                return Redirect("/Compras");
            }
            else
            {
                return View();
            }
        }
    }
}
