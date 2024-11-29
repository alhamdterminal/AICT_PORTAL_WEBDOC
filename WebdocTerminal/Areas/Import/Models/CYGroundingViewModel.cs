using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CYGroundingViewModel
    {
        public long ContainerId { get; set; }

        public string VIRNumber { get; set; }

        public string ContainerNo { get; set; }
         
        public double? Weight { get; set; }

        public string Location { get; set; }

        public string GoodName { get; set; }
        public string Count { get; set; }

        public string ManifestedSealNumber { get; set; }
        public string Description { get; set; }
        public string IndexNo { get; set; }

        public string HandlingCode { get; set; }

        public string ActivityType { get; set; }

        public string Status { get; set; }

        public bool? IsChecked { get; set; }
    }
}
