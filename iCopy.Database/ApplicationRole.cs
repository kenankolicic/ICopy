using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace iCopy.Database
{
    public class ApplicationRole : IdentityRole<int>
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
