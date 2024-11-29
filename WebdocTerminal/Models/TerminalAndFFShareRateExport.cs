using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TerminalAndFFShareRateExport")]
    public class TerminalAndFFShareRateExport : CommonProperties
    {
        public long TerminalAndFFShareRateExportId { get; set; }
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

        public long? ShippingAgentExportId { get; set; }
        public ShippingAgentExport ShippingAgentExport { get; set; } 
        public long? GoodsHeadExportId { get; set; }
        public GoodsHeadExport GoodsHeadExport { get; set; }
        public string IndexCargoType { get; set; }
         
    }
}
 
