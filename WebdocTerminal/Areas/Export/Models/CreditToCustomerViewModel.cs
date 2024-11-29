using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class CreditToCustomerViewModel
    {
        public long EnteringCargoId { get; set; }
        public long CreditToCustomerId { get; set; }
        public string AuthorizedBy { get; set; }
        public int AuthorizedDays { get; set; }
        public double InvoiceAmount { get; set; }
        public string InvoiceNo { get; set; }
        public string LPNo { get; set; }
        public long VesselId { get; set; }
        public long VoyageId { get; set; }
    }
}
