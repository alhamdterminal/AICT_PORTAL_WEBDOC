using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("WorkOrderCSD")]
    public class WorkOrderCSD : CommonProperties
    {
        public long WorkOrderCSDId { get; set; }
        public long WorkOrderNumber { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public long CFS20 { get; set; }
        public long CFS40 { get; set; }
        public long CFS45 { get; set; }
        public long CY20 { get; set; }
        public long CY40 { get; set; }
        public long CY45 { get; set; }
        public long DocumentAmount { get; set; }
        public long SealAmount { get; set; }
        public long EntryAmount { get; set; }
        public string Remarks { get; set; }
        public string IGM { get; set; }

        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
    }
}
