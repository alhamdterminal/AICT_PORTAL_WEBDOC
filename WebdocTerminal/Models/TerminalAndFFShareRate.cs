using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TerminalAndFFShareRate")]
    public class TerminalAndFFShareRate : CommonProperties
    {
        public long TerminalAndFFShareRateId { get; set; }
        public double AICTRate20 { get; set; }

        public double AICTRate40 { get; set; }

        public double AICTRate45 { get; set; }

        public double AICTCBMRate { get; set; }
         
        public double AICTPerIndexRate { get; set; }
         
        //ff
        public double FFRate20 { get; set; }

        public double FFRate40 { get; set; }

        public double FFRate45 { get; set; }

        public double FFCBMRate { get; set; }

        public double FFPerIndexRate { get; set; }

        // total rate
        public double TotalAICTRate20 { get; set; }

        public double TotalAICTRate40 { get; set; }

        public double TotalAICTRate45 { get; set; }

        public double TotalAICTCBMRate { get; set; }

        public double TotalAICTPerIndexRate { get; set; }

        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        //public long? ConsigneId { get; set; }
        //public Consigne Consigne { get; set; }
        //public long? ClearingAgentId { get; set; }
        //public ClearingAgent ClearingAgent { get; set; }
        public long? GoodsHeadId { get; set; }
        public GoodsHead GoodsHead { get; set; }

        //public long? PortAndTerminalId { get; set; }
        //public PortAndTerminal PortAndTerminal { get; set; }
        //public long? ISOCodeHeadId { get; set; }
        //public ISOCodeHead ISOCodeHead { get; set; }
        public string IndexCargoType { get; set; }
        //public long? ShipperId { get; set; }
        //public Shipper Shipper { get; set; }



    }
}
