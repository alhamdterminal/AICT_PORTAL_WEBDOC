using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class EmptyContainerReceivedViewModel
    {
        public long EmptyContainerReceivedId { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public DateTime? ActualArrive { get; set; }
        public string Line { get; set; }
        public string Principal { get; set; }
        public string ConsigneeName { get; set; }
        public string ClearningAgentName { get; set; }
        public string PortOfLoading { get; set; }
        public long? ShippinAgentId { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
    }
}
