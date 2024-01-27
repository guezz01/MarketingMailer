using MarketingMailer.Models;

namespace MarketingMailer.Services
{
    public interface IMailService
    {
        Task<string> SendEmails(EmailModel emailModel);
        Task SendEmail(string to, string subject, string body, List<AttachmentModel> attachmentList);
    }
}
