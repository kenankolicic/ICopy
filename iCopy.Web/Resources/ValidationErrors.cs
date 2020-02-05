using Microsoft.Extensions.Localization;

namespace iCopy.Web
{
    public class ValidationErrors
    {
        private readonly IStringLocalizer<ValidationErrors> localizer;

        public ValidationErrors(IStringLocalizer<ValidationErrors> localizer)
        {
            this.localizer = localizer;
        }

        #region ErrorMessages

        public string ErrThisFieldIsRequired => localizer[nameof(ErrThisFieldIsRequired)];
        public string ErrEnterValidNumber => localizer[nameof(ErrEnterValidNumber)];
        public string ErrEnterTheSameValueAgain => localizer[nameof(ErrEnterTheSameValueAgain)];
        public string EmailWrongFormat => localizer[nameof(EmailWrongFormat)];
        public string ErrMaxNumberOfCharacters => localizer[nameof(ErrMaxNumberOfCharacters)];
        public string ErrMinNumberOfCharacters => localizer[nameof(ErrMinNumberOfCharacters)];

        #endregion


        public string LocalizedString(string value)
        {
            return localizer[value];
        }
    }
}
