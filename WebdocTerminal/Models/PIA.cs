using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class PIA : CommonProperties
    { 
        public long PIAId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string Category { get; set; }

        public string Origin { get; set; }

        public string GDNo { get; set; }

        public string ShippingLine { get; set; }

        public string ContainerNo { get; set; }

        public string DestinationCountry { get; set; }

        public string DestinationPort { get; set; }

        public string Shipper { get; set; }

        public string ClearingAgent { get; set; }

        public string SealNo { get; set; }

        public string CertificateNo { get; set; }

        public string OperationType { get; set; }

        public DateTime? Performed { get; set; }

        public string ShipperNTN { get; set; }

        public string BookingNo { get; set; }

        public string FileName { get; set; }


    }
}
