using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TellySheet")]
    public class TellySheet : CommonProperties
    {
        public TellySheet()
        {
            DestuffedContainers = new List<DestuffedContainer>();

        }
        [Key]
        public long TellySheetId { get; set; }

        public string TellyClerk { get; set; }

        public DateTime? DestuffDate { get; set; }

        public string DayNight { get; set; }
        public string ContainerType { get; set; }

        public long? ShippingAgentId { get; set; }

        public ShippingAgent ShippingAgent { get; set; }

        public List<DestuffedContainer> DestuffedContainers { get; set; }
    }
}
