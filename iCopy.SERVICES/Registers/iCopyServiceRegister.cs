using AutoMapper;
using iCopy.SERVICES.Auth;
using iCopy.SERVICES.IServices;
using iCopy.SERVICES.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace iCopy.SERVICES.Registers
{
    public static class iCopyServiceRegister
    {
        public static IServiceCollection AddiCopyServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapper.Mapper).Assembly);
            services.AddScoped<ICRUDService<Model.Request.Country, Model.Request.Country, Model.Response.Country, Model.Request.CountrySearch, int>,
                CountryService>();
            services.AddScoped<IReadService<Model.Response.Country, Model.Request.CountrySearch, int>, CountryService>();
            services.AddScoped<IReadService<Model.Response.City, Model.Request.CitySearch, int>, CityService>();
            services.AddScoped<ICRUDService<Model.Request.City, Model.Request.City, Model.Response.City, Model.Request.CitySearch, int>,
                CityService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICopierService, CopierService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfilePhotoService, ProfilePhotoService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IUserClaimsPrincipalFactory<Database.ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPrintRequestService, PrintRequestService>();
            services.AddScoped<IPrintRequestFile, PrintRequestFileService>();
            return services;
        }
    }
}
