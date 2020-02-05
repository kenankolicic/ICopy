using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace iCopy.Database
{
    public class ApplicationUserPrintRequestFile : BaseEntity<int>
    {
        public int ApplicationUserId { get; set; }
        [ForeignKey(nameof(ProfilePhoto))]
        public int PrintRequestFileId { get; set; }
        public PrintRequestFile PrintRequestFile { get; set; }
    }
}
