using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class CYContainerViewModel
    {
        public int ContainerSize { get; set; }
        public string Virno { get; set; }
        public string ContainerNo { get; set; }
        public double? ContainerAmount { get; set; }
        public string Stauts { get; set; }
        public long CountOfPart { get; set; }
    }
}
