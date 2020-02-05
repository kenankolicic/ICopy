namespace iCopy.Model.Response
{
    public class Client
    {
        public int ID { get; set; }
        public Model.Response.ApplicationUser ApplicationUser { get; set; }
        public Model.Response.Person Person { get; set; }
        public int PersonId { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
