using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class GDInfoViewModel
    {
        public double NoOfPackages { get; set; }

        public DateTime? Performed { get; set; }

        public DateTime? SubmitedDate { get; set; }

        public DateTime? GateOutDate { get; set; }
    }
}
