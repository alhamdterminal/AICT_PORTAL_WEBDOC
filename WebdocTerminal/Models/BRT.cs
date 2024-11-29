using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("BRT")] 
    public class BRT : CommonProperties
    {

        public BRT()
        {
            BrtItems = new List<BrtItem>();
        }
        public long BRTId { get; set; }

        public string Location { get; set; }

        public long? BRTLocationId { get; set; }

        public BRTLocation BRTLocation { get; set; }
        
        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CyContainerId { get; set; }

        public CYContainer CYContainer { get; set; }

        public List<BrtItem> BrtItems { get; set; }

    }
}
