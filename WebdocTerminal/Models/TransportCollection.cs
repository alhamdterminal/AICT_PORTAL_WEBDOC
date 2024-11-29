using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TransportCollection")]
    public class TransportCollection : CommonProperties
    {
        public long TransportCollectionId { get; set; }
        public string TransportCollectionName { get; set; }
         
        public string ShippingagentCode { get; set; }

         
        public long? ClearningAgentCode { get; set; }
         
        public long? ConsigneeCode { get; set; }
         
        public long? ShipperCode { get; set; }
          
        public long? GoodHeadCode { get; set; }
         
        public long? PortAndTerminalCode { get; set; }
          
        public long? IsoCodeHeadCode { get; set; }
         
        public string CargoType { get; set; }
        public List<TransportCollectionTariff> TransportCollectionTariffs { get; set; }


    }
}
