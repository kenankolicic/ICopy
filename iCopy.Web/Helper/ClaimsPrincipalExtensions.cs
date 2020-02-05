using System.Linq;
using System.Security.Claims;
using iCopy.Model.Enum;

namespace iCopy.Web.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasRole(this ClaimsPrincipal principal, Enum.Roles role)
        {
            return principal.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == role.ToString());
        }
        public static bool HasRoles(this ClaimsPrincipal principal, params Enum.Roles[] role)
        {
            return principal.HasClaim(x => x.Type == ClaimTypes.Role && role.Any(y => y.ToString() == x.Value));
        }
    }
}
