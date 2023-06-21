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
    public class OrdenComprasController : Controller
    {
        private readonly SoffDataContext _context;

        public OrdenComprasController(SoffDataContext context)
        {
            _context = context;
        }

        // GET: OrdenCompras
        public async Task<IActionResult> Index()
        {
            var soffDataContext = _context.OrdenCompras.Include(o => o.IdCompraNavigation).Include(o => o.IdInsumoNavigation);
            return View(await soffDataContext.ToListAsync());
        }

        // GET: OrdenCompras/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.OrdenCompras == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenCompras
                .Include(o => o.IdCompraNavigation)
                .Include(o => o.IdInsumoNavigation)
                .FirstOrDefaultAsync(m => m.IdOrden == id);
            if (ordenCompra == null)
            {
                return NotFound();
            }

            return View(ordenCompra);
        }

        // GET: OrdenCompras/Create
        public IActionResult Create()
        {
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra");
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo");
            return View();
        }

        // POST: OrdenCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrden,IdCompra,IdInsumo,Cantidad,PrecioUnitario,Total")] OrdenCompra ordenCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", ordenCompra.IdCompra);
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", ordenCompra.IdInsumo);
            return View(ordenCompra);
        }

        // GET: OrdenCompras/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.OrdenCompras == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenCompras.FindAsync(id);
            if (ordenCompra == null)
            {
                return NotFound();
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", ordenCompra.IdCompra);
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", ordenCompra.IdInsumo);
            return View(ordenCompra);
        }

        // POST: OrdenCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdOrden,IdCompra,IdInsumo,Cantidad,PrecioUnitario,Total")] OrdenCompra ordenCompra)
        {
            if (id != ordenCompra.IdOrden)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenCompraExists(ordenCompra.IdOrden))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", ordenCompra.IdCompra);
            ViewData["IdInsumo"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", ordenCompra.IdInsumo);
            return View(ordenCompra);
        }

        // GET: OrdenCompras/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.OrdenCompras == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenCompras
                .Include(o => o.IdCompraNavigation)
                .Include(o => o.IdInsumoNavigation)
                .FirstOrDefaultAsync(m => m.IdOrden == id);
            if (ordenCompra == null)
            {
                return NotFound();
            }

            return View(ordenCompra);
        }

        // POST: OrdenCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.OrdenCompras == null)
            {
                return Problem("Entity set 'SoffDataContext.OrdenCompras'  is null.");
            }
            var ordenCompra = await _context.OrdenCompras.FindAsync(id);
            if (ordenCompra != null)
            {
                _context.OrdenCompras.Remove(ordenCompra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenCompraExists(long id)
        {
          return (_context.OrdenCompras?.Any(e => e.IdOrden == id)).GetValueOrDefault();
        }
    }
}
