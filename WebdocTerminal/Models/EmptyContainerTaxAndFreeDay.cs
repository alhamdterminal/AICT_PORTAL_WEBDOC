using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyContainerTaxAndFreeDay")]
    public class EmptyContainerTaxAndFreeDay : CommonProperties
    {

        public long EmptyContainerTaxAndFreeDayId { get; set; }
        public long FreeDays { get; set; }
        public long SalesTax { get; set; }
        public long ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }

    }
}
