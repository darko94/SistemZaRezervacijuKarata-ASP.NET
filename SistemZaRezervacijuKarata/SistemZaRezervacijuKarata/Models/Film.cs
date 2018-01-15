using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemZaRezervacijuKarata.Models
{
    public class Film
    {
        public int ID { get; set; }
        [Required]
        public string Naslov { get; set; }
        [Display(Name = "Originalni naslov")]
        [Required]
        public string OrgininalniNaslov { get; set; }
        [Display(Name = "Početak prikazivanja")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        [DataType(DataType.Date)]
        public DateTime PocetakPrikazivanja { get; set; }
        [Display(Name = "Trajanje (min)")]
        [Required]
        public string DuzinaTranjanja { get; set; }
        [Display(Name = "Država")]
        [Required]
        public string Drzava { get; set; }
        [Required]
        public string Godina { get; set; }
        [Display(Name = "Žanr")]
        [Required]
        public string Zanr { get; set; }
        [Required]
        public string Opis { get; set; }
        [Display(Name = "YouTube URL")]
        [Required]
        public string YoutubeUrl { get; set; }
        [Display(Name = "Slika URL")]
        [Required]
        public string SlikaUrl { get; set; }

        public virtual ICollection<Projekcija> Projekcije { get; set; }
    }
}
