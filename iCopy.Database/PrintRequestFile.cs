﻿using System;
using System.Collections.Generic;
using System.Text;

namespace iCopy.Database
{
    public class PrintRequestFile : BaseEntity<int>
    {
        public string Path { get; set; }
        public string FileSystemPath { get; set; }
        public Int64 SizeInBytes { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
    }
}
