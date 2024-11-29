using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ServicesType")]
    public class ServicesType : CommonProperties
    {
        public long ServicesTypeId { get; set; }
        public string ServicesTypeCode { get; set; } 
        public string ServicesTypeName { get; set; } 
    }
}
