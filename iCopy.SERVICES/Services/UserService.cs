using AutoMapper;
using iCopy.Database;
using iCopy.Database.Context;
using iCopy.Model.Request;
using iCopy.Model.Response;
using iCopy.SERVICES.Exceptions;
using iCopy.SERVICES.Extensions;
using iCopy.SERVICES.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using ApplicationUser = iCopy.Model.Response.ApplicationUser;
using Enum = iCopy.Model.Enum.Enum;

namespace iCopy.SERVICES.Services
{
    public class UserService : AuthService<Database.ApplicationUser, Model.Request.ApplicationUserInsert, Model.Request.ApplicationUserUpdate, Model.Response.ApplicationUser, Model.Request.ApplicationUserSearch, int>, IUserService
    {
        private readonly IMapper mapper;
        private readonly AuthContext context;
        private readonly IPasswordHasher<Database.ApplicationUser> PasswordHasher;
        private readonly UserManager<Database.ApplicationUser> UserManager;
        private readonly DBContext database;
        private readonly IUserClaimsPrincipalFactory<Database.ApplicationUser> ClaimsPrincipalFactory;

        public UserService(
            AuthContext ctx,
            DBContext database,
            IMapper mapper,
            IUserClaimsPrincipalFactory<Database.ApplicationUser> ClaimsPrincipalFactory,
            IPasswordHasher<Database.ApplicationUser> PasswordHasher,
            UserManager<Database.ApplicationUser> UserManager) : base(ctx, mapper)
        {
            this.mapper = mapper;
            this.context = ctx;
            this.PasswordHasher = PasswordHasher;
            this.UserManager = UserManager;
            this.database = database;
            this.ClaimsPrincipalFactory = ClaimsPrincipalFactory;
        }

        public async Task<Model.Response.ApplicationUser> InsertAsync(Model.Request.ApplicationUserInsert user, params Enum.Roles[] roles)
        {
            Database.ApplicationUser model = mapper.Map<Database.ApplicationUser>(user);
            try
            {
                model.PasswordHash = PasswordHasher.HashPassword(model, user.Password);
                context.Users.Add(model);
                await context.SaveChangesAsync();
                foreach (var item in roles)
                {
                    Database.ApplicationRole role = await context.Roles.FirstOrDefaultAsync(x => x.Name == item.ToString());
                    context.Add(new Database.ApplicationUserRole() { RoleId = role.Id, UserId = model.Id });
                    await context.SaveChangesAsync();
                }

                // TODO: Dodati log operaciju
            }
            catch (Exception e)
            {
                // TODO Dodati log operaciju
                throw e;
            }
            return mapper.Map<Model.Response.ApplicationUser>(model);
        }

        public async Task<Model.Response.ApplicationUser> UpdatePassword(int applicationUserId, ChangePassword password)
        {
            Database.ApplicationUser user = await context.Users.FindAsync(applicationUserId);
            if(PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password.CurrentPassword) != PasswordVerificationResult.Success)
                throw new ModelStateException(nameof(password.CurrentPassword), "Current password is not correct");
            user.PasswordHash = PasswordHasher.HashPassword(user, password.NewPassword);
            try
            {
                await context.SaveChangesAsync();
                // TODO: Dodati log
            }
            catch (Exception e)
            {
                // TODO: Dodati log
                throw e;
            }   
            return mapper.Map<Model.Response.ApplicationUser>(user);
        }

        public async Task<string> GenerateAccountActivationToken(int applicationUserId)
        {
            Database.ApplicationUser user = await context.Users.FindAsync(applicationUserId);
            string token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            context.ApplicationUserTokens.Add(new Database.ApplicationUserToken()
            {
                TokenType = TokenType.AccountActivation,
                UserId = user.Id,
                Value = token
            });
            await context.SaveChangesAsync();
            return token;
        }

        public override async Task<ApplicationUser> UpdateAsync(int id, Model.Request.ApplicationUserUpdate entity)
        {
            Database.ApplicationUser user = await context.Users.FindAsync(id);
            if (user.UserName != entity.Username && await CheckUsernameUnique(id, entity.Username))
                throw new ModelStateException(nameof(Database.ApplicationUser.UserName), "Username is already in use");
            if (user.Email != entity.Email && await CheckEmailUnique(id, entity.Email)) 
                throw  new ModelStateException(nameof(Database.ApplicationUser.Email), "Email is already is use");
            if(user.PhoneNumber != entity.PhoneNumber && await CheckPhoneNumberUnique(id, entity.PhoneNumber))
                throw new ModelStateException(nameof(Database.ApplicationUser.Email), "Phone number is already is use");
            context.Users.Attach(user);
            mapper.Map(entity, user);
            context.Users.Update(user);
            try
            {
                await context.SaveChangesAsync();
                // TODO: Dodati log
            }
            catch (Exception e)
            {
                //TODO: Dodati log
                throw e;
            }

            return mapper.Map<Model.Response.ApplicationUser>(user);
        }

        private Task<bool> CheckUsernameUnique(int id, string username)
        {
            return context.Users.AnyAsync(x => x.UserName == username && x.Id != id);
        }

        private Task<bool> CheckEmailUnique(int id, string email)
        {
            return context.Users.AnyAsync(x => x.Email == email && x.Id != id);
        }

        private Task<bool> CheckPhoneNumberUnique(int id, string phoneNumber)
        {
            return context.Users.AnyAsync(x => x.PhoneNumber == phoneNumber && x.Id != id);
        }

        public override async Task<Tuple<List<Model.Response.ApplicationUser>, int>> GetByParametersAsync(ApplicationUserSearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            var query = context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search.Username))
                query = query.Where(x => x.UserName.Contains(search.Username));
            if (!string.IsNullOrWhiteSpace(search.Email))
                query = query.Where(x => x.Email.Contains(search.Email));
            if (search.Active != null)
                query = query.Where(x => x.Active == search.Active);

            if (nameOfColumnOrder == nameof(Database.ApplicationUser.UserName))
                query = query.OrderByAscDesc(x => x.UserName, order);
            else if (nameOfColumnOrder == nameof(Database.ApplicationUser.Email))
                query = query.OrderByAscDesc(x => x.Email, order);
            else if (nameOfColumnOrder == nameof(Database.ApplicationUser.PhoneNumber))
                query = query.OrderByAscDesc(x => x.PhoneNumber, order);

            return new Tuple<List<Model.Response.ApplicationUser>, int>(mapper.Map<List<Model.Response.ApplicationUser>>(await query.Skip(start).Take(length).ToListAsync()), await query.CountAsync());
        }

        public virtual async Task<Model.Response.ApplicationUser> ChangeActiveStatusAsync(int id)
        {
            Database.ApplicationUser model = await context.Users.FindAsync(id);
            model.Active = !model.Active;
            try
            {
                context.Users.Update(model);
                await context.SaveChangesAsync();
                // TODO: Dodati log operaciju

            }
            catch (Exception e)
            {
                //TODO: Dodati log operaciju
                throw e;
            }
            return mapper.Map<Model.Response.ApplicationUser>(model);
        }

        public async Task<bool> ActivateUserAccount(int id, string token)
        {
            Database.ApplicationUser user = await context.Users.FindAsync(id);
            Database.ApplicationUserToken dbToken = await context.ApplicationUserTokens.FirstOrDefaultAsync(x => x.TokenType == TokenType.AccountActivation && x.Value == token && x.Active);
            if (dbToken != null)
            {
                user.EmailConfirmed = true;
                user.ModifiedDate = DateTime.Now;
                dbToken.Active = false;
                dbToken.ModifiedDate = DateTime.Now;
                context.ApplicationUserTokens.Update(dbToken);
                context.Users.Update(user);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }

}
