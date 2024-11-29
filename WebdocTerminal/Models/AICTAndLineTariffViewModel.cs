using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class AICTAndLineTariffViewModel
    {
         
        public long ContainerIndexId { get; set; }
         
        public string ContainerNo { get; set; }
        public string Virnumber { get; set; }
        public int? IndexNumber { get; set; }
        public bool DestuffMark { get; set; }
        public bool GateInMark { get; set; }

        public string ContainerSize { get; set; }
        public string CargoType { get; set; }
        public double BillToLineAmount { get; set; }
        public double WaiVerAmount { get; set; }
         
        public DateTime? InvoiceDate { get; set; }

        public DateTime? DestuffDate { get; set; }

        public DateTime? GateInDate { get; set; }

        public long ClearingAgentId { get; set; }
           
        public double? Weight { get; set; }
        public double OtherCharges { get; set; }
        public double? CBM { get; set; }
        public double? MeasurementCBM { get; set; }
        public double? ExaminationRequestCBM { get; set; }
        public double? FFCBM { get; set; }
        public double? HigherCBM { get; set; }
         
        public double StorageTotal { get; set; }
         
        public string IGM { get; set; }
        public long? TariffInformationId { get; set; }
         
    }
}
