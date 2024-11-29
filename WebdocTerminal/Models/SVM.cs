using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("SVM")]
    public class SVM : CommonProperties
    {
        public long SVMId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string VehicleNumber { get; set; }

        public string BondedCarrierId { get; set; }

        public string BondedCarrierNTN { get; set; }

        public string BondedCarrierName { get; set; }

        public string PCCSSSealNumber { get; set; }

        public string SealingOfficerName { get; set; }

        public DateTime? Performed { get; set; }

        public string OpType { get; set; }

        public string GDNumber { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;

    }
}
