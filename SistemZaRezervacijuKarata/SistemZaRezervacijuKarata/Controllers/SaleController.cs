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
    public class SaleController : Controller
    {
        private readonly SistemZaRezervacijuKarataContext _context;

        public SaleController(SistemZaRezervacijuKarataContext context)
        {
            _context = context;
        }

        // GET: Sale
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sala.ToListAsync());
        }

        // GET: Sale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Sale/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sale/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,BrojSedista,Tehnologija")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // GET: Sale/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala.SingleOrDefaultAsync(m => m.ID == id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }

        // POST: Sale/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naziv,BrojSedista,Tehnologija")] Sala sala)
        {
            if (id != sala.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.ID))
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
            return View(sala);
        }

        // GET: Sale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sala = await _context.Sala.SingleOrDefaultAsync(m => m.ID == id);
            _context.Sala.Remove(sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaExists(int id)
        {
            return _context.Sala.Any(e => e.ID == id);
        }
    }
}
