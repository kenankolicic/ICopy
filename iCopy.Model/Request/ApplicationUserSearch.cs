namespace iCopy.Model.Request
{
    public class ApplicationUserSearch
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public bool? Active { get; set; }
    }
}
