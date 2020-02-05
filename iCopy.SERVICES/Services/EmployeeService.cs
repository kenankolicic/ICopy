using AutoMapper;
using iCopy.Database.Context;
using iCopy.Model.Request;
using iCopy.SERVICES.Exceptions;
using iCopy.SERVICES.Extensions;
using iCopy.SERVICES.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCopy.SERVICES.Services
{
    public class EmployeeService : CRUDService<Database.Employee, Model.Request.Employee, Model.Request.Employee, Model.Response.Employee, Model.Request.EmployeeSearch, int>, IEmployeeService
    {
        private readonly AuthContext auth;
        private readonly IUserService UserService;
        private readonly IProfilePhotoService ProfilePhotoService;
        private readonly IPasswordHasher<Database.ApplicationUser> PasswordHasher;

        public EmployeeService(DBContext ctx,
            IMapper mapper,
            AuthContext auth,
            IUserService UserService,
            IProfilePhotoService ProfilePhotoService,
            IPasswordHasher<Database.ApplicationUser> PasswordHasher) : base(ctx, mapper)
        {
            this.auth = auth;
            this.UserService = UserService;
            this.ProfilePhotoService = ProfilePhotoService;
            this.PasswordHasher = PasswordHasher;
        }

        public async Task<Model.Response.ApplicationUser> UpdatePassword(int applicationUserId, ChangePassword password)
        {
            Database.ApplicationUser user = await auth.Users.FindAsync(applicationUserId);
            if (PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password.CurrentPassword) != PasswordVerificationResult.Success)
                throw new ModelStateException(nameof(password.CurrentPassword), "Current password is not correct");
            user.PasswordHash = PasswordHasher.HashPassword(user, password.NewPassword);
            try
            {
                await auth.SaveChangesAsync();
                // TODO: Dodati log
            }
            catch (Exception e)
            {
                // TODO: Dodati log
                throw e;
            }
            return mapper.Map<Model.Response.ApplicationUser>(user);
        }

        public override async Task<Model.Response.Employee> GetByIdAsync(int id)
        {
            Model.Response.Employee employee = mapper.Map<Model.Response.Employee>(await ctx.Employees.Include(x => x.Copier).Include(x => x.Person).ThenInclude(x => x.City).FirstOrDefaultAsync(x => x.ID == id));
            employee.User = mapper.Map<Model.Response.ApplicationUser>(await auth.Users.FindAsync(employee.ApplicationUserId));
            employee.ProfilePhoto = await ProfilePhotoService.GetByApplicationUserId(employee.ApplicationUserId);
            return employee;
        }

        public override async Task<Model.Response.Employee> InsertAsync(Model.Request.Employee entity)
        {
            Database.Employee model = mapper.Map<Database.Employee>(entity);
            try
            {
                Model.Response.ApplicationUser user = await UserService.InsertAsync(entity.User, Model.Enum.Enum.Roles.Employee);
                model.ApplicationUserId = user.ID;
                ctx.Employees.Add(model);
                await ctx.SaveChangesAsync();
                if (entity.ProfilePhoto != null)
                {
                    entity.ProfilePhoto.ApplicationUserId = user.ID;
                    await ProfilePhotoService.InsertAsync(entity.ProfilePhoto);
                }
                // TODO: Dodati log operaciju
            }
            catch (Exception e)
            {
                //TODO: Dodati log operaciju
                throw e;
            }
            return mapper.Map<Model.Response.Employee>(model);
        }

        public override async Task<Model.Response.Employee> UpdateAsync(int id, Model.Request.Employee entity)
        {
            try
            {
                Model.Response.Employee employee = await base.UpdateAsync(id, entity);
                if (entity.ProfilePhoto != null)
                {
                    entity.ProfilePhoto.ApplicationUserId = employee.ApplicationUserId;
                    await ProfilePhotoService.InsertAsync(entity.ProfilePhoto);
                }
                // TODO: Dodati Log
                return employee;
            }
            catch (Exception e)
            {
                // TODO: Dodati log
                throw e;
            }
        }

        public override async Task<Model.Response.Employee> DeleteAsync(int id)
        {
            Database.Employee employee = await ctx.Employees.FindAsync(id);
            try
            {
                IEnumerable<Database.ApplicationUserProfilePhoto> employeeProfile = await ctx.ApplicationUserProfilePhotos.Where(x => x.ApplicationUserId == employee.ApplicationUserId).ToListAsync();
                IEnumerable<Database.ProfilePhoto> profilePhotos = await ctx.ProfilePhotos.Where(x => employeeProfile.Any(y => y.ProfilePhotoId == x.ID)).ToListAsync();
                if (employeeProfile != null)
                {
                    ctx.ApplicationUserProfilePhotos.RemoveRange(employeeProfile);
                    await ctx.SaveChangesAsync();
                }

                if (profilePhotos != null)
                {
                    ctx.ProfilePhotos.RemoveRange(profilePhotos);
                    await ctx.SaveChangesAsync();
                }
                ctx.Employees.Remove(employee);
                await ctx.SaveChangesAsync();
                await UserService.DeleteAsync(employee.ApplicationUserId);
                // TODO: Dodati log operaciju
            }
            catch (Exception e)
            {
                //TODO: Dodati log operaciju
                throw e;
            }

            return mapper.Map<Model.Response.Employee>(employee);
        }

        public override async Task<List<Model.Response.Employee>> TakeRecordsByNumberAsync(int take = 15)
        {
            List<Model.Response.Employee> employee = mapper.Map<List<Model.Response.Employee>>(await ctx.Employees.Include(x => x.Person).Include(x => x.Copier).Take(take).ToListAsync());
            employee.ForEach(x => x.User = mapper.Map<Model.Response.ApplicationUser>(auth.Users.Find(x.ApplicationUserId)));
            return employee;
        }

        public override async Task<Tuple<List<Model.Response.Employee>, int>> GetByParametersAsync(EmployeeSearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            var query = ctx.Employees.Include(x => x.Copier).ThenInclude(x => x.Company).Include(x => x.Person.City).ThenInclude(x => x.Country).AsQueryable();
            if (search.CopierId != null)
                query = query.Where(x => x.Person.CityId == search.CopierId);
            if (search.CompanyId != null)
                query = query.Where(x => x.Copier.CompanyId == search.CompanyId);
            if (search.Gender != null)
                query = query.Where(x => x.Person.Gender == search.Gender);
            if (!string.IsNullOrWhiteSpace(search.FirstName))
                query = query.Where(x => x.Person.FirstName.Contains(search.FirstName));
            if (!string.IsNullOrWhiteSpace(search.LastName))
                query = query.Where(x => x.Person.LastName.Contains(search.LastName));
            if (search.Active != null)
                query = query.Where(x => x.Active == search.Active);

            if (nameOfColumnOrder == nameof(Database.Employee.ID))
                query = query.OrderByAscDesc(x => x.ID, order);
            else if (nameOfColumnOrder == nameof(Database.Employee.Person.FirstName))
                query = query.OrderByAscDesc(x => x.Person.FirstName, order);
            else if (nameOfColumnOrder == nameof(Database.Employee.Person.LastName))
                query = query.OrderByAscDesc(x => x.Person.LastName, order);

            var data = mapper.Map<List<Model.Response.Employee>>(await query.Skip(start).Take(length).ToListAsync());
            return new Tuple<List<Model.Response.Employee>, int>(data, await query.CountAsync());
        }
    }
}
