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
                .Where(a => a.IdAutorPrincipal == idUser) // Use the correct property name for the author's ID
                .ToListAsync();
            if (!articole.IsNullOrEmpty())
                return View(articole); // Pass the filtered articles to the view
            else return NotFound();
        }
    }
}
