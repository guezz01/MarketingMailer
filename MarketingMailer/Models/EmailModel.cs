namespace MarketingMailer.Models
{
    public class EmailModel
    {
        public string Subject { get; set; }
        public string Email { get; set; }
        public List<UserData> UserList { get; set; }
        public List<AttachmentModel> AttachmentList { get; set; }
    }
}
