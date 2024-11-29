using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ClearingAgent")]
    public class ClearingAgent : CommonProperties
    {
        public long ClearingAgentId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string NTNNumber { get; set; }

        public string ChallanNumber { get; set; }
        public string LicenceNumber { get; set; }

        public string BillDateTypeLCL { get; set; }
        public string BillDateTypeFCL { get; set; }
        public string BillDateTypeCY { get; set; }

    }
}
