using System.Threading.Tasks;

namespace iCopy.SERVICES.IServices
{
    public interface IProfilePhotoService : ICRUDService<Model.Request.ProfilePhoto, Model.Request.ProfilePhoto, Model.Response.ProfilePhoto, object, int>
    {
        Task<Model.Response.ProfilePhoto> GetByApplicationUserId(int applicationUserId);
        Task<bool> DeleteByApplicationUserIdAsync(int applicationUserId);
    }
}


