using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ExaminationIHCViewModel
    {
        public string ContainerNumber { get; set; }
        public string VirNumber { get; set; }
        public int Size { get; set; }
        public string CaroType { get; set; }
        public long? GroundingFreedays { get; set; }
        public long EMCount { get; set; }
        public long ESCount { get; set; }
        
    }
}
