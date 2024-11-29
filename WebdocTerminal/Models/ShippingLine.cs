using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ShippingLine")]
    public class ShippingLine : CommonProperties
    {
        public ShippingLine()
        { 
            EmptyReceivings = new List<EmptyReceiving>();
        }

        public long ShippingLineId { get; set; }

        public string ShippingLineName { get; set; }

        public string ShippingLineCode { get; set; }
        public string NTNNumber { get; set; }

        public string ShippingLineAgent { get; set; }
         
        public List<EmptyReceiving> EmptyReceivings { get; set; }

    }
}
