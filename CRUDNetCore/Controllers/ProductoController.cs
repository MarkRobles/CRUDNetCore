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
    public class ProductoController : Controller
    {
        private readonly MapCelTestContext _context;

        public ProductoController(MapCelTestContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var mapCelTestContext = _context.Producto.Include(p => p.UnqGensubCategoriaLinkNavigation);
            return View(await mapCelTestContext.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.UnqGensubCategoriaLinkNavigation)
                .FirstOrDefaultAsync(m => m.UnqGenproductoKey == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["UnqGensubCategoriaLink"] = new SelectList(_context.SubCategoria, "UnqGensubCategoriaKey", "VchCodigo");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnqGenproductoKey,VchDescripcion,UnqGensubCategoriaLink")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.UnqGenproductoKey = Guid.NewGuid();
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnqGensubCategoriaLink"] = new SelectList(_context.SubCategoria, "UnqGensubCategoriaKey", "VchCodigo", producto.UnqGensubCategoriaLink);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["UnqGensubCategoriaLink"] = new SelectList(_context.SubCategoria, "UnqGensubCategoriaKey", "VchCodigo", producto.UnqGensubCategoriaLink);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UnqGenproductoKey,VchDescripcion,UnqGensubCategoriaLink")] Producto producto)
        {
            if (id != producto.UnqGenproductoKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.UnqGenproductoKey))
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
            ViewData["UnqGensubCategoriaLink"] = new SelectList(_context.SubCategoria, "UnqGensubCategoriaKey", "VchCodigo", producto.UnqGensubCategoriaLink);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.UnqGensubCategoriaLinkNavigation)
                .FirstOrDefaultAsync(m => m.UnqGenproductoKey == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var producto = await _context.Producto.FindAsync(id);
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(Guid id)
        {
            return _context.Producto.Any(e => e.UnqGenproductoKey == id);
        }
    }
}
