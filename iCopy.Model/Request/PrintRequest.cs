using iCopy.Database;
using System.ComponentModel.DataAnnotations;

namespace iCopy.Model.Request
{
    public class PrintRequest
    {
        [Required(ErrorMessage = "ErrNoStatus")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "ErrNoOptions")]
        public PrintPagesOptions Options { get; set; }

        [Required(ErrorMessage = "ErrNoSide")]
        public SidePrintOption Side { get; set; }

        [Required(ErrorMessage = "ErrNoOrientation")]
        public Orientation Orientation { get; set; }

        [Required(ErrorMessage = "ErrNoLetter")]
        public Letter Letter { get; set; }

        [Required(ErrorMessage = "ErrNoPages")]
        public PagePerSheet Pages { get; set; }

        [Required(ErrorMessage = "ErrNoCollate")]
        public CollatedPrintOptions Collate { get; set; }

        [Required(ErrorMessage = "ErrNoCopier")]
        public int CopierId { get; set; }

        public PrintRequestFile PrintRequestFile { get; set; }
        public string FilePath { get; set; }
        public int ClientId { get; set; }
    }
}
