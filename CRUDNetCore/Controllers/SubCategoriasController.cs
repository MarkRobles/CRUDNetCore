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
    public class SubCategoriasController : Controller
    {
        private readonly MapCelTestContext _context;

        public SubCategoriasController(MapCelTestContext context)
        {
            _context = context;
        }

        // GET: SubCategorias
        public async Task<IActionResult> Index()
        {
            var mapCelTestContext = _context.SubCategoria.Include(s => s.UnqGencategoriaLinkNavigation);
            return View(await mapCelTestContext.ToListAsync());
        }

        // GET: SubCategorias/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoria = await _context.SubCategoria
                .Include(s => s.UnqGencategoriaLinkNavigation)
                .FirstOrDefaultAsync(m => m.UnqGensubCategoriaKey == id);
            if (subCategoria == null)
            {
                return NotFound();
            }

            return View(subCategoria);
        }

        // GET: SubCategorias/Create
        public IActionResult Create()
        {
            ViewData["UnqGencategoriaLink"] = new SelectList(_context.Categoria, "UnqGencategoriaKey", "VchCodigo");
            return View();
        }

        // POST: SubCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnqGensubCategoriaKey,UnqGencategoriaLink,VchCodigo,VchDescripcion")] SubCategoria subCategoria)
        {
            if (ModelState.IsValid)
            {
                subCategoria.UnqGensubCategoriaKey = Guid.NewGuid();
                _context.Add(subCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnqGencategoriaLink"] = new SelectList(_context.Categoria, "UnqGencategoriaKey", "VchCodigo", subCategoria.UnqGencategoriaLink);
            return View(subCategoria);
        }

        // GET: SubCategorias/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoria = await _context.SubCategoria.FindAsync(id);
            if (subCategoria == null)
            {
                return NotFound();
            }
            ViewData["UnqGencategoriaLink"] = new SelectList(_context.Categoria, "UnqGencategoriaKey", "VchCodigo", subCategoria.UnqGencategoriaLink);
            return View(subCategoria);
        }

        // POST: SubCategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UnqGensubCategoriaKey,UnqGencategoriaLink,VchCodigo,VchDescripcion")] SubCategoria subCategoria)
        {
            if (id != subCategoria.UnqGensubCategoriaKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoriaExists(subCategoria.UnqGensubCategoriaKey))
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
            ViewData["UnqGencategoriaLink"] = new SelectList(_context.Categoria, "UnqGencategoriaKey", "VchCodigo", subCategoria.UnqGencategoriaLink);
            return View(subCategoria);
        }

        // GET: SubCategorias/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoria = await _context.SubCategoria
                .Include(s => s.UnqGencategoriaLinkNavigation)
                .FirstOrDefaultAsync(m => m.UnqGensubCategoriaKey == id);
            if (subCategoria == null)
            {
                return NotFound();
            }

            return View(subCategoria);
        }

        // POST: SubCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var subCategoria = await _context.SubCategoria.FindAsync(id);
            _context.SubCategoria.Remove(subCategoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoriaExists(Guid id)
        {
            return _context.SubCategoria.Any(e => e.UnqGensubCategoriaKey == id);
        }
    }
}
