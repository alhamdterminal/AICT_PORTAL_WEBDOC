using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("DOItemExport")]
    public class DOItemExport : CommonProperties
    {
        public long DOItemExportId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TruckNumber { get; set; }
        public string PackageType { get; set; }
        public string Status { get; set; }
        public int NumberOfPackage { get; set; }
        public long GatePassExportId { get; set; }
        public GatePassExport GatePassExport { get; set; }
    }
}
