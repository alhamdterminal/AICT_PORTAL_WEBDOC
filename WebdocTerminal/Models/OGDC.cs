using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OGDC : CommonProperties
    {
        public long OGDCId { get; set; }

        public int? RecordSequence { get; set; }

        public string ContainerNumber { get; set; }

        public string VehicleNumber { get; set; }

        public string GDNumber { get; set; }

        public double NumberofPackages { get; set; }

        public string ShippingLineCode { get; set; }

        public string ShippingLineNTN { get; set; }

        public string ShippingLineName { get; set; }

        public string ContainerConsolidationStatus { get; set; }

        public string OperationType { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public bool IsProcessed { get; set; } = false;

    }
}
