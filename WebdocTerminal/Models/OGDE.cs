using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OGDE : CommonProperties
    {
        public long OGDEId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string GDNumber { get; set; }

        public string VehicleNo { get; set; }

        public string ContainerNo { get; set; }

        public string FormENumber { get; set; }

        public string DocumentRequired { get; set; }

        public string HSCode { get; set; }

        public string HSCodeDescription { get; set; }

        public string MarksAndNumber { get; set; }

        public double NoOfPackages { get; set; }

        public string PackageType { get; set; }

        public double GrossWeight { get; set; }

        public string DestinationCountry { get; set; }

        public string DestinationPort { get; set; }

        public string OperationType { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }
    }
}
