using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ImportFielsViewModel
    {
        public string VirNumber { get; set; }
        public string FileName { get; set; } 
        public string ContainerNumber { get; set; } 
        public string BlNumber { get; set; }
        public string GDNumber { get; set; }
        public int? IndexNo { get; set; }
        public string Type { get; set; }
        public DateTime? Performerd { get; set; }
    }
}
