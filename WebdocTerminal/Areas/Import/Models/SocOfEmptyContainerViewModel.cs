using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Areas.Import.Models
{
    public class SocOfEmptyContainerViewModel 
    {
        public SocOfEmptyContainerViewModel()
        {
            InvoiceItems = new List<InvoiceItem>();
        }

        public string Containerno { get; set; }
        public string Containersize { get; set; }
        public string Actualarrive { get; set; }
        public string Type { get; set; }
        public string Line { get; set; }
        public string Principal { get; set; }
        public string ConsigneeName { get; set; }
        public string ClearingAgentName { get; set; }
        public string PortOfLoading { get; set; }
        public string Vessel { get; set; }
        public string Voyage { get; set; }
        public string StorageDays { get; set; }
        public string Salestax { get; set; }
        public string AfterSalesTax { get; set; }
        public string TotalCharges { get; set; }
        public string GrandTotal { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
