using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class RequestProfile
    {
        public Person RequestPersonProfile { get; set; }
        [Required(ErrorMessage = "ErrNoRequestPersonProfile")]

        public int? RequestPersonProfileId { get; set; }
        public Company RequestCompanyProfile { get; set; }
        [Required(ErrorMessage = "ErrNoRequestCompanyProfile")]

        public int? RequestCompanyProfileId { get; set; }

        public Copier RequestCopierProfile { get; set; }
        [Required(ErrorMessage = "ErrNoRequestCopierProfile")]

        public int? RequestCopierProfileId { get; set; }
    }
}
