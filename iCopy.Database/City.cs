using System.ComponentModel.DataAnnotations.Schema;

namespace iCopy.Database
{
    public class City : BaseEntity<int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int PostalCode { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryID { get; set; }
        public Country Country { get; set; }
    }
}
