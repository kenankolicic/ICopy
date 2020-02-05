using System;
using System.ComponentModel.DataAnnotations;
using iCopy.Model.Attributes;

namespace iCopy.Model.Request
{
    public class Copier
    {
        [Required(ErrorMessage = "ErrNoName")]
        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        public string Name { get; set; }

        [MaxLength(300, ErrorMessage = "ErrMaxLength")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ErrNoStartWorkingTime")]
        [DataType(DataType.Time)]
        public TimeSpan StartWorkingTime { get; set; }

        [Required(ErrorMessage = "ErrNoEndWorkingTime")]
        [DataType(DataType.Time)]
        [GreaterThanOrEqualTimeSpan(nameof(StartWorkingTime), ErrorMessage = "ErrEndTimeLessThenStartTime")]
        public TimeSpan EndWorkingTime { get; set; }

        [MaxLength(100, ErrorMessage = "ErrMaxLength")]
        [DataType(DataType.Url, ErrorMessage = "ErrUrl")]
        public string Url { get; set; }

        [Required(ErrorMessage = "ErrNoPhoneNumber")]
        [MaxLength(50, ErrorMessage = "ErrMaxLength")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "ErrNoCity")]
        public int? CityId { get; set; }

        [Required(ErrorMessage = "ErrNoCompany")]
        public int CompanyId { get; set; }

        public bool Active { get; set; }
        public Model.Request.ApplicationUserInsert User { get; set; }
        public Model.Request.ProfilePhoto ProfilePhoto { get; set; }
    }
}
