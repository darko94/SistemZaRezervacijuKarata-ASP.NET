using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SistemZaRezervacijuKarata.Services;

namespace SistemZaRezervacijuKarata.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Potvrda naloga",
                $"Poštovani,</br></br>Uspešno ste kreirali nalog! Molimo Vas da potvrdite Vaš nalog klikom na sledeće dugme: <a href='{HtmlEncoder.Default.Encode(link)}'><button>POTVRDI NALOG</button></a>");
        }
    }
}
