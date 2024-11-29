using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class EdiMessage : CommonProperties
    {
        public long EdiMessageId { get; set; }
        public string FileName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Exception { get; set; }
    }
}
