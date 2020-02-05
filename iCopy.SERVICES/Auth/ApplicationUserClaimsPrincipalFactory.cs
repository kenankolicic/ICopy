using iCopy.Database;
using iCopy.Database.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iCopy.SERVICES.Auth
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<Database.ApplicationUser, Database.ApplicationRole>
    {
        private readonly DBContext context;

        public ApplicationUserClaimsPrincipalFactory(DBContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            this.context = context;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);

            // Given name
            var name = await context.Companies
                .Select(x => x.Name)
                .Union(context.Copiers.Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.Name))
                .Union(context.Clients.Include(x => x.Person).Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => string.Concat(x.Person.FirstName, " ", x.Person.LastName)))
                .Union(context.Employees.Include(x => x.Person).Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => string.Concat(x.Person.FirstName, " ", x.Person.LastName)))
                .Union(context.Administrators.Include(x => x.Person).Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => string.Concat(x.Person.FirstName, " ", x.Person.LastName)))
                .FirstOrDefaultAsync();

            var id = await context.Companies
                .Select(x => x.ID)
                .Union(context.Copiers.Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.ID))
                .Union(context.Clients.Include(x => x.Person).Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.ID))
                .Union(context.Employees.Include(x => x.Person).Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.ID))
                .Union(context.Administrators.Include(x => x.Person).Where(x => x.ApplicationUserId == user.Id && x.Active).Select(x => x.ID))
                .FirstOrDefaultAsync();

            identity.AddClaim(new Claim(ApplicationUserClaimTypes.Id, id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, name));

            // Profile photo
            var profileImagePath = await context.ApplicationUserProfilePhotos.Include(x => x.ProfilePhoto).FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id && x.Active);
            if (profileImagePath != null)
                identity.AddClaim(new Claim(ApplicationUserClaimTypes.ProfilePhotoPath, profileImagePath.ProfilePhoto.Path));
            
            return identity;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            return new ClaimsPrincipal(await GenerateClaimsAsync(user));
        }

    }
}
