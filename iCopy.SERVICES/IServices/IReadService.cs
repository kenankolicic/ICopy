using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iCopy.SERVICES.IServices
{
    public interface IReadService<TResult, TSearch, TPk> 
    {
        Task<List<TResult>> GetAllAsync();
        Task<TResult> GetByIdAsync(TPk id);
        Task<List<TResult>> GetByParametersAsync(TSearch search);
        Task<Tuple<List<TResult>, int>> GetByParametersAsync(TSearch search, string order, string nameOfColumnOrder, int start, int length);
        Task<int> GetNumberOfRecordsAsync();
        Task<List<TResult>> TakeRecordsByNumberAsync(int take = 15);
        Task<List<TResult>> GetAllActiveAsync();
    }
}
