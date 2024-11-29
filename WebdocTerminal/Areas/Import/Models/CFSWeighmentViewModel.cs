using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CFSWeighmentViewModel
    {
        public long ? ContainerIndexId { get; set; }

        public long ? CYContainerId { get; set; }

        public string VIRNo { get; set; }

        public string BLNo { get; set; }

        public string indexNo { get; set; }

        public string ContainerNo { get; set; }

        public double? BLGrossWeight { get; set; }

        public double? FoundWeight { get; set; }

        public string HandlingCode { get; set; }

        public bool IsChecked { get; set; }
    }
}
