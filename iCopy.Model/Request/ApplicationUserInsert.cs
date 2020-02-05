using System.ComponentModel.DataAnnotations;
using iCopy.Model.Attributes;

namespace iCopy.Model.Request
{
    public class ApplicationUserInsert
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ErrNoEmail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "EmailWrongFormat")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        [Unique(Type = UniqueAttribute.Email, ErrorMessage = "ErrUniqueEmail")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "ErrNoUsername")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        [Unique(Type = UniqueAttribute.Username, ErrorMessage = "ErrUniqueUsername")]
        [UsernamePolicy(ErrorMessage = "ErrUsernamePolicy")]
        public string Username { get; set; }
        [Required(ErrorMessage = "ErrNoPhoneNumber")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "ErrTypePhoneNumber")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        [Unique(Type = UniqueAttribute.PhoneNumber, ErrorMessage = "ErrUniquePhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "ErrNoPassword")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        [MinLength(8, ErrorMessage = "ErrMinLenghtPassword")]
        [PasswordPolicy(ErrorMessage = "ErrPasswordPolicy")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "ErrNoPasswordConfirm")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        [MinLength(8, ErrorMessage = "ErrMinLenghtPasswordConfirm")]
        [Compare(nameof(Password), ErrorMessage = "ErrNotSamePasswordConfirm")]
        [PasswordPolicy(ErrorMessage = "asdasd")]
        public string PasswordConfirm { get; set; }
    }
}
