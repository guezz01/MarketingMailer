using MarketingMailer.Models;
using System.Net.Mail;
using System.Net;

namespace MarketingMailer.Services
{
    public class MailService : IMailService
    {
        public MailService()
        {
            
        }

        public async Task SendEmails(EmailModel emailModel)
        {
            foreach (var user in emailModel.UserList)
            {

                // Send email for each user without attachment (you can add attachment logic if needed)
                await SendEmail(user.Email, emailModel.Subject, emailModel.Email, "");

                // Sleep for 10 seconds
                await Task.Delay(10000);
            }
        }

        public async Task SendEmail(string to, string subject, string body, string attachmentPath)
        {
            using (var client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("aymenguezz@gmail.com", "lkywgeyfvvarwcjq");

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("testofguezz@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                    To = { to }
                };

                // Attachments logic
                if (!string.IsNullOrEmpty(attachmentPath))
                {
                    mailMessage.Attachments.Add(new Attachment(attachmentPath));
                }

                try
                {
                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine($"Email sent to: {to}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email to {to}: {ex.Message}");
                    // Log the error to the error_log.txt file
                    await File.AppendAllTextAsync("error_log.txt", $"{DateTime.Now} - {to}: {ex.Message}\n");
                }
            }
        }

    }
}
