using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ServicesSection")]
    public class ServicesSection : CommonProperties
    {
        public long ServicesSectionId { get; set; }
        public string ServicesSectionCode { get; set; } 
    }
}
