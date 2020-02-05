using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace iCopy.Web.Resources
{
    public class Constants
    {
        private readonly IStringLocalizer localizer;
        public Constants(IStringLocalizerFactory localizer)
        {
            this.localizer = localizer.Create(nameof(Constants), new AssemblyName(typeof(Constants).Assembly.FullName).Name);
        }

        public IEnumerable<string> Genders
        {
            get
            {
                IEnumerable<string> data = localizer[nameof(Genders)].ToString().Split(';');
                return data;
            }
        }

        public string EmailActivationAccountSubject()
        {
            return localizer[nameof(EmailActivationAccountSubject)];
        }
        public string EmailActivationAccountBody(string link)
        {
            return localizer[nameof(EmailActivationAccountBody), link];
        }
    }
}
