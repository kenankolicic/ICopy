using System;
using Microsoft.AspNetCore.Identity;

namespace iCopy.Database
{
    public class ApplicationRoleClaim : IdentityRoleClaim<int>
    {
        public ApplicationRole ApplicationRole { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
    }
}
