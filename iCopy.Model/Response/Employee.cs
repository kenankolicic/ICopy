namespace iCopy.Model.Response
{
    public class Employee
    {
        public int ID { get; set; }
        public bool Active { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int CopierId { get; set; }
        public Copier Copier { get; set; }
        public ApplicationUser User { get; set; }
        public ProfilePhoto ProfilePhoto { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
