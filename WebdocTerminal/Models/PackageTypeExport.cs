using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class PackageTypeExport : CommonProperties
    {
        public long PackageTypeExportId { get; set; }
        public string Code { get; set; }
        public string PackageType { get; set; }
        public string MaterialType { get; set; }
    }
}
