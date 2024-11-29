using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmailAlert")]
    public class EmailAlert : CommonProperties
    {
        public long EmailAlertId { get; set; }
        public string VIRNumber { get; set; }
        public string ContainerNumber { get; set; }
        public DateTime CreateDT { get; set; }
        public long Diff { get; set; }
        public string Remarks { get; set; }
    }
}
