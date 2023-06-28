using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soffapp.Models;
using soffapp.Models.ViewModels;

namespace soffapp.Controllers
{
    public class DetalleInsumoController : Controller
    {
        private readonly SoffDataContext _context;

        public DetalleInsumoController(SoffDataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Create(int id)
        {
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdProducto = int.Parse(splitData[splitData.Length - 1]);
            ViewBag.IdProducto = IdProducto;

            ViewBag.Detalles = _context.DetalleInsumos.Where(d => d.AsociacionProductos.Where(a => a.IdProducto == IdProducto).Any()).Select(x => new { x.IdDetalle, x.IdInsumo, x.Cantidad, x.Medida, x.IdInsumoNavigation }).ToList();

            ViewBag.Insumos = await _context.Insumos.Select(x => new { x.IdInsumo, x.Nombre }).ToListAsync();
            Tuple<DetalleInsumo, Producto, AsociacionProducto> models = new Tuple<DetalleInsumo, Producto, AsociacionProducto>(new DetalleInsumo(), new Producto(), new AsociacionProducto());
            return View(models);
        }
        [HttpPost]
        public IActionResult Create([Bind(Prefix = "Item1")] DetalleInsumo detalleInsumo, [Bind(Prefix = "Item2")] Producto producto, [Bind(Prefix = "Item3")] AsociacionProducto asociacionProducto)
        {
            var insumo = _context.Insumos.Where(x => x.IdInsumo == detalleInsumo.IdInsumo).FirstOrDefault()!;
            _context.Add(detalleInsumo); 
            _context.SaveChanges();
            _context.Add(new AsociacionProducto() { IdProducto = producto.IdProducto, IdDetalleInsumo = detalleInsumo.IdDetalle });
            _context.SaveChanges();

            return Redirect($"/DetalleInsumo/Create/{producto.IdProducto}");
        }

        public async Task<IActionResult> Edit(int id)
        {
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdProducto = int.Parse(splitData[splitData.Length - 1]);
            ViewBag.IdProducto = IdProducto;

            var producto =_context.Productos.FirstOrDefault(p => p.IdProducto == IdProducto);

            ViewBag.NombreProducto = producto.Nombre;
            ViewBag.PrecioProducto = producto.Precio;

            ViewBag.Detalles = _context.DetalleInsumos.Where(d => d.AsociacionProductos.Where(a => a.IdProducto == IdProducto).Any()).Select(x => new { x.IdDetalle, x.IdInsumo, x.Cantidad, x.Medida, x.IdInsumoNavigation }).ToList();

            ViewBag.Insumos = await _context.Insumos.Select(x => new { x.IdInsumo, x.Nombre }).ToListAsync();
            Tuple<DetalleInsumo, Producto, AsociacionProducto> models = new Tuple<DetalleInsumo, Producto, AsociacionProducto>(new DetalleInsumo(), new Producto(), new AsociacionProducto());
            return View(models);

        }
        [HttpPost]
        public IActionResult Edit([Bind(Prefix = "Item1")] DetalleInsumo detalleInsumo, [Bind(Prefix = "Item2")] Producto producto, [Bind(Prefix = "Item3")] AsociacionProducto asociacionProducto)
        {
            var insumo = _context.Insumos.Where(x => x.IdInsumo == detalleInsumo.IdInsumo).FirstOrDefault()!;
            _context.Update(detalleInsumo);
            _context.SaveChanges();
            _context.Update(new AsociacionProducto() { IdProducto = producto.IdProducto, IdDetalleInsumo = detalleInsumo.IdDetalle });
            _context.SaveChanges();

            return Redirect($"/DetalleInsumo/Edit/{producto.IdProducto}");
        }

        public async Task<IActionResult> Delete(String id, int otroId)
        {
            var orden = await _context.DetalleInsumos.FindAsync(long.Parse(id));
            if (orden == null)
            {
                return Redirect($"/DetalleInsumo/Create/{otroId}");
            }
            else
            {
                var asociacion = _context.AsociacionProductos.FirstOrDefault(a => a.IdProducto == otroId && a.IdDetalleInsumo == long.Parse(id));
                if (asociacion != null)
                    _context.AsociacionProductos.Remove(asociacion);
                    _context.DetalleInsumos.Remove(orden);
                    _context.SaveChanges();
                    return Redirect($"/DetalleInsumo/Create/{otroId}");
            }
        }
        public async Task<IActionResult> ConfirmSale([Bind(Prefix = "Item1")] DetalleInsumo detalleInsumo, [Bind(Prefix = "Item2")] Producto producto, [Bind(Prefix = "Item3")] AsociacionProducto asociacionProducto)
        {
            if (ModelState.IsValid)
            {
                var Producto = _context.Productos.Where(x => x.IdProducto == producto.IdProducto).FirstOrDefault()!;
                Producto.Nombre = producto.Nombre;
                Producto.Precio = producto.Precio;
                _context.Update(Producto);
                _context.SaveChanges();
                return Redirect("/Productos");
            }
            else
            {
                ViewBag.NombreProducto = producto.Nombre;
                ViewBag.PrecioProducto = producto.Precio;

                ViewBag.Detalles = _context.DetalleInsumos.Where(d => d.AsociacionProductos.Where(a => a.IdProducto == producto.IdProducto).Any()).Select(x => new { x.IdDetalle, x.IdInsumo, x.Cantidad, x.Medida, x.IdInsumoNavigation }).ToList();

                ViewBag.Insumos = await _context.Insumos.Select(x => new { x.IdInsumo, x.Nombre }).ToListAsync();
                var model = new Tuple<DetalleInsumo, Producto, AsociacionProducto>(detalleInsumo, producto, asociacionProducto);
                return View($"Create", model);
            }
        }

        public async Task<IActionResult> DeleteEdit(String id, int otroId)
        {
            var orden = await _context.DetalleInsumos.FindAsync(long.Parse(id));
            if (orden == null)
            {
                return Redirect($"/DetalleInsumo/Edit/{otroId}");
            }
            else
            {
                var asociacion = _context.AsociacionProductos.FirstOrDefault(a => a.IdProducto == otroId && a.IdDetalleInsumo == long.Parse(id));
                if (asociacion != null)
                    _context.AsociacionProductos.Remove(asociacion);
                _context.DetalleInsumos.Remove(orden);
                _context.SaveChanges();
                return Redirect($"/DetalleInsumo/Edit/{otroId}");
            }
        }
    }
}
