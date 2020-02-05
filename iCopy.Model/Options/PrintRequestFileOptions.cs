using System;
using System.Collections.Generic;
using System.Text;

namespace iCopy.Model.Options
{
    public class PrintRequestFileOptions
    {
        public string Path { get; set; }
        public string[] SupportedContentTypes { get; set; }
        public string[] SupportedExtensions { get; set; }
        public int MaxSize { get; set; }
    }
}
