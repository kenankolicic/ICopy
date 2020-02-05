using iCopy.Database;
using System;

namespace iCopy.Model.Response
{
    public class PrintRequest
    {
        public string ID { get; set; }
        public Status Status { get; set; }
        public string FilePath { get; set; }
        public PrintPagesOptions Options { get; set; }
        public SidePrintOption Side { get; set; }
        public Orientation Orientation { get; set; }
        public Letter Letter { get; set; }
        public PagePerSheet Pages { get; set; }
        public CollatedPrintOptions Collate { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }
        public int CopierId { get; set; }

        public Copier Copier { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
