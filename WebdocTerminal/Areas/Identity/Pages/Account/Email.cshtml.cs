using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdocTerminal.Helpers;

namespace WebdocTerminal.Areas.Identity.Pages.Account
{
    public class EmailModel : PageModel
    {
        private readonly IEmailSender _emailSender;

        public EmailModel(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public string EmailStatusMessage { get; set; }

        [Required]
        [BindProperty]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var email = Email;

            var subject = "Email Test";

            var message = string.Format("Nome : {0}\nNumero : {1}\nE-Mail : {2}\nMessaggio :\n{3}","Hola", "Salute", "Bonjour", "Salam");

            await _emailSender.SendEmailAsync(email, subject, message);

            EmailStatusMessage = "Send test email was successful.";

            return Page();
        }
    }
}