using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using soffapp.Models;

namespace soffapp.Controllers
{
    public class ProductosController : Controller
    {
        private readonly SoffDataContext _context;

        public ProductosController(SoffDataContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return _context.Productos != null ?
                        View(await _context.Productos.ToListAsync()):
                        Problem("Entity set 'SoffDataContext.Productos'  is null.");
        }

        public async Task<IActionResult> Details()
        {
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdProducto = int.Parse(splitData[splitData.Length - 1]);
            ViewBag.IdProducto = IdProducto;

            ViewBag.Detalles = _context.DetalleInsumos.Where(d => d.AsociacionProductos.Where(a => a.IdProducto == IdProducto).Any()).Select(x => new { x.IdInsumo, x.Cantidad, x.Medida, x.IdInsumoNavigation}).ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            producto.Nombre = "";
            producto.Precio = 0;
            producto.Estado = true;
            _context.Add(producto);
            await _context.SaveChangesAsync();

            return Redirect($"/DetalleInsumo/Create/{producto.IdProducto}");
        }
        public async Task<IActionResult> Delete(string id)
        {
            var productoDetalles = await _context.Productos
                .Include(v => v.OrdenVenta).Include(a=> a.AsociacionProductos)
                .FirstOrDefaultAsync(v => v.IdProducto == long.Parse(id));
            if (productoDetalles == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var detalles = productoDetalles.OrdenVenta.ToList();
                if (productoDetalles.OrdenVenta.Count() > 0)
                {
                    foreach (var detalle in detalles)
                    {
                        _context.OrdenVenta.Remove(detalle);
                        _context.SaveChanges();
                    }
                    _context.Productos.Remove(productoDetalles);
                    _context.SaveChanges();
                }
                else
                {
                    var detallesAsociacion = productoDetalles.AsociacionProductos.ToList();
                    if (productoDetalles.AsociacionProductos.Count() > 0)
                    {
                        foreach (var detalle in detallesAsociacion)
                        {
                            _context.AsociacionProductos.Remove(detalle);
                            _context.SaveChanges();
                        }
                        _context.Productos.Remove(productoDetalles);
                        _context.SaveChanges();
                    }
                    else
                    {
                        _context.Productos.Remove(productoDetalles);
                        _context.SaveChanges();
                    }
                }

            }
            return RedirectToAction("Index");
        }
    }
}