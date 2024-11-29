using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("MasterShippingAgent")]
    public class MasterShippingAgent : CommonProperties
    {
        public long MasterShippingAgentId { get; set; }
        public long MasterShippingAgentCode { get; set; }
        public string MasterShippingAgentName { get; set; }
        public string CreatedDate { get; set; }
    }
}
