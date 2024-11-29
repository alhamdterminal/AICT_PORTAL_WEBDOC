using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class ExportLocationCodeList : CommonProperties
    {
        public long ExportLocationCodeListId { get; set; }
        public string UNCODE { get; set; }
        public string CountryCode { get; set; }
        public string PortCode { get; set; }
        public string CountryName { get; set; }
        public string PortName { get; set; }
    }
}
