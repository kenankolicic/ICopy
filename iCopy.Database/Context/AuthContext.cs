using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace iCopy.Database.Context
{
    public class AuthContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim,
        ApplicationUserRole, IdentityUserLogin<int>, ApplicationRoleClaim, ApplicationUserAuthenticationToken>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var item in builder.Model.GetEntityTypes())
            {
                //Debugger.Launch();
                if (item.ClrType.GetProperty("CreatedDate") != null && item.ClrType.GetProperty("Active") != null)
                {
                    builder.Entity(item.ClrType).Property("Active").HasDefaultValue(true).HasDefaultValueSql("1").ValueGeneratedOnAdd();
                    builder.Entity(item.ClrType).Property("CreatedDate").HasDefaultValue(DateTime.Now).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
                }
            }
            builder.Entity<ApplicationUser>().Property(x => x.ChangePassword).HasDefaultValue(true).HasDefaultValueSql("1").ValueGeneratedOnAdd();
            builder.Entity<ApplicationUser>().Property(x => x.LockoutEnabled).HasDefaultValue(true).HasDefaultValueSql("1").ValueGeneratedOnAdd();
            builder.Entity<ApplicationUser>().Property(x => x.SecurityStamp).HasDefaultValue(Guid.NewGuid().ToString()).ValueGeneratedOnAddOrUpdate();
            builder.Entity<ApplicationUserRole>().HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            builder.Entity<ApplicationUserRole>().HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
            builder.Entity<ApplicationUserToken>().Property(x => x.TokenType).HasConversion(x => x.ToString(), x => (TokenType) Enum.Parse(typeof(TokenType), x));
        }
    }
}
