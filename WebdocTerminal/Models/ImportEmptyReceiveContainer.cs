using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ImportEmptyReceiveContainer")]
    public class ImportEmptyReceiveContainer : CommonProperties
    {
        public long ImportEmptyReceiveContainerId { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerSize { get; set; }
        public string Type { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime ActualArrivalDate { get; set; }
        public string TruckNumber { get; set; }
        public double? Weight { get; set; }
        public string Remarks { get; set; }
        public string Condition { get; set; }
        public long ERINumber { get; set; }
        public string GurantteNumber { get; set; }
        public long? ShippingLineId { get; set; }
        public ShippingLine ShippingLine { get; set; }
        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        public long? ClearingAgentId { get; set; }
        public ClearingAgent ClearingAgent { get; set; }

        public long? ConsigneId { get; set; }
        public Consigne Consigne { get; set; }


    }
}
