using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("FailedFile")]
    public class FailedFile : CommonProperties
    {
        public long FailedFileId { get; set; }
        public string FileName { get; set; }
        public string Message { get; set; }

    }
}
