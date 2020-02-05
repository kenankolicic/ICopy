using AutoMapper;
using iCopy.Model.Request;
using iCopy.Database.Context;
using iCopy.SERVICES.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCopy.SERVICES.Services
{
    public class CityService : CRUDService<Database.City, Model.Request.City, Model.Request.City, Model.Response.City, Model.Request.CitySearch, int>
    {
        public CityService(DBContext ctx, IMapper mapper) : base(ctx, mapper)
        {
        }

        public override async Task<Tuple<List<Model.Response.City>, int>> GetByParametersAsync(CitySearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            var query = ctx.Cities.Include(x => x.Country).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name.Contains(search.Name, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(search.ShortName))
                query = query.Where(x => x.ShortName.Contains(search.ShortName, StringComparison.CurrentCultureIgnoreCase));
            if (search.CountryID != null)
                query = query.Where(x => x.CountryID == search.CountryID);
            if (search.PostalCode != null)
                query = query.Where(x => x.PostalCode == search.PostalCode);
            if (search.Active != null)
                query = query.Where(x => x.Active == search.Active);

            if (nameof(Database.City.Name) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.Name, order);
            else if (nameof(Database.City.ShortName) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.ShortName, order);
            else if (nameof(Database.City.CountryID) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.Country.Name, order);
            else if (nameof(Database.City.ID) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.ID, order);

            var count = await query.CountAsync();
            query = query.Skip(start).Take(length);

            return new Tuple<List<Model.Response.City>, int>(mapper.Map<List<Model.Response.City>>(await query.ToListAsync()), count);
        }

        public async override Task<List<Model.Response.City>> TakeRecordsByNumberAsync(int take = 15)
        {
            return mapper.Map<List<Model.Response.City>>(await ctx.Cities.Include(x => x.Country).OrderBy(x => x.ID).Take(take).ToListAsync());
        }
    }
}
