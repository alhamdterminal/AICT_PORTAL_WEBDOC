using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GroundingFreeDay")]
    public class GroundingFreeDay : CommonProperties
    {
        public long GroundingFreeDayId { get; set; }
        public long? GroundingFreeDays { get; set; }
        public long? StorageFreeFreeDays { get; set; }
        public string CargoType { get; set; }
        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        public long? ConsigneId { get; set; }
        public Consigne Consigne { get; set; }

        public long? ClearingAgentId { get; set; }
        public ClearingAgent ClearingAgent { get; set; }
         
        public long? GoodsHeadId { get; set; }
        public GoodsHead GoodsHead { get; set; }


    }
}
