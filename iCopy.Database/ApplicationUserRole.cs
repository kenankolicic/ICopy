using Microsoft.AspNetCore.Identity;
using System;

namespace iCopy.Database
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationRole Role { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
    }
}
