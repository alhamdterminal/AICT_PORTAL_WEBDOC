using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ModifyPoint")]
    public class ModifyPoint : CommonProperties
    {
        public long ModifyPointId { get; set; }
        public string ModifyPointType { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
    }
}
