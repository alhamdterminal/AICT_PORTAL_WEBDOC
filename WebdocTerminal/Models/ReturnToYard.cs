using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ReturnToYard")]
    public class ReturnToYard : CommonProperties
    {
        public long ReturnToYardId { get; set; }
        public string ReturnToYardName { get; set; }
        public string DocumentReceived { get; set; }
        public DateTime? ReturnToYardDate { get; set; }
        public long EmptyGateOutContainerId { get; set; }
        public EmptyGateOutContainer EmptyGateOutContainer { get; set; }
    }
}
