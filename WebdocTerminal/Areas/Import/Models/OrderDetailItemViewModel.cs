using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class OrderDetailItemViewModel
    {
        public string TruckNumber { get; set; }

        public int NumberofPackages { get; set; }

        public string PackageType { get; set; }
    }
}
