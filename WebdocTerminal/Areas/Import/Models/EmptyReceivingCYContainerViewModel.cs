using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Areas.Import.Models
{
    public class EmptyReceivingCYContainerViewModel
    {
        public string VIRNo { get; set; }
        public string ContainerNo { get; set; }
        public int Size { get; set; }
        public string ShippingLineName { get; set; }
        public string BLNo { get; set; }
        public string CSContainerNumber { get; set; }
        public string CSSize { get; set; }
        public long? EmptyReceivedShippingLineId { get; set; }
        public string CSVehicleNumer { get; set; }
        public string CSCondition { get; set; }
        public double CSTireWeight { get; set; }
        public long? CSEmptyContainerReceiveId { get; set; }
        public CSEmptyContainerReceive CSEmptyContainerReceive { get; set; }
    }
}
