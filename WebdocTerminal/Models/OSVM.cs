using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OSVM : CommonProperties
    {


        public long OSVMId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNo { get; set; }

        public string ContainerNo { get; set; }

        public string VehicleNo { get; set; }

        public string GDNumber { get; set; }

        public string BondedCarrierNTN { get; set; }

        public string BondedCarrierName { get; set; }

        public string PCCSSSealNumber { get; set; }

        public string SealingOfficerName { get; set; }

        public DateTime? Performed { get; set; }

        public string OPType { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

    }
}
