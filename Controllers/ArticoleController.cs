using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAppTest.Data;
using WebAppTest.Models;
using OfficeOpenXml;

namespace WebAppTest.Controllers
{
    public class ArticoleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articole
        public async Task<IActionResult> Index()
        {
            var articole = await _context.Articole.ToListAsync();
            var reviste = await _context.Reviste.ToListAsync();
            var persoane = await _context.Persoane.ToListAsync();
            var model = Tuple.Create(articole, reviste, persoane);
            return View(model);
        }

        // GET: Articole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articol = await _context.Articole
                .FirstOrDefaultAsync(m => m.IdArticol == id);
            if (articol == null)
            {
                return NotFound();
            }

            return View(articol);
        }

        // GET: Articole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdArticol,IdAutorPrincipal,IdRevista,TitluArticol,AnPublicare,NrPagini,NrCoautori")] Articol articol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articol);
        }

        // GET: Articole/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articol = await _context.Articole.FindAsync(id);
            if (articol == null)
            {
                return NotFound();
            }
            return View(articol);
        }

        // POST: Articole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdArticol,IdAutorPrincipal,IdRevista,TitluArticol,AnPublicare,NrPagini,NrCoautori")] Articol articol)
        {
            if (id != articol.IdArticol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticolExists(articol.IdArticol))
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
            return View(articol);
        }

        // GET: Articole/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articol = await _context.Articole
                .FirstOrDefaultAsync(m => m.IdArticol == id);
            if (articol == null)
            {
                return NotFound();
            }

            return View(articol);
        }

        // POST: Articole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articol = await _context.Articole.FindAsync(id);
            if (articol != null)
            {
                _context.Articole.Remove(articol);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticolExists(int id)
        {
            return _context.Articole.Any(e => e.IdArticol == id);
        }
        public async Task<IActionResult> List(int idUser)
        {
            // Fetch articles where the author ID matches the provided user ID
            if (idUser == null)
            {
                return NotFound(nameof(idUser));
            }

            var articole = await _context.Articole
                .Include(a => a.Autor)  // Include the related Autor entity
                .Include(a => a.Referinta) // Include the related Referinta entity (Revista)
                .Where(a => a.IdAutorPrincipal == idUser)
                .ToListAsync();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (!articole.IsNullOrEmpty())
            {
                // Check if user wants to download Excel
                if (Request.Query.ContainsKey("exportExcel"))
                {
                    var stream = new MemoryStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Articles");

                        // Add header row
                        worksheet.Cells[1, 1].Value = "ID";
                        worksheet.Cells[1, 2].Value = "Title";
                        worksheet.Cells[1, 3].Value = "Year of Publication";
                        worksheet.Cells[1, 4].Value = "Pages";
                        worksheet.Cells[1, 5].Value = "Co-Authors";
                        worksheet.Cells[1, 6].Value = "Author Name";
                        worksheet.Cells[1, 7].Value = "Journal Name";

                        worksheet.Cells[1, 1, 1, 7].Style.Font.Bold = true;

                        // Add data rows
                        for (int i = 0; i < articole.Count; i++)
                        {
                            worksheet.Cells[i + 2, 1].Value = articole[i].IdArticol;
                            worksheet.Cells[i + 2, 2].Value = articole[i].TitluArticol;
                            worksheet.Cells[i + 2, 3].Value = articole[i].AnPublicare;
                            worksheet.Cells[i + 2, 4].Value = articole[i].NrPagini;
                            worksheet.Cells[i + 2, 5].Value = articole[i].NrCoautori;
                            worksheet.Cells[i + 2, 6].Value = articole[i].Autor?.Nume; // Assuming Persoana has a 'Nume' property
                            worksheet.Cells[i + 2, 7].Value = articole[i].Referinta?.TitluRevista; // Assuming Revista has a 'NumeRevista' property
                        }

                        // Auto-fit columns
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        // Save to stream
                        package.Save();
                    }

                    stream.Position = 0;
                    string excelName = $"Articles-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

                    // Return the Excel file
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }

                return View(articole); // Pass the filtered articles to the view
            }
            else
            {
                return NotFound();
            }
        }

    }
}
