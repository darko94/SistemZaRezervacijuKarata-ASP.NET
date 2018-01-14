using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemZaRezervacijuKarata.Models
{
    public class Film
    {
        public int ID { get; set; }
        public string Naslov { get; set; }
        public string OrgininalniNaslov { get; set; }
        public DateTime PocetakPrikazivanja { get; set; }
        public string DuzinaTranjanja { get; set; }
        public string Drzava { get; set; }
        public string Godina { get; set; }
        public string Zanr { get; set; }
        public string Opis { get; set; }
        public string YoutubeUrl { get; set; }
        public string SlikaUrl { get; set; }

        public virtual ICollection<Projekcija> Projekcije { get; set; }
    }
}
