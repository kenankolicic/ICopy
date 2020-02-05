using iCopy.SERVICES.Auth;
using System.Security.Claims;

namespace iCopy.SERVICES.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GivenName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.GivenName);
        }

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.FindFirstValue(ApplicationUserClaimTypes.Id));
        }
    }
}
