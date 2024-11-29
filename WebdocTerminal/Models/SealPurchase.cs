using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("SealPurchase")]
    public class SealPurchase : CommonProperties
    {
        public long SealPurchaseId { get; set; }
        public string SealPurchaseNo { get; set; }
        public long SealFrom { get; set; }
        public long SealTo { get; set; } 
        public long CurrentSealNumber { get; set; } 
        public bool AssignStatus { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Rate { get; set; }         
        public long TotalSeal { get; set; }
        public long RemaingSeal { get; set; } 
    }
}
