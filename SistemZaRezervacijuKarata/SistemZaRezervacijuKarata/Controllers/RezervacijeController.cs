using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemZaRezervacijuKarata.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SistemZaRezervacijuKarata.Controllers
{
    [Authorize]
    public class RezervacijeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SistemZaRezervacijuKarataContext _context;

        public RezervacijeController(SistemZaRezervacijuKarataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public async Task<IActionResult> Create(int projekcijaId)
        {

            var projekcija = await _context.Projekcija
                .Include(p => p.Film)
                .Include(p => p.Sala)
                .SingleOrDefaultAsync(p => p.ID == projekcijaId);


            

            Rezervacija rezervacija = new Rezervacija();
            

            rezervacija.ProjekcijaId = projekcijaId;
            rezervacija.Projekcija = projekcija;
           

            
            return View(rezervacija);
        }
        


        // POST: Rezervacije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ApplicationUserId,ProjekcijaId,BrojKarata")] Rezervacija rezervacija)
        {
            var projekcija = await _context.Projekcija
                .Include(p => p.Film)
                .Include(p => p.Sala)
                .SingleOrDefaultAsync(p => p.ID == rezervacija.ProjekcijaId);

            if(rezervacija.BrojKarata > projekcija.SlobodnoSedista)
            {
                ModelState.AddModelError("BrojKarata", "Nažalost, nema dovoljno slobodnih mesta za odabranu projekciju. Molimo pokušajte rezervaciju manjeg broja karata ili neku drugu projekciju.");
                rezervacija.Projekcija = projekcija;
                return View(rezervacija);
            }

            var appUser = await _userManager.FindByEmailAsync(rezervacija.ApplicationUserId);

            rezervacija.ApplicationUserId = appUser.Id;

            
            projekcija.SlobodnoSedista -= rezervacija.BrojKarata;
            
            _context.Update(projekcija);
            await _context.SaveChangesAsync();
            
            

            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(FilmoviController.Index));
            }
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
            ViewData["ProjekcijaId"] = new SelectList(_context.Projekcija, "ID", "ID", rezervacija.ProjekcijaId);
            return View(rezervacija);
        }

        // POST: Rezervacije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ApplicationUserId,ProjekcijaId,BrojKarata")] Rezervacija rezervacija)
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
