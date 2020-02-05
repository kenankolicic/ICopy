using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class CopierSearch
    {
        [MaxLength(100, ErrorMessage = "ErrMaxLenght")]
        public string Name { get; set; }

        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public int? CompanyID { get; set; }
        public bool? Active { get; set; }
    }
}
