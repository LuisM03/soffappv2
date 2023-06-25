using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using soffapp.Models;

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
            return View();
        }
        public async Task<IActionResult> Create(int id)
        {
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdProducto = int.Parse(splitData[splitData.Length - 1]);
            ViewBag.IdProducto = IdProducto;

            ViewBag.Detalles = _context.DetalleInsumos.Where(d => d.AsociacionProductos.Where(a => a.IdProducto == IdProducto).Any()).ToList();

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
            //            return View(new Tuple<DetalleInsumo, Producto, AsociacionProducto>(detalleInsumo, producto, asociacionProducto));

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
        public IActionResult ConfirmSale([Bind(Prefix = "Item2")] Producto producto)
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
                return View();
            }
        }
    }
}
