using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class ExportFilesViewModel
    {
        public string GDNumber { get; set; }
        public string FileName { get; set; }
         
        public DateTime? Performerd { get; set; }
    }
}
