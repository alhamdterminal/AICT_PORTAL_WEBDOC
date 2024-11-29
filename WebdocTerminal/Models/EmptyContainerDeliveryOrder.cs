using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyContainerDeliveryOrder")]
    public class EmptyContainerDeliveryOrder : CommonProperties
    {
        public long EmptyContainerDeliveryOrderId { get; set; }

        public DateTime? Date { get; set; }
        public DateTime? ValidTo { get; set; }         
        public string InvoiceNo { get; set; }
        public long DONumber { get; set; }
        public string Remarks { get; set; }
        public long EmptyContainerReceivedId { get; set; }
        public EmptyContainerReceived EmptyContainerReceived { get; set; }

    }
}
