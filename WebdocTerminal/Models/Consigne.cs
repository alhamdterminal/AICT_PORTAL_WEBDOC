using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Consigne")]
    public class Consigne : CommonProperties
    {
        public long ConsigneId { get; set; }
        public string ConsigneName { get; set; }
        public string ConsigneNTN { get; set; }
        public string STRegistrationNo { get; set; }
        public string ConsigneCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string BillDateTypeLCL { get; set; }
        public string BillDateTypeFCL { get; set; }
        public string BillDateTypeCY { get; set; }
        public string ConsigneePhoneNumber { get; set; }
        public string  ConsigneeAddress { get; set; }
    }
}
