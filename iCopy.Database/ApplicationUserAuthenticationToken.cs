using System;
using Microsoft.AspNetCore.Identity;

namespace iCopy.Database
{
    public class ApplicationUserAuthenticationToken : IdentityUserToken<int>
    {
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
