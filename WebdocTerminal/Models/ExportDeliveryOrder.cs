using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class ExportDeliveryOrder : CommonProperties
    {
        public ExportDeliveryOrder()
        {
            GatePasses = new List<GatePassExport>();
        }
        public long ExportDeliveryOrderId { get; set; }
        public long DONumber { get; set; }
        public string Remarks { get; set; }
        public DateTime? DODate { get; set; }
        public string GDNumber { get; set; }
        public bool IsDeivered { get; set; }
        public long? EnteringCargoId { get; set; }
        public EnteringCargo EnteringCargo { get; set; }
        public List<GatePassExport> GatePasses { get; set; }
    }
}
