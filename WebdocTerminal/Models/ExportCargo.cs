using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExportCargo")]
    public class ExportCargo : CommonProperties
    {
        public ExportCargo()
        {
            ExportBRTs = new List<ExportBRT>();
        }
        public long ExportCargoId { get; set; }

        public string VehicleNumber { get; set; }

        public string ShipperSeal { get; set; }

        public long? LoadingProgramId { get; set; }

        public LoadingProgram LoadingProgram { get; set; }

        public List<ExportBRT> ExportBRTs { get; set; }
    }
}
