using MarketingMailer.Models;

namespace MarketingMailer.Models
{
    public class EmailModel
    {
        /// <example>test</example>
        public string Subject { get; set; }
        /// <example>test body</example>
        public string Email { get; set; }
        public List<UserModel> UserList { get; set; }
        public List<AttachmentModel> AttachmentList { get; set; }
    }
}