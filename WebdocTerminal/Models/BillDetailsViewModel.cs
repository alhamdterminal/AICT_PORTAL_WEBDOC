using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class BillDetailsViewModel : CommonProperties
    {
        public string BillNo { get; set; }

        public DateTime? BillDate { get; set; }
    }
}
