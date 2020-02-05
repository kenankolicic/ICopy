using System.Threading.Tasks;

namespace iCopy.SERVICES.IServices
{
    public interface ICRUDService <TInsert, TUpdate, TResult, TSearch, TKey> : IReadService<TResult, TSearch, TKey>
    {
        Task<TResult> InsertAsync(TInsert entity);
        Task<TResult> UpdateAsync(TKey id, TUpdate entity);
        Task<TResult> DeleteAsync(TKey id);
        Task<TResult> ChangeActiveStatusAsync(TKey id);
    }
}
