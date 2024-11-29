using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Transporter")]
    public class Transporter : CommonProperties
    {
        public Transporter()
        {
            GateoutExports = new List<GateoutExport>();
        }

        public long TransporterId { get; set; }
        public string TransporterName { get; set; }
        public string VehicleNumber { get; set; }
        public List<GateoutExport> GateoutExports { get; set; }
    }
}
