using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExportVehicle")]
    public class ExportVehicle : CommonProperties
    {
        public long ExportVehicleId { get; set; }

        public string VehicleNumber { get; set; }
        public string ShipperSeal { get; set; }

        public long? EnteringCargoId { get; set; }

        public EnteringCargo EnteringCargo { get; set; }
    }
}
