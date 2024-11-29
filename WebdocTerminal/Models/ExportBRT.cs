using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExportBRT")]
    public class ExportBRT : CommonProperties
    {
        public long ExportBRTId { get; set; }

        public string Location { get; set; }

        public long? ExportCargoId { get; set; }

        public ExportCargo ExportCargo { get; set; }
    }
}
