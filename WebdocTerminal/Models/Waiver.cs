using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Waiver")]
    public class Waiver : CommonProperties
    {

        public long WaiverId { get; set; }
        public string WaiverNumber { get; set; }
        public string Category { get; set; }
        public string Remarks { get; set; }
        public long? ContainerIndexId { get; set; }
        public long? CYContainerId { get; set; }
        //public long TariffId { get; set; }
        public string InvoiceNumber { get; set; }
        public double TariffAmount { get; set; }
        public double Discount { get; set; }
        public double TariffHeadAmount { get; set; }
        public string Description { get; set; }
       // public double StorageAmount { get; set; }
        public bool IsWaive { get; set; }
        public bool InvoiceCreated { get; set; }



    }
}
