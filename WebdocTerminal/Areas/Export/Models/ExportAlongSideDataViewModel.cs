using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class ExportAlongSideDataViewModel
    {
        public long SerialNumber { get; set; }
        public string GDNumber { get; set; }
        public DateTime? CargoReceivedDate { get; set; }
        public string ShippingAgent { get; set; }
        public string ClearingAgent { get; set; }
        public string Shipper { get; set; }
        public string PortOfDischarge { get; set; }
        public string VesselVoyage { get; set; }
        public long NumberofPackge { get; set; }
        public string PackgeType { get; set; }
        public double CBM { get; set; }
        public double Weight { get; set; }
        public string AllowLoading { get; set; }
        public string NOC { get; set; }
        public DateTime? PaperReady { get; set; }
        public DateTime? Anf { get; set; }
        public string Remarks { get; set; }
    }
}
