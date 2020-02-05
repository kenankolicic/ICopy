using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace iCopy.Web.Helper
{
    public static class Session
    {
        public static void Set<T>(this ISession session, string key, T entity) => session.SetString(key, JsonConvert.SerializeObject(entity));

        public static T Get<T>(this ISession session, string key) where T : class => !string.IsNullOrWhiteSpace(session.GetString(key)) ? JsonConvert.DeserializeObject<T>(session.GetString(key)) : null;

        public static class Keys
        {
            public static class Upload
            {
                public static string ProfileImage => nameof(ProfileImage);
                public static string PrintRequestFile => nameof(PrintRequestFile);
            }

        }
    }
}
