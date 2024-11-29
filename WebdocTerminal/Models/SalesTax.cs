using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("SalesTax")]
    public class SalesTax : CommonProperties
    {
        public long SalesTaxId { get; set; }

        public int?  SalesTaxAmount { get; set; }
        public string Type { get; set; }
    }
}
