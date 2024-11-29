using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GDI")]
    public class GDI : CommonProperties
    {
        public long GDIId { get; set; }

        public int? TotalRecord { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string ConsigneeName { get; set; }

        public string ConsigneeNTN { get; set; }

        public string ClearingAgent { get; set; }

        public string ExporterName { get; set; }

        public string DestPort { get; set; }

        public string DestCountry { get; set; }

        public string PortOfShipment { get; set; }

        public string ShipmentCountry { get; set; }

        public double? DutyValue { get; set; }

        public DateTime? Performed { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
