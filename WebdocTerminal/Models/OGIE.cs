using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OGIE : CommonProperties
    {
        public long OGIEId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string GDNumber { get; set; }

        public int? NoOfPackages { get; set; }

        public string PackageType { get; set; }

        public double Weight { get; set; }

        public DateTime? GateIn { get; set; }

        public string VehicleNumber { get; set; }

        public string GateInStatus { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public bool IsProcessed { get; set; } = false;


    }
}
