using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Core.Models
{
    public class FileResponceModel
    {
        public string? Name { get; set; }

        public string? ContentType { get; set; }

        public byte[]? FileContent { get; set; }
    }
}
