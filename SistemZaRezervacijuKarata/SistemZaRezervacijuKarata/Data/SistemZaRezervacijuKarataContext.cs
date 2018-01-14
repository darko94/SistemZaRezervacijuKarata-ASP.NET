using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemZaRezervacijuKarata.Models;

namespace SistemZaRezervacijuKarata.Models
{
    public class SistemZaRezervacijuKarataContext : DbContext
    {
        public SistemZaRezervacijuKarataContext (DbContextOptions<SistemZaRezervacijuKarataContext> options)
            : base(options)
        {
        }

        public DbSet<SistemZaRezervacijuKarata.Models.Film> Film { get; set; }

        public DbSet<SistemZaRezervacijuKarata.Models.Sala> Sala { get; set; }

        public DbSet<SistemZaRezervacijuKarata.Models.Projekcija> Projekcija { get; set; }
    }
}
