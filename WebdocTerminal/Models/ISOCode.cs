using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ISOCode")]
    public class ISOCode : CommonProperties
    {
        public long ISOCodeId { get; set; }
        public string Code { get; set; }
        public string Descrition { get; set; }
        public string ContainerSize { get; set; }
        public long ISOCodeHeadId { get; set; }
        public ISOCodeHead ISOCodeHead { get; set; }

    }
}
