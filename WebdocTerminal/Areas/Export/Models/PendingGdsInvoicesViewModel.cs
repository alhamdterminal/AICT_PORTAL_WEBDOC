using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class PendingGdsInvoicesViewModel
    {
        public long SerialNumber { get; set; }
        public string GDNumber { get; set; }
        public DateTime? GateInDate { get; set; }
        public String Vessel { get; set; }
        public String Voyage { get; set; }
        public DateTime ETD { get; set; }
        public string ShippingAgent { get; set; }
        public string ClearingAgent { get; set; }
        public string Shipper { get; set; }
        public double CBM { get; set; }
        public double Weight { get; set; }

    }
}
