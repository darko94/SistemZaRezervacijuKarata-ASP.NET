using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemZaRezervacijuKarata.Models
{
    public class Projekcija
    {
        public int ID { get; set; }

        public int FilmId { get; set; }
        public virtual Film Film { get; set; }
        public int SalaId { get; set; }
        public virtual Sala Sala { get; set; }

        public DateTime Datum { get; set; }
        public DateTime Vreme { get; set; }
        public int SlobodnoSedista { get; set; }
    }
}
