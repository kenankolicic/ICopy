using System.ComponentModel.DataAnnotations.Schema;

namespace iCopy.Database
{
    public class ApplicationUserProfilePhoto : BaseEntity<int>
    {
        public int ApplicationUserId { get; set; }
        [ForeignKey(nameof(ProfilePhoto))]
        public int ProfilePhotoId { get; set; }
        public ProfilePhoto ProfilePhoto { get; set; }
    }
}
