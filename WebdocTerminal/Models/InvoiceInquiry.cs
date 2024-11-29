using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("InvoiceInquiry")]
    public class InvoiceInquiry : CommonProperties
    {

        public long InvoiceInquiryId { get; set; }
        public string CallerName { get; set; }
        public DateTime InquiryDate { get; set; }
        public double CBM { get; set; }
        public long Ammount { get; set; }
        public string InquiryAbout { get; set; }
        public string Remarks { get; set; }
        public long? ContainerIndexId { get; set; }
        public ContainerIndex ContainerIndex { get; set; }
        public long? CYContainerId { get; set; }
        public CYContainer CYContainer { get; set; }


    }
}
