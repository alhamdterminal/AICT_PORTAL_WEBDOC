using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class CommonProperties
    {
        public bool IsDeleted { get; set; }

        public DateTime DeleteDate { get; set; } = DateTime.Now;
    }
}
