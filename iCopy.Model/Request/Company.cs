using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class Company
    {
        [Required(ErrorMessage = "ErrNoName")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ErrNoContactAgent")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string ContactAgent { get; set; }
        [Required(ErrorMessage = "ErrNoJIB")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string Jib { get; set; }
        [Required(ErrorMessage = "ErrNoPhoneNumber")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "ErrTypePhoneNumber")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "ErrNoContactEmail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "EmailWrongFormat")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string Email { get; set; }
        [Required(ErrorMessage = "ErrNoAddress")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string Address { get; set; }
        [Required(ErrorMessage = "ErrNoCity")]
        public int? CityId { get; set; }
        public bool Active { get; set; }
        public Model.Request.ApplicationUserInsert User { get; set; }
        public Model.Request.ProfilePhoto ProfilePhoto { get; set; }
    }
}
