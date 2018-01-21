using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemZaRezervacijuKarata.Models
{
    public class Rezervacija
    {
        public int ID { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser Korisnik { get; set; }

        public int ProjekcijaId { get; set; }
        public virtual Projekcija Projekcija { get; set; }

        [Required]
        [Display(Name = "Broj karata")]
        [Range(1, 5, ErrorMessage = "Možete rezervisati minium 1, a maksimalno 5 karata.")]
        public int BrojKarata { get; set; }
    }
}
