namespace iCopy.ExternalServices.Model
{
    public class MailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailServer MailServer { get; set; }
    }

    public class MailServer
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
    }
}
