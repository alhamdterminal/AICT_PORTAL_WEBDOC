using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PortOfLoading")]
    public class PortOfLoading : CommonProperties
    {
        public long PortOfLoadingId { get; set; }
        public string PortOfLoadingName { get; set; }
    }
}
