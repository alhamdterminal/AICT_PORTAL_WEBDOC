using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExportDropOfTicket")]
    public class ExportDropOfTicket : CommonProperties
    {
        public long ExportDropOfTicketId { get; set; }
        public string GDNumber { get; set; }
        public string ShippingLine { get; set; }
        public string TruckNumber { get; set; }
        public string ManifestedSealNumber { get; set; }
        public DateTime? InTime { get; set; }
    }
}
