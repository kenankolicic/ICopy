using AutoMapper;
using iCopy.Model.Request;
using iCopy.SERVICES.Extensions;
using iCopy.SERVICES.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using iCopy.Database.Context;
using Company = iCopy.Model.Response.Company;

namespace iCopy.SERVICES.Services
{
    public class CompanyService : CRUDService<Database.Company, Model.Request.Company, Model.Request.Company, Model.Response.Company, Model.Request.CompanySearch, int>, ICompanyService
    {
        private readonly AuthContext auth;
        private readonly IUserService UserService;
        private readonly IProfilePhotoService ProfilePhotoService;

        public CompanyService(
            DBContext ctx,
            IMapper mapper,
            AuthContext auth,
            IUserService UserService,
            IProfilePhotoService ProfilePhotoService) : base(ctx, mapper)
        {
            this.auth = auth;
            this.UserService = UserService;
            this.ProfilePhotoService = ProfilePhotoService;
        }

        public override async Task<Model.Response.Company> GetByIdAsync(int id)
        {
            Model.Response.Company company = mapper.Map<Model.Response.Company>(await ctx.Companies.Include(x => x.City).ThenInclude(x => x.Country).FirstOrDefaultAsync(x => x.ID == id));
            company.User = mapper.Map<Model.Response.ApplicationUser>(await auth.Users.FindAsync(company.ApplicationUserId));
            company.ProfilePhoto = await ProfilePhotoService.GetByApplicationUserId(company.ApplicationUserId);
            return company;
        }

        public override async Task<List<Model.Response.Company>> TakeRecordsByNumberAsync(int take = 15)
        {
            List<Model.Response.Company> items = mapper.Map<List<Model.Response.Company>>(await ctx.Companies.Include(x => x.City).ToListAsync());
            return items;
        }

        public override async Task<Tuple<List<Model.Response.Company>, int>> GetByParametersAsync(CompanySearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            var query = ctx.Companies
                .Include(x => x.City)
                .AsQueryable();
            if (search.Active != null)
                query = query.Where(x => x.Active == search.Active);
            if (search.CityID != null)
                query = query.Where(x => x.CityId == search.CityID);
            if (search.CountryID != null)
                query = query.Where(x => x.City.CountryID == search.CountryID);
            if (!string.IsNullOrWhiteSpace(search.Address))
                query = query.Where(x => x.Address.Contains(search.Address, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(search.ContactAgent))
                query = query.Where(x => x.ContactAgent.Contains(search.ContactAgent, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name.Contains(search.Name, StringComparison.CurrentCultureIgnoreCase));

            if (nameof(Database.Company.ID) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.ID, order);
            else if (nameof(Database.Company.Name) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.Name, order);
            else if (nameof(Database.Company.ContactAgent) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.ContactAgent, order);
            else if (nameof(Database.Company.PhoneNumber) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.PhoneNumber, order);
            else if (nameof(Database.Company.CityId) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.City.Name, order);

            var data = mapper.Map<List<Model.Response.Company>>(await query.Skip(start).Take(length).ToListAsync());
            return new Tuple<List<Model.Response.Company>, int>(data, await query.CountAsync());
        }

        public override async Task<Model.Response.Company> DeleteAsync(int id)
        {
            Database.Company company = await ctx.Companies.FindAsync(id);
            try
            {
                ctx.Companies.Remove(company);
                await ctx.SaveChangesAsync();

                await ProfilePhotoService.DeleteByApplicationUserIdAsync(company.ApplicationUserId);

                await UserService.DeleteAsync(company.ApplicationUserId);
                // TODO: Dodati log operaciju
            }
            catch (Exception e)
            {
                //TODO: Dodati log operaciju
                throw e;
            }

            return mapper.Map<Model.Response.Company>(company);
        }

        public override async Task<Model.Response.Company> InsertAsync(Model.Request.Company entity)

        {
            Database.Company model = mapper.Map<Database.Company>(entity);
            try
            {
                Model.Response.ApplicationUser user = await UserService.InsertAsync(entity.User, Model.Enum.Enum.Roles.Company);
                model.ApplicationUserId = user.ID;
                if (entity.ProfilePhoto != null)
                {
                    entity.ProfilePhoto.ApplicationUserId = user.ID;
                    await ProfilePhotoService.InsertAsync(entity.ProfilePhoto);
                }

                ctx.Companies.Add(model);
                await ctx.SaveChangesAsync();
                // TODO: Dodati log operaciju
            }
            catch (Exception e)
            {
                //TODO: Dodati log operaciju
                throw e;
            }

            return mapper.Map<Model.Response.Company>(model);
        }

        public override async Task<Company> UpdateAsync(int id, Model.Request.Company entity)
        {
            try
            {
                Model.Response.Company company = await base.UpdateAsync(id, entity);
                if (entity.ProfilePhoto != null)
                {
                    entity.ProfilePhoto.ApplicationUserId = company.ApplicationUserId;
                    await ProfilePhotoService.InsertAsync(entity.ProfilePhoto);
                }
                // TODO: Dodati Log
                return company;
            }
            catch (Exception e)
            {
                // TODO: Dodati log
                throw e;
            }
        }
    }
}
