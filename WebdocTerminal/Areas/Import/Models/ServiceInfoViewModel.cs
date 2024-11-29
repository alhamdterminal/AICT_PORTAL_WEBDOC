using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ServiceInfoViewModel
    {
        public long ServiceInfoId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public string Type { get; set; }
        public double Rate { get; set; }
        public string NatureOfPayment { get; set; }
    }
}
