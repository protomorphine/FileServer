using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Core.Models
{
    public class Config
    {
        public string FileDir { get; set; } = String.Empty;

        public string ConnectionString { get; set; } = String.Empty;
    }
}
