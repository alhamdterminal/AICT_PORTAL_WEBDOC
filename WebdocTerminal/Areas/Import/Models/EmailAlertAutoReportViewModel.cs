using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class EmailAlertAutoReportViewModel
    {
        public long SerialNo { get; set; }
        public string Line { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string DischargePort { get; set; }
        public string VesselArrivalDate { get; set; }
        public string AICTArrivalDate { get; set; }
        public string EmptyGateOutDate { get; set; }
    }
}
