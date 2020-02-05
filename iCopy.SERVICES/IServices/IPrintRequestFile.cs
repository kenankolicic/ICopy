using System.Threading.Tasks;

namespace iCopy.SERVICES.IServices
{
    interface IPrintRequestFile : ICRUDService<Model.Request.PrintRequestFile, Model.Request.PrintRequestFile, Model.Response.PrintRequestFile, object, int>
    {
        Task<Model.Response.PrintRequestFile> GetByApplicationUserId(int applicationUserId);
        Task<bool> DeleteByApplicationUserIdAsync(int applicationUserId);
    }
}
