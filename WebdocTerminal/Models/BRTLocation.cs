using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("BRTLocation")]
    public class BRTLocation :CommonProperties
    {
        public long BRTLocationId { get; set; }
        public string BRTLocationName { get; set; }
        public string BRTLocationCode { get; set; }
    }
}
