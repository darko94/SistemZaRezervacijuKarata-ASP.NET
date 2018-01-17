using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemZaRezervacijuKarata.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} mora imati između {2} i {1} karaktera.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdite lozinku")]
        [Compare("Password", ErrorMessage = "Lozinka i potvrdna lozinka se ne poklapaju.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
