using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Vessel")]
    public class Vessel : CommonProperties
    {
        public Vessel()
        {
            Voyages = new List<Voyage>();
        }
        public long VesselId { get; set; }

        public string VesselName { get; set; }

        public string IGMYear { get; set; }

        public string IGM { get; set; }

        public List<Voyage> Voyages { get; set; }
    }
}
