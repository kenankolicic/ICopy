using iCopy.ExternalServices.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace iCopy.ExternalServices
{
    public static class ExternalServicesRegister
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}
