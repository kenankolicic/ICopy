using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace iCopy.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UsernamePolicy : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Regex regex = new Regex(@"^(?=[A-Za-z0-9])[A-Za-z0-9._]{3,20}$");
            if (!regex.IsMatch(value.ToString())) return new ValidationResult(string.Format((IFormatProvider)CultureInfo.CurrentCulture, ErrorMessageString));
            return ValidationResult.Success;
        }
    }
}
