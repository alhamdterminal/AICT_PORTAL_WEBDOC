using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GDIO")]
    public class GDIO : CommonProperties
    {
        public long GDIOId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public int? IndexNumber { get; set; }

        public string CRN { get; set; }

        public string ConsigneeName { get; set; }

        public string ConsigneeNTN { get; set; }

        public string ClearingAgent { get; set; }

        public string ExporterName { get; set; }

        public string DestinationPort { get; set; }

        public string DestinationCountry { get; set; }

        public string PortOfShipment { get; set; }

        public string ShipmentCountry { get; set; }

        public double? DutyValue { get; set; }

        public double? NoOfPackages { get; set; }

        public string PackageType { get; set; }

        public string CargoType { get; set; }

        public string OpType { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
