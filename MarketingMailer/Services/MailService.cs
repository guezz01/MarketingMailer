using MarketingMailer.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace MarketingMailer.Services
{
    public class MailService : IMailService
    {

        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task <string> SendEmails(EmailModel emailModel)
        {
            foreach (var user in emailModel.UserList)
            {
                string subject = emailModel.Subject;
                string body = ReplacePlaceholders(emailModel.Email, emailModel.Placeholders, user.PlaceholdersValues);

                await SendEmail(user.Email, subject, body, emailModel.AttachmentList);
            }

            return "Emails sent successfully.";
        }

        public async Task SendEmail(string to, string subject, string body, List<AttachmentModel> attachmentList)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var smtpUsername = _configuration["EmailSettings:Username"];
            var smtpPassword = _configuration["EmailSettings:Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["EmailSettings:displayName"], smtpUsername));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;

            foreach (var attachment in attachmentList)
            {
                if (!string.IsNullOrEmpty(attachment.FileName) && !string.IsNullOrEmpty(attachment.Path))
                {
                    using (var stream = new MemoryStream(File.ReadAllBytes(attachment.Path + attachment.FileName)))
                    {
                        bodyBuilder.Attachments.Add(attachment.Path + attachment.FileName, stream);
                    }
                }
            }

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpPort, true);
                await client.AuthenticateAsync(smtpUsername, smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

        }
        private string ReplacePlaceholders(string emailBody, List<Placeholder> placeholders, List<string> placeholdersValues)
        {
            for(int i = 0; i < placeholders.Count; i++)
            {
                if (placeholdersValues.Count > i)
                {
                    emailBody = emailBody.Replace("["+$"{placeholders[i].Label}"+ "]", placeholdersValues[i]);
                }
                else
                {
                    emailBody = emailBody.Replace("[" + $"{placeholders[i].Label}" + "]", "");
                }
            }
            return emailBody;
        }

    }
}
