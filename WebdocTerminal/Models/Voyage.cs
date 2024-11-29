using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Voyage")]
    public class Voyage : CommonProperties
    {

        public long VoyageId { get; set; }

        public string VIRNo { get; set; }

        public string VoyageNo { get; set; }

        public DateTime? BerthOn { get; set; }

        public string BerthAt { get; set; }

        public long? VesselId { get; set; }

        public Vessel Vessel { get; set; }

        public List<ContainerIndex> ContainerIndices { get; set; }
    }
}
