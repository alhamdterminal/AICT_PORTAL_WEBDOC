using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DOCodeItem")]
    public class DOCodeItem : CommonProperties
    {
        public long DOCodeItemId { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public long? DeliveryOrderId { get; set; }
        public DeliveryOrder DeliveryOrder { get; set; }
    }
}
