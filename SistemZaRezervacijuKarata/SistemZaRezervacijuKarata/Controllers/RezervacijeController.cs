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
using SistemZaRezervacijuKarata.Services;

namespace SistemZaRezervacijuKarata.Controllers
{
    [Authorize]
    public class RezervacijeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SistemZaRezervacijuKarataContext _context;
        private readonly IEmailSender _emailSender;

        public RezervacijeController(SistemZaRezervacijuKarataContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: Rezervacije
        public async Task<IActionResult> Index()
        {
            var sistemZaRezervacijuKarataContext = _context.Rezervacija.Include(r => r.Korisnik).Include(r => r.Projekcija).Include(r => r.Projekcija.Film);
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

                string ulaznice = "";
                if (rezervacija.BrojKarata == 1)
                {
                    ulaznice = " ulaznicu";
                }
                else if (rezervacija.BrojKarata == 5)
                {
                    ulaznice = " ulaznica";
                }
                else
                {
                    ulaznice = " ulaznice";
                }

                string message = "Poštovani," +
                "</br></br>" +
                "Uspešno ste rezervisali " + rezervacija.BrojKarata + ulaznice + " za film " + projekcija.Film.Naslov + "!" +
                "</br>" +
                "Projekcija filma je " + projekcija.Datum.ToString("dd.MM.yyyy.") + " u " + projekcija.Vreme.ToString("HH:mm") + " sati." +
                "</br></br>" +
                "Molimo Vas da budete u bioskopu najkasnije pola sata pre projekcije, kako biste preuzeli svoje ulaznice." +
                "</br></br>" +
                "Hvala što koristite naš sistem!";


                await _emailSender.SendEmailAsync(appUser.Email, "Uspešna rezervacija", message);

                return RedirectToAction(nameof(RezervacijeController.Success));
            }
            return View(rezervacija);
        }

        //GET: Rezervacije/Success 
        public IActionResult Success()
        {
            return View();
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
                .Include(r => r.Projekcija.Film)
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
            var rezervacija = await _context.Rezervacija
                 .Include(r => r.Projekcija)
                 .SingleOrDefaultAsync(m => m.ID == id);

            var projekcija = rezervacija.Projekcija;

            projekcija.SlobodnoSedista += rezervacija.BrojKarata;

            _context.Update(projekcija);
            await _context.SaveChangesAsync();

            _context.Rezervacija.Remove(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Projekcije");
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacija.Any(e => e.ID == id);
        }
    }
}
