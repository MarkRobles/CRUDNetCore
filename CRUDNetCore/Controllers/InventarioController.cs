using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDNetCore.Models;

namespace CRUDNetCore.Controllers
{
    public class InventarioController : Controller
    {
        private readonly MapCelTestContext _context;

        public InventarioController(MapCelTestContext context)
        {
            _context = context;
        }

        // GET: Inventario
        public async Task<IActionResult> Index()
        {
            var mapCelTestContext = _context.Inventario.Include(i => i.UnqGenproductoLinkNavigation);
            return View(await mapCelTestContext.ToListAsync());
        }

        // GET: Inventario/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .Include(i => i.UnqGenproductoLinkNavigation)
                .FirstOrDefaultAsync(m => m.UnqInvinventarioKey == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventario/Create
        public IActionResult Create()
        {
            ViewData["UnqGenproductoLink"] = new SelectList(_context.Producto, "UnqGenproductoKey", "VchDescripcion");
            return View();
        }

        // POST: Inventario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnqInvinventarioKey,VchSku,VchNumeroSerie,IntCantidad,UnqGenproductoLink")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                inventario.UnqInvinventarioKey = Guid.NewGuid();
                _context.Add(inventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnqGenproductoLink"] = new SelectList(_context.Producto, "UnqGenproductoKey", "VchDescripcion", inventario.UnqGenproductoLink);
            return View(inventario);
        }

        // GET: Inventario/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            ViewData["UnqGenproductoLink"] = new SelectList(_context.Producto, "UnqGenproductoKey", "VchDescripcion", inventario.UnqGenproductoLink);
            return View(inventario);
        }

        // POST: Inventario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UnqInvinventarioKey,VchSku,VchNumeroSerie,IntCantidad,UnqGenproductoLink")] Inventario inventario)
        {
            if (id != inventario.UnqInvinventarioKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.UnqInvinventarioKey))
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
            ViewData["UnqGenproductoLink"] = new SelectList(_context.Producto, "UnqGenproductoKey", "VchDescripcion", inventario.UnqGenproductoLink);
            return View(inventario);
        }

        // GET: Inventario/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .Include(i => i.UnqGenproductoLinkNavigation)
                .FirstOrDefaultAsync(m => m.UnqInvinventarioKey == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var inventario = await _context.Inventario.FindAsync(id);
            _context.Inventario.Remove(inventario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioExists(Guid id)
        {
            return _context.Inventario.Any(e => e.UnqInvinventarioKey == id);
        }
    }
}
