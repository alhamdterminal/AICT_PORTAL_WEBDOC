using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ISOCodeHead")]
    public class ISOCodeHead : CommonProperties
    {
        public long ISOCodeHeadId { get; set; }
        public string ISOCodeHeadDescription { get; set; }
    }
}
