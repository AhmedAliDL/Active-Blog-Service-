using Active_Blog_Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Active_Blog_Service.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private static Dictionary<string, DateTime> userCooldowns = new();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(SendMailViewModel sendMailViewModel)
        {
            string fromEmail = "coinershot@gmail.com";
            string fromPassword = "joku nzah tscr fpje";
            var toEmailAddress = User.Identity.Name;

            // Check cooldown
            if (userCooldowns.TryGetValue(toEmailAddress, out var cooldownEndTime))
            {
                if (DateTime.UtcNow < cooldownEndTime)
                {
                    var remainingCooldown = cooldownEndTime - DateTime.UtcNow;
                    return Json(new { success = false, message = "Cooldown active", remainingTime = remainingCooldown.TotalMilliseconds });
                }
            }

            // Set new cooldown
            var timeOfMinutes = TimeSpan.FromMinutes(1);
            userCooldowns[toEmailAddress] = DateTime.UtcNow.Add(timeOfMinutes);

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, fromPassword)
            };

            await client.SendMailAsync(
                 new MailMessage(from: fromEmail,
                 to: toEmailAddress,
                 subject: sendMailViewModel.Subject,
                 body: sendMailViewModel.Body
                 ));

            return Json(new { success = true });
        }


    }



}
