using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class EmailAlertsViewModel
    {
        public string VIRNumber { get; set; }
        public string ContainerNumber { get; set; }
        public DateTime CreateDT { get; set; }
        public long Diff { get; set; }
        public string Remarks { get; set; }
    }
}
