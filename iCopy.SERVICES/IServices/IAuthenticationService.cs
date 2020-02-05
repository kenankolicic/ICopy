using System.Threading.Tasks;
using iCopy.Model.Response;

namespace iCopy.SERVICES.IServices
{
    public interface IAuthenticationService
    {
        Task<LoginResult> Authenticate(Model.Request.Login login);
    }
}
