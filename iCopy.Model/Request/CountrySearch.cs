using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class CountrySearch
    {
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string ShortName { get; set; }
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string PhoneCode { get; set; }
        public bool? Active { get; set; }
    }
}
