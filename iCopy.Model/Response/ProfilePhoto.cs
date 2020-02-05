using System;

namespace iCopy.Model.Response
{
    public class ProfilePhoto
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public string FileSystemPath { get; set; }
        public Int64 SizeInBytes { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Format { get; set; }
        public string Base64Encoded { get; set; }
    }
}
