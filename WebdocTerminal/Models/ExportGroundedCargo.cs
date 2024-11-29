using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExportGroundedCargo")]
    public class ExportGroundedCargo : CommonProperties
    {
        public long ExportGroundedCargoId { get; set; }

        public int? Weight { get; set; }

        public string Location { get; set; }

        public string ActivityType { get; set; }

        public string GDNumber { get; set; }

        public long? EnteringCargoId { get; set; }

        public EnteringCargo EnteringCargo { get; set; }

    }
}
