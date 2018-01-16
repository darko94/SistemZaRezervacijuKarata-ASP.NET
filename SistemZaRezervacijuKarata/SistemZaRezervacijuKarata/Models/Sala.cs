using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemZaRezervacijuKarata.Models
{
    public class Sala
    {
        public int ID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Display(Name = "Broj sedišta")]
        [Required]
        public int BrojSedista { get; set; }
        [Required]
        public string Tehnologija { get; set; }
    }
}
