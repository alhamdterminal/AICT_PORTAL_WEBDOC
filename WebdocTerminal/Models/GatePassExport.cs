using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GatePassExport")]
    public class GatePassExport : CommonProperties
    {
        public GatePassExport()
        {
            DOItemExports = new List<DOItemExport>();
        }
        public long GatePassExportId { get; set; }

        public DateTime? DODate { get; set; }

        public string GatePassNumber { get; set; }

        public string Remarks { get; set; }
        public string GDNumber { get; set; }

        public int? TotalPackages { get; set; }

        public int? BalancePackages { get; set; }

        public DateTime? GatePassDate { get; set; }

        public int? Delivered { get; set; }

        public int? TotalDelivered { get; set; }

        public long ExportDeliveryOrderId { get; set; }

        public ExportDeliveryOrder DeliveryOrder { get; set; }

        public List<DOItemExport> DOItemExports { get; set; }

    }
}
