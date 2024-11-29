using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ListOfUpCommingContainersViewModel
    {
        public long SerialNo { get; set; }
        public string VIRNumber { get; set; }
        public string ContainerNo { get; set; }
        public string BLNo { get; set; }
        public double BLGrossWeight { get; set; }
        public int IndexNo { get; set; }
        public string VesselName { get; set; }
        public DateTime? VesselArrival { get; set; }
        public string BerthAt { get; set; }
        public string Commodity { get; set; }
        public string ManifestedSealNumber { get; set; }
        public string ISOCodeName { get; set; }
      
    
    }
}
