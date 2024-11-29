using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GDCR")]
    public class GDCR : CommonProperties
    {
        public long GDCRId { get; set; }
        public string OldContainerNumber { get; set; }
        public string NewContainerNumber { get; set; }
        public string GDNumber { get; set; }
        public string BLNumber { get; set; }
        public string VirNumber { get; set; }
        public string ContainerConsolidation { get; set; }
        public string Status { get; set; }
        public string OperationType { get; set; }
        public string Flag1 { get; set; }
        public string Flag2 { get; set; }
        public string Flag3 { get; set; }
        public bool IsContainerStuffed { get; set; }
        public bool IsSubmit { get; set; }
        public bool IsGateOut { get; set; }
        public DateTime? Performed { get; set; }
        public string MessageFrom { get; set; }
        public string MessageTo { get; set; }
        public string FileName { get; set; }
        public bool IsProcessed { get; set; } = false;
        public DateTime? CreateDT { get; set; } = DateTime.Now;

        public long? OrderNumber { get; set; }
    }
}
