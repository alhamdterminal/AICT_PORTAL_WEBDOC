using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("IGMO")]
    public class IGMO : CommonProperties
    {
        public long  IGMOId { get; set; }

        public int?  TotalRecord { get; set; }

        public int?  RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public int? IndexNumber { get; set; }

        public string Status { get; set; }

        public string ContainerNumber { get; set; }

        public double? ContainerGrossWeight { get; set; }

        public double? BLGrossWeight { get; set; }

        public string HSCode{ get; set; }

        public string Description { get; set; }

        public string IMOClass{ get; set; }

        public string MarksAndNumber { get; set; }

        public double? NumberofPackages { get; set; }

        public string PackageType { get; set; }

        public string ISOCode { get; set; }

        public string CountryCode { get; set; }

        public string DestinationCode { get; set; }

        public string ManifestedSealNumber { get; set; }

        public string ShippingLine { get; set; }

        public string OpType { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }

        public DateTime? CreateDT { get; set; } = DateTime.Now;
    }
}
