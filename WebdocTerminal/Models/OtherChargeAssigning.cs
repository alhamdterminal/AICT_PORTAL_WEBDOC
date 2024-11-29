using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("OtherChargeAssigning")]
    public class OtherChargeAssigning : CommonProperties
    {
        public long OtherChargeAssigningId { get; set; }
        public long ContainerIndexId { get; set; }
        public long CyContainerId { get; set; }
        public string ChargeName { get; set; }
        public string AmountType { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public double ChargeQuantity { get; set; }
        public double ChargeAmount { get; set; }
        public string Remarks { get; set; }
        public bool InvoiceCreated { get; set; }
        public bool AmountAssign { get; set; }
        public string InvoiceNumber { get; set; }
        public long? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

    }
}
