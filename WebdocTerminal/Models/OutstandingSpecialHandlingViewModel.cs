using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OutstandingSpecialHandlingViewModel
    {
        public string VirNo { get; set; }
        public long IndexNo { get; set; }
        public string ContainerNo { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public long AgingDays { get; set; }
        public string ClearingAgentName { get; set; }
        public string ConsigneName { get; set; }
        public long SpecialTotalBalance { get; set; }
    }
}
