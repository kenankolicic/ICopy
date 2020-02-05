using AutoMapper;
using iCopy.Database;
using iCopy.SERVICES.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCopy.Database.Context;

namespace iCopy.SERVICES.Services
{
    public class ReadService<TModel, TResult, TSearch, TKey> : IReadService<TResult, TSearch, TKey> 
        where TModel : BaseEntity<TKey>
    {
        private readonly DBContext ctx;
        private readonly IMapper mapper;

        public ReadService(DBContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public virtual async Task<List<TResult>> GetAllActiveAsync()
        {
            return mapper.Map<List<TResult>>(await ctx.Set<TModel>().Where(x => x.Active).ToListAsync());
        }

        public virtual async Task<List<TResult>> GetAllAsync()
        {
            return mapper.Map<List<TResult>>(await ctx.Set<TModel>().ToListAsync());
        }

        public virtual async Task<TResult> GetByIdAsync(TKey id)
        {
            return mapper.Map<TResult>(await ctx.Set<TModel>().FindAsync(id));
        }

        public virtual async Task<List<TResult>> GetByParametersAsync(TSearch search)
        {
            return mapper.Map<List<TResult>>(await ctx.Set<TModel>().ToListAsync());
        }

        public virtual async Task<Tuple<List<TResult>, int>> GetByParametersAsync(TSearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            return new Tuple<List<TResult>, int>(mapper.Map<List<TModel>, List<TResult>>(await ctx.Set<TModel>().Skip(start).Take(length).ToListAsync()), length);
        }

        public virtual async Task<int> GetNumberOfRecordsAsync()
        {
            return await ctx.Set<TModel>().CountAsync();
        }

        public virtual async Task<List<TResult>> TakeRecordsByNumberAsync(int take = 15)
        {
            return mapper.Map<List<TResult>>(await ctx.Set<TModel>().Take(take).ToListAsync());
        }
    }
}
