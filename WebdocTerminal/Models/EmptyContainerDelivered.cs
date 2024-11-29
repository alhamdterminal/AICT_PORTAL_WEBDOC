using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyContainerDelivered")]
    public class EmptyContainerDelivered : CommonProperties
    {
        public long EmptyContainerDeliveredId { get; set; }
        public string   ExportVessel { get; set; }
        public string   PortOfDischarge { get; set; }
        public string   FinalDestination { get; set; }
        public long? ShipperId { get; set; }
        public Shipper Shipper { get; set; }

        public string ExportBookingNo { get; set; }

        public long? ConsigneId { get; set; }
        public Consigne Consigne { get; set; }

        public string TruckNo { get; set; }
        public string Shift { get; set; }
        public string Remarks { get; set; }
        public DateTime? DelDate { get; set; }

        public long? OffHireLocationId { get; set; }
        public OffHireLocation OffHireLocation { get; set; }

        public long? ShippingLineId { get; set; }
        public ShippingLine ShippingLine { get; set; }

        public long? RandDClerkId { get; set; }
        public RandDClerk RandDClerk { get; set; }
        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }

        public long EmptyContainerReceivedId { get; set; }
        public EmptyContainerReceived EmptyContainerReceived { get; set; }
    }
}
