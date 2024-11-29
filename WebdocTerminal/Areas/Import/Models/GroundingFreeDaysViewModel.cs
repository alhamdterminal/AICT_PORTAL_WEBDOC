using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class GroundingFreeDaysViewModel
    {
        public long GroundingFreeDayId { get; set; }
        public long? GroundingFreeDays { get; set; }
        public long? StorageFreeFreeDays { get; set; }
        public string ShippingAgentName { get; set; }
        public string ConsigneeName { get; set; }
        public string ClearingAgentName { get; set; }
        public string GoodHeadName { get; set; }

        public string CargoType { get; set; }
    }
}

