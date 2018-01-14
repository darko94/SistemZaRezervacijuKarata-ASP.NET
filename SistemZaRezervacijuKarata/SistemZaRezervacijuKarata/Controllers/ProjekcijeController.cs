using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemZaRezervacijuKarata.Models;

namespace SistemZaRezervacijuKarata.Controllers
{
    public class ProjekcijeController : Controller
    {
        private readonly SistemZaRezervacijuKarataContext _context;

        public ProjekcijeController(SistemZaRezervacijuKarataContext context)
        {
            _context = context;
        }

        // GET: Projekcije
        public async Task<IActionResult> Index()
        {
            var sistemZaRezervacijuKarataContext = _context.Projekcija.Include(p => p.Film).Include(p => p.Sala);
            return View(await sistemZaRezervacijuKarataContext.ToListAsync());
        }

        // GET: Projekcije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekcija = await _context.Projekcija
                .Include(p => p.Film)
                .Include(p => p.Sala)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (projekcija == null)
            {
                return NotFound();
            }

            return View(projekcija);
        }

        // GET: Projekcije/Create
        public IActionResult Create()
        {
            ViewData["FilmId"] = new SelectList(_context.Film, "ID", "ID");
            ViewData["SalaId"] = new SelectList(_context.Sala, "ID", "ID");
            return View();
        }

        // POST: Projekcije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FilmId,SalaId,Datum,Vreme,SlobodnoSedista")] Projekcija projekcija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projekcija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Film, "ID", "ID", projekcija.FilmId);
            ViewData["SalaId"] = new SelectList(_context.Sala, "ID", "ID", projekcija.SalaId);
            return View(projekcija);
        }

        // GET: Projekcije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekcija = await _context.Projekcija.SingleOrDefaultAsync(m => m.ID == id);
            if (projekcija == null)
            {
                return NotFound();
            }
            ViewData["FilmId"] = new SelectList(_context.Film, "ID", "ID", projekcija.FilmId);
            ViewData["SalaId"] = new SelectList(_context.Sala, "ID", "ID", projekcija.SalaId);
            return View(projekcija);
        }

        // POST: Projekcije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FilmId,SalaId,Datum,Vreme,SlobodnoSedista")] Projekcija projekcija)
        {
            if (id != projekcija.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projekcija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjekcijaExists(projekcija.ID))
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
            ViewData["FilmId"] = new SelectList(_context.Film, "ID", "ID", projekcija.FilmId);
            ViewData["SalaId"] = new SelectList(_context.Sala, "ID", "ID", projekcija.SalaId);
            return View(projekcija);
        }

        // GET: Projekcije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projekcija = await _context.Projekcija
                .Include(p => p.Film)
                .Include(p => p.Sala)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (projekcija == null)
            {
                return NotFound();
            }

            return View(projekcija);
        }

        // POST: Projekcije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projekcija = await _context.Projekcija.SingleOrDefaultAsync(m => m.ID == id);
            _context.Projekcija.Remove(projekcija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjekcijaExists(int id)
        {
            return _context.Projekcija.Any(e => e.ID == id);
        }
    }
}
