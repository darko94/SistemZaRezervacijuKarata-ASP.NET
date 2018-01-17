using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemZaRezervacijuKarata.Models;
using Microsoft.AspNetCore.Authorization;

namespace SistemZaRezervacijuKarata.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FilmoviController : Controller
    {
        private readonly SistemZaRezervacijuKarataContext _context;

        public FilmoviController(SistemZaRezervacijuKarataContext context)
        {
            _context = context;
        }

        // GET: Filmovi
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Film.ToListAsync());
        }

        // GET: Filmovi/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .SingleOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }

            film.Projekcije = _context.Projekcija.Include(p => p.Sala).Where(p => p.FilmId == id).ToList();

            return View(film);
        }

        // GET: Filmovi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naslov,OrgininalniNaslov,PocetakPrikazivanja,DuzinaTranjanja,Drzava,Godina,Zanr,Opis,YoutubeUrl,SlikaUrl")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Filmovi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.SingleOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Filmovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naslov,OrgininalniNaslov,PocetakPrikazivanja,DuzinaTranjanja,Drzava,Godina,Zanr,Opis,YoutubeUrl,SlikaUrl")] Film film)
        {
            if (id != film.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.ID))
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
            return View(film);
        }

        // GET: Filmovi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .SingleOrDefaultAsync(m => m.ID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Filmovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.SingleOrDefaultAsync(m => m.ID == id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.ID == id);
        }
    }
}
