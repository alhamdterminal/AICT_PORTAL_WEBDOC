using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{

    [Table("DeliveredYardExport")]
    public class DeliveredYardExport : CommonProperties
    {
        public long DeliveredYardExportId { get; set; }
        public string DeliveredYardName { get; set; }
    }
}
