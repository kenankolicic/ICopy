using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class Country
    {
        [Required(ErrorMessage = "ErrNoName")]
        [MinLength(5, ErrorMessage = "ErrMinMaxName")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ErrNoShortName")]
        [MinLength(2, ErrorMessage = "ErrMinMaxShortName")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string ShortName { get; set; }
        [Required(ErrorMessage = "ErrNoPhoneNumberCode")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string PhoneNumberCode { get; set; }
        [Required(ErrorMessage = "ErrNoPhoneNumberRegex")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string PhoneNumberRegex { get; set; }
        public bool Active { get; set; }
    }
}
