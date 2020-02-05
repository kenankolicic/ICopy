using AutoMapper;
using iCopy.Database.Context;
using iCopy.Model.Request;
using iCopy.Model.Response;
using iCopy.SERVICES.Extensions;
using iCopy.SERVICES.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCopy.SERVICES.Services
{
    public class CopierService : CRUDService<Database.Copier, Model.Request.Copier, Model.Request.Copier, Model.Response.Copier, Model.Request.CopierSearch, int>, ICopierService
    {
        private readonly AuthContext auth;
        private readonly IUserService UserService;
        private readonly IProfilePhotoService ProfilePhotoService;

        public CopierService(
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

        public override async Task<Model.Response.Copier> GetByIdAsync(int id)
        {
            Model.Response.Copier copier = mapper.Map<Model.Response.Copier>(await ctx.Copiers.Include(x => x.City).ThenInclude(x => x.Country).Include(x => x.Company).FirstOrDefaultAsync(x => x.ID == id));
            copier.User = mapper.Map<Model.Response.ApplicationUser>(await auth.Users.FindAsync(copier.ApplicationUserId));
            copier.ProfilePhoto = mapper.Map<Model.Response.ProfilePhoto>((await ctx.ApplicationUserProfilePhotos.Include(x => x.ProfilePhoto).FirstOrDefaultAsync(x => x.ApplicationUserId == copier.ApplicationUserId && x.Active))?.ProfilePhoto);
            return copier;
        }

        public override async Task<List<Model.Response.Copier>> TakeRecordsByNumberAsync(int take = 15)
        {
            List<Model.Response.Copier> items = mapper.Map<List<Model.Response.Copier>>(await ctx.Copiers.Include(x => x.Company).Include(x => x.City).ThenInclude(x => x.Country).ToListAsync());
            return items;
        }

        public override async Task<Tuple<List<Model.Response.Copier>, int>> GetByParametersAsync(CopierSearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            var query = ctx.Copiers.Include(x => x.Company).Include(x => x.City).ThenInclude(x => x.Country).AsQueryable();
            if (search.CityID != null)
                query = query.Where(x => x.CityId == search.CityID);
            if (search.CountryID != null)
                query = query.Where(x => x.City.CountryID == search.CountryID);
            if (search.CompanyID != null)
                query = query.Where(x => x.CompanyId == search.CompanyID);
            if (search.Active != null)
                query = query.Where(x => x.Active == search.Active);
            if (!string.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name.Contains(search.Name));

            if (nameOfColumnOrder == nameof(Database.Copier.ID))
                query = query.OrderByAscDesc(x => x.ID, order);
            else if (nameOfColumnOrder == nameof(Database.Copier.Name))
                query = query.OrderByAscDesc(x => x.Name, order);
            else if(nameOfColumnOrder == nameof(Database.Copier.CityId))
                query = query.OrderByAscDesc(x => x.CityId, order);

            var data = mapper.Map<List<Model.Response.Copier>>(await query.Skip(start).Take(length).ToListAsync());
            return new Tuple<List<Model.Response.Copier>, int>(data, await query.CountAsync());
        }

        public override async Task<Model.Response.Copier> DeleteAsync(int id)
        {
            Database.Copier copier = await ctx.Copiers.FindAsync(id);
            try
            {
                IEnumerable<Database.ApplicationUserProfilePhoto> copierProfile = await ctx.ApplicationUserProfilePhotos.Where(x => x.ApplicationUserId == copier.ApplicationUserId).ToListAsync();
                IEnumerable<Database.ProfilePhoto> profilePhotos = await ctx.ProfilePhotos.Where(x => copierProfile.Any(y => y.ProfilePhotoId == x.ID)).ToListAsync();
                if (copierProfile != null)
                {
                    ctx.ApplicationUserProfilePhotos.RemoveRange(copierProfile);
                    await ctx.SaveChangesAsync();
                }

                if (profilePhotos != null)
                {
                    ctx.ProfilePhotos.RemoveRange(profilePhotos);
                    await ctx.SaveChangesAsync();
                }
                ctx.Copiers.Remove(copier);
                await ctx.SaveChangesAsync();
                await UserService.DeleteAsync(copier.ApplicationUserId);
                // TODO: Dodati log operaciju
            }
            catch (Exception e)
            {
                //TODO: Dodati log operaciju
                throw e;
            }

            return mapper.Map<Model.Response.Copier>(copier);
        }

        public override async Task<Model.Response.Copier> InsertAsync(Model.Request.Copier entity)
        {
            Database.Copier model = mapper.Map<Database.Copier>(entity);
            try
            {
                Model.Response.ApplicationUser user = await UserService.InsertAsync(entity.User, Model.Enum.Enum.Roles.Copier);
                model.ApplicationUserId = user.ID;
                ctx.Copiers.Add(model);
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

            return mapper.Map<Model.Response.Copier>(model);
        }

        public override async Task<Model.Response.Copier> UpdateAsync(int id, Model.Request.Copier entity)
        {
            try
            {
                Model.Response.Copier copier = await base.UpdateAsync(id, entity);
                if (entity.ProfilePhoto != null)
                {
                    entity.ProfilePhoto.ApplicationUserId = copier.ApplicationUserId;
                    await ProfilePhotoService.InsertAsync(entity.ProfilePhoto);
                }
                // TODO: Dodati Log
                return copier;
            }
            catch (Exception e)
            {
                // TODO: Dodati log
                throw e;
            }
        }
    }
}
