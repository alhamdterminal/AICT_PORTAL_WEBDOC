using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ContainerIndexDetail
    {
        public long SerialNo { get; set; }
        public long ContainerIndexId { get; set; }
        public string VirNo { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string IsoCodeType { get; set; }
        public string Status { get; set; }
        public string CargoType { get; set; }
        public string ManifestedSealNumber { get; set; }
        public int? IndexNo { get; set; }
        public double CBM { get; set; }
        public double ManifestedWeight { get; set; }
        public double MeasurementCBM { get; set; }
        public string BLNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long? ShippingAgentId { get; set; }
        public long? ShippingLineId { get; set; }
        public int NoOfPackages { get; set; }
        public string PackageType { get; set; }
         public long? ShipperId { get; set; }
        public long? GoodsHeadId { get; set; }
        public long? ConsigneId { get; set; }
        public string MarksAndNumber { get; set; }
        public string Description { get; set; }
        public long? PortAndTerminalId { get; set; }
        public long? PortOfLoadingId { get; set; }
        public double? BLGrossWeight { get; set; }
        public double? WeightIntan { get; set; }
        public bool? IsPartialContainer { get; set; }

        public bool IsDGCargo { get; set; }

        public string VesselName { get; set; }
        public string VoyageNo { get; set; }
        public DateTime? ETA { get; set; }

    }
}
