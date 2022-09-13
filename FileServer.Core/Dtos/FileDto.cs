using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServer.Core.Dtos
{
    public class FileDto
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Id файла
        /// </summary>
        public Guid FileId { get; set; }
    }
}
