using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemZaRezervacijuKarata.Models
{
    public class Projekcija
    {
        public int ID { get; set; }
        [Required]
        public int FilmId { get; set; }
        public virtual Film Film { get; set; }
        [Required]
        public int SalaId { get; set; }
        public virtual Sala Sala { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime Vreme { get; set; }
        [Display(Name = "Slobodno sedišta")]
        [Required]
        public int SlobodnoSedista { get; set; }

        public virtual ICollection<Rezervacija> Rezervacije { get; set; }
    }
}
