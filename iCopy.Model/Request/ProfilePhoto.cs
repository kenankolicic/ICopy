using System;

namespace iCopy.Model.Request
{
    public class ProfilePhoto
    {
        public bool Active { get; set; }
        public int ApplicationUserId { get; set; }
        public string Path { get; set; }
        public string FileSystemPath { get; set; }
        public Int64 SizeInBytes { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Format { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public float XResolution { get; set; }
        public float YResolution { get; set; }
        public string ResolutionUnit { get; set; }
    }
}
