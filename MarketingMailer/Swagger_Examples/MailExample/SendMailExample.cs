using MarketingMailer.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MarketingMailer.Swagger_Examples.MailExample
{
    public class SendMailExample : IExamplesProvider<EmailModel>
    {
        public EmailModel GetExamples()
        {
            return new EmailModel
            {
                Subject = "test",
                Email = "test body",
                UserList = new List<UserModel>
            {
                new UserModel
                {
                    Email = "testofguezz@gmail.com"
                }
            },
                AttachmentList = [],
                Placeholders = [],
            };
        }
    }
}
