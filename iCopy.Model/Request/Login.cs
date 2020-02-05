using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class Login
    {
        [Required(ErrorMessage = "ErrNoUsername")]
        [MaxLength(100, ErrorMessage = "ErrMaxLengthUsername")]
        public string Username { get; set; }
        [Required(ErrorMessage = "ErrNoPassword")]
        [MaxLength(100, ErrorMessage = "ErrMaxLengthPassword")]
        [DataType(DataType.Password, ErrorMessage = "ErrDataTypePassword")]
        public string Password { get; set; }
    }
}
