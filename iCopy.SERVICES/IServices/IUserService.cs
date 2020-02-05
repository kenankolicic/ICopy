using System.Threading.Tasks;

namespace iCopy.SERVICES.IServices
{
    public interface IUserService : IAuthService<Model.Request.ApplicationUserInsert, Model.Request.ApplicationUserUpdate, Model.Response.ApplicationUser, Model.Request.ApplicationUserSearch, int>
    {
        Task<Model.Response.ApplicationUser> InsertAsync(Model.Request.ApplicationUserInsert user, params Model.Enum.Enum.Roles[] roles);
        Task<Model.Response.ApplicationUser> UpdatePassword(int applicationUserId, Model.Request.ChangePassword password);
        Task<string> GenerateAccountActivationToken(int applicationUserId);
        Task<Model.Response.ApplicationUser> ChangeActiveStatusAsync(int id);
        Task<bool> ActivateUserAccount(int id, string token);
    }
}
