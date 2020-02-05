using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
namespace iCopy.Database
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public bool ChangePassword { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
