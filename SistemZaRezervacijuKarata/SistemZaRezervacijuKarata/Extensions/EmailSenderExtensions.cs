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
                $"Molimo potvrdite Vaš nalog klikom na sledeći link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
