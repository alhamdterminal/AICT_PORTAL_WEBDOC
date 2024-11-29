using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class TPReceiveVehicle : CommonProperties
    { 
        public long TPReceiveVehicleId { get; set; }
        public string VehicleNo { get; set; }
        public bool ContainerPresent { get; set; }
        public string CotnainerNo { get; set; }
        public string SealNo { get; set; }
        public string ContainerGrossWeight { get; set; }
        public string DriverName { get; set; }
        public string DriverCNIC { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long ClearingAgentExportId { get; set; }
        public ClearingAgentExport ClearingAgentExport { get; set; }
        public string AgentRepresentative { get; set; }
        public string AgentCNIC { get; set; }
        public string Remarks { get; set; }
    }
}
