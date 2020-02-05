namespace iCopy.Web.Helper
{
    public class Settings
    {
        public static class Routes
        {

            public static class City
            {
                public const string Index = "/location/city/index";
                public const string Insert = "/location/city/insert";
                public const string Update = "/location/city/update";
                public const string Delete = "/location/city/delete";
                public const string GetData = "/location/city/getdata";
                public const string ChangeActiveStatus = "/location/city/changeactivestatus";
            }
            public static class Country
            {
                public const string Index = "/location/country/index";
                public const string Insert = "/location/country/insert";
                public const string Update = "/location/country/update";
                public const string Delete = "/location/country/delete";
                public const string GetData = "/location/country/getdata";
                public const string ChangeActiveStatus = "/location/country/changeactivestatus";
            }
            public static class Company
            {
                public const string Index = "/administration/company/index";
                public const string Insert = "/administration/company/insert";
                public const string Update = "/administration/company/update";
                public const string Delete = "/administration/company/delete";
                public const string GetData = "/administration/company/getdata";
                public const string ChangeActiveStatus = "/administration/company/changeactivestatus";
                public const string Details = "/administration/company/details";
            }
            public static class Copier
            {
                public const string Index = "/administration/copier/index";
                public const string Insert = "/administration/copier/insert";
                public const string Update = "/administration/copier/update";
                public const string Delete = "/administration/copier/delete";
                public const string GetData = "/administration/copier/getdata";
                public const string ChangeActiveStatus = "/administration/copier/changeactivestatus";
                public const string Details = "/administration/copier/details";
            }
            public static class Employee
            {
                public const string Index = "/administration/employee/index";
                public const string Insert = "/administration/employee/insert";
                public const string Update = "/administration/employee/update";
                public const string Delete = "/administration/employee/delete";
                public const string GetData = "/administration/employee/getdata";
                public const string ChangeActiveStatus = "/administration/employee/changeactivestatus";
                public const string Details = "/administration/employee/details";
            }
            public static class User
            {
                public const string Index = "/administration/user/index";
                public const string ChangeActiveStatus = "/administration/user/changeactivestatus";
                public const string GetData = "/administration/user/getdata";
                public const string Update = "/administration/user/update";
                public const string UpdatePassword = "/administration/user/updatepassword";
                public const string Details = "/administration/user/details";
            }
            public static class PrintRequest
            {
                public const string Index = "/administration/printrequest/index";
                public const string Insert = "/administration/printrequest/insert";
                public const string Update = "/administration/printrequest/update";
                public const string Delete = "/administration/printrequest/delete";
                public const string GetData = "/administration/printrequest/getdata";
                public const string ChangeActiveStatus = "/administration/printrequest/changeactivestatus";
                public const string Details = "/administration/printrequest/details";
            }
            public static class SelectList
            {
                public const string Cities = "/selectlist/cities";
                public const string CitiesByCountry = "/selectlist/CitiesByCountry";
                public const string CopiersByCompanyId = "/selectlist/copiersbycompanyid";
            }
            public static class Upload
            {
                public const string UploadProfileImage = "/upload/UploadProfileImage";
                public const string RemoveUploadedProfileImage = "/upload/RemoveUploadedProfileImage";
                public const string UploadFile = "/upload/UploadPrintRequestFile";
                public const string RemoveUploadedFile = "/upload/RemoveUploadedFile";
            }
            public static class SignUp
            {
                public const string Index = "/auth/signup/index";
            }
            public static class Login
            {
                public const string Index = "/auth/login/";
                public const string Logout = "/auth/login/logout";
                public const string ActivateAcount = "/auth/login/activate";
            }
            public static class Dashboard
            {
                public const string Index = "/administration/dashboard/";
            }
        }
        public static class Defaults
        {
            public static class Photo
            {
                public const string ProfilePhoto = "assets/media/users/default_user.png";
            }
        }
    }
}
