using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CustomerNOC")]
    public class CustomerNOC : CommonProperties
    {

        public long CustomerNOCId { get; set; }

        public DateTime? EmailReceived { get; set; }

        public string NameOfPerson { get; set; }

        public string ContactNumber { get; set; }

        public string GDNumber { get; set; }

        public long? ShippingAgentExportId { get; set; }

        public long? ShippingAgentExportBId { get; set; }

        public ShippingAgentExport ShippingAgentExport { get; set; }


    }
}
