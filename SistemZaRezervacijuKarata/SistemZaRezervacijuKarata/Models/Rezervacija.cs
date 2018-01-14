using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemZaRezervacijuKarata.Models
{
    public class Rezervacija
    {
        public int ID { get; set; }

        public int KorisnikId { get; set; }
        public virtual Korisnik Korisnik { get; set; }

        public int ProjekcijaId { get; set; }
        public virtual Projekcija Projekcija { get; set; }

        public int BrojKarata { get; set; }
    }
}
