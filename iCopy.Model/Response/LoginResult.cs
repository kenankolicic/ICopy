using System.Collections.Generic;
using System.Security.Claims;

namespace iCopy.Model.Response
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
