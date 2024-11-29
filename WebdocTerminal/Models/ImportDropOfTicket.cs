using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ImportDropOfTicket")]
    public class ImportDropOfTicket : CommonProperties
    {

        public long ImportDropOfTicketId { get; set; }
        public string VirNumber { get; set; }
        public string ContainerNo { get; set; }
        public string VesselName { get; set; }
        public string ShippingLine { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string TruckNumber { get; set; }
        public string ManifestedSealNumber { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }

    }
}
