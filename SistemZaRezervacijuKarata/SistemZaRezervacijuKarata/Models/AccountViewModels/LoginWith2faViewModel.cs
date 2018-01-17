using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemZaRezervacijuKarata.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "{0} mora imati između {2} i {1} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Kod za autentikaciju")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Sačuvaj ovaj uređaj")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
