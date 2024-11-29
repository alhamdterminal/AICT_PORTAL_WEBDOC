using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CargoInformationViewModel
    {
        public long CotnainerId { get; set; }

        public string AgentName { get; set; }
        public string LineName { get; set; }
        public long? ShippingAgentId { get; set; }

        public string IGM { get; set; }

        public int? IndexNo { get; set; }
        public string BLNo { get; set; }

        public double? CBM { get; set; }

        public string ContainerNo { get; set; }
        public string VirNo { get; set; }

        public int Size { get; set; }
        public string Status { get; set; }

        public double? ManifestedWeight { get; set; }

        public double? FoundWeight { get; set; }
        public string FoundSealNumber { get; set; }

        public int? ManifestedQTY { get; set; }

        public string ManifestedUOP { get; set; }

        public int? FoundQTY { get; set; }

        public string FoundUOP { get; set; }

        public string Description { get; set; }
        public string CRNumber { get; set; }

        public string MarksAndNumber { get; set; }

        public string Location { get; set; }

        public string DestuffedRemark { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public long DoNo { get; set; }
        public string DeliveryOrderNumber { get; set; }
        public DateTime? DoDate { get; set; }

        public string GatePassNo { get; set; }
        public DateTime? GatePassDate { get; set; }
 
        public DateTime? GateInDate { get; set; }
        public DateTime? ExamDate { get; set; }

        public DateTime? DestuffDate { get; set; }

        public DateTime? VesselArrivalDate { get; set; }
        public string VesselName { get; set; }
        public string VoyageNumber { get; set; }
        public DateTime? IGMDate { get; set; }

        public string TruckIn { get; set; }
        public string FCLLCL { get; set; }
        public string TruckOut { get; set; }
        public string GoodType { get; set; }

        public DateTime? DeliveryDate { get; set; }
        public long NumberOFPackages { get; set; }
        public string ConsigneeName { get; set; }

        public string ClearingAgentName { get; set; }

        public DateTime? ExaminDate { get; set; }
        public string ExaminationStatus { get; set; }
        public string ReleaseStatus { get; set; }
        public string CrloStatus { get; set; }
        public string DGCargo { get; set; }
        public double ActualQty { get; set; }
        public double Balance { get; set; }
        public string ShippingLineName { get; set; }
        public double DisPackages { get; set; }
        public double DisQty { get; set; }
        public string Brtlocation { get; set; }
        public string LineDoNumber { get; set; }

    }
}
