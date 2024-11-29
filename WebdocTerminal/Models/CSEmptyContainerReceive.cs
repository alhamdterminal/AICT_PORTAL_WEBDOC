using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CSEmptyContainerReceive")]
    public class CSEmptyContainerReceive : CommonProperties
    {
        public long CSEmptyContainerReceiveId { get; set; }
        public string ContainerNo { get; set; }
        public string Size { get; set; }
        public long? ISOCodeHeadId { get; set; }
        public ISOCodeHead ISOCodeHead { get; set; }
        public string Shift { get; set; }
        public long? ConsigneId { get; set; }
        public Consigne Consigne { get; set; }
        public long? ClearingAgentId { get; set; }
        public ClearingAgent ClearingAgent { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string TrukNo { get; set; }
        public long? TransporterId { get; set; }
        public Transporter Transporter { get; set; }
        public string DamageType  { get; set; }
        public string OtherRemarks  { get; set; }
        public DateTime? OutDate  { get; set; }
        public double TireWeight { get; set; }
        public bool IsInUse { get; set; }

    }
}
