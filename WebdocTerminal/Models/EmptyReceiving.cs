using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyReceiving")]
    public class EmptyReceiving : CommonProperties
    {
        public long EmptyReceivingId { get; set; }

        public string CRONumber { get; set; }

        public string ContainerNumber { get; set; }

        public string ContainerSize { get; set; }

        public double? ContainerTareWeight { get; set; }

        public string ContainerCondition { get; set; }

        public string VehicleNumber { get; set; }
        public DateTime? RecevingDate { get; set; }

        public long? ShippingLineId { get; set; }

        public ShippingLine ShippingLine { get; set; }

        public long? ShippingAgentExportId { get; set; }

        public ShippingAgentExport ShippingAgentExport { get; set; }
    }
}
