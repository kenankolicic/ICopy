using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCopy.Database
{
    public class Copier : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan StartWorkingTime { get; set; }

        public TimeSpan EndWorkingTime { get; set; }

        public string Url { get; set; }

        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        public City City { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public int ApplicationUserId { get; set; }
    }
}
