using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class OnlineTrackingViewModel
    {
        public string VirNo { get; set; }
        public string BLNo { get; set; }
        public string IndexNo { get; set; }
        public string GDNo { get; set; }
        public string ContainerNo { get; set; }
        public string Category { get; set; }
        public string LotNo { get; set; }
        public string ContainerSize { get; set; }
        public string Weight { get; set; }
        public string NoOfPackages { get; set; }
        public string PackageType { get; set; }
        public string BLGrossWeight  { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string MarksAndNumber { get; set; }
        public string ShippingLineName { get; set; }
        public string VesselName { get; set; }
        public string VoyageNo { get; set; }
        public DateTime? GateInDate { get; set; }
        public DateTime? GroundingDate { get; set; } 
        public DateTime? GateOutDate { get; set; }
    }
}
