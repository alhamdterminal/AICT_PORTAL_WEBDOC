using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyContainerInvoice")]
    public class EmptyContainerInvoice : CommonProperties
    {
        public EmptyContainerInvoice()
        {
            EmptyContainerInvoiceItems = new List<EmptyContainerInvoiceItem>(); 

        }
        public long EmptyContainerInvoiceId { get; set; }

        public string InvoiceNo { get; set; }
        public string Year { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public int Size { get; set; }         
        public int StorageDays { get; set; }
        public double TotalCharges { get; set; }
        public int SalesTax { get; set; }
        public double AfterSalesTax { get; set; } 
        public double GrandTotal { get; set; } 
        public long EmptyContainerReceivedId { get; set; }
        public EmptyContainerReceived EmptyContainerReceived { get; set; }

        public List<EmptyContainerInvoiceItem> EmptyContainerInvoiceItems { get; set; }
    }
}
