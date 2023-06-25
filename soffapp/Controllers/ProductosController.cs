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
                       View(await _context.Productos.ToListAsync()) :
                       Problem("Entity set 'SoffDataContext.Productos'  is null.");
            
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
            var producto = await _context.Productos.FindAsync(long.Parse(id));
            if (producto == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
