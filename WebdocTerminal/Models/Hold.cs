using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Hold")]
    public class Hold : CommonProperties
    {
        public long HoldId { get; set; }

        //public long? ContainerIndexId { get; set; }

        //public ContainerIndex ContainerIndex { get; set; }

        //public long? CYContainerId { get; set; }

        //public CYContainer CYContainer { get; set; }

        public string VirNo { get; set; }
        public long IndexNo { get; set; }
        public string Type { get; set; }

        public string SpecialInstructions { get; set; }

        public string AuctionLotNo { get; set; }

        public DateTime? HoldDate { get; set; }

        public bool IsHold { get; set; }
        public string Role { get; set; }
        public string RO { get; set; }

        public string HoldType { get; set; }
        public string Nature { get; set; }
        public string RemoveInstruction { get; set; }


    }
}
