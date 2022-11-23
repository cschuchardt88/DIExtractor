using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIExtractor
{
    public class DIFileInfo
    {
        public string Filename { get; set; }
        public uint Offset { get; set; }
        public uint Length { get; set; }
        public uint Index { get; set; }
    }
}
