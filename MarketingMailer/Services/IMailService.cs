using MarketingMailer.Models;

namespace MarketingMailer.Services
{
    public interface IMailService
    {
        Task SendEmails(EmailModel emailModel);
        Task SendEmail(string to, string subject, string body, string attachmentPath);
    }
}
