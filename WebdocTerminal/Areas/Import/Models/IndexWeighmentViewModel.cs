using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class IndexWeighmentViewModel
    {
        public DateTime? GateInDate  { get; set; }
        public string ContainerNo { get; set; }
        public double ManifestedWeight { get; set; }
        public int DischargeQTY { get; set; }
        public int NumberOfPackages { get; set; }
        public string BLNo { get; set; }
         
    }
}
