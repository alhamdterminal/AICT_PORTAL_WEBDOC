using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class ContainerSizeAmount  : CommonProperties
    {
        public long ContainerSizeAmountId { get; set; }
        public double  AmountSize20 { get; set; }
        public double AmountSize40 { get; set; }
        public double AmountSize45 { get; set; }
    }
}
