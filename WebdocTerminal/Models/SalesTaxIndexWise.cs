using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("SalesTaxIndexWise")]
    public class SalesTaxIndexWise : CommonProperties
    {
        public long SalesTaxIndexWiseId { get; set; }
        public long ? ContainerIndexId { get; set; }
        public long ? CYContainerId { get; set; }
        public long ?  SalesTaxAmount { get; set; }
    }
}
