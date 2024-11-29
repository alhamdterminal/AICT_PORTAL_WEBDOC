using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class GateOutViewModel
    {
        public string Key { get; set; }
        public long ExportContainerId { get; set; }

        public string ContainerNumber { get; set; }

        public string GDNumber { get; set; }

        public string Status { get; set; }

        public string LineSeal { get; set; }

        public string BondedCarrierName { get; set; }

        public DateTime? GateoutTime { get; set; }

        public string VehicleNumber { get; set; }

        public string PCCSSSeal { get; set; }

        public string BondedCarrierNTN { get; set; }

        public long PortAndTerminalId { get; set; }

        public string CountryofExit { get; set; }

        public double ContainerGrossWeight { get; set; }

        public string Location { get; set; }

        public long? TransporterId { get; set; }

        public bool IsChecked { get; set; }
    }
}
