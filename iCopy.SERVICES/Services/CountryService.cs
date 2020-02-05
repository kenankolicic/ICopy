using AutoMapper;
using iCopy.Model.Request;
using iCopy.SERVICES.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCopy.Database.Context;

namespace iCopy.SERVICES.Services
{
    public class CountryService : CRUDService<Database.Country, Model.Request.Country, Model.Request.Country, Model.Response.Country, Model.Request.CountrySearch, int>
    {
        public CountryService(DBContext ctx, IMapper mapper) : base(ctx, mapper)
        {
        }

        public override async Task<Tuple<List<Model.Response.Country>, int>> GetByParametersAsync(CountrySearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            var query = ctx.Countries.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search.Name))
                query = query.Where(x => x.Name.Contains(search.Name, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(search.ShortName))
                query = query.Where(x => x.ShortName.Contains(search.ShortName, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(search.PhoneCode))
                query = query.Where(x => x.PhoneNumberCode == search.PhoneCode);
            if (search.Active != null)
                query = query.Where(x => x.Active == search.Active);

            if (nameof(Database.Country.Name) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.Name, order);
            else if (nameof(Database.Country.ShortName) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.ShortName, order);
            else if (nameof(Database.Country.PhoneNumberCode) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.PhoneNumberCode, order);
            else if (nameof(Database.Country.ID) == nameOfColumnOrder)
                query = query.OrderByAscDesc(x => x.ID, order);

            var count = await query.CountAsync();
            query = query.Skip(start).Take(length);
            return new Tuple<List<Model.Response.Country>, int>(mapper.Map<List<Model.Response.Country>>(await query.ToListAsync()), count);
        }
    }
}
