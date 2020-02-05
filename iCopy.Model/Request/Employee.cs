using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iCopy.Model.Request
{
    public class Employee
    {
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        [Required(ErrorMessage = "ErrNoCopier")]
        public int? CopierId { get; set; }
        public Copier Copier { get; set; }
        [Required(ErrorMessage = "ErrNoPassword")]
        public int Password { get; set; }
        public bool Active { get; set; }
        public Model.Request.ApplicationUserInsert User { get; set; }
        public Model.Request.ProfilePhoto ProfilePhoto { get; set; }
    }
}
