using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ShipperExport")]
    public class ShipperExport : CommonProperties
    {
        public long ShipperExportId { get; set; }

        public string ShipperName { get; set; }

        public string ContactPerson { get; set; }
        public string NTNNumber { get; set; }

        public string Address { get; set; }

        public string TelephoneNumber { get; set; }

        public string EmailAddress { get; set; }
         
    }
}
