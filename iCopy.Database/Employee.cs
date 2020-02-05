using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace iCopy.Database
{
    public class Employee : BaseEntity<int>
    {
        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string Password { get; set; }
        [ForeignKey(nameof(Copier))]
        public int CopierId { get; set; }
        public Copier Copier { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
