using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EDI_Tradelens_Lookup")]
    public class EDI_Tradelens_Lookup
    {
        public long EDI_Tradelens_LookupId { get; set; }
        public DateTime? ModifyPoint { get; set; }
        public bool CopyToMearskEnabled { get; set; }
    }
}
