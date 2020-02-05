using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class CitySearch
    {
        public int? CountryID { get; set; }
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string ShortName { get; set; }
        public int? PostalCode { get; set; }
        public bool? Active { get; set; }
    }
}
