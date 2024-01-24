using MarketingMailer.Models;
using MarketingMailer.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketingMailer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        IMailService _mailService;
        public MailController(IMailService mailService) { 
            _mailService = mailService;
        }

        [HttpPost("send-emails")]
        public async Task<IActionResult> SendEmails()
        {
            var userData = new List<UserData>
        {
            new UserData { Email = "ferieltirari@gmail.com", FirstName = "Feriel", LastName = "Tirari" },
            new UserData { Email = "testofguezz@gmail.com", FirstName = "Ahmed", LastName = "Test" }
            // Add more user data as needed
        };

            try
            {
                // Use await to ensure that the CSV is read before sending emails
                //await ReadCSV(userData);
                await _mailService.SendEmails(userData);

                return Ok("Emails sent. Check error_log.txt for failed emails.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
