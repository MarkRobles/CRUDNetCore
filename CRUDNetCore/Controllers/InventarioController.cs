using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDNetCore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;

namespace CRUDNetCore.Controllers
{
    public class InventarioController : Controller
    {
        private readonly MapCelTestContext _context;


        public InventarioController(MapCelTestContext context)
        {
            _context = context;
        }


        public IActionResult Importar()
        {
           
            List<Inventario> ListaInventario = new List<Inventario>();
            string rootFolder = "C:/Users/Marcos/Documents/Share/";
            string fileName = @"ImportarInventario.xlsx";

            string celdaExcel, version = String.Empty;
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Inventario"];
                int totalRows = workSheet.Dimension.Rows;
                int totalColums = workSheet.Dimension.Columns;


                Inventario objInventario = new Inventario();
                List<String> CamposTabla = new List<string>();

                for (int i = 1; i <= totalRows; i++)
                {
                    for (int j = 1; j <= totalColums; j++)
                    {
                        //Obtener version de plantilla
                        if (workSheet.Cells[1, 1].Value.ToString() == "version")
                        {
                            version = workSheet.Cells[i, j + 1].Value.ToString();
                        }

                        //Agregar nombres de campos
                        if (i == 2)
                        {
                            CamposTabla.Add(workSheet.Cells[i, j].Value.ToString());
                        }

                        //Obtener registros
                        if (i > 2)
                        {

                            objInventario.UnqInvinventarioKey = Guid.NewGuid();
                            if (workSheet.Cells[i, j].Value == null)
                            {
                                objInventario.VchSku = "NADA";
                            }
                            else { objInventario.VchSku = workSheet.Cells[i, j].Value.ToString(); }

                            if (workSheet.Cells[i, j + 1].Value == null)
                            {
                                objInventario.VchNumeroSerie = "NADA";
                            }
                            else
                            {
                                objInventario.VchNumeroSerie = workSheet.Cells[i, j + 1].Value.ToString();
                            }
                            

                            if (workSheet.Cells[i, j + 2].Value == null)
                            {
                                objInventario.IntCantidad = 0;
                            }
                            else { objInventario.IntCantidad = Convert.ToInt32(workSheet.Cells[i, j + 2].Value); }

                            objInventario.UnqGenproductoLink = Guid.Parse("1F3038BF-01D8-44C9-AA64-533D622289AE");


                            _context.Inventario.AddRange(objInventario);
                            _context.SaveChanges();
                         
                            break;
                        }
                    }
                }
            }
            return View();           
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
