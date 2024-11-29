using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class StuffingConfirmationViewModel
    {
        public long CCMOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequenceNo { get; set; }

        public string Category { get; set; }

        public string VIRNo { get; set; }
        public string ContainerNumberIGMO { get; set; }

        public string BLNo { get; set; }

        public int? IndexNo { get; set; }

        public string VehicleNo { get; set; }

        public string BondedCarrierId { get; set; }

        public string BondedCarrierName { get; set; }

        public string ContainerNo { get; set; }

        public double? Weight { get; set; }

        public double? NoOfPackages { get; set; }

        public string OpType { get; set; }

        public DateTime? Performed { get; set; }

        public string TPNo { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public Boolean Status{ get; set; }
    }
}
