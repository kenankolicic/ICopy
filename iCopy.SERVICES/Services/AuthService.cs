using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iCopy.Database.Context;
using iCopy.SERVICES.IServices;
using Microsoft.EntityFrameworkCore;

namespace iCopy.SERVICES.Services
{
    public class AuthService<TModel, TInsert, TUpdate, TResult, TSearch, TKey> :  IAuthService<TInsert, TUpdate, TResult, TSearch, TKey> where TModel : class
    {
        private readonly AuthContext context;
        private readonly IMapper mapper;

        public AuthService(AuthContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public virtual async Task<TResult> InsertAsync(TInsert entity)
        {
            TModel model = mapper.Map<TModel>(entity);
            context.Set<TModel>().Attach(model);
            context.Set<TModel>().Add(model);
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
            return mapper.Map<TResult>(model);
        }

        public virtual async Task<TResult> UpdateAsync(TKey id, TUpdate entity)
        {
            TModel model = await context.Set<TModel>().FindAsync(id);
            context.Set<TModel>().Attach(model);
            mapper.Map(model, entity);
            context.Set<TModel>().Update(model);
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

            return mapper.Map<TResult>(model);
        }

        public virtual async Task<TResult> DeleteAsync(TKey id)
        {
            TModel model = await context.Set<TModel>().FindAsync(id);
            context.Set<TModel>().Remove(model);
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

            return mapper.Map<TResult>(model);
        }

        public virtual async Task<List<TResult>> GetAllAsync()
        {
            return mapper.Map<List<TResult>>(await context.Set<TModel>().ToListAsync());
        }

        public virtual async Task<TResult> GetByIdAsync(TKey id)
        {
            return mapper.Map<TResult>(await context.Set<TModel>().FindAsync(id));
        }

        public virtual async Task<List<TResult>> GetByParametersAsync(TSearch search)
        {
            return mapper.Map<List<TResult>>(await context.Set<TModel>().ToListAsync());
        }

        public virtual async Task<int> GetNumberOfRecordsAsync()
        {
            return await context.Set<TModel>().CountAsync();
        }

        public virtual async Task<List<TResult>> TakeRecordsByNumberAsync(int take = 15)
        {
            return mapper.Map<List<TResult>>(await context.Set<TModel>().Take(take).ToListAsync());
        }

        public virtual async Task<Tuple<List<TResult>, int>> GetByParametersAsync(TSearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            return new Tuple<List<TResult>, int>(mapper.Map<List<TModel>, List<TResult>>(await context.Set<TModel>().Skip(start).Take(length).ToListAsync()), length);
        }
    }
}
