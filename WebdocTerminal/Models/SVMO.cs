using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("SVMO")]
    public class SVMO : CommonProperties
    {
        public long SVMOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequenceNo { get; set; }

        public string VIRNo { get; set; }

        public string ContainerNo { get; set; }

        public string VehicleNo { get; set; }

        public string BondedCarrierId { get; set; }

        public string BondedCarrierName { get; set; }

        public string PCCSSSealNumber { get; set; }

        public string SealingOfficerName { get; set; }

        public DateTime? Performed { get; set; }

        public string OpType { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
