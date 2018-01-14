using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemZaRezervacijuKarata.Models
{
    public class Sala
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int BrojSedista { get; set; }
        public string Tehnologija { get; set; }
    }
}
