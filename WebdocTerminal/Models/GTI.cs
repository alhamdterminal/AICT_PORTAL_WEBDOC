using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class GTI : CommonProperties
    {
        public long GTIId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string Category { get; set; }

        public string ContainerNo { get; set; }

        public string SealNumber { get; set; }

        public string StowCode { get; set; }

        public DateTime? GateInDate { get; set; }

        public string VehicleNumber { get; set; }

        public string PackingListRetained { get; set; }

        public string InvoiceListRetained { get; set; }

        public string Status { get; set; }

        public DateTime? Performed { get; set; }

        public string GDNumber { get; set; }

        public string FileName { get; set; }

    }
}
