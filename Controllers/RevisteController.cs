using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppTest.Data;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class RevisteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RevisteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reviste
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reviste.ToListAsync());
        }

        // GET: Reviste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revista = await _context.Reviste
                .FirstOrDefaultAsync(m => m.IdRevista == id);
            if (revista == null)
            {
                return NotFound();
            }

            return View(revista);
        }

        // GET: Reviste/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviste/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRevista,TipRevista,TitluRevista,Issn,AnAparitie,Adresa,Tara")] Revista revista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(revista);
        }

        // GET: Reviste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revista = await _context.Reviste.FindAsync(id);
            if (revista == null)
            {
                return NotFound();
            }
            return View(revista);
        }

        // POST: Reviste/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRevista,TipRevista,TitluRevista,Issn,AnAparitie,Adresa,Tara")] Revista revista)
        {
            if (id != revista.IdRevista)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevistaExists(revista.IdRevista))
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
            return View(revista);
        }

        // GET: Reviste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revista = await _context.Reviste
                .FirstOrDefaultAsync(m => m.IdRevista == id);
            if (revista == null)
            {
                return NotFound();
            }

            return View(revista);
        }

        // POST: Reviste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revista = await _context.Reviste.FindAsync(id);
            if (revista != null)
            {
                _context.Reviste.Remove(revista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevistaExists(int id)
        {
            return _context.Reviste.Any(e => e.IdRevista == id);
        }
    }
}
