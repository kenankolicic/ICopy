using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCopy.SERVICES.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Database.Country, Model.Response.Country>().ReverseMap();
            CreateMap<Database.Country, Model.Request.Country>().ReverseMap();
            CreateMap<Model.Response.Country, Model.Request.Country>().ReverseMap();

            CreateMap<Database.City, Model.Response.City>().ReverseMap();
            CreateMap<Database.City, Model.Request.City>().ReverseMap();
            CreateMap<Model.Response.City, Model.Request.City>().ReverseMap();

            CreateMap<Database.Company, Model.Response.Company>().ReverseMap();
            CreateMap<Database.Company, Model.Request.Company>().ReverseMap();
            CreateMap<Model.Response.Company, Model.Request.Company>().ReverseMap();

            CreateMap<Database.Copier, Model.Response.Copier>().ReverseMap();
            CreateMap<Database.Copier, Model.Request.Copier>().ReverseMap();
            CreateMap<Model.Response.Copier, Model.Request.Copier>().ReverseMap();

            CreateMap<Database.Employee, Model.Response.Employee>().ReverseMap();
            CreateMap<Database.Employee, Model.Request.Employee>().ReverseMap();
            CreateMap<Model.Response.Employee, Model.Request.Employee>().ReverseMap();  
            
            CreateMap<Database.Person, Model.Request.Person>().ReverseMap();
            CreateMap<Database.Person, Model.Response.Person>().ForMember(x => x.Gender, x => x.MapFrom(y => y.Gender.ToString())).ReverseMap();
            CreateMap<Model.Request.Person, Model.Response.Person>().ReverseMap();

            CreateMap<Database.Client, Model.Request.Client>().ReverseMap();
            CreateMap<Database.Client, Model.Response.Client>().ReverseMap();

            CreateMap<Database.ProfilePhoto, Model.Response.ProfilePhoto>().ReverseMap();
            CreateMap<Database.ProfilePhoto, Model.Request.ProfilePhoto>().ReverseMap();

            CreateMap<Database.ApplicationUser, Model.Request.ApplicationUserInsert>()
                .ReverseMap()
                .ForMember(x => x.NormalizedUserName, opt => opt.MapFrom(y => y.Username.ToUpper()))
                .ForMember(x => x.NormalizedEmail, opt => opt.MapFrom(y => y.Email.ToUpper()))
                .ForMember(x => x.SecurityStamp, opt => Guid.NewGuid().ToString());
            CreateMap<Database.ApplicationUser, Model.Request.ApplicationUserUpdate>()
                .ReverseMap()
                .ForMember(x => x.NormalizedUserName, opt => opt.MapFrom(y => y.Username.ToUpper()))
                .ForMember(x => x.NormalizedEmail, opt => opt.MapFrom(y => y.Email.ToUpper()))
                .ForMember(x => x.SecurityStamp, opt => Guid.NewGuid().ToString());
            CreateMap<Database.ApplicationUser, Model.Response.ApplicationUser>().ReverseMap();

            CreateMap<Database.PrintRequest, Model.Request.PrintRequest>().ReverseMap();

            #region SELECT LISTS
            CreateMap<Database.Country, SelectListItem>()
                .ForMember(x => x.Text, y => y.MapFrom(c => c.Name))
                .ForMember(x => x.Value, y => y.MapFrom<string>(c => c.ID.ToString()))
                .ReverseMap();
            CreateMap<Database.City, SelectListItem>()
                .ForMember(x => x.Text, y => y.MapFrom(c => c.Name))
                .ForMember(x => x.Value, y => y.MapFrom(c => c.ID.ToString()))
                .ReverseMap();
            CreateMap<Database.Company, SelectListItem>()
                .ForMember(x => x.Text, y => y.MapFrom(c => c.Name))
                .ForMember(x => x.Value, y => y.MapFrom(c => c.ID.ToString()))
                .ReverseMap();
            CreateMap<Database.Copier, SelectListItem>()
                .ForMember(x => x.Text, y => y.MapFrom(c => c.Name))
                .ForMember(x => x.Value, y => y.MapFrom(c => c.ID.ToString()))
                .ReverseMap();
            #endregion

        }
    }
}
