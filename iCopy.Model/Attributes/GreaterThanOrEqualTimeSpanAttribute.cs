using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace iCopy.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class GreaterThanOrEqualTimeSpanAttribute : ValidationAttribute
    {
        public string PropertyName { get; set; }

        public GreaterThanOrEqualTimeSpanAttribute(string PropertyName)
        {
            this.PropertyName = PropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetRuntimeProperty(this.PropertyName);
            if(property == (PropertyInfo)null)
                return new ValidationResult(string.Format((IFormatProvider)CultureInfo.CurrentCulture, "UnknownProperty"));
            object objB = property.GetValue(validationContext.ObjectInstance, (object[])null);
            if (TimeSpan.Compare((TimeSpan) value, (TimeSpan) objB) < 0)
                return new ValidationResult(string.Format((IFormatProvider)CultureInfo.CurrentCulture, ErrorMessage));
            return ValidationResult.Success;
        }
    }
}
