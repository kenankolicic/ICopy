using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCopy.Database
{
    public class PrintRequest : BaseEntity<int>
    {
        public Status Status { get; set; }
        public string FilePath { get; set; }
        public PrintPagesOptions Options { get; set; }
        public SidePrintOption Side { get; set; }
        public Orientation Orientation { get; set; }
        public Letter Letter { get; set; }
        public PagePerSheet Pages { get; set; }
        public CollatedPrintOptions Collate { get; set; }

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(Copier))]
        public int CopierId { get; set; }
        public Copier Copier { get; set; }

        public DateTime RequestDate { get; set; }
    }

    public enum Status
    {
        OnHold = 1,
        InProgress =2,
        Completed=3,
        Rejected = 4
    }

    public enum PrintPagesOptions
    {
        All = 1,
        Even = 2 ,
        Odd = 3,
        Custom = 4
    }

    public enum SidePrintOption
    {
        PrintOneSided = 1 ,
        PrintBothSides = 2 
    }

    public enum Orientation
    {
        Portrait = 1, 
        Landscape = 2
    }

    public enum Letter
    {
        A1 = 1,
        A2 = 2,
        A3 = 3,
        A4 = 4,
        A5 = 5,
        A6 = 6
    }

    public enum PagePerSheet
    {
        OnePage = 1,
        TwoPages = 2,
        FourPages = 3, 
        SixPages = 4,
        EightPages = 5 , 
        SixteenPages = 6
    }

    public enum CollatedPrintOptions
    {
        Collated = 1 ,
        Uncollated = 2
    }
}
