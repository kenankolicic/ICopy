using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace iCopy.Web.Helper
{
    public static class Localization
    {
        public static IList<CultureInfo> SupportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("bs-Latn-BA")
        };
        public static readonly string CurrentCultureCookieName = "iCopy.Culture.CurrentCulture";

        public static IServiceCollection ConfigureLocalization(this IServiceCollection serviceDescriptors)
        {
            return serviceDescriptors.Configure<RequestLocalizationOptions>(config =>
            {
                config.DefaultRequestCulture = new RequestCulture("en-US");
                config.SupportedCultures = SupportedCultures;
                config.SupportedUICultures = SupportedCultures;
                config.RequestCultureProviders.OfType<CookieRequestCultureProvider>().First().CookieName = CurrentCultureCookieName;
            });
        }

    }
}
