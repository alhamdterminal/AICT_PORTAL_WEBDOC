using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EDOHold")]
    public class EDOHold : CommonProperties
    {
        public long EDOHoldId { get; set; }
        public bool IsEDOHold { get; set; }
        public string Remarks { get; set; }
        public string VirNo { get; set; }
        public long IndexNo { get; set; }

        //public long ContainerIndexId { get; set; }
        //public ContainerIndex ContainerIndex { get; set; }
    }
}
