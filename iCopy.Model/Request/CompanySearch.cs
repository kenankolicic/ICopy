using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class CompanySearch
    {
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string Address { get; set; }
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string ContactAgent { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public bool? Active { get; set; }
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string JIB { get; set; }
    }
}
