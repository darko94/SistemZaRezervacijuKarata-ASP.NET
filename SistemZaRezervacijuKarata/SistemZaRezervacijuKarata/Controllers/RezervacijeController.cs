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
    public class RezervacijeController : Controller
    {
        private readonly SistemZaRezervacijuKarataContext _context;

        public RezervacijeController(SistemZaRezervacijuKarataContext context)
        {
            _context = context;
        }

        // GET: Rezervacije
        public async Task<IActionResult> Index()
        {
            var sistemZaRezervacijuKarataContext = _context.Rezervacija.Include(r => r.Korisnik).Include(r => r.Projekcija);
            return View(await sistemZaRezervacijuKarataContext.ToListAsync());
        }

        // GET: Rezervacije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .Include(r => r.Korisnik)
                .Include(r => r.Projekcija)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // GET: Rezervacije/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "ID", "ID");
            ViewData["ProjekcijaId"] = new SelectList(_context.Projekcija, "ID", "ID");
            return View();
        }

        // POST: Rezervacije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,KorisnikId,ProjekcijaId,BrojKarata")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "ID", "ID", rezervacija.KorisnikId);
            ViewData["ProjekcijaId"] = new SelectList(_context.Projekcija, "ID", "ID", rezervacija.ProjekcijaId);
            return View(rezervacija);
        }

        // GET: Rezervacije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija.SingleOrDefaultAsync(m => m.ID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "ID", "ID", rezervacija.KorisnikId);
            ViewData["ProjekcijaId"] = new SelectList(_context.Projekcija, "ID", "ID", rezervacija.ProjekcijaId);
            return View(rezervacija);
        }

        // POST: Rezervacije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,KorisnikId,ProjekcijaId,BrojKarata")] Rezervacija rezervacija)
        {
            if (id != rezervacija.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.ID))
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
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "ID", "ID", rezervacija.KorisnikId);
            ViewData["ProjekcijaId"] = new SelectList(_context.Projekcija, "ID", "ID", rezervacija.ProjekcijaId);
            return View(rezervacija);
        }

        // GET: Rezervacije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacija
                .Include(r => r.Korisnik)
                .Include(r => r.Projekcija)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // POST: Rezervacije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.Rezervacija.SingleOrDefaultAsync(m => m.ID == id);
            _context.Rezervacija.Remove(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacija.Any(e => e.ID == id);
        }
    }
}
