using iCopy.Database.Context;
using iCopy.Model.Request;
using iCopy.Model.Response;
using iCopy.SERVICES.Exceptions;
using iCopy.SERVICES.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ApplicationUser = iCopy.Database.ApplicationUser;

namespace iCopy.SERVICES.Services
{
    [AllowAnonymous]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthContext authContext;
        private readonly IPasswordHasher<ApplicationUser> PasswordHasher;
        private readonly DBContext dbContext;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> ClaimsPrincipalFactory;

        public AuthenticationService(
            AuthContext authContext,
            IPasswordHasher<Database.ApplicationUser> PasswordHasher,
            DBContext dbContext,
            IUserClaimsPrincipalFactory<Database.ApplicationUser> ClaimsPrincipalFactory
            )
        {
            this.authContext = authContext;
            this.PasswordHasher = PasswordHasher;
            this.dbContext = dbContext;
            this.ClaimsPrincipalFactory = ClaimsPrincipalFactory;
        }

        public async Task<LoginResult> Authenticate(Login login)
        {
            Database.ApplicationUser user = await authContext.Users.SingleOrDefaultAsync(x => x.UserName == login.Username || x.Email == login.Username);
            if (user == null)
                throw new ModelStateException(nameof(login), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "User is not active"));
            if (!user.Active && user.EmailConfirmed && (user.LockoutEnd ?? DateTime.Now) < DateTime.Now)
                throw new ModelStateException(nameof(login), "User is not active");
            if (PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password) != PasswordVerificationResult.Success)
                throw new ModelStateException(nameof(login), "Wrong password");

            if (!(await dbContext.Companies.Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.Active)
                .Union(await dbContext.Copiers.Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.Active).ToListAsync())
                .Union(await dbContext.Employees.Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.Active).ToListAsync())
                .Union(await dbContext.Clients.Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.Active).ToListAsync())
                .Union(await dbContext.Administrators.Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.Active).ToListAsync())
                .AnyAsync())
            )
            {
                throw new ModelStateException(nameof(login), "User is deactivated");
            }

            ClaimsPrincipal principal = await ClaimsPrincipalFactory.CreateAsync(user);

            return new LoginResult()
            {
                Success = true,
                ClaimsPrincipal = principal
            };
        }
    }
}
