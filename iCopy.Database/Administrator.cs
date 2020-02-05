using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCopy.Database
{
    public class Administrator : BaseEntity<int>
    {
        public int ApplicationUserId { get; set; }
        [ForeignKey(nameof(Person))]
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}
