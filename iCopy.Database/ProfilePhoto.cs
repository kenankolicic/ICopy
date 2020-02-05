using System;

namespace iCopy.Database
{
    public class ProfilePhoto : BaseEntity<int>
    {
        public string Path { get; set; }
        public string FileSystemPath { get; set; }
        public Int64 SizeInBytes { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Format { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal XResolution { get; set; }
        public decimal YResolution { get; set; }
        public string ResolutionUnit { get; set; }
    }
}
