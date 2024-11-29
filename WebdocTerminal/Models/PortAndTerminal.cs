using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PortAndTerminal")]
    public class PortAndTerminal : CommonProperties
    {
         
        public long PortAndTerminalId { get; set; }

        public string PortName { get; set; }

        public string TerminalCode { get; set; }

        public string TerminalName { get; set; }

        public string PortOfDischarge { get; set; }

        public string Destination { get; set; }
        public double RateAmount20 { get; set; }
        public double RateAmount40 { get; set; }
        public double RateAmount45 { get; set; } 

    }
}
