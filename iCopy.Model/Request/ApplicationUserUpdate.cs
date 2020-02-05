using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class ApplicationUserUpdate
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ErrNoEmail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "EmailWrongFormat")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "ErrNoUsername")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string Username { get; set; }
        [Required(ErrorMessage = "ErrNoPhoneNumber")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "ErrTypePhoneNumber")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string PhoneNumber { get; set; }
        public bool ChangePassword { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool Active { get; set; }
    }
}
