using MarketingMailer.Models;
using MarketingMailer.Services;
using MarketingMailer.Swagger_Examples.MailExample;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace MarketingMailer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        public MailController(IMailService mailService) { 
            _mailService = mailService;
        }
        [SwaggerRequestExample(typeof(EmailModel), typeof(SendMailExample))]
        [HttpPost("send-emails")]
        //[SwaggerRequestExample(typeof(EmailModel), typeof(SendMailExample))]
        public async Task<IActionResult> SendEmails(EmailModel emailModel)
        {

            try
            {
                // Use await to ensure that the CSV is read before sending emails
                //await ReadCSV(userData);
                var response = await _mailService.SendEmails(emailModel);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
