using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("OffHireLocation")]
    public class OffHireLocation : CommonProperties
    {
        public long OffHireLocationId { get; set; }
        public string OffHireLocationName { get; set; }
    }
}
