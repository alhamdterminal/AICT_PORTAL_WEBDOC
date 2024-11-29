using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("BillToLine")]
    public class BillToLine : CommonProperties
    {
        public long BillToLineId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string IndexType { get; set; }
        public string VirNo { get; set; }
        public long IndexNo { get; set; } 
        public string Remarks { get; set; }
        public string Type { get; set; }
        public double TariffAmount { get; set; }
        public double InvoiceAmount { get; set; }
        public bool InoviceCreated { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
