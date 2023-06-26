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
    public class InsumoesController : Controller
    {
        private readonly SoffDataContext _context;

        public InsumoesController(SoffDataContext context)
        {
            _context = context;
        }

        // GET: Insumoes
        

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            else
            {
                List<Insumo> lista = _context.Insumos.Include(c => c.Proveedor).ToList();
                return View(lista);
            }
        }

        // GET: Insumoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos
                .Include(i => i.Proveedor)
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        [HttpGet]
        // GET: Insumoes/Create
        public IActionResult Create()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedors, "IdProveedor", "IdProveedor");
            return View();
        }

        // POST: Insumoes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumo,IdProveedor,Nombre,FechaCaducidad,Stock,Medida,Precio,Estado")] Insumo insumo)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(insumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //else
            {

            ViewData["IdProveedor"] = new SelectList(_context.Proveedors, "IdProveedor", "IdProveedor", insumo.IdProveedor);
            return View(insumo);
            }
        }

        // GET: Insumoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null)
            {
                return NotFound();
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedors, "IdProveedor", "IdProveedor", insumo.IdProveedor);
            return View(insumo);
        }

        // POST: Insumoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdInsumo,IdProveedor,Nombre,FechaCaducidad,Stock,Medida,Precio,Estado")] Insumo insumo)
        {
            //if (id != insumo.IdInsumo)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsumoExists(insumo.IdInsumo))
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
            ViewData["IdProveedor"] = new SelectList(_context.Proveedors, "IdProveedor", "IdProveedor", insumo.IdProveedor);
            return View(insumo);
        }

        // GET: Insumoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos
                .Include(i => i.Proveedor)
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        // POST: Insumoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Insumos == null)
            {
                return Problem("Entity set 'SoffDataContext.Insumos'  is null.");
            }
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo != null)
            {
                _context.Insumos.Remove(insumo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsumoExists(long id)
        {
          return (_context.Insumos?.Any(e => e.IdInsumo == id)).GetValueOrDefault();
        }
    }
}
