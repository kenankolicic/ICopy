namespace iCopy.Model.Request
{
    public class AccountActivate
    {
        public int ApplicationUserId { get; set; }
        public string ActivationEmailToken { get; set; }
    }
}
