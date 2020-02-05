using System.ComponentModel.DataAnnotations.Schema;

namespace iCopy.Database
{
    public class Client : BaseEntity<int>
    {
        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
