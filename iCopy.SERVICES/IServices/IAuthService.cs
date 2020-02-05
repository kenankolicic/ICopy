using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iCopy.SERVICES.IServices
{
    public interface IAuthService<TInsert, TUpdate, TResult, TSearch, TKey>
    {
        Task<TResult> InsertAsync(TInsert entity);
        Task<TResult> UpdateAsync(TKey id, TUpdate entity);
        Task<TResult> DeleteAsync(TKey id);
        Task<int> GetNumberOfRecordsAsync();
        Task<List<TResult>> TakeRecordsByNumberAsync(int take = 15);
        Task<Tuple<List<TResult>, int>> GetByParametersAsync(TSearch search, string order, string nameOfColumnOrder, int start, int length);
        Task<TResult> GetByIdAsync(TKey id);
    }
}
