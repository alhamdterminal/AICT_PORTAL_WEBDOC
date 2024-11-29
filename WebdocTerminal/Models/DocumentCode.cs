using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DocumentCode")]
    public class DocumentCode : CommonProperties
    {
        public long DocumentCodeId { get; set; }
        public string  Code { get; set; }
        public string  DocumentName { get; set; }

    }
}
