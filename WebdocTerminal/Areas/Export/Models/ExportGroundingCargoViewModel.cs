using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class ExportGroundingCargoViewModel
    {
        public long EnteringCargoId { get; set; }

        public string GDNumber { get; set; }

        public int? Weight { get; set; }

        public string Location { get; set; }

        public string ActivityType { get; set; }

        public string HandlingCode { get; set; }

        public string Category { get; set; }

        public bool? IsChecked { get; set; }
    }
}
