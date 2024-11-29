using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{

    [Table("EmptyContainerReceived")]
    public class EmptyContainerReceived : CommonProperties
    {
        public long EmptyContainerReceivedId { get; set; }        
        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        public DateTime? ArrivedFrom { get; set; }
        public long? ShippingLineId { get; set; }
        public ShippingLine ShippingLine { get; set; }         
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public long? ISOCodeHeadId { get; set; }
        public ISOCodeHead ISOCodeHead { get; set; }
        public long? ClearingAgentId { get; set; }
        public ClearingAgent ClearingAgent { get; set; }         
        public long? ConsigneId { get; set; }
        public Consigne Consigne { get; set; }
        public DateTime? ActualArrive { get; set; }
        public long? TransporterId { get; set; }
        public Transporter Transporter { get; set; }
        public string Shift { get; set; }
        public string TruckNo { get; set; }
        public string Remarks { get; set; }
        public string DamageType { get; set; }
        public string PickLocation { get; set; }
        public string GuranteeNo { get; set; }
        public string GatePassNo { get; set; }
        public bool IsEmptyReceived { get; set; }
        public bool IsEmptyGateOut { get; set; }
        public bool IsEmptyOut { get; set; }


    }
}
