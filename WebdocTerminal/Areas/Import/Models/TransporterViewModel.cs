using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class TransporterViewModel
    {
        public long ContainerId { get; set; } 
        public string ContainerNo { get; set; } 
        public string VIRNo { get; set; } 
        public string BlNumber { get; set; } 
        public string ContainerSize { get; set; } 
        public string Status { get; set; } 
        public long? TransporterId { get; set; } 
        public bool IsGateIn { get; set; } 
                      
                            
                             
    }
}
