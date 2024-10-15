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
    public class PersoaneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersoaneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Persoane
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persoane.ToListAsync());
        }

        // GET: Persoane/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persoana = await _context.Persoane
                .FirstOrDefaultAsync(m => m.IdPersoana == id);
            if (persoana == null)
            {
                return NotFound();
            }

            return View(persoana);
        }

        // GET: Persoane/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persoane/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPersoana,Nume,Prenume,DataNasterii")] Persoana persoana)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persoana);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persoana);
        }

        // GET: Persoane/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persoana = await _context.Persoane.FindAsync(id);
            if (persoana == null)
            {
                return NotFound();
            }
            return View(persoana);
        }

        // POST: Persoane/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPersoana,Nume,Prenume,DataNasterii")] Persoana persoana)
        {
            if (id != persoana.IdPersoana)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persoana);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersoanaExists(persoana.IdPersoana))
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
            return View(persoana);
        }

        // GET: Persoane/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persoana = await _context.Persoane
                .FirstOrDefaultAsync(m => m.IdPersoana == id);
            if (persoana == null)
            {
                return NotFound();
            }

            return View(persoana);
        }

        // POST: Persoane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persoana = await _context.Persoane.FindAsync(id);
            if (persoana != null)
            {
                _context.Persoane.Remove(persoana);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersoanaExists(int id)
        {
            return _context.Persoane.Any(e => e.IdPersoana == id);
        }
    }
}
